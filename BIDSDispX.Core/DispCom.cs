using System;
using System.Threading.Tasks;
using TR.BIDSsv;
using Xamarin.Forms;

namespace TR.BIDSDispX.Core
{
  public static class DispCom
  {
    static public double WindowHeight { get; set; }
    static public double WindowWidth { get; set; }
    static public ContentPage MainPageInstance { private get; set; }

    private static IBIDSDispX currentView = null;

    public static IBIDSDispX CurrentView
    {
      get => currentView;
      set
      {
        if (currentView != null)
        {
          Common.BSMDChanged -= currentView.OnBSMDChanged;
          Common.OpenDChanged -= currentView.OnOpenDChanged;
          Common.PanelDChanged -= currentView.OnPanelDChanged;
          Common.SoundDChanged -= currentView.OnSoundDChanged;
          currentView.OnUnloaded();
        }
        currentView = value;
        if (currentView != null)
        {
          currentView.OnLoaded();
          Common.BSMDChanged += currentView.OnBSMDChanged;
          Common.OpenDChanged += currentView.OnOpenDChanged;
          Common.PanelDChanged += currentView.OnPanelDChanged;
          Common.SoundDChanged += currentView.OnSoundDChanged;
        }

        ViewChange(currentView.FirstView);
      }
    }

    public static event EventHandler ViewChanged;
    public static void ViewChange(View v = null) => ViewChanged?.Invoke(v, null);

    public static void AppExit()
    {
      Common.Remove();
      Common.Dispose();
      System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
    }

    static public Task DisplayAlert(string title, string Msg, string CancelBtnText)
      => MainPageInstance.DisplayAlert(title, Msg, CancelBtnText);
    static public Task<bool> DisplayAlert(string title, string Msg, string AcceptBtnText, string CancelBtnText)
      => MainPageInstance.DisplayAlert(title, Msg, AcceptBtnText, CancelBtnText);

  }
}
