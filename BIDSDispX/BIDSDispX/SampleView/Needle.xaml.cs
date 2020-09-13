
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.SampleView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Needle : ContentView, IAngleAndValMinMax
	{
		public uint MoveTimeLength { get; set; } = 50;

		public Color NeedleColor
		{
			get => MainNeedle.BackgroundColor;
			set => MainNeedle.BackgroundColor = value;
		}
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
			get => MainNeedle.Width;
			set => MainNeedle.ScaleX = value / 100;
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

		public Thickness NeedleMargin
		{
			get => MainNeedle.Margin;
			set => MainNeedle.Margin = value;
		}

		public Needle()
		{
			InitializeComponent();
		}
	}
}