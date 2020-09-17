using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.SampleView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingView : ContentView
	{
		public event EventHandler ChangeOptsAccepted;
		public SettingView()
		{
			InitializeComponent();
		}

		private async void Save_Button_Clicked(object sender, EventArgs e)
		{
			//ref : https://docs.microsoft.com/ja-jp/xamarin/xamarin-forms/user-interface/pop-ups
			if (await Core.DispCom.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No"))
			{
				ChangeOptsAccepted?.Invoke(this, null);

				IsVisible = false;
			}
		}

		private async void Back_withoutSave_Clicked(object sender, EventArgs e)
		{
			if (await Core.DispCom.DisplayAlert("Question?", "ABC\nWould you like to play a game", "Yes", "No"))
			{
				//ChangeOptsAccepted?.Invoke(this, null);

				IsVisible = false;
			}

		}
	}

	public class MeterViewSettings : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void PropCngEvInv(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		#region Properties
		private double __Radius = 100;
		public double Radius
		{
			get => __Radius;
			set
			{
				if (__Radius == value)
					return;

				__Radius = value;
				PropCngEvInv(nameof(Radius));
			}
		}

		private double __MarginX = 100;
		public double MarginX
		{
			get => __MarginX;
			set
			{
				if (__MarginX == value)
					return;

				__MarginX = value;
				PropCngEvInv(nameof(MarginX));
			}
		}

		private double __MarginY = 100;
		public double MarginY
		{
			get => __MarginY;
			set
			{
				if (__MarginY == value)
					return;

				__MarginY = value;
				PropCngEvInv(nameof(MarginY));
			}
		}

		private double __Shadow_DstX = 2;
		public double Shadow_DstX
		{
			get => __Shadow_DstX;
			set
			{
				if (__Shadow_DstX == value)
					return;

				__Shadow_DstX = value;
				PropCngEvInv(nameof(Shadow_DstX));
			}
		}

		private double __Shadow_DstY = 2;
		public double Shadow_DstY
		{
			get => __Shadow_DstY;
			set
			{
				if (__Shadow_DstY == value)
					return;

				__Shadow_DstY = value;
				PropCngEvInv(nameof(Shadow_DstY));
			}
		}

		private double __Shadow_Opacity = 0.4;
		public double Shadow_Opacity
		{
			get => __Shadow_Opacity;
			set
			{
				if (__Shadow_Opacity == value)
					return;

				__Shadow_Opacity = value;
				PropCngEvInv(nameof(Shadow_Opacity));
			}
		}

		private Thickness __Needle_Padding = new Thickness(0);
		public Thickness Needle_Padding
		{
			get => __Needle_Padding;
			set
			{
				if (__Needle_Padding == value)
					return;

				__Needle_Padding = value;
				PropCngEvInv(nameof(Needle_Padding));
			}
		}

		private double __Needle_Height = 10;
		public double Needle_Height
		{
			get => __Needle_Height;
			set
			{
				if (__Needle_Height == value)
					return;

				__Needle_Height = value;
				PropCngEvInv(nameof(Needle_Height));
			}
		}

		private double __MarkLHeight = 100;
		public double MarkLHeight
		{
			get => __MarkLHeight;
			set
			{
				if (__MarkLHeight == value)
					return;

				__MarkLHeight = value;
				PropCngEvInv(nameof(MarkLHeight));
			}
		}

		private double __MarkMHeight = 100;
		public double MarkMHeight
		{
			get => __MarkMHeight;
			set
			{
				if (__MarkMHeight == value)
					return;

				__MarkMHeight = value;
				PropCngEvInv(nameof(MarkMHeight));
			}
		}

		private double __MarkSHeight = 100;
		public double MarkSHeight
		{
			get => __MarkSHeight;
			set
			{
				if (__MarkSHeight == value)
					return;

				__MarkSHeight = value;
				PropCngEvInv(nameof(MarkSHeight));
			}
		}

		private double __MarkLWidth = 100;
		public double MarkLWidth
		{
			get => __MarkLWidth;
			set
			{
				if (__MarkLWidth == value)
					return;

				__MarkLWidth = value;
				PropCngEvInv(nameof(MarkLWidth));
			}
		}

		private double __MarkMWidth = 100;
		public double MarkMWidth
		{
			get => __MarkMWidth;
			set
			{
				if (__MarkMWidth == value)
					return;

				__MarkMWidth = value;
				PropCngEvInv(nameof(MarkMWidth));
			}
		}

		private double __MarkSWidth = 100;
		public double MarkSWidth
		{
			get => __MarkSWidth;
			set
			{
				if (__MarkSWidth == value)
					return;

				__MarkSWidth = value;
				PropCngEvInv(nameof(MarkSWidth));
			}
		}

		private int __MarkLStep = 100;
		public int MarkLStep
		{
			get => __MarkLStep;
			set
			{
				if (__MarkLStep == value)
					return;

				__MarkLStep = value;
				PropCngEvInv(nameof(MarkLStep));
			}
		}

		private int __MarkMStep = 100;
		public int MarkMStep
		{
			get => __MarkMStep;
			set
			{
				if (__MarkMStep == value)
					return;

				__MarkMStep = value;
				PropCngEvInv(nameof(MarkMStep));
			}
		}

		private int __MarkSStep = 100;
		public int MarkSStep
		{
			get => __MarkSStep;
			set
			{
				if (__MarkSStep == value)
					return;

				__MarkSStep = value;
				PropCngEvInv(nameof(MarkSStep));
			}
		}

		private Color __MarkLColor = Color.Black;
		public Color MarkLColor
		{
			get => __MarkLColor;
			set
			{
				if (__MarkLColor == value)
					return;

				__MarkLColor = value;
				PropCngEvInv(nameof(MarkLColor));
			}
		}

		private Color __MarkMColor = Color.Black;
		public Color MarkMColor
		{
			get => __MarkMColor;
			set
			{
				if (__MarkMColor == value)
					return;

				__MarkMColor = value;
				PropCngEvInv(nameof(MarkMColor));
			}
		}

		private Color __MarkSColor = Color.Black;
		public Color MarkSColor
		{
			get => __MarkSColor;
			set
			{
				if (__MarkSColor == value)
					return;

				__MarkSColor = value;
				PropCngEvInv(nameof(MarkSColor));
			}
		}

		private double __Label_Padding = 100;
		public double Label_Padding
		{
			get => __Label_Padding;
			set
			{
				if (__Label_Padding == value)
					return;

				__Label_Padding = value;
				PropCngEvInv(nameof(Label_Padding));
			}
		}

		private double __Label_FontSize = 100;
		public double Label_FontSize
		{
			get => __Label_FontSize;
			set
			{
				if (__Label_FontSize == value)
					return;

				__Label_FontSize = value;
				PropCngEvInv(nameof(Label_FontSize));
			}
		}
		#endregion
	}
}