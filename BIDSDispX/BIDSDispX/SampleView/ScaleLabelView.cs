
using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace TR.BIDSDispX.SampleView
{
	public class ScaleLabelView : Grid, IAngleAndValMinMax
	{

		#region Fields for Properties
		private double __MaxValAngle = 210;
		private double __MinValAngle = -30;
		private double __MinValue = 0;
		private double __MaxValue = 160;
		private int __LabelStep = 10;
		private Color __TextColor = Color.Black;
		private double __Radius = 80;
		private double __FontSize = 16;
		private string __FontFamily = string.Empty;
		private FontAttributes __FontAttributes = FontAttributes.None;
		private double __Circle_Padding = 0;
		#endregion
		#region Properties
		/// <summary>最大値をとるときの目盛の角度[deg]  ←:0, ↑:90, →:180</summary>
		public double MaxValAngle
		{
			get => __MaxValAngle;
			set
			{
				if (__MaxValAngle == value)
					return;

				__MaxValAngle = value;
				LabsAngleUpdated();
			}
		}

		/// <summary>最小値をとるときの目盛の角度[deg]  ←:0, ↑:90, →:180</summary>
		public double MinValAngle
		{
			get => __MinValAngle;
			set
			{
				if (__MinValAngle == value)
					return;

				__MinValAngle = value;
				LabsAngleUpdated();
			}
		}

		/// <summary>表示の最小値[km/h]</summary>
		public double MinValue
		{
			get => __MinValue;
			set
			{
				if (__MinValue == value)
					return;

				__MinValue = value;
				PropUpdated();
			}
		}

		/// <summary>表示の最大値[km/h]</summary>
		public double MaxValue
		{
			get => __MaxValue;
			set
			{
				if (__MaxValue == value)
					return;

				__MaxValue = value;
				PropUpdated();
			}
		}

		/// <summary>文字を配置する間隔[km/h]</summary>
		public int LabelStep
		{
			get => __LabelStep;
			set
			{
				if (__LabelStep == value)
					return;

				__LabelStep = value;
				PropUpdated();//ラベル数が変わる可能性があるため再描画
			}
		}

		/// <summary>目盛の色</summary>
		public Color TextColor
		{
			get => __TextColor;
			set
			{
				if (__TextColor == value)
					return;

				__TextColor = value;
				ToApplyChangesForChildrenInmainThread((i) => ((Label)(((ContentView)Children[i]).Content)).TextColor = value);
			}
		}

		/// <summary>使用する円領域の半径設定[dp]</summary>
		public double Radius
		{
			get => __Radius;
			set
			{
				if (__Radius == value)
					return;

				__Radius = value;
				MainThread.BeginInvokeOnMainThread(() => HeightRequest = WidthRequest = value * 2);
			}
		}

		/// <summary>フォントサイズ[たぶんdp]</summary>
		public double FontSize
		{
			get => __FontSize;
			set
			{
				if (__FontSize == value)
					return;

				__FontSize = value;
				ToApplyChangesForChildrenInmainThread((i) => ((Label)(((ContentView)Children[i]).Content)).FontSize = value);
			}
		}

		/// <summary>フォント(Emptyでデフォルトフォント)</summary>
		public string FontFamily
		{
			get => __FontFamily;
			set
			{
				if (__FontFamily == value)
					return;

				__FontFamily = value;
				ToApplyChangesForChildrenInmainThread((i) => ((Label)(((ContentView)Children[i]).Content)).FontFamily = value);
			}
		}

		/// <summary>文字のスタイル(なし, 太字, 斜体, 太字&斜体)</summary>
		public FontAttributes FontAttributes
		{
			get => __FontAttributes;
			set
			{
				if (__FontAttributes == value)
					return;

				__FontAttributes = value;
				ToApplyChangesForChildrenInmainThread((i) => ((Label)(((ContentView)Children[i]).Content)).FontAttributes = value);
			}
		}

		/// <summary>ScaleLabelViewに割り当てられたエリアと使用する円領域の間のすきまの大きさ</summary>
		public double Circle_Padding
		{
			get => __Circle_Padding;
			set
			{
				if (__Circle_Padding == value)
					return;

				__Circle_Padding = value;
				ToApplyChangesForChildrenInmainThread((i) => Children[i].Margin = new Thickness(value));
			}
		}

		public double Circle_Radius => Radius - Circle_Padding;
		#endregion

		public ScaleLabelView() => PropUpdated();
		


		private bool PropUpdateAlreadyRequested = false;
		public void PropUpdated()
		{
			if (PropUpdateAlreadyRequested)
				return;//二重で処理する必要はない

			PropUpdateAlreadyRequested = true;

			MainThread.BeginInvokeOnMainThread(() =>
			{
				Children?.Clear();//既存の表示を削除

				for (int i = (int)MinValue; i <= MaxValue; i += LabelStep)
				{
					double angle = ((double)i).GetAngle(this);
					Children.Add(new ContentView
					{
						Content = new Label
						{
							HorizontalOptions = LayoutOptions.Start,//左端
							VerticalOptions = LayoutOptions.Center,//上下方向に中央
							AnchorX = 0.5,
							AnchorY = 0.5,
							Rotation = -angle,
							Text = i.ToString(),
							TextColor = TextColor,
							FontSize = FontSize,
							FontFamily = FontFamily,
							FontAttributes = FontAttributes,
						},
						AnchorX = 0.5,
						AnchorY = 0.5,
						BackgroundColor = Color.Transparent,
						Rotation = angle,
						Margin = new Thickness(Circle_Padding),
					});
				}

				PropUpdateAlreadyRequested = false;//処理完了済を記録
			});

		}


		private bool LabsAngleUpdatedAlreadyRequested = false;
		private void LabsAngleUpdated()
		{
			if (PropUpdateAlreadyRequested || LabsAngleUpdatedAlreadyRequested)
				return;

			LabsAngleUpdatedAlreadyRequested = true;

			MainThread.BeginInvokeOnMainThread(() =>
			{
				if (PropUpdateAlreadyRequested)
					return;

				for (int i = 0; i < Children.Count; i++)
				{
					double angle = (MinValue + (i * LabelStep)).GetAngle(this);
					((ContentView)Children[i]).Content.Rotation = -angle;
					Children[i].Rotation = angle;
				}
			});
		}

		private void ToApplyChangesForChildrenInmainThread(Action<int> act) =>
			MainThread.BeginInvokeOnMainThread(() =>
			{
				if (Children?.Count > 0)
					for (int i = 0; i < Children.Count; i++)
						act.Invoke(i);
			});
	}
}
