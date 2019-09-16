using System;
using TR.BIDSSMemLib;
using Xamarin.Forms;

namespace TR.BIDSDispX.ModTemp
{
  public class IFClass : IBIDSDispX
  {
    public ContentView FirstView { get; } = new FirstView();

    public void OnBSMDChanged(object sender, SMemLib.BSMDChangedEArgs e)
    {
      //必要に応じてcvのMethodを呼び出す形。
    }

    public void OnLoaded()
    {
      //必要に応じてcvのMethodを呼び出す形。
    }

    public void OnOpenDChanged(object sender, SMemLib.OpenDChangedEArgs e)
    {
      //必要に応じてcvのMethodを呼び出す形。
    }

    public void OnPanelDChanged(object sender, SMemLib.ArrayDChangedEArgs e)
    {
      //必要に応じてcvのMethodを呼び出す形。
    }

    public void OnSoundDChanged(object sender, SMemLib.ArrayDChangedEArgs e)
    {
      //必要に応じてcvのMethodを呼び出す形。
    }

    public void OnUnloaded()
    {
      //必要に応じてcvのMethodを呼び出す形。
    }
  }
}
