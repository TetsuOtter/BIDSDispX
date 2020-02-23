using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TR.BIDSDispX.Core
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SizeSelector : ContentView
  {
    List<Size> Sizes;
    public EventHandler SizeDecided;
    private Size SelectedSize = new Size(0, 0);
    public SizeSelector() => InitializeComponent();

    public void SizesSet(List<Size> s, int defInd = 0)
    {
      if (!(s?.Count > 0)) return;
      Sizes = s;
      SizeLV.ItemsSource = Sizes;
      SelectedSize = Sizes[Sizes.Count > defInd ? defInd : 0];
      SizeLV.SelectedItem = SelectedSize;
    }

    private void Button_Clicked(object sender, EventArgs e) => DispCom.ViewChange();

    private void Enter_Clicked(object sender, EventArgs e) => SizeDecided?.Invoke(SelectedSize, null);

    private void SizeLV_ItemSelected(object sender, SelectedItemChangedEventArgs e) => SelectedSize = (Size)e.SelectedItem;
  }
}