
using System;
using System.Text;

using TR.BIDSDispX.Core.UFuncs;
using TR.BIDSSMemLib;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace TR.BIDSDispX.SampleView
{
	public class MeterView : ContentView, IBIDSDispX
	{
		static readonly double RadiusValue = Math.Min(DeviceDisplay.MainDisplayInfo.Height, DeviceDisplay.MainDisplayInfo.Width).Dp2Px() / 2.1;
		static readonly double MarginX = 50.Dp2Px();
		static readonly double MarginY = 50.Dp2Px();
		static readonly double Shadow_DstX = 4.Dp2Px();
		static readonly double Shadow_DstY = 4.Dp2Px();
		static readonly double Shadow_Opacity = 0.5;
		static readonly double Needle_Padding = 10.Dp2Px();
		static readonly double Needle_Height = 12.Dp2Px();
		static readonly double SMV_L_Height = 8.Dp2Px();
		static readonly double SMV_M_Height = 4.Dp2Px();
		static readonly double SMV_S_Height = 2.Dp2Px();
		static readonly double SMV_L_Width = 32.Dp2Px();
		static readonly double SMV_M_Width = 32.Dp2Px();
		static readonly double SMV_S_Width = 16.Dp2Px();
		static readonly int SMV_L_Step = 20;
		static readonly int SMV_M_Step = 10;
		static readonly int SMV_S_Step = 2;
		static readonly Color SMV_L_Color = Color.Red;
		static readonly Color SMV_M_Color = Color.Aqua;
		static readonly Color SMV_S_Color = Color.Black;


		Button plus_btn = new Button
		{
			Text = "+",
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.End,
			Margin = new Thickness(80)
		};
		Button minus_btn = new Button
		{
			Text = "-",
			HorizontalOptions = LayoutOptions.End,
			VerticalOptions = LayoutOptions.End,
			Margin = new Thickness(80)
		};
		Label Lab = new Label
		{
			VerticalOptions = LayoutOptions.Start
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
			Margin = new Thickness(5)
		};

		Needle needle_black_shadow = new Needle
		{
			Margin = new Thickness(Needle_Padding + Shadow_DstX, Needle_Padding + Shadow_DstY, 0, 0),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			NeedleOpacity = Shadow_Opacity,
			Radius = RadiusValue - Needle_Padding,
			NeedleHeight = Needle_Height,
		};
		Needle needle_black = new Needle
		{
			Margin = new Thickness(Needle_Padding, Needle_Padding, 0, 0),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			Radius = RadiusValue - Needle_Padding,
			NeedleHeight = Needle_Height,
		};

		ScaleMarksView SMV_L = new ScaleMarksView()
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			MarkStep = SMV_L_Step,
			MarkColor = SMV_L_Color,

			MarkHeight = SMV_L_Height,
			MarkWidth = SMV_L_Width,
		};

		ScaleMarksView SMV_M = new ScaleMarksView()
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			MarkStep = SMV_M_Step,
			MarkColor = SMV_M_Color,

			MarkHeight = SMV_M_Height,
			MarkWidth = SMV_M_Width,
		};

		ScaleMarksView SMV_S = new ScaleMarksView()
		{
			Margin = new Thickness(0),
			Radius = RadiusValue,

			MarkStep = SMV_S_Step,
			MarkColor = SMV_S_Color,

			MarkHeight = SMV_S_Height,
			MarkWidth = SMV_S_Width,
		};



		double __ValueToShow_Black = 0;
		double ValueToShow_Black
		{
			get => __ValueToShow_Black;
			set
			{
				if (value == __ValueToShow_Black)
					return;

				needle_black_shadow.ValueToShow = needle_black.ValueToShow = value;
				Lab.Text = new StringBuilder().Append("ValueToShow:").Append(value).Append("\nAngle:").Append(needle_black.Angle).ToString();
				__ValueToShow_Black = value;
			}
		}

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
							SMV_L,
							SMV_M,
							SMV_S,
							needle_black_shadow,
							needle_black,
						}
					},
					Lab,
					plus_btn,
					minus_btn,
					back_btn,
					open_settingView_btn,
					settingView,
				},
			};

			back_btn.Clicked += (s, e) => Core.DispCom.ViewChange();

			open_settingView_btn.Clicked += (s, e) => settingView.IsVisible = true;

			settingView.ChangeOptsAccepted += SettingView_ChangeOptsAccepted;

			plus_btn.Clicked += (s, e) => ValueToShow_Black += 5;
			minus_btn.Clicked += (s, e) => ValueToShow_Black -= 5;


			SizeChanged += MeterView_SizeChanged;
			DeviceDisplay.MainDisplayInfoChanged += (s, e) =>
			{
				SMV_L.PropUpdated();
				SMV_M.PropUpdated();
				SMV_S.PropUpdated();
				needle_black_shadow.PropUpdated();
				needle_black.PropUpdated();
			};
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