/*
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using Xamarin.Forms;

namespace TR.BIDSDispX.Core.PnlElms
{
  public class DigitalNumberCV : SKCanvasView
  {
    Random rdm;
    public DigitalNumberCV()
    {
      rdm = new Random();
      HeightRequest = 1000;
      WidthRequest = 1000;
      //Touch += DigitalNumberCV_Touch;
      PaintSurface += DigitalNumberCV_PaintSurface;
      
    }
    private SKPaint rectPaint = new SKPaint
    {
      StrokeWidth = 4,
      IsAntialias = true,
      Color = SKColors.Blue
    };
    private SKPaint circlePaint = new SKPaint
    {
      StrokeWidth = 4,
      IsAntialias = true,
      Color = SKColors.Green
    };
    private SKPaint linePaint = new SKPaint
    {
      StrokeWidth = 4,
      IsAntialias = true,
      Color = SKColors.Red
    };
    private void DigitalNumberCV_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
      var canvas = e.Surface.Canvas;

      try
      {
        Stream fileStream = File.OpenRead("num.bmp");
        using (var stream = new SKManagedStream(fileStream))
        using (var bitmap = SKBitmap.Decode(stream))
        using (var paint = new SKPaint())
        {
          canvas.DrawBitmap(bitmap, SKRect.Create(new SKPoint(0, 0), new SKSize(300f, 300f)), paint);
        }
      }
      catch (Exception ex)
      {
        using(var pat=new SKPaint())
        {
          pat.IsAntialias = false;
          pat.Color = SKColors.Black;
          pat.TextSize = 16f;
          canvas.DrawText(ex.ToString(), new SKPoint(100f, 100f), pat);
        }
      }
      // 四角形を描画
      canvas.DrawRect(new SKRect(10, 10, 200, 200), rectPaint);
      // 円を描画
      canvas.DrawCircle(320, 110, 100, circlePaint);
      // 線を描画
      canvas.DrawLine(430, 10, 630, 210, linePaint);
      canvas.DrawLine(630, 10, 430, 210, linePaint);




      using (var paint = new SKPaint())
      {
        paint.IsAntialias = false;
        paint.Color = new SKColor(0x2c, 0x3e, 0x50);
        paint.StrokeCap = SKStrokeCap.Round;
        

        // create the Xamagon path
        using (var path = new SKPath())
        {
          path.MoveTo(71.4311121f, 56f);
          path.CubicTo(68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
          path.LineTo(43.0238921f, 97.5342563f);
          path.CubicTo(41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
          path.LineTo(64.5928855f, 143.034271f);
          path.CubicTo(65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
          path.LineTo(114.568946f, 147f);
          path.CubicTo(117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
          path.LineTo(142.976161f, 105.465744f);
          path.CubicTo(144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
          path.LineTo(121.407172f, 59.965729f);
          path.CubicTo(120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
          path.LineTo(71.4311121f, 56f);
          path.Close();

          // draw the Xamagon path
          canvas.DrawPath(path, paint);


          paint.TextSize = 64.0f;
          paint.TextScaleX = 3;
          
          paint.IsAntialias = false;
          paint.Color = new SKColor(0x42, 0x81, 0xA4);
          paint.IsStroke = false;

          // draw the text
          canvas.DrawText("Skia", 0.0f, 64.0f, paint);
        }
      }

    }

    private void DigitalNumberCV_Touch(object sender, SKTouchEventArgs e)
    {
      InvalidateSurface();
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
      //e.Surface.Canvas.Clear(new SKColor((uint)rdm.Next()));
      
      base.OnPaintSurface(e);
      using (var surface = SKSurface.Create(new SKImageInfo() { Width = 640, Height = 480, AlphaType = SKAlphaType.Premul, ColorType = SKColorType.Rgba8888 }))
      {
        SKCanvas myCanvas = surface.Canvas;

        using (var paint = new SKPaint())
        {
          paint.IsAntialias = false;
          paint.Color = new SKColor(0x2c, 0x3e, 0x50);
          paint.StrokeCap = SKStrokeCap.Round;

          // create the Xamagon path
          using (var path = new SKPath())
          {
            path.MoveTo(71.4311121f, 56f);
            path.CubicTo(68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
            path.LineTo(43.0238921f, 97.5342563f);
            path.CubicTo(41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
            path.LineTo(64.5928855f, 143.034271f);
            path.CubicTo(65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
            path.LineTo(114.568946f, 147f);
            path.CubicTo(117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
            path.LineTo(142.976161f, 105.465744f);
            path.CubicTo(144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
            path.LineTo(121.407172f, 59.965729f);
            path.CubicTo(120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
            path.LineTo(71.4311121f, 56f);
            path.Close();

            // draw the Xamagon path
            myCanvas.DrawPath(path, paint);

            
          }

          e.Surface.Draw(myCanvas, 0, 0, paint);
        }

        
      }
    }

    #region Bindable Prop. (IndexX)
    static public readonly BindableProperty IndexXProp = BindableProperty.Create(nameof(IndexX), typeof(int), typeof(DigitalNumber), 0, defaultBindingMode: BindingMode.OneWay);

    public int IndexX
    {
      get => (int)GetValue(IndexXProp);
      set
      {
        SetValue(IndexXProp, value);
        InvalidateSurface();
      }
    }
    #endregion
    #region Bindable Prop. (IndexY)
    static public readonly BindableProperty IndexYProp = BindableProperty.Create(nameof(IndexY), typeof(int), typeof(DigitalNumber), 0, defaultBindingMode: BindingMode.OneWay);

    public int IndexY
    {
      get => (int)GetValue(IndexYProp);
      set
      {
        SetValue(IndexYProp, value);
        InvalidateSurface();
      }
    }
    #endregion

  }
}
*/