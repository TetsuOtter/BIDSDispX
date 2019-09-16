using System;
using System.ComponentModel;
using TR.BIDSSMemLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CATSDisp : ContentView, IBIDSDispX
  {
    DispString ds;

    private readonly Color ColRG = Color.FromRgb(0xFF, 0xFF, 0x00);
    private readonly Color ColR = Color.FromRgb(0xFF, 0x00, 0x00);
    private readonly Color ColG = Color.FromRgb(0x00, 0xFF, 0x00);
    private readonly Color ColB = Color.FromRgb(0x00, 0x00, 0xFF);
    private readonly Color ColW = Color.FromRgb(0xFF, 0xFF, 0xFF);

    public ContentView FirstView => this;

    public CATSDisp()
    {
      InitializeComponent();
      ds = new DispString();
      ds.PIType = ds.PITypes[0];
      BindingContext = ds;
    }

    public void OnPanelDChanged(object sender, SMemLib.ArrayDChangedEArgs e)
    {
      if(ds.PIType == ds.PITypes[0])
      {
        //Upper
        switch (e.NewArray[20])
        {
          case 1:
            ds.UpperStr = "ATS";
            ds.UpperColor = ColRG;
            break;
          case 2:
            ds.UpperStr = "ATS";
            ds.UpperColor = ColG;
            break;
          case 3:
            ds.UpperStr = "C-ATS";
            ds.UpperColor = ColG;
            break;
          default:
            ds.UpperStr = string.Empty;
            break;
        }

        //Center1
        int CenVal = 0;
        string CStr = string.Empty;
        Color CCol = Color.Transparent;
        if (e.NewArray[21] > 0) { CCol = ColG; CenVal = e.NewArray[21]; }
        else if (e.NewArray[22] > 0){ CCol = ColRG; CenVal = e.NewArray[22]; }
        else if (e.NewArray[23] > 0){ CCol = ColR; CenVal = e.NewArray[23]; }
        else CCol = Color.Transparent;

        if (CenVal == 0) CStr = string.Empty;
        else if (CenVal <= 3) CStr = ((10 * CenVal) + 5).ToString();
        else if (CenVal < 10) CStr = ((4 + CenVal) * 5).ToString();
        else if (CenVal == 10) CStr = "68";
        else CStr = ((3 + CenVal) * 5).ToString();

        //Center2
        switch (e.NewArray[24])
        {
          case 1:
            CStr = "0";
            CCol = ColR;
            break;
          case 2:
            CStr = "A0";
            CCol = ColR;
            break;
          case 3:
            CStr = "7.5";
            CCol = ColG;
            break;
          case 4:
            CStr = "L";
            CCol = ColG;
            break;
          case 5:
            CStr = "L";
            CCol = ColRG;
            break;
          case 6:
            CStr = "L";
            CCol = ColR;
            break;
          case 7:
            CStr = "NC";
            CCol = ColG;
            break;
          case 8:
            CStr = "NC";
            CCol = ColR;
            break;
          case 9:
            CStr = "NB";
            CCol = ColR;
            break;
          case 10:
            CStr = "×";
            CCol = ColR;
            break;
          case 11:
            CStr = "EB";
            CCol = ColR;
            break;
          case 12:
            CStr = "EM";
            CCol = ColR;
            break;
          case 13:
            CStr = "M15";
            CCol = ColG;
            break;
          case 14:
            CStr = "M15";
            CCol = ColR;
            break;
          case 15:
            CStr = "U15";
            CCol = ColG;
            break;
          case 16:
            CStr = "U15";
            CCol = ColR;
            break;
        }

        ds.CenterColor = CCol;
        ds.CenterStr = CStr;


        string LStr = string.Empty;
        Color LCol = Color.Transparent;
        Color LBCol = Color.Transparent;
        //Lower1
        switch (e.NewArray[25])
        {
          case 0:
            LStr = string.Empty;
            break;
          case 1:
            LStr = "15";
            LCol = ColR;
            break;
          case 2:
            LStr = "45";
            LCol = ColRG;
            break;
          case 3:
            LStr = "70";
            LCol = ColRG;
            break;
          case 4:
            LStr = "Ｐ";
            LCol = ColRG;
            break;
          case 5:
            LStr = "P接近";
            LCol = ColRG;
            break;
          case 6:
            LStr = "停Ｐ";
            LCol = ColG;
            break;
          case 7:
            LStr = "停Ｐ";
            LCol = ColRG;
            break;
          case 8:
            LStr = "停Ｐ";
            LCol = ColR;
            break;
          case 9:
            LStr = "Ｇ";
            LCol = ColG;
            break;
        }

        //Lower2
        switch (e.NewArray[26])
        {
          case 1:
            LStr = "普通";
            LCol = ColW;
            break;
          case 2:
            LStr = "✈急";
            LCol = ColW;
            LBCol = ColB;
            break;
          case 3:
            LStr = "特急";
            LCol = ColW;
            LBCol = ColR;
            break;
          case 4:
            LStr = "快特";
            LCol = ColW;
            LBCol = ColB;
            break;
          case 5:
            LStr = "✈快";
            LCol = ColW;
            LBCol = ColB;
            break;
          case 6:
            LStr = "WING";
            LCol = ColW;
            LBCol = ColG;
            break;
          case 7:
            LStr = "回送";
            LCol = ColRG;
            break;
        }

        ds.LowerColor = LCol;
        ds.LowerStr = LStr;
        ds.LowerBGBGColor = LBCol;

        return;
      }
      if (ds.PIType == ds.PITypes[1])
      {
        return;
      }
    }

    private void goBack(object sender, EventArgs e) => TR.BIDSDispX.Core.DispCom.ViewChange();
    

    private void ChangeBright(object sender, EventArgs e)
    {
      switch(ds.Brightness)
      {
        case 0:
          ds.Brightness = 0.1;
          break;
        case 0.1:
          ds.Brightness = 0.3;
          break;
        case 0.3:
          ds.Brightness = 0.5;
          break;
        case 0.5:
          ds.Brightness = 0.6;
          break;
        case 0.6:
          ds.Brightness = 0.0;
          break;
      }
    }

    private void goSetting(object sender, EventArgs e) => ds.SettingVisib = !ds.SettingVisib;
    

    private void FSUPlus(object sender, EventArgs e) => ds.FontSizeU += 2.0;
    private void FSUMinus(object sender, EventArgs e) => ds.FontSizeU -= 2.0;
    private void FSCPlus(object sender, EventArgs e) => ds.FontSizeC += 2.0;
    private void FSCMinus(object sender, EventArgs e) => ds.FontSizeC -= 2.0;
    private void FSLPlus(object sender, EventArgs e) => ds.FontSizeL += 2.0;
    private void FSLMinus(object sender, EventArgs e) => ds.FontSizeL -= 2.0;

    public void OnLoaded() { }

    public void OnSoundDChanged(object sender, SMemLib.ArrayDChangedEArgs e) { }

    public void OnBSMDChanged(object sender, SMemLib.BSMDChangedEArgs e) { }

    public void OnOpenDChanged(object sender, SMemLib.OpenDChangedEArgs e) { }

    public void OnUnloaded() { }
  }

  internal class DispString : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;


    private string piType = "NNN2_C-ATS";

    private string upperStr = "C-ATS";
    private string centerStr = "160";
    private string lowerStr = "✈特";

    private Color upperColor = Color.White;
    private Color centerColor = Color.Red;
    private Color lowerColor = Color.Lime;
    private Color lowerBGColor = Color.Red;

    private double brightness = 0.0;

    private double fontSizeU = 180;
    private double fontSizeC = 220;
    private double fontSizeL = 190;

    private bool settingVisib;


    public string PIType
    {
      get => piType;
      set
      {
        piType = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PIType"));
      }
    }

    public string[] PITypes
    {
      get => new string[]
      {
        "NNN2_C-ATS",
        "Ask_ATS-Ps"
      };
    }

    public string UpperStr
    {
      get => upperStr;
      set
      {
        upperStr = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpperStr"));
      }
    }
    public string CenterStr
    {
      get => centerStr;
      set
      {
        centerStr = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CenterStr"));
      }
    }
    public string LowerStr
    {
      get => lowerStr;
      set
      {
        lowerStr = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LowerStr"));
      }
    }

    public Color UpperColor
    {
      get => upperColor;
      set
      {
        upperColor = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpperColor"));
      }
    }
    public Color CenterColor
    {
      get => centerColor;
      set
      {
        centerColor = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CenterColor"));
      }
    }
    public Color LowerColor
    {
      get => lowerColor;
      set
      {
        lowerColor = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LowerColor"));
      }
    }
    public Color LowerBGBGColor
    {
      get => lowerBGColor;
      set
      {
        lowerBGColor = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LowerBGColor"));
      }
    }

    public double Brightness
    {
      get => brightness;
      set
      {
        brightness = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Brightness"));
      }
    }

    public double FontSizeU
    {
      get => fontSizeU;
      set
      {
        fontSizeU = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FontSizeU"));
      }
    }
    public double FontSizeC
    {
      get => fontSizeC;
      set
      {
        fontSizeC = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FontSizeC"));
      }
    }
    public double FontSizeL
    {
      get => fontSizeL;
      set
      {
        fontSizeL = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FontSizeL"));
      }
    }

    public bool SettingVisib
    {
      get => settingVisib;
      set
      {
        settingVisib = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SettingVisib"));
      }
    }
  }
}