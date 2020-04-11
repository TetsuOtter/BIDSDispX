using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDisp
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class View1 : ContentView
  {
    DispString ds;

    public View1()
    {
      InitializeComponent();
      ds = new DispString();
      BindingContext = ds;
    }

    public void OnBSMDChanged(object sender, TR.BIDSSMemLib.SMemLib.BSMDChangedEArgs e)
    {
      if (e.NewData.StateData.T > e.OldData.StateData.T) ds.SpeedD = e.NewData.StateData.V;
    }

    private void BackBtnEv(object sender, EventArgs e)
    {
      TR.BIDSDispX.Core.DispCom.ViewChange();
    }
  }


  internal class DispString : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private string speedStr = "0.0 km/h";
    private double speedD = 0.0;

    public string SpeedStr
    {
      get => speedStr;
      set
      {
        speedStr = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SpeedStr"));
      }
    }

    public double SpeedD
    {
      get => speedD;
      set
      {
        speedD = value;
        SpeedStr = speedD.ToString("0.0") + " km/h";
      }
    }
  }

}