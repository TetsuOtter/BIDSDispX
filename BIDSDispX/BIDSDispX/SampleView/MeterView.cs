
using System;
using System.Text;

using TR.BIDSDispX.Core;
using TR.BIDSDispX.Core.UFuncs;
using TR.BIDSSMemLib;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace TR.BIDSDispX.SampleView
{
	public class MeterView : ContentView, IBIDSDispX
	{
		static readonly double RadiusValue = Math.Min(DispCom.WindowWidth, DispCom.WindowHeight) / 2.1; //Math.Min(DeviceDisplay.MainDisplayInfo.Height, DeviceDisplay.MainDisplayInfo.Width).Px2Dp() / 2.1;
		static readonly double MarginX = 0;
		static readonly double MarginY = 0;
		static readonly double Shadow_Dst = RadiusValue / 128;
		static readonly double Shadow_Opacity = 0.5;

		static readonly double Needle_Padding = RadiusValue / 16;
		static readonly double Needle1_Height = 0.8 * RadiusValue / 16;
		static readonly double Needle2_Height = RadiusValue / 16;
		static readonly double Needle1_Width = RadiusValue * 1.1;
		static readonly double Needle2_Width = RadiusValue * 1.15;
		static readonly Color Needle1_Color = Color.Black;
		static readonly Color Needle2_Color = Color.Red;
		static readonly double Needle1_TriangleWidth = RadiusValue;
		static readonly double Needle2_TriangleWidth = RadiusValue;
		static readonly int Needle1_TriangleStepCount = (int)(Needle1_TriangleWidth * 1.5);
		static readonly int Needle2_TriangleStepCount = (int)(Needle2_TriangleWidth * 1.5);

		static readonly double SMV_L_Height = RadiusValue / 32;
		static readonly double SMV_M_Height = RadiusValue / 64;
		static readonly double SMV_S_Height = RadiusValue / 64;
		static readonly double SMV_L_Width = RadiusValue / 8;
		static readonly double SMV_M_Width = RadiusValue / 8;
		static readonly double SMV_S_Width = RadiusValue / 16;
		static readonly int SMV_L_Step = 200;
		static readonly int SMV_M_Step = 50;
		static readonly int SMV_S_Step = 10;
		static readonly Color SMV_L_Color = Color.Red;
		static readonly Color SMV_M_Color = Color.Aqua;
		static readonly Color SMV_S_Color = Color.Black;

		static readonly double MaxValue = 1000;
		static readonly double MinValue = 0;
		static readonly double MaxValAngle = 225;
		static readonly double MinValAngle = -45;

		static readonly double Label_Padding = RadiusValue / 6.5;
		static readonly double Label_FontSize = RadiusValue / 6;
		static readonly string Label_FontFamily = "IPA_Gothic";
		static readonly FontAttributes Label_FontAtt = FontAttributes.Bold | FontAttributes.Italic;

		static readonly double PlusMinusBtn_MarginLR = 80;
		static readonly double PlusMinusBtn1_MarginBottom = 100;
		static readonly double PlusMinusBtn2_MarginBottom = 50;
		static readonly double PlusMinusBtn_Height = 40;
		static readonly double PlusMinusBtn_Width = (DispCom.WindowWidth - (PlusMinusBtn_MarginLR * 2)) / 3;
		static readonly int PlusMinus_Step = 20;

		Button plus_btn = new Button
		{
			Text = "+",
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.End,
			Margin = new Thickness(PlusMinusBtn_MarginLR, 0, PlusMinusBtn_MarginLR, PlusMinusBtn1_MarginBottom),
			HeightRequest = PlusMinusBtn_Height,
			WidthRequest = PlusMinusBtn_Width,
		};
		Button minus_btn = new Button
		{
			Text = "-",
			HorizontalOptions = LayoutOptions.End,
			VerticalOptions = LayoutOptions.End,
			Margin = new Thickness(PlusMinusBtn_MarginLR, 0, PlusMinusBtn_MarginLR, PlusMinusBtn1_MarginBottom),
			HeightRequest = PlusMinusBtn_Height,
			WidthRequest = PlusMinusBtn_Width,
		};
		Button plus_btn2 = new Button
		{
			Text = "+",
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.End,
			Margin = new Thickness(PlusMinusBtn_MarginLR, 0, PlusMinusBtn_MarginLR, PlusMinusBtn2_MarginBottom),
			HeightRequest = PlusMinusBtn_Height,
			WidthRequest = PlusMinusBtn_Width,
		};
		Button minus_btn2 = new Button
		{
			Text = "-",
			HorizontalOptions = LayoutOptions.End,
			VerticalOptions = LayoutOptions.End,
			Margin = new Thickness(PlusMinusBtn_MarginLR, 0, PlusMinusBtn_MarginLR, PlusMinusBtn2_MarginBottom),
			HeightRequest = PlusMinusBtn_Height,
			WidthRequest = PlusMinusBtn_Width,
		};
		Label Lab = new Label
		{
			VerticalOptions = LayoutOptions.Start,
			FontSize = 16
		};
		Button back_btn = new Button
		{
			Text = "BackToHome",
			HorizontalOptions = LayoutOptions.Start,//左端
			VerticalOptions = LayoutOptions.End,//下端
			Margin = new Thickness(5)
		};
		SettingView settingView = new SettingView
		{
			IsVisible = false,
			Margin = new Thickness(0),
		};
		Button open_settingView_btn = new Button
		{
			Text = "Show SettingView",
			HorizontalOptions = LayoutOptions.End,//右端
			VerticalOptions = LayoutOptions.End,//下端
			Margin = new Thickness(5),
		};

		Needle Needle1_shadow = new Needle
		{
			Margin = new Thickness(Shadow_Dst),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			Opacity = Shadow_Opacity,
			Radius = RadiusValue,
			NeedleHeight = Needle1_Height,
			NeedleWidth = Needle1_Width,
			Circle_Padding = Needle_Padding,
			Angle = MinValAngle,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,

			Triangle_StepCount = Needle1_TriangleStepCount,
			Triangle_Width = Needle1_TriangleWidth,
		};
		Needle Needle1_main = new Needle
		{
			Margin = new Thickness(0),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			Radius = RadiusValue,
			NeedleHeight = Needle1_Height,
			NeedleWidth = Needle1_Width,
			Circle_Padding = Needle_Padding,
			NeedleColor = Needle1_Color,
			Angle = MinValAngle,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,

			Triangle_StepCount = Needle1_TriangleStepCount,
			Triangle_Width = Needle1_TriangleWidth,
		};

		Needle Needle2_shadow = new Needle
		{
			Margin = new Thickness(Shadow_Dst),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			Opacity = Shadow_Opacity,
			Radius = RadiusValue,
			NeedleHeight = Needle2_Height,
			NeedleWidth = Needle2_Width,
			Circle_Padding = Needle_Padding,
			Angle = MinValAngle,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,

			Triangle_StepCount = Needle2_TriangleStepCount,
			Triangle_Width = Needle2_TriangleWidth,
		};
		Needle Needle2_main = new Needle
		{
			Margin = new Thickness(0),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			Radius = RadiusValue,
			NeedleHeight = Needle2_Height,
			NeedleWidth = Needle2_Width,
			Circle_Padding = Needle_Padding,
			NeedleColor = Needle2_Color,
			Angle = MinValAngle,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,

			Triangle_StepCount = Needle2_TriangleStepCount,
			Triangle_Width = Needle2_TriangleWidth,
		};

		ScaleMarksView SMV_L = new ScaleMarksView()
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			MarkStep = SMV_L_Step,
			MarkColor = SMV_L_Color,

			MarkHeight = SMV_L_Height,
			MarkWidth = SMV_L_Width,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,
		};

		ScaleMarksView SMV_M = new ScaleMarksView()
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			MarkStep = SMV_M_Step,
			MarkColor = SMV_M_Color,

			MarkHeight = SMV_M_Height,
			MarkWidth = SMV_M_Width,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,
		};

		ScaleMarksView SMV_S = new ScaleMarksView()
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			MarkStep = SMV_S_Step,
			MarkColor = SMV_S_Color,

			MarkHeight = SMV_S_Height,
			MarkWidth = SMV_S_Width,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,
		};

		ScaleLabelView ScaleLabel_Shadow = new ScaleLabelView
		{
			Margin = new Thickness(Shadow_Dst, Shadow_Dst, -Shadow_Dst, -Shadow_Dst),
			Radius = RadiusValue,
			Opacity = Shadow_Opacity,

			LabelStep = SMV_L_Step,
			TextColor = Color.Black,
			FontFamily = Label_FontFamily,
			FontSize = Label_FontSize,
			FontAttributes = Label_FontAtt,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,

			Circle_Padding = Label_Padding,
		};
		ScaleLabelView ScaleLabel = new ScaleLabelView
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			LabelStep = SMV_L_Step,
			TextColor = Color.Black,
			FontFamily = Label_FontFamily,
			FontSize = Label_FontSize,
			FontAttributes = Label_FontAtt,

			MaxValAngle = MaxValAngle,
			MinValAngle = MinValAngle,
			MaxValue = MaxValue,
			MinValue = MinValue,

			Circle_Padding = Label_Padding,
		};


		double __ValueToShow_Needle1 = 0;
		double ValueToShow_Needle1
		{
			get => __ValueToShow_Needle1;
			set
			{
				if (value == __ValueToShow_Needle1)
					return;

				Needle1_shadow.ValueToShow = Needle1_main.ValueToShow = value;
				
				__ValueToShow_Needle1 = value;
				Lab_Update();
			}
		}

		double __ValueToShow_Needle2 = 0;
		double ValueToShow_Needle2
		{
			get => __ValueToShow_Needle2;
			set
			{
				if (value == __ValueToShow_Needle2)
					return;

				Needle2_shadow.ValueToShow = Needle2_main.ValueToShow = value;
				
				__ValueToShow_Needle2 = value;
				Lab_Update();
			}
		}

		void Lab_Update() => Lab.Text = new StringBuilder()
					.Append("# Needle1 (Black)").Append("\n")
					.Append("    ValueToShow:").Append(Needle1_main.ValueToShow).Append("\n")
					.Append("    Angle:").Append(Needle1_main.Angle).Append("\n")
					.Append("# Needle2 (Red)").Append("\n")
					.Append("    ValueToShow:").Append(Needle2_main.ValueToShow).Append("\n")
					.Append("    Angle:").Append(Needle2_main.Angle)
					.ToString();

		public MeterView()
		{
			Content = new Grid
			{
				Children = {
					new Grid
					{
						Margin = new Thickness(MarginX,MarginY),
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						Children =
						{
							SMV_S,
							SMV_M,
							SMV_L,
							ScaleLabel_Shadow,
							ScaleLabel,
							Needle2_shadow,
							Needle2_main,
							Needle1_shadow,
							Needle1_main,
						}
					},
					Lab,
					plus_btn,
					minus_btn,
					plus_btn2,
					minus_btn2,
					back_btn,
					open_settingView_btn,
					settingView,
				},
			};

			back_btn.Clicked += (s, e) => Core.DispCom.ViewChange();

			open_settingView_btn.Clicked += (s, e) => settingView.IsVisible = true;

			settingView.ChangeOptsAccepted += SettingView_ChangeOptsAccepted;

			plus_btn.Clicked += (s, e) => ValueToShow_Needle1 += PlusMinus_Step;
			minus_btn.Clicked += (s, e) => ValueToShow_Needle1 -= PlusMinus_Step;
			plus_btn2.Clicked += (s, e) => ValueToShow_Needle2 += PlusMinus_Step;
			minus_btn2.Clicked += (s, e) => ValueToShow_Needle2 -= PlusMinus_Step;


			SizeChanged += MeterView_SizeChanged;
			/*DeviceDisplay.MainDisplayInfoChanged += (s, e) =>
			{
				SMV_L.PropUpdated();
				SMV_M.PropUpdated();
				SMV_S.PropUpdated();
				Needle1_shadow.PropUpdated();
				Needle1_main.PropUpdated();
				Needle2_shadow.PropUpdated();
				Needle2_main.PropUpdated();
			};*/
		}

		private void SettingView_ChangeOptsAccepted(object sender, System.EventArgs e)
		{
		}

		private void MeterView_SizeChanged(object sender, System.EventArgs e)
		{
		}

		public ContentView FirstView { get => this; }

		public void OnBSMDChanged(object sender, SMemLib.BSMDChangedEArgs e)
		{
			
		}

		public void OnLoaded()
		{
		}

		public void OnOpenDChanged(object sender, SMemLib.OpenDChangedEArgs e)
		{
			
		}

		public void OnPanelDChanged(object sender, SMemLib.ArrayDChangedEArgs e)
		{
			
		}

		public void OnSoundDChanged(object sender, SMemLib.ArrayDChangedEArgs e)
		{
			
		}

		public void OnUnloaded()
		{
			
		}
	}
}