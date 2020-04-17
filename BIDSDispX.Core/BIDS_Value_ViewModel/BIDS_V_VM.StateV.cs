using System;
using System.ComponentModel;

namespace TR.BIDSDispX.Core
{
  public partial class BIDS_Value_ViewModel : INotifyPropertyChanged, IDisposable
  {
    #region kmph
    private double __kmph = 0;
    public double kmph_v { get => __kmph; }
    public string kmph { get => kmph_v.ToString(); }

    public int kmphd0_v { get => kmph_v.GetDigitVal(0); }
    public string kmphd0 { get => kmphd0_v.ToString(); }

    public int kmphd1_v { get => UFunc.GetDigitVal(kmph_v, 1); }
    public string kmphd1 { get => kmphd1_v.ToString(); }

    public int kmphd2_v { get => UFunc.GetDigitVal(kmph_v, 2); }
    public string kmphd2 { get => kmphd2_v.ToString(); }
    #endregion

    #region bc
    private double __bc = 0;
    public double bc_v { get => __bc; }
    public string bc { get => bc_v.ToString(); }

    public int bcd0_v { get => bc_v.GetDigitVal(0); }
    public string bcd0 { get => bcd0_v.ToString(); }

    public int bcd1_v { get => UFunc.GetDigitVal(bc_v, 1); }
    public string bcd1 { get => bcd1_v.ToString(); }

    public int bcd2_v { get => UFunc.GetDigitVal(bc_v, 2); }
    public string bcd2 { get => bcd2_v.ToString(); }
    #endregion

    #region mr
    private double __mr = 0;
    public double mr_v { get => __mr; }
    public string mr { get => mr_v.ToString(); }

    public int mrd0_v { get => mr_v.GetDigitVal(0); }
    public string mrd0 { get => mrd0_v.ToString(); }

    public int mrd1_v { get => UFunc.GetDigitVal(mr_v, 1); }
    public string mrd1 { get => mrd1_v.ToString(); }

    public int mrd2_v { get => UFunc.GetDigitVal(mr_v, 2); }
    public string mrd2 { get => mrd2_v.ToString(); }
    #endregion

    #region er
    private double __er = 0;
    public double er_v { get => __er; }
    public string er { get => er_v.ToString(); }

    public int erd0_v { get => er_v.GetDigitVal(0); }
    public string erd0 { get => erd0_v.ToString(); }

    public int erd1_v { get => UFunc.GetDigitVal(er_v, 1); }
    public string erd1 { get => erd1_v.ToString(); }

    public int erd2_v { get => UFunc.GetDigitVal(er_v, 2); }
    public string erd2 { get => erd2_v.ToString(); }
    #endregion

    #region bp
    private double __bp = 0;
    public double bp_v { get => __bp; }
    public string bp { get => bp_v.ToString(); }

    public int bpd0_v { get => bp_v.GetDigitVal(0); }
    public string bpd0 { get => bpd0_v.ToString(); }

    public int bpd1_v { get => UFunc.GetDigitVal(bp_v, 1); }
    public string bpd1 { get => bpd1_v.ToString(); }

    public int bpd2_v { get => UFunc.GetDigitVal(bp_v, 2); }
    public string bpd2 { get => bpd2_v.ToString(); }
    #endregion

    #region sap
    private double __sap = 0;
    public double sap_v { get => __sap; }
    public string sap { get => sap_v.ToString(); }

    public int sapd0_v { get => sap_v.GetDigitVal(0); }
    public string sapd0 { get => sapd0_v.ToString(); }

    public int sapd1_v { get => UFunc.GetDigitVal(sap_v, 1); }
    public string sapd1 { get => sapd1_v.ToString(); }

    public int sapd2_v { get => UFunc.GetDigitVal(sap_v, 2); }
    public string sapd2 { get => sapd2_v.ToString(); }
    #endregion

    #region dst
    private double __dst = 0;
    public double dst_v { get => __dst; }
    public string dst { get => dst_v.ToString(); }

    public int dstd0_v { get => dst_v.GetDigitVal(0); }
    public string dstd0 { get => dstd0_v.ToString(); }

    public int dstd1_v { get => UFunc.GetDigitVal(dst_v, 1); }
    public string dstd1 { get => dstd1_v.ToString(); }

    public int dstd2_v { get => UFunc.GetDigitVal(dst_v, 2); }
    public string dstd2 { get => dstd2_v.ToString(); }

    public int dstd3_v { get => UFunc.GetDigitVal(dst_v, 3); }
    public string dstd3 { get => dstd3_v.ToString(); }

    public int dstd4_v { get => UFunc.GetDigitVal(dst_v, 4); }
    public string dstd4 { get => dstd4_v.ToString(); }

    public int dstd5_v { get => UFunc.GetDigitVal(dst_v, 5); }
    public string dstd5 { get => dstd5_v.ToString(); }

    public int dstd6_v { get => UFunc.GetDigitVal(dst_v, 6); }
    public string dstd6 { get => dstd6_v.ToString(); }
    #endregion

    #region door
    private int __door = 0;
    public int door_v { get => __door; }
    public string door { get => door_v.ToString(); }
    #endregion

    #region csc
    private int __csc = 0;
    public int csc_v { get => __csc; }
    public string csc { get => csc_v.ToString(); }
    #endregion

    #region power
    private int __power = 0;
    public int power_v { get => __power; }
    public string power { get => power_v.ToString(); }
    #endregion

    #region brake
    private int __brake = 0;
    public int brake_v { get => __brake; }
    public string brake { get => brake_v.ToString(); }
    #endregion

    #region hour
    private int __hour = 0;
    public int hour_v { get => __hour; }
    public string hour { get => hour_v.ToString(); }
    #endregion

    #region min
    private int __min = 0;
    public int min_v { get => __min; }
    public string min { get => min_v.ToString(); }
    #endregion

    #region sec
    private int __sec = 0;
    public int sec_v { get => __sec; }
    public string sec { get => sec_v.ToString(); }
    #endregion
  }
}
