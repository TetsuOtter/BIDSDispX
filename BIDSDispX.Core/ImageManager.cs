using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TR.BIDSDispX.Core
{
  static public class ImageManager
  {
    static private List<byte[]> ImageStock = new List<byte[]>();

    static public void init() => ImageStock.Clear();
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ba">Image byte array</param>
    /// <returns>Image ID(index)</returns>
    static public int ImageAdd(byte[] ba)
    {
      if (ba == null) throw new ArgumentNullException();
      if (ba.Length <= 0) throw new ArgumentException();

      int? eind = null;
      Parallel.For(0, ImageStock.Count, (i) =>
      {
        if (eind!=null) return;
        if (ImageStock[i].Length != ba.Length) return;

        bool tf = true;
        Parallel.For(0, ba.Length, (ind) =>
        {
          tf &= ImageStock[i][ind] == ba[ind];
        });
        if (tf) eind = i;
      });

      if (eind != null)
      {
        byte[] nba = new byte[ba.Length];
        Array.Copy(ba, nba, ba.Length);

        ImageStock.Add(nba);
        return ImageStock.Count - 1;
      }
      else return eind ?? 0;
    }

    static public int ImageAdd(ImageSource imgS)
    {
      if (imgS == null) throw new ArgumentNullException();
      
      return ImageAdd(ba:null);
    }

    static public int ImageAdd(Image img)
    {
      var ms = new MemoryStream();
      
      return ImageAdd(ba: null);
    }

    static public Image ImageGet(int index)
    {
      return null;
    }
  }
}
