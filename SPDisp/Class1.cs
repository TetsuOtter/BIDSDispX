using System;
using TR.BIDSDispX;
using Xamarin.Forms;

namespace SPDisp
{
  public class Class1 : IBIDSDispX
  {
    View1 v;
    public Class1() => v = new View1();

    public ContentView FirstView => v;

    public void OnBSMDChanged(object sender, TR.BIDSSMemLib.SMemLib.BSMDChangedEArgs e) => v.OnBSMDChanged(sender, e);

    public void OnLoaded() { }

    public void OnOpenDChanged(object sender, TR.BIDSSMemLib.SMemLib.OpenDChangedEArgs e) { }

    public void OnPanelDChanged(object sender, TR.BIDSSMemLib.SMemLib.ArrayDChangedEArgs e) { }

    public void OnSoundDChanged(object sender, TR.BIDSSMemLib.SMemLib.ArrayDChangedEArgs e) { }

    public void OnUnloaded() { }
  }
}
