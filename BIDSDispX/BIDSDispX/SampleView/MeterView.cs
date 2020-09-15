
using System.Text;

using TR.BIDSSMemLib;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace TR.BIDSDispX.SampleView
{
	public class MeterView : ContentView, IBIDSDispX
	{
		Button plus_btn = new Button
		{
			Text = "+",
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Center,
			Margin = new Thickness(50)
		};
		Button minus_btn = new Button
		{
			Text = "-",
			HorizontalOptions = LayoutOptions.End,
			VerticalOptions = LayoutOptions.Center,
			Margin = new Thickness(50)
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
			Margin = new Thickness(51, 102, 0, 0),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			NeedleOpacity = 0.5,
			Radius = 150
		};
		Needle needle_black = new Needle
		{
			Margin = new Thickness(50, 100, 0, 0),
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Start,
			Radius = 150
		};

		ScaleMarksView SMV = new ScaleMarksView()
		{
			Margin = new Thickness(40, 90, 0, 0),
			Radius = 160
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
					SMV,
					needle_black_shadow,
					needle_black,
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

			Dispatcher.BeginInvokeOnMainThread(() => SMV.PropsUpdated());

			SizeChanged += MeterView_SizeChanged;
		}

		private void SettingView_ChangeOptsAccepted(object sender, System.EventArgs e)
		{
			//throw new System.NotImplementedException();
			SMV.PropsUpdated();
		}

		private void MeterView_SizeChanged(object sender, System.EventArgs e)
		{
			//throw new System.NotImplementedException();
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