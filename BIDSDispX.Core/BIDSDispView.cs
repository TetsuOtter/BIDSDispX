using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using TR.BIDSsv;

namespace TR.BIDSDispX.Core
{
  public abstract class BIDSDispView : ContentView, IDisposable
  {
    public BIDSDispView()
    {
      Common.BSMDChanged += Common_BSMDChanged;
      Common.OpenDChanged += Common_OpenDChanged;
      Common.PanelDChanged += Common_PanelDChanged;
      Common.SoundDChanged += Common_SoundDChanged;
    }

    protected abstract void Common_SoundDChanged(object sender, BIDSSMemLib.SMemLib.ArrayDChangedEArgs e);
    protected abstract void Common_PanelDChanged(object sender, BIDSSMemLib.SMemLib.ArrayDChangedEArgs e);
    protected abstract void Common_OpenDChanged(object sender, BIDSSMemLib.SMemLib.OpenDChangedEArgs e);
    protected abstract void Common_BSMDChanged(object sender, BIDSSMemLib.SMemLib.BSMDChangedEArgs e);


    #region IDisposable Support
    private bool disposedValue = false; // 重複する呼び出しを検出するには

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
        }

        // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
        // TODO: 大きなフィールドを null に設定します。
        Common.BSMDChanged -= Common_BSMDChanged;
        Common.OpenDChanged -= Common_OpenDChanged;
        Common.PanelDChanged -= Common_PanelDChanged;
        Common.SoundDChanged -= Common_SoundDChanged;
        disposedValue = true;
      }
    }

    // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
    // ~BIDSDispView()
    // {
    //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
    //   Dispose(false);
    // }

    // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
    void IDisposable.Dispose()
    {
      // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
      Dispose(true);
      // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
      // GC.SuppressFinalize(this);
    }
    #endregion


  }
}
