using Xamarin.Forms;
using TR.BIDSSMemLib;

namespace TR.BIDSDispX
{
  public interface IBIDSDispX
  {
    ContentView FirstView { get; }

    void OnLoaded();

    void OnPanelDChanged(object sender, SMemLib.ArrayDChangedEArgs e);
    void OnSoundDChanged(object sender, SMemLib.ArrayDChangedEArgs e);
    void OnBSMDChanged(object sender, SMemLib.BSMDChangedEArgs e);
    void OnOpenDChanged(object sender, SMemLib.OpenDChangedEArgs e);

    void OnUnloaded();
  }
}
