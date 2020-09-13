using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TR.BIDSDispX.SampleView
{
	public class ScaleMarksView : Grid, IAngleAndValMinMax
	{
		public double MaxValAngle { get; set; } = 210;
		public double MinValAngle { get; set; } = -30;
		public double MinValue { get; set; } = 0;
		public double MaxValue { get; set; } = 160;

		List<BoxView> Marks_L = new List<BoxView>();
		/// <summary>文字表示付きの目盛の基本設定</summary>
		public Marks Mark_L_Base { get; set; } = new Marks
		{
			Mark_Step = 20,
			HeightRequest = 4,
			WidthRequest = 20,
			Color = Color.Red
		};

		List<BoxView> Marks_M = new List<BoxView>();
		/// <summary>中サイズの目盛の基本設定</summary>
		public Marks Mark_M_Base { get; set; } = new Marks
		{
			Mark_Step = 10,
			HeightRequest = 2,
			WidthRequest = 20,
		};

		List<BoxView> Marks_S = new List<BoxView>();
		/// <summary>小サイズの目盛の基本設定</summary>
		public Marks Mark_S_Base { get; set; } = new Marks
		{
			Mark_Step = 5,
			HeightRequest = 2,
			WidthRequest = 10,
		};

		double __Radius = 110;
		public double Radius
		{
			get => __Radius;
			set
			{
				__Radius = value;
				HeightRequest = WidthRequest = __Radius * 2;
			}
		}

		public ScaleMarksView()
		{
			HorizontalOptions = VerticalOptions = LayoutOptions.Start;
			HeightRequest = WidthRequest = Radius * 2;
		}

		public void PropsUpdated()
		{
			Children.Clear();

			Marks_S = Mark_S_Base.GetBVList(this, Radius);
			Marks_M = Mark_M_Base.GetBVList(this, Radius);
			Marks_L = Mark_L_Base.GetBVList(this, Radius);

			if (Marks_L?.Count > 0)
				for (int i = 0; i < Marks_L.Count; i++)
					Children.Add(Marks_L[i]);

			if (Marks_M?.Count > 0)
				for (int i = 0; i < Marks_M.Count; i++)
					Children.Add(Marks_M[i]);

			if (Marks_S?.Count > 0)
				for (int i = 0; i < Marks_S.Count; i++)
					Children.Add(Marks_S[i]);
		}

		public class Marks
		{
			public bool IsEnabled { get; set; } = true;
			public LayoutOptions HorizontalOptions { get; set; } = LayoutOptions.Start;
			public LayoutOptions VerticalOptions { get; set; } = LayoutOptions.Start;
			public Color Color { get; set; } = Color.Black;
			public double HeightRequest { get; set; } = 2;
			public double WidthRequest { get; set; } = 10;

			public int Mark_Step { get; set; } = 2;

			public List<BoxView> GetBVList(in IReadOnlyAngleAndValMinMax minmax, in double Radius)
			{
				List<BoxView> ret = new List<BoxView>();

				if (IsEnabled)
					for (int i = (int)minmax.MinValue; i <= minmax.MaxValue; i += Mark_Step)
						ret.Add(GetMarkBV(minmax, Radius, i));

				return ret;
			}

			public BoxView GetMarkBV(in IReadOnlyAngleAndValMinMax minmax, double Radius, in double numOfStep)
			{
				BoxView bv = new BoxView
				{
					HorizontalOptions = HorizontalOptions,
					VerticalOptions = VerticalOptions,
					Color = Color,
					HeightRequest = HeightRequest,
					WidthRequest = WidthRequest,
					AnchorY = 0.5,
				};
				double angle = numOfStep.GetAngle(minmax);
				bv.SizeChanged += (s, e) =>
				{
					BoxView b = s as BoxView;
					b.AnchorX = Radius / b.Width;
					b.Margin = new Thickness(0, Radius - (b.Height / 2), 0, 0);
					_ = bv.RotateTo(angle, 3000);
				};
				//_ = bv.RotateTo(angle, 0);

				return bv;
			}

			//public void SetAngle(BoxView bv, in IReadOnlyAngleAndValMinMax minmax) => _ = bv.RotateTo(NumOfStep.GetAngle(minmax), 0);
		}
	}

}