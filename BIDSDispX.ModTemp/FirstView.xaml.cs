using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.ModTemp
{
  //表示の制御を実装する

  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class FirstView : ContentView
  {
    public FirstView()
    {
      InitializeComponent();
    }
  }
}