using System;
using System.Runtime.CompilerServices;

using Xamarin.Essentials;

namespace TR.BIDSDispX.Core.UFuncs
{
  static public class UFunc
  {
    private const int TRUE_VALUE = 1;
    private const int FALSE_VALUE = 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public int GetDigitVal(this double value, int pos) => (int)(Math.Sqrt(value) / Math.Pow(10, pos)) % 10;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public int GetDigitVal(this float value, int pos) => GetDigitVal(value, pos);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public int GetDigitVal(this int value, int pos) => GetDigitVal(value, pos);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public int ToInt32(this bool value) => value ? TRUE_VALUE : FALSE_VALUE;


    /// <summary>ピクセル値をDP(160dp=1inch, 64dp=1cm)に変換する</summary>
    /// <param name="DPvalue">ピクセル値</param>
    /// <returns>入力されたpx値と同等のdp値</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public double Px2Dp(this in int DPvalue) => ((double)DPvalue).Px2Dp();

    /// <summary>ピクセル値をDP(160dp=1inch, 64dp=1cm)に変換する</summary>
    /// <param name="DPvalue">ピクセル値</param>
    /// <returns>入力されたpx値と同等のdp値</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public double Px2Dp(this in double DPvalue) => DPvalue / DeviceDisplay.MainDisplayInfo.Density;
  }
}
