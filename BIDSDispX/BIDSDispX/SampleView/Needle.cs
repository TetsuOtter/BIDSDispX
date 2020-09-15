
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.SampleView
{
	public class Needle : ContentView, IAngleAndValMinMax
	{
		private Grid MainNeedle = new Grid
		{
			AnchorX = 1,
			AnchorY = 0.5,
			HeightRequest = 10,
			WidthRequest = 100,
			BackgroundColor = Color.Transparent,
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start
		};
		ColorPropToBind CPTB = new ColorPropToBind();
		public Needle()
		{
			this.Content = MainNeedle;

			Dispatcher.BeginInvokeOnMainThread(() => NeedleReDraw(NeedleHeight));
		}

		public uint MoveTimeLength { get; set; } = 50;

		public Color NeedleColor{ get => CPTB.Color_ToBind; set => CPTB.Color_ToBind = value; }
		public double NeedleOpacity { get => MainNeedle.Opacity; set => MainNeedle.Opacity = value; }
		public double MinValAngle { get; set; } = -30;//←・が0度 ↑が90度 ・→が180度
		public double MinValue { get; set; } = 0;
		public double MaxValAngle { get; set; } = 210;//←・が0度 ↑が90度 ・→が180度
		public double MaxValue { get; set; } = 160;

		double __ValueToShow = 0;
		public double ValueToShow
		{
			get => __ValueToShow;
			set
			{
				if (__ValueToShow == value)
					return;

				Angle = value.GetAngle(this);
				
				__ValueToShow = value;
			}
		}
		public double Radius
		{
			get => MainNeedle.Width;//__Radius;
			set
			{
				MainNeedle.WidthRequest = value;//MainNeedle.ScaleX = (__Radius = value) / MainNeedle.Width;
				Dispatcher.BeginInvokeOnMainThread(() => NeedleReDraw(NeedleHeight));
			}
		}
		public double NeedleHeight
		{
			get => MainNeedle.Height;
			set
			{
				MainNeedle.HeightRequest = value;
				Dispatcher.BeginInvokeOnMainThread(() => NeedleReDraw(NeedleHeight));
			}
		}

		double __Angle = 0;
		public double Angle
		{
			get => __Angle;
			set
			{
				if (__Angle == value)
					return;

				Dispatcher.BeginInvokeOnMainThread(async () => await MainNeedle.RotateTo(value, MoveTimeLength));

				__Angle = value;
			}
		}

		private int __Triangle_StepCount = 20;
		public int Triangle_StepCount
		{
			get => __Triangle_StepCount;
			set
			{
				if (__Triangle_StepCount != value)
				{
					__Triangle_StepCount = value;
					NeedleReDraw(NeedleHeight);
				}
			}
		}

		private double __Triangle_Width = 10;
		public double Triangle_Width
		{
			get => __Triangle_Width;
			set
			{
				if (__Triangle_Width != value)
				{
					__Triangle_Width = value;
					NeedleReDraw(NeedleHeight);
				}
			}
		}


		private void NeedleReDraw(double N_Height)
		{
			MainNeedle.Margin = new Thickness(0, Radius - (N_Height / 2), 0, 0);

			MainNeedle.Children.Clear();

			double CurrentTriMarginY = N_Height / 2;
			double dH = CurrentTriMarginY / Triangle_StepCount;

			for (double i = 0; i < Triangle_Width; i += (Triangle_Width / Triangle_StepCount))
			{
				CurrentTriMarginY -= dH;

				BoxView BV = new BoxView
				{
					Margin = new Thickness(i, CurrentTriMarginY),
					HorizontalOptions = LayoutOptions.Start,
					VerticalOptions = LayoutOptions.Center,
					WidthRequest = Triangle_Width / Triangle_StepCount,
				};
				BV.SetBinding(BoxView.ColorProperty, new Binding(nameof(CPTB.Color_ToBind), source: CPTB));
				MainNeedle.Children.Add(BV);
			}

			BoxView BV_Main = new BoxView
			{
				Margin = new Thickness(Triangle_Width, 0, 0, 0),
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = Radius - Triangle_Width
			};
			BV_Main.SetBinding(BoxView.ColorProperty, new Binding(nameof(CPTB.Color_ToBind), source: CPTB));
			MainNeedle.Children.Add(BV_Main);
		}

		private class ColorPropToBind : INotifyPropertyChanged
		{
			Color __Color_ToBind = Color.Black;
			public Color Color_ToBind
			{
				get => __Color_ToBind;
				set
				{
					if (value != __Color_ToBind)
					{
						__Color_ToBind = value;
						OnPropertyChanged(nameof(Color_ToBind));
					}
				}
			}

			public event PropertyChangedEventHandler PropertyChanged;
			protected void OnPropertyChanged(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
		}
	}
}