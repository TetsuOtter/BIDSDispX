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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public double Dp2Px(this in int DPvalue) => ((double)DPvalue).Dp2Px();
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public double Dp2Px(this in double DPvalue) => DPvalue / DeviceDisplay.MainDisplayInfo.Density;
  }
}
