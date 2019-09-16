using System;
using System.ComponentModel;
using Xamarin.Forms;
using TR.BIDSsv;
using TR.BIDSDispX.Core;

namespace TR.BIDSDispX
{
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      Common.Start(5);
      communication cm = new communication();
      cm.Connect(string.Empty);
      Common.Add(ref cm);

      DispCom.ViewChanged += MainPage_PageChange;

      DispCom.CurrentView = new StartPage();
    }

    private void MainPage_PageChange(object sender, EventArgs e) => Content = (View)sender ?? new StartPage();
    

    ~MainPage(){
      DispCom.CurrentView?.OnUnloaded();
      Common.Dispose();
    }

  }
}
