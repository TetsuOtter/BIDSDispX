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

	public class MeterViewSettings : INotifyPropertyChanged, IAngleAndValMinMax
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void PropCngEvInv(string s) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));

		#region Fields
		private double __Radius = 100;
		private Thickness __Margin = new Thickness(10);
		private double __MinValAngle = 2;
		private double __MaxValAngle = 2;
		private double __MinValue = 2;
		private double __MaxValue = 2;

		private Thickness __Needle_Shadow_Padding = new Thickness(2);
		private double __Shadow_Opacity = 0.4;
		private Thickness __Needle_Padding = new Thickness(0);
		private double __Needle_Height = 10;
		private Color __Needle1Color = Color.Black;
		private Color __Needle2Color = Color.Red;
		private double __Needle1Step = 0.1;
		private double __Needle2Step = 0.1;
		private UsableKeys.Keys __Needle1Key = UsableKeys.Keys.bc;
		private UsableKeys.Keys __Needle2Key = UsableKeys.Keys.mr;
		private bool __IsNeedle1Enabled = true;
		private bool __IsNeedle2Enabled = true;

		private bool __IsMarkLEnabled = true;
		private bool __IsMarkMEnabled = true;
		private bool __IsMarkSEnabled = true;
		private double __MarkLHeight = 100;
		private double __MarkMHeight = 100;
		private double __MarkSHeight = 100;
		private double __MarkLWidth = 100;
		private double __MarkMWidth = 100;
		private double __MarkSWidth = 100;
		private int __MarkLStep = 100;
		private int __MarkMStep = 100;
		private int __MarkSStep = 100;
		private Color __MarkLColor = Color.Black;
		private Color __MarkMColor = Color.Black;
		private Color __MarkSColor = Color.Black;
		private Thickness __MarkLPadding = new Thickness(0);
		private Thickness __MarkMPadding = new Thickness(0);
		private Thickness __MarkSPadding = new Thickness(0);

		private bool __IsLabelEnabled = true;
		private double __Label_Padding = 100;
		private double __Label_FontSize = 100;
		private int __Label_Step = 100;
		#endregion

		#region Properties
		#region Basic Props
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

		public Thickness Margin
		{
			get => __Margin;
			set
			{
				if (__Margin == value)
					return;

				__Margin = value;
				PropCngEvInv(nameof(Margin));
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
				PropCngEvInv(nameof(MinValAngle));
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
				PropCngEvInv(nameof(MaxValAngle));
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
				PropCngEvInv(nameof(MinValue));
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
				PropCngEvInv(nameof(MaxValue));
			}
		}
		#endregion
		#region Needle Props
		public Thickness Needle_Shadow_Padding
		{
			get => __Needle_Shadow_Padding;
			set
			{
				if (__Needle_Shadow_Padding == value)
					return;

				__Needle_Shadow_Padding = value;
				PropCngEvInv(nameof(Needle_Shadow_Padding));
			}
		}

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

		public Color Needle1Color
		{
			get => __Needle1Color;
			set
			{
				if (__Needle1Color == value)
					return;

				__Needle1Color = value;
				PropCngEvInv(nameof(Needle1Color));
			}
		}

		public Color Needle2Color
		{
			get => __Needle2Color;
			set
			{
				if (__Needle2Color == value)
					return;

				__Needle2Color = value;
				PropCngEvInv(nameof(Needle2Color));
			}
		}

		public double Needle1Step
		{
			get => __Needle1Step;
			set
			{
				if (__Needle1Step == value)
					return;

				__Needle1Step = value;
				PropCngEvInv(nameof(Needle1Step));
			}
		}

		public double Needle2Step
		{
			get => __Needle2Step;
			set
			{
				if (__Needle2Step == value)
					return;

				__Needle2Step = value;
				PropCngEvInv(nameof(Needle2Step));
			}
		}

		public UsableKeys.Keys Needle1Key
		{
			get => __Needle1Key;
			set
			{
				if (__Needle1Key == value)
					return;

				__Needle1Key = value;
				PropCngEvInv(nameof(Needle1Key));
			}
		}

		public UsableKeys.Keys Needle2Key
		{
			get => __Needle2Key;
			set
			{
				if (__Needle2Key == value)
					return;

				__Needle2Key = value;
				PropCngEvInv(nameof(Needle2Key));
			}
		}

		public bool IsNeedle1Enabled
		{
			get => __IsNeedle1Enabled;
			set
			{
				if (__IsNeedle1Enabled == value)
					return;

				__IsNeedle1Enabled = value;
				PropCngEvInv(nameof(IsNeedle1Enabled));
			}
		}

		public bool IsNeedle2Enabled
		{
			get => __IsNeedle2Enabled;
			set
			{
				if (__IsNeedle2Enabled == value)
					return;

				__IsNeedle2Enabled = value;
				PropCngEvInv(nameof(IsNeedle2Enabled));
			}
		}
		#endregion
		#region Mark Props
		public bool IsMarkLEnabled
		{
			get => __IsMarkLEnabled;
			set
			{
				if (__IsMarkLEnabled == value)
					return;

				__IsMarkLEnabled = value;
				PropCngEvInv(nameof(IsMarkLEnabled));
			}
		}

		public bool IsMarkMEnabled
		{
			get => __IsMarkMEnabled;
			set
			{
				if (__IsMarkMEnabled == value)
					return;

				__IsMarkMEnabled = value;
				PropCngEvInv(nameof(IsMarkMEnabled));
			}
		}

		public bool IsMarkSEnabled
		{
			get => __IsMarkSEnabled;
			set
			{
				if (__IsMarkSEnabled == value)
					return;

				__IsMarkSEnabled = value;
				PropCngEvInv(nameof(IsMarkSEnabled));
			}
		}

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

		public Thickness MarkLPadding
		{
			get => __MarkLPadding;
			set
			{
				if (__MarkLPadding == value)
					return;

				__MarkLPadding = value;
				PropCngEvInv(nameof(MarkLPadding));
			}
		}

		public Thickness MarkMPadding
		{
			get => __MarkMPadding;
			set
			{
				if (__MarkMPadding == value)
					return;

				__MarkMPadding = value;
				PropCngEvInv(nameof(MarkMPadding));
			}
		}

		public Thickness MarkSPadding
		{
			get => __MarkSPadding;
			set
			{
				if (__MarkSPadding == value)
					return;

				__MarkSPadding = value;
				PropCngEvInv(nameof(MarkSPadding));
			}
		}
		#endregion

		#region Label Props
		public bool IsLabelEnabled
		{
			get => __IsLabelEnabled;
			set
			{
				if (__IsLabelEnabled == value)
					return;

				__IsLabelEnabled = value;
				PropCngEvInv(nameof(IsLabelEnabled));
			}
		}

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

		public int Label_Step
		{
			get => __Label_Step;
			set
			{
				if (__Label_Step == value)
					return;

				__Label_Step = value;
				PropCngEvInv(nameof(Label_Step));
			}
		}
		#endregion
		#endregion
	}
}