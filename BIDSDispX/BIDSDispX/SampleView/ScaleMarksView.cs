
using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
namespace TR.BIDSDispX.SampleView
{
	public class ScaleMarksView : Grid, IAngleAndValMinMax
	{
		#region Fields for Properties
		private double __MaxValAngle = 210;
		private double __MinValAngle = -30;
		private double __MinValue = 0;
		private double __MaxValue = 160;
		private int __MarkStep = 10;
		private Color __MarkColor = Color.Black;
		private double __MarkHeight = 2;
		private double __MarkWidth = 10;
		double __Radius = 110;
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
				MarksAngleUpdated();
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
				MarksAngleUpdated();
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

		/// <summary>目盛を配置する間隔[km/h]</summary>
		public int MarkStep
		{
			get => __MarkStep;
			set
			{
				if (__MarkStep == value)
					return;

				__MarkStep = value;
				PropUpdated();
			}
		}

		/// <summary>目盛の色</summary>
		public Color MarkColor
		{
			get => __MarkColor;
			set
			{
				if (__MarkColor == value)
					return;

				__MarkColor = value;
				ToApplyChangesForChildrenInmainThread((i) => ((BoxView)((ContentView)Children[i]).Content).Color = value);
			}
		}

		/// <summary>目盛の高さ[dp]</summary>
		public double MarkHeight
		{
			get => __MarkHeight;
			set
			{
				if (__MarkHeight == value)
					return;

				__MarkHeight = value;

				ToApplyChangesForChildrenInmainThread((i) => ((ContentView)Children[i]).Content.HeightRequest = value);
			}
		}


		/// <summary>目盛の幅[dp]</summary>
		public double MarkWidth
		{
			get => __MarkWidth;
			set
			{
				if (__MarkWidth == value)
					return;

				__MarkWidth = value;

				ToApplyChangesForChildrenInmainThread((i) => ((ContentView)Children[i]).Content.WidthRequest = value);
			}
		}

		/// <summary>半径[dp]</summary>
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

		/// <summary>ScaleMarksViewの枠と目盛の外円とのすきま[dp]</summary>
		public double Circle_Padding
		{
			get => __Circle_Padding;
			set
			{
				if (__Circle_Padding == value)
					return;

				__Circle_Padding = value;
				ToApplyChangesForChildrenInmainThread((i) => Children[i].Margin = MarksMargin);
			}
		}

		#region Direct Attatch Properties
		#endregion
		#region get-only Properties
		public Thickness MarksMargin => new Thickness(Circle_Padding);
		public double MarksRadius => Radius - Circle_Padding;
		public double MarksAnchorX => MarksRadius / MarkWidth;
		#endregion
		#endregion

		public ScaleMarksView()
		{
			HorizontalOptions = VerticalOptions = LayoutOptions.Start;
			
			HeightRequest = WidthRequest = Radius * 2;
		}

		/// <summary>PropUpdatedの処理が重複して呼ばれていないかをチェック</summary>
		private bool PropUpdateAlreadyRequested = false;
		public void PropUpdated()
		{
			if (PropUpdateAlreadyRequested)
				return;//二重で処理する必要はない

			PropUpdateAlreadyRequested = true;

			MainThread.BeginInvokeOnMainThread(() =>
			{
				Children?.Clear();//既存の表示を削除

				for (int i = (int)MinValue; i <= MaxValue; i += MarkStep)
					Children.Add(new ContentView
					{
						Content =
							new BoxView
							{
								HorizontalOptions = LayoutOptions.Start,
								VerticalOptions = LayoutOptions.Center,
								//AnchorY = 0.5,
								Margin = MarksMargin,
								//Rotation = ((double)i).GetAngle(this),
								Color = MarkColor,
								HeightRequest = MarkHeight,
								WidthRequest = MarkWidth,
								//AnchorX = MarksAnchorX
							},
						Rotation = ((double)i).GetAngle(this),
						BackgroundColor = Color.Transparent,
						WidthRequest = Radius * 2,
						HeightRequest = Radius * 2,
						AnchorX = 0.5,
						AnchorY = 0.5,
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.Start,
						Margin = new Thickness(0),
					});

				PropUpdateAlreadyRequested = false;//処理完了済を記録
			});

		}

		private bool MarksAngleUpdatedAlreadyRequested = false;
		public void MarksAngleUpdated()
		{
			if (PropUpdateAlreadyRequested || MarksAngleUpdatedAlreadyRequested)
				return;//PropUpdatedが要求されてたら, そっちからやる.

			MarksAngleUpdatedAlreadyRequested = true;
			MainThread.BeginInvokeOnMainThread(() =>
			{
				MarksAngleUpdatedAlreadyRequested = false;

				if (PropUpdateAlreadyRequested)
					return;

				for (int i = 0; i < Children.Count; i++)
					Children[i].Rotation = (MinValue + (i * MarkStep)).GetAngle(this);
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