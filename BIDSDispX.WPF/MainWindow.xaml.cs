using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.BIDSDispX.WPF
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Xamarin.Forms.Platform.WPF.FormsApplicationPage
  {
    public MainWindow()
    {
      InitializeComponent();
      BorderThickness = new Thickness(0);
      BorderBrush = Brushes.Transparent;
      Xamarin.Forms.Forms.SetFlags("Shapes_Experimental");
      Xamarin.Forms.Forms.Init();
      LoadApplication(new BIDSDispX.App());
    }

    private void FormsApplicationPage_Closed(object sender, EventArgs e) => Core.DispCom.AppExit();

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (WindowState != WindowState.Maximized) return;
      Border borderWindow = this.Template.FindName("BorderWindow", this) as Border;
      if (borderWindow != null)
      {
        borderWindow.BorderThickness = new Thickness(0);
      }
      Grid CmdBar = this.Template.FindName("PART_CommandsBar", this) as Grid;
      if (CmdBar != null)
      {
        CmdBar.Visibility = Visibility.Collapsed;
      }
    }
  }
}
