using System;
using TR.BIDSsv;
using Xamarin.Forms;

namespace TR.BIDSDispX.Core
{
  public static class DispCom
  {
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
  }
}
