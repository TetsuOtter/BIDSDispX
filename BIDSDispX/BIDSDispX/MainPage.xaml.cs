using System;
using System.ComponentModel;
using Xamarin.Forms;
using TR.BIDSsv;
using TR.BIDSDispX.Core;
using System.Threading.Tasks;

namespace TR.BIDSDispX
{
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetTitleView(this, null);
			Common.Start(NO_SMEM_MODE: true);

			#region BinaryAS Setting
			Common.AutoSendSetting.BasicBVE5AS = false;
			Common.AutoSendSetting.BasicCommonAS = false;
			Common.AutoSendSetting.BasicConstAS = false;
			Common.AutoSendSetting.BasicHandleAS = false;
			Common.AutoSendSetting.BasicOBVEAS = false;
			Common.AutoSendSetting.BasicPanelAS = false;
			Common.AutoSendSetting.BasicSoundAS = false;
			#endregion

			IBIDSsv cm = new udp();
			cm.Connect(string.Empty);
			Common.Add(ref cm);

			DispCom.MainPageInstance = this;

			DispCom.ViewChanged += MainPage_PageChange;

			DispCom.CurrentView = new StartPage();
		}

		private void MainPage_PageChange(object sender, EventArgs e) => Content = (View)sender ?? new StartPage();
		


		~MainPage(){
			DispCom.CurrentView?.OnUnloaded();
			Common.Dispose();
		}

		protected override void OnDisappearing()
		{
			Common.Dispose();
			base.OnDisappearing();
		}
	}
}
