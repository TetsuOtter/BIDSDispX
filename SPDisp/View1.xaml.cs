using System;
using System.ComponentModel;
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
			//if (e.NewData.StateData.T > e.OldData.StateData.T) ds.SpeedD = e.NewData.StateData.V;
			if (e.NewData.StateData.T != e.OldData.StateData.T) _ = Task.Run(() =>
				 {
					 TimeSpan ts = TimeSpan.FromMilliseconds(e.NewData.StateData.T);
					 //ds.SpeedStr = new StringBuilder().Append(ts.Hours.ToString("00")).Append(':').Append(ts.Minutes.ToString("00")).Append(':').Append(ts.Seconds.ToString("00")).ToString();
					 //ds.SpeedStr = new StringBuilder().Append(ts.Hours).Append(':').Append(ts.Minutes).Append(':').Append(ts.Seconds).Append('.').Append(ts.Milliseconds).ToString();
					 ds.HH = ts.Hours;
					 ds.MM = ts.Minutes;
					 ds.SS = ts.Seconds;
				 });
			//if (!Equals(e.OldData.StateData.Z, e.NewData.StateData.Z)) ds.SpeedD = e.NewData.StateData.Z;
			//if (!Equals(e.OldData.StateData.V, e.NewData.StateData.V)) ds.SpeedD = e.NewData.StateData.V;
		}

		private void BackBtnEv(object sender, EventArgs e)
		{
			TR.BIDSDispX.Core.DispCom.ViewChange();
		}
	}


	internal class DispString : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string speedStr = "0.0";
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
				SpeedStr = speedD.ToString("0.0");// + " kmph";
			}
		}
		private string HHStr = "00";
		private int hh = 0;
		public int HH
		{
			get => hh;
			set
			{
				if (hh != value)
				{
					hh = HH;
					HHStr = value.ToString("00");
				}
			}
		}
		private string MMStr = "00";
		private int mm = 0;
		public int MM
		{
			get => mm;
			set
			{
				if (mm != value)
				{
					mm = MM;
					MMStr = value.ToString("00");
				}
			}
		}
		private int ss = -1;
		public int SS
		{
			get => ss;
			set
			{
				if (ss != value)
				{
					ss = SS;
					SpeedStr = new StringBuilder(HHStr).Append(':').Append(MMStr).Append(':').Append(value.ToString("00")).ToString();// + " kmph";
				}
			}
		}

	}

}