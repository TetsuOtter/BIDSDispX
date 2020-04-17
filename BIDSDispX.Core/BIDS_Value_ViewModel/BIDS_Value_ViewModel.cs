using System;
using System.Threading.Tasks;
using System.ComponentModel;
using TR.BIDSsv;

namespace TR.BIDSDispX.Core
{
  //refer : https://qiita.com/smbkrysk14/items/e5f97b1ddbef9b318426

  public partial class BIDS_Value_ViewModel : INotifyPropertyChanged, IDisposable
  {
    public event PropertyChangedEventHandler PropertyChanged;
    public BIDS_Value_ViewModel()
    {
      Common.BSMDChanged += Common_BSMDChanged;
      Common.OpenDChanged += Common_OpenDChanged;
      Common.PanelDChanged += Common_PanelDChanged;
      Common.SoundDChanged += Common_SoundDChanged;
    }

    const int ArrLen_MAX = 256;
    private void Common_SoundDChanged(object sender, BIDSSMemLib.SMemLib.ArrayDChangedEArgs e)
      => Parallel.For(0, ArrLen_MAX, 
        (i) => ValueRefresherInt(ref __sound[i], e.NewArray?.Length > i ? e.NewArray[i] : 0, e.OldArray?.Length > i ? e.OldArray[i] : 0, "sound" + i.ToString()));
    private void Common_PanelDChanged(object sender, BIDSSMemLib.SMemLib.ArrayDChangedEArgs e)
      => Parallel.For(0, ArrLen_MAX, 
        (i) => ValueRefresherInt(ref __ats[i], e.NewArray?.Length > i ? e.NewArray[i] : 0, e.OldArray?.Length > i ? e.OldArray[i] : 0, "ats" + i.ToString()));

    private void Common_OpenDChanged(object sender, BIDSSMemLib.SMemLib.OpenDChangedEArgs e)
    {
      
    }

    private void Common_BSMDChanged(object sender, BIDSSMemLib.SMemLib.BSMDChangedEArgs e)
    {
      ValueRefresher(ref __kmph, e.NewData.StateData.V, e.OldData.StateData.V, "kmph");

      ValueRefresher(ref __bc, e.NewData.StateData.BC, e.OldData.StateData.BC, "bc");
      ValueRefresher(ref __mr, e.NewData.StateData.MR, e.OldData.StateData.MR, "mr");
      ValueRefresher(ref __er, e.NewData.StateData.ER, e.OldData.StateData.ER, "er");
      ValueRefresher(ref __bp, e.NewData.StateData.BP, e.OldData.StateData.BP, "bp");
      ValueRefresher(ref __sap, e.NewData.StateData.SAP, e.OldData.StateData.SAP, "sap");

      ValueRefresher(ref __dst, e.NewData.StateData.Z, e.OldData.StateData.Z, "dst", 7);

      ValueRefresherInt(ref __door, e.NewData.IsDoorClosed.ToInt32(), e.OldData.IsDoorClosed.ToInt32(), "door", -1);
      ValueRefresherInt(ref __csc, e.NewData.HandleData.C, e.OldData.HandleData.C, "csc", -1);
      ValueRefresherInt(ref __power, e.NewData.HandleData.P, e.OldData.HandleData.P, "power", -1);
      ValueRefresherInt(ref __brake, e.NewData.HandleData.B, e.OldData.HandleData.B, "brake", -1);

      TimeSpan ots = TimeSpan.FromMilliseconds(e.OldData.StateData.T);
      TimeSpan nts = TimeSpan.FromMilliseconds(e.NewData.StateData.T);
      ValueRefresherInt(ref __hour, nts.Hours, ots.Hours, "hour", -1);
      ValueRefresherInt(ref __min, nts.Minutes, ots.Minutes, "min", -1);
      ValueRefresherInt(ref __sec, nts.Seconds, ots.Seconds, "sec", -1);
    }

    #region Useful Methods
    private void InvokeBIDSVP(in string propName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName + "_v"));
    }
    private void ValueRefresher(ref double vars, double newValue, double oldValue, string propName, int digitMax = 3)
    {
      if (newValue != oldValue)
      {
        vars = newValue;
        InvokeBIDSVP(propName);

        if (digitMax < 0) return;

        for (int i = 0; i < digitMax; i++)
          if (newValue.GetDigitVal(i) != newValue.GetDigitVal(i))
            InvokeBIDSVP(propName + "d" + i.ToString());
      }
    }
    private void ValueRefresherInt(ref int vars, int newValue, int oldValue, string propName, int digitMax = 3)
    {
      if (newValue != oldValue)
      {
        vars = newValue;
        InvokeBIDSVP(propName);

        if (digitMax < 0) return;

        for (int i = 0; i < digitMax; i++)
          if (newValue.GetDigitVal(i) != newValue.GetDigitVal(i))
            InvokeBIDSVP(propName + "d" + i.ToString());
      }
    }

    private static Hands CtrlHands(Hands hands,int? power,int? brake,int? reverser)
    {
      hands.P = power ?? hands.P;
      hands.B = brake ?? hands.B;
      hands.R = reverser ?? hands.R;

      return hands;
    }
    #endregion

    #region IDisposable Support
    private bool disposedValue = false; // 重複する呼び出しを検出するには

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
        }

        // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
        // TODO: 大きなフィールドを null に設定します。

        Common.BSMDChanged -= Common_BSMDChanged;
        Common.OpenDChanged -= Common_OpenDChanged;
        Common.PanelDChanged -= Common_PanelDChanged;
        Common.SoundDChanged -= Common_SoundDChanged;

        disposedValue = true;
      }
    }

    // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
    // ~BIDS_Value_ViewModel()
    // {
    //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
    //   Dispose(false);
    // }

    // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
    void IDisposable.Dispose()
    {
      // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
      Dispose(true);
      // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}
