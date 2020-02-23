using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TR.BIDSDispX.Core;
using TR.BIDSSMemLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class StartPage : ContentView, IBIDSDispX
  {
    private SetValue SVClas;
    private OnlineSync OSync;

    public StartPage()
    {
      InitializeComponent();
      SVClas = new SetValue() { Speed = "Initial String" };
      OSync = new OnlineSync();
      FindDlls();

      BindingContext = SVClas;

      LicenseLab.Text = Properties.Resources.LicenseFile;
    }

    public void OnBSMDChanged(object sender, SMemLib.BSMDChangedEArgs e)
    {
      if (IsEnabled) SVClas.Speed = e.NewData.StateData.V.ToString();
    }

    private void CATS_Start(object sender, EventArgs e) => DispCom.CurrentView = new CATSDisp();
    //private void CATS_Start(object sender, EventArgs e) => DispCom.CurrentView = new drivesup.RootClass();

    private string FDPath => OSync.ModsFolderName;

    public ContentView FirstView => this;

    private void FindDlls()
    {
      SVClas.Msg = string.Empty;
      List<string> fnames = new List<string>();

      var mlsl = OSync.FileReadList();
      if (mlsl != null && mlsl.Count > 0)
        for (int i = 0; i < mlsl.Count; i++)
          fnames.Add(mlsl[i].ModName);

      if (!(fnames?.Count > 0)) SVClas.Dlls = new string[] { "There are no dlls in ↓", FDPath };
      else SVClas.Dlls = fnames?.ToArray();
    }

    private void LoadLib(object sender, EventArgs e)
    {
      try
      {
        var cv = OSync.LoadLib(SVClas.SelectedDll);
        if (cv == null) SVClas.Msg = "Can't find the selected file";
        else DispCom.CurrentView = cv;
      }
      catch (FileNotFoundException)
      {
        SVClas.Msg = "FileNotFound : " + SVClas.SelectedDll;
        return;
      }
      catch (BadImageFormatException)
      {
        SVClas.Msg = "Is it really dll? : " + SVClas.SelectedDll;
        return;
      }
    }

    private void Liblist_Reflesh(object sender, EventArgs e) => FindDlls();

    private void ShowLicense(object sender, EventArgs e) => SVClas.LicenseLabVisible = !SVClas.LicenseLabVisible;

    private void Add_Local_Lib(object sender, EventArgs e)
    {
      try
      {
        OSync.AddMod();
      } catch (Exception ex)
      {
        SVClas.Msg = ex.Message;
      }
    }

    public void OnLoaded() { }

    public void OnPanelDChanged(object sender, SMemLib.ArrayDChangedEArgs e) { }

    public void OnSoundDChanged(object sender, SMemLib.ArrayDChangedEArgs e) { }

    public void OnOpenDChanged(object sender, SMemLib.OpenDChangedEArgs e) { }

    public void OnUnloaded() { }

    private void AppExitEv(object sender, EventArgs e) => DispCom.AppExit();
  }

  internal class SetValue : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private string speed;
    private string[] dlls;
    private string selectedDll;
    private string msg;

    private bool llvis = false;

    public string Speed
    {
      get => speed;
      set
      {
        speed = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed"));
      }
    }

    public string[] Dlls
    {
      get => dlls;
      set
      {
        dlls = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dlls"));
      }
    }
    public string SelectedDll
    {
      get => selectedDll;
      set
      {
        selectedDll = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedDll"));
      }
    }
    public string Msg
    {
      get => msg;
      set
      {
        msg = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Msg"));
      }
    }

    public bool LicenseLabVisible
    {
      get => llvis;
      set
      {
        llvis = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LicenseLabVisible"));
      }
    }
  }
}