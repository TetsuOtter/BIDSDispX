using System.Runtime.CompilerServices;

namespace TR.BIDSDispX.SampleView
{
	public interface IReadOnlyAngleAndValMinMax
	{
		double MinValAngle { get; }//←・が0度 ↑が90度 ・→が180度
		double MinValue { get; }
		double MaxValAngle { get; }//←・が0度 ↑が90度 ・→が180度
		double MaxValue { get; }
	}
	public interface IWriteOnlyAngleAndValMinMax
	{
		double MinValAngle { set; }//←・が0度 ↑が90度 ・→が180度
		double MinValue { set; }
		double MaxValAngle { set; }//←・が0度 ↑が90度 ・→が180度
		double MaxValue { set; }
	}
	public interface IAngleAndValMinMax : IReadOnlyAngleAndValMinMax, IWriteOnlyAngleAndValMinMax
	{
		new double MinValAngle { get; set; }//←・が0度 ↑が90度 ・→が180度
		new double MinValue { get; set; }
		new double MaxValAngle { get; set; }//←・が0度 ↑が90度 ・→が180度
		new double MaxValue { get; set; }
	}

	static public class AngleAndValMinMax_Funcs
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static public double GetAngle(this in double val, in IReadOnlyAngleAndValMinMax minmax)
			=> val <= minmax.MinValue ? minmax.MinValAngle : //最小表示値よりも小さい
					minmax.MaxValue <= val ? minmax.MaxValAngle : //最大表示値よりも大きい

					((val - minmax.MinValue) *//最小表示値からどれくらいの差(=動き)があるか
					((minmax.MaxValAngle - minmax.MinValAngle) / (minmax.MaxValue - minmax.MinValue)))//1km/hあたりの変化角度
					+ minmax.MinValAngle;
	}
}
