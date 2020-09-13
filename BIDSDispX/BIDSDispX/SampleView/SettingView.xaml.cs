using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.SampleView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingView : ContentView
	{
		public event EventHandler ChangeOptsAccepted;
		public SettingView()
		{
			InitializeComponent();
		}

		private async void Save_Button_Clicked(object sender, EventArgs e)
		{
			//ref : https://docs.microsoft.com/ja-jp/xamarin/xamarin-forms/user-interface/pop-ups
			if (await Core.DispCom.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No"))
			{
				ChangeOptsAccepted?.Invoke(this, null);

				IsVisible = false;
			}
		}

		private async void Back_withoutSave_Clicked(object sender, EventArgs e)
		{
			if (await Core.DispCom.DisplayAlert("Question?", "ABC\nWould you like to play a game", "Yes", "No"))
			{
				//ChangeOptsAccepted?.Invoke(this, null);

				IsVisible = false;
			}

		}
	}
}