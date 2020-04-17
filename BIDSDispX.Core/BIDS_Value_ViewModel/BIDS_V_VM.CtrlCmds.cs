using System;
using System.ComponentModel;
using System.Windows.Input;
using TR.BIDSsv;
using Xamarin.Forms;

namespace TR.BIDSDispX.Core
{
  public partial class BIDS_Value_ViewModel : INotifyPropertyChanged, IDisposable
  {
    public readonly ICommand Ctrl_PowerH_Up = new Command(() => CtrlHands(Common.Ctrl_Hand, Common.Ctrl_Hand.P + 1, null, null));
    public readonly ICommand Ctrl_PowerH_Down = new Command(() => CtrlHands(Common.Ctrl_Hand, Common.Ctrl_Hand.P - 1, null, null));

    public readonly ICommand Ctrl_BrakeH_Up = new Command(() => CtrlHands(Common.Ctrl_Hand, null, Common.Ctrl_Hand.B + 1, null));
    public readonly ICommand Ctrl_BrakeH_Down = new Command(() => CtrlHands(Common.Ctrl_Hand, null, Common.Ctrl_Hand.B - 1, null));

    public readonly ICommand Ctrl_Reverser_F = new Command(() => CtrlHands(Common.Ctrl_Hand, null, null, 1));
    public readonly ICommand Ctrl_Reverser_N = new Command(() => CtrlHands(Common.Ctrl_Hand, null, null, 0));
    public readonly ICommand Ctrl_Reverser_R = new Command(() => CtrlHands(Common.Ctrl_Hand, null, null, -1));

    public readonly ICommand Ctrl_Horn1_Push = new Command(() => Common.Ctrl_Key[0] = true);
    public readonly ICommand Ctrl_Horn1_Release = new Command(() => Common.Ctrl_Key[0] = false);

    public readonly ICommand Ctrl_Horn2_Push = new Command(() => Common.Ctrl_Key[1] = true);
    public readonly ICommand Ctrl_Horn2_Release = new Command(() => Common.Ctrl_Key[1] = false);

    public readonly ICommand Ctrl_MusicHorn_Push = new Command(() => Common.Ctrl_Key[2] = true);
    public readonly ICommand Ctrl_MusicHorn_Release = new Command(() => Common.Ctrl_Key[2] = false);

    public readonly ICommand Ctrl_ATSKey_S_Push = new Command(() => Common.Ctrl_Key[4] = true);
    public readonly ICommand Ctrl_ATSKey_S_Release = new Command(() => Common.Ctrl_Key[4] = false);

    public readonly ICommand Ctrl_ATSKey_A1_Push = new Command(() => Common.Ctrl_Key[5] = true);
    public readonly ICommand Ctrl_ATSKey_A1_Release = new Command(() => Common.Ctrl_Key[5] = false);

    public readonly ICommand Ctrl_ATSKey_A2_Push = new Command(() => Common.Ctrl_Key[6] = true);
    public readonly ICommand Ctrl_ATSKey_A2_Release = new Command(() => Common.Ctrl_Key[6] = false);

    public readonly ICommand Ctrl_ATSKey_B1_Push = new Command(() => Common.Ctrl_Key[7] = true);
    public readonly ICommand Ctrl_ATSKey_B1_Release = new Command(() => Common.Ctrl_Key[7] = false);

    public readonly ICommand Ctrl_ATSKey_B2_Push = new Command(() => Common.Ctrl_Key[8] = true);
    public readonly ICommand Ctrl_ATSKey_B2_Release = new Command(() => Common.Ctrl_Key[8] = false);

    public readonly ICommand Ctrl_ATSKey_C1_Push = new Command(() => Common.Ctrl_Key[9] = true);
    public readonly ICommand Ctrl_ATSKey_C1_Release = new Command(() => Common.Ctrl_Key[9] = false);

    public readonly ICommand Ctrl_ATSKey_C2_Push = new Command(() => Common.Ctrl_Key[10] = true);
    public readonly ICommand Ctrl_ATSKey_C2_Release = new Command(() => Common.Ctrl_Key[10] = false);

    public readonly ICommand Ctrl_ATSKey_D_Push = new Command(() => Common.Ctrl_Key[11] = true);
    public readonly ICommand Ctrl_ATSKey_D_Release = new Command(() => Common.Ctrl_Key[11] = false);

    public readonly ICommand Ctrl_ATSKey_E_Push = new Command(() => Common.Ctrl_Key[12] = true);
    public readonly ICommand Ctrl_ATSKey_E_Release = new Command(() => Common.Ctrl_Key[12] = false);

    public readonly ICommand Ctrl_ATSKey_F_Push = new Command(() => Common.Ctrl_Key[13] = true);
    public readonly ICommand Ctrl_ATSKey_F_Release = new Command(() => Common.Ctrl_Key[13] = false);

    public readonly ICommand Ctrl_ATSKey_G_Push = new Command(() => Common.Ctrl_Key[14] = true);
    public readonly ICommand Ctrl_ATSKey_G_Release = new Command(() => Common.Ctrl_Key[14] = false);

    public readonly ICommand Ctrl_ATSKey_H_Push = new Command(() => Common.Ctrl_Key[15] = true);
    public readonly ICommand Ctrl_ATSKey_H_Release = new Command(() => Common.Ctrl_Key[15] = false);

    public readonly ICommand Ctrl_ATSKey_I_Push = new Command(() => Common.Ctrl_Key[16] = true);
    public readonly ICommand Ctrl_ATSKey_I_Release = new Command(() => Common.Ctrl_Key[16] = false);

    public readonly ICommand Ctrl_ATSKey_J_Push = new Command(() => Common.Ctrl_Key[17] = true);
    public readonly ICommand Ctrl_ATSKey_J_Release = new Command(() => Common.Ctrl_Key[17] = false);

    public readonly ICommand Ctrl_ATSKey_K_Push = new Command(() => Common.Ctrl_Key[18] = true);
    public readonly ICommand Ctrl_ATSKey_K_Release = new Command(() => Common.Ctrl_Key[18] = false);

    public readonly ICommand Ctrl_ATSKey_L_Push = new Command(() => Common.Ctrl_Key[19] = true);
    public readonly ICommand Ctrl_ATSKey_L_Release = new Command(() => Common.Ctrl_Key[19] = false);
  }
}
