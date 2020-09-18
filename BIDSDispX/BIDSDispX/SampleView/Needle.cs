
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.SampleView
{
	public class Needle : ContentView, IAngleAndValMinMax, INotifyPropertyChanged
	{
		private Grid MainNeedle = new Grid
		{
			HeightRequest = 10,
			WidthRequest = 100,
			BackgroundColor = Color.Transparent,
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Center,
		};
		
		public Needle()
		{
			this.Content = MainNeedle;

			AnchorX = 0.5;
			AnchorY = 0.5;

			PropUpdated();
		}

		#region Fields for Properties
		private Color __NeedleColor = Color.Black;
		private double __MinValAngle = -30;//←・が0度 ↑が90度 ・→が180度
		private double __MinValue = 0;
		private double __MaxValAngle = 210;//←・が0度 ↑が90度 ・→が180度
		private double __MaxValue = 160;
		double __ValueToShow = 0;
		private double __Radius = 100;
		private double __NeedleHeight = 10;
		private int __Triangle_StepCount = 20;
		private double __Triangle_Width = 10;
		private double __Circle_Padding = 0;
		#endregion

		#region Properties
		public Color NeedleColor
		{
			get => __NeedleColor;
			set
			{
				if (__NeedleColor == value)
					return;

				__NeedleColor = value;
				ToApplyChangesForChildrenInmainThread((i) => ((BoxView)MainNeedle.Children[i]).Color = value);
			}
		}

		public double MinValAngle
		{
			get => __MinValAngle;
			set
			{
				if (__MinValAngle == value)
					return;

				__MinValAngle = value;
				AngleUpdate();
			}
		}

		public double MinValue
		{
			get => __MinValue;
			set
			{
				if (__MinValue == value)
					return;

				__MinValue = value;
				AngleUpdate();
			}
		}
		
		public double MaxValAngle
		{
			get => __MaxValAngle;
			set
			{
				if (__MaxValAngle == value)
					return;

				__MaxValAngle = value;
				AngleUpdate();
			}
		}

		public double MaxValue
		{
			get => __MaxValue;
			set
			{
				if (__MaxValue == value)
					return;

				__MaxValue = value;
				AngleUpdate();
			}
		}

		public double ValueToShow
		{
			get => __ValueToShow;
			set
			{
				if (__ValueToShow == value)
					return;

				__ValueToShow = value;
				AngleUpdate();
			}
		}
		
		public double Radius
		{
			get => __Radius;
			set
			{
				if (__Radius == value)
					return;

				__Radius = value;
				MainThread.BeginInvokeOnMainThread(() => { HeightRequest = WidthRequest = value * 2; });
			}
		}

		public double NeedleHeight
		{
			get => __NeedleHeight;
			set
			{
				if (__NeedleHeight == value)
					return;

				__NeedleHeight = value;
				MainThread.BeginInvokeOnMainThread(() => MainNeedle.HeightRequest = value);
				PropUpdated();//描画のやり直し
			}
		}


		public int Triangle_StepCount
		{
			get => __Triangle_StepCount;
			set
			{
				if (__Triangle_StepCount == value)
					return;

				__Triangle_StepCount = value;
				PropUpdated();//描画のやり直し
			}
		}

		public double Triangle_Width
		{
			get => __Triangle_Width;
			set
			{
				if (__Triangle_Width == value)
					return;

				__Triangle_Width = value;				
				PropUpdated();//描画のやり直し
			}
		}

		public double Circle_Padding
		{
			get => __Circle_Padding;
			set
			{
				if (__Circle_Padding == value)
					return;

				__Circle_Padding = value;
				MainThread.BeginInvokeOnMainThread(() => MainNeedle.Margin = new Thickness(Circle_Padding));
			}
		}

		#region Direct Attatch Properties
		public double NeedleWidth { get => MainNeedle.Width; set => MainThread.BeginInvokeOnMainThread(() => { MainNeedle.WidthRequest = value; }); }
		public double Angle { get => Rotation; set => MainThread.BeginInvokeOnMainThread(() => Rotation = value); }//針だけじゃなく, ベースごと回転させる.
		#endregion
		#endregion


		/// <summary>PropUpdatedの処理が重複して呼ばれていないかをチェック</summary>
		private bool PropUpdateAlreadyRequested = false;
		public void PropUpdated()
		{
			if (PropUpdateAlreadyRequested)
				return;//二重で処理する必要はない

			PropUpdateAlreadyRequested = true;

			Dispatcher.BeginInvokeOnMainThread(() =>
			{
				MainNeedle.Children.Clear();

				double CurrentTriMarginY = NeedleHeight / 2;
				double dH = CurrentTriMarginY / Triangle_StepCount;

				CurrentTriMarginY -= dH;//初回の処理用

				for (double i = 0; i < Triangle_Width; i += (Triangle_Width / Triangle_StepCount), CurrentTriMarginY -= dH)
					MainNeedle.Children.Add(new BoxView
					{
						Margin = new Thickness(i, CurrentTriMarginY,0,CurrentTriMarginY),
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand,
						
						Color = NeedleColor
					});

				PropUpdateAlreadyRequested = false;
			});
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AngleUpdate() => Angle = ValueToShow.GetAngle(this);

		private void ToApplyChangesForChildrenInmainThread(Action<int> act) =>
			MainThread.BeginInvokeOnMainThread(() =>
			{
				if (MainNeedle.Children?.Count > 0)
					for (int i = 0; i < MainNeedle.Children.Count; i++)
						act.Invoke(i);
			});
	}
}