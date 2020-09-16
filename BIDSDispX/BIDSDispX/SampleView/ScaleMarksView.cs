
using Xamarin.Forms;
namespace TR.BIDSDispX.SampleView
{
	public class ScaleMarksView : Grid, IAngleAndValMinMax
	{
		#region Properties
		private double __MaxValAngle = 210;
		/// <summary>最大値をとるときの目盛の角度[deg]  ←:0, ↑:90, →:180</summary>
		public double MaxValAngle
		{
			get => __MaxValAngle;
			set
			{
				if (__MaxValAngle == value)
					return;

				__MaxValAngle = value;
				PropUpdated();
			}
		}

		private double __MinValAngle = -30;
		/// <summary>最小値をとるときの目盛の角度[deg]  ←:0, ↑:90, →:180</summary>
		public double MinValAngle
		{
			get => __MinValAngle;
			set
			{
				if (__MinValAngle == value)
					return;

				__MinValAngle = value;
				PropUpdated();
			}
		}

		private double __MinValue = 0;
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

		private double __MaxValue = 160;
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

		private int __MarkStep = 10;
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

		private Color __MarkColor = Color.Black;
		/// <summary>目盛の色</summary>
		public Color MarkColor
		{
			get => __MarkColor;
			set
			{
				if (__MarkColor == value)
					return;

				__MarkColor = value;
				PropUpdated();
			}
		}

		private double __MarkHeight = 2;
		/// <summary>目盛の高さ</summary>
		public double MarkHeight
		{
			get => __MarkHeight;
			set
			{
				if (__MarkHeight == value)
					return;

				__MarkHeight = value;
				PropUpdated();
			}
		}

		private double __MarkWidth = 10;
		/// <summary>目盛の幅</summary>
		public double MarkWidth
		{
			get => __MarkWidth;
			set
			{
				if (__MarkWidth == value)
					return;

				__MarkWidth = value;
				PropUpdated();
			}
		}

		double __Radius = 110;
		public double Radius
		{
			get => __Radius;
			set
			{
				if (__Radius == value)
					return;

				__Radius = value;
				HeightRequest = WidthRequest = __Radius * 2;

				PropUpdated();
			}
		}
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

			Dispatcher.BeginInvokeOnMainThread(() =>
			{
				Children?.Clear();//既存の表示を削除

				for(int i=(int)MinValue; i <= MaxValue; i += MarkStep)
				{
					BoxView bv = new BoxView
					{
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.Start,
						Color = MarkColor,
						HeightRequest = MarkHeight,
						WidthRequest = MarkWidth,
						AnchorY = 0.5,
						AnchorX = Radius / MarkWidth,
						Margin = new Thickness(0, Radius - (MarkHeight / 2), 0, 0)
					};

					Children.Add(bv);

					_ = bv.RotateTo(((double)i).GetAngle(this), 1000);
				}

				PropUpdateAlreadyRequested = false;//処理完了済を記録
			});

		}
	}

}