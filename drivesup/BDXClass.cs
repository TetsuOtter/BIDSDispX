using System;
using TR.BIDSDispX;
using TR.BIDSSMemLib;
using Xamarin.Forms;

namespace TR.drivesup
{
  public class BDXClass : IBIDSDispX
  {
    public ContentView FirstView => new RootView();

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
