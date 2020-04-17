using Xamarin.Forms;

namespace TR.BIDSDispX.Core.PnlElms
{
  //Refer : https://qiita.com/sukobuto/items/c8a12070324e713828ea

  public class DigitalNumber : ContentView
  {
    Grid MomG = new Grid();
    Image dnImg = new Image();
    DigitalNumberCV dncv = new DigitalNumberCV();
    public DigitalNumber()
    {
      /*MomG.WidthRequest = IntervalX;
      MomG.MinimumWidthRequest = IntervalX;
      MomG.HeightRequest = IntervalY;
      MomG.MinimumHeightRequest = IntervalY;*/
      //dnImg.Source = "C:\\num.bmp";
      
      //MomG.Children.Add(dnImg);
      
      Content = dncv;
      
    }

    #region Properties

    #region Bindable Prop. (IndexX)
    static public readonly BindableProperty IndexXProp = BindableProperty.Create(nameof(IndexX), typeof(int), typeof(DigitalNumber), 0, defaultBindingMode: BindingMode.OneWay);

    public int IndexX { get => (int)GetValue(IndexXProp); set => SetValue(IndexXProp, value); }
    #endregion
    #region Bindable Prop. (IndexY)
    static public readonly BindableProperty IndexYProp = BindableProperty.Create(nameof(IndexY), typeof(int), typeof(DigitalNumber), 0, defaultBindingMode: BindingMode.OneWay);

    public int IndexY { get => (int)GetValue(IndexYProp); set => SetValue(IndexYProp, value); }
    #endregion

    public ImageSource SourcePath { get; set; }
    public int IntervalX { get; set; }
    public int IntervalY { get; set; }

    #endregion
  }
}
