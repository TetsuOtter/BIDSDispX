using GLib;
using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace TR.BIDSDispX.GTK
{
  class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      ExceptionManager.UnhandledException += OnUnhandledException;

      GtkOpenGL.Init();
      GtkThemes.Init();
      Gtk.Application.Init();
      Forms.Init();
      var app = new App();
      var window = new FormsWindow();
      window.LoadApplication(app);
      window.SetApplicationTitle("BIDSDIspX.Gtk");
      window.BorderWidth = 0;

      window.Show();
      window.Fullscreen();
      Gtk.Application.Run();
    }

    private static void OnUnhandledException(UnhandledExceptionArgs args)
    {
      System.Diagnostics.Debug.WriteLine($"Unhandled GTK# exception: {args.ExceptionObject}");
    }
  }
}
