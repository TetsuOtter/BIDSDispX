using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TR.BIDSDispX
{
  internal class OnlineSync
  {
    private readonly string ModsListFName = "ModsList.csv";
    private readonly string ModsListURLsFName = "ModsListURLs.txt";
    public readonly string ModsFolderName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BIDSDispX", "mods");

    internal void UpdateLists(string[] urls)
    {
      if (urls == null || urls.Length <= 0) return;

      List<ModsListStructure> mlist = new List<ModsListStructure>();

      Parallel.For(0, urls.Length, (i) =>
      {
        string s = (new WebClient())?.DownloadString(urls[i]);

        string[] sa = s?.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        if (sa?.Length > 0)
          for (int l = 0; l < sa.Length; l++)
            mlist.Add(new ModsListStructure(sa[l]));
      });

      if (!(mlist?.Count > 0)) return;
      
      List<ModsListStructure> LocalMList = ByteArr2ModsList(FileRead(ModsListFName));

      if (LocalMList?.Count > 0)
      {
        Parallel.For(0, mlist.Count, (i) =>
        {
          bool WasFound = false;
          if (LocalMList?.Count > 0)
          {
            for (int l = 0; l < LocalMList.Count; l++)
            {
              if (LocalMList[l].ModName != mlist[i].ModName) continue;

              if (LocalMList[l].RemoteUpdateTime != mlist[i].RemoteUpdateTime)
              {
                LocalMList[l].RemotePath = mlist[i].RemotePath;
                LocalMList[l].RemoteUpdateTime = mlist[i].RemoteUpdateTime;
              }

              WasFound = true;
              break;
            }
          }

          if (!WasFound) LocalMList.Add(mlist[i]);
        });
      }

      if (LocalMList?.Count > 0) FileWrite(ModsListFName, ModsList2ByteArr(LocalMList));
      
    }

    internal string[] GetLocalList() => FileReadAllLine(ModsListFName);
    internal string[] GetURLsList() => FileReadAllLine(ModsListURLsFName);

    internal void UpdateMod(string ModName) => UpdateMod(new string[] { ModName });
    internal void UpdateMod(string[] ModNames)
    {
      //Get LocalList
      //Check(Get) Remote Path
      //Download All(Allow Overwrite)
      //DL Success => Copy Remote UpdateTime to Local UpdateTime
      //DL Failed  => Set the Remote Data to string.Empty
      //LocalList Update
      
    }

    internal IBIDSDispX LoadLib(string ModName)
    {
      List<ModsListStructure> mlsl = ByteArr2ModsList(FileRead(ModsListFName));

      int ind = FindIndex(mlsl, ModName);

      if (ind < 0) return null;

      string typeName = string.Empty;
      string interfaceName = typeof(IBIDSDispX).FullName;
      Assembly assembly = Assembly.LoadFrom(mlsl[ind].LocalPath);

      if (assembly == null) return null;

      foreach (var type in assembly.GetTypes())
      {
        if (type.IsClass && type.IsPublic && !type.IsAbstract && type.GetInterface(interfaceName) != null)
        {
          typeName = type.FullName;
          break;
        }
      }

      return (IBIDSDispX)assembly.CreateInstance(typeName);
    }

    internal void DeleteMod(string ModName) => DeleteMod(new string[] { ModName });
    internal void DeleteMod(string[] sa)
    {
      if (sa == null || sa.Length <= 0) return;
      List<ModsListStructure> mlsl = ByteArr2ModsList(FileRead(ModsListFName));//Open List

      for(int a = 0; a < sa.Length; a++)
      {
        int ind = FindIndex(mlsl, sa[a]);

        if (ind >= 0)
        {
          if (!string.IsNullOrWhiteSpace(mlsl[ind].LocalPath))
            File.Delete(mlsl[ind].LocalPath);//Delete Library
          
          mlsl.RemoveAt(ind);//Remove from ModsList
        }
      }

      FileWrite(ModsListFName, ModsList2ByteArr(mlsl));
    }

    /// <summary>
    /// Install from Local
    /// </summary>
    /// <returns></returns>
    internal async void AddMod()
    {
      var file = await CrossFilePicker.Current.PickFile(new string[] { ".dll" });

      if (file == null) return;
      try
      {
        string fpath = string.Empty;
        fpath = file.FilePath;
        string TargetPath = Path.Combine(ModsFolderName, file.FileName);
        File.Copy(fpath, TargetPath, true);

        AddMod(new ModsListStructure() { LocalPath = TargetPath, LocalUpdateTime = 0, ModName = file.FileName });
      }
      catch (Exception) { throw; }
    }

    private void AddMod(ModsListStructure mls)
    {
      List<ModsListStructure> mlsl = StringArr2ModsList(FileReadAllLine(ModsListFName));//Open ListFile & Read List
      if (mlsl == null) mlsl = new List<ModsListStructure>();
      mlsl.Add(mls);//Add a Content

      FileWrite(ModsListFName, ModsList2ByteArr(mlsl));
    }

    private FileStream OpenFile(string fname)
    {
      try
      {
        return File.Open(Path.Combine(ModsFolderName, fname), FileMode.OpenOrCreate);
      }
      catch (DirectoryNotFoundException)
      {
        Directory.CreateDirectory(ModsFolderName);
        return OpenFile(fname);
      }
      catch (Exception) { throw; }
    }


    internal void GetModsListURLs(out string[] sa) => sa = GetModsListURLs();
    internal string[] GetModsListURLs() => FileReadAllLine(ModsListURLsFName);

    internal string[] FileReadAllLine(string fname) => FileReadString(fname).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    internal string FileReadString(string fname) => Encoding.UTF8.GetString(FileRead(fname));
    internal byte[] FileRead(string fname)
    {
      byte[] ba = null;
      using (var stm = OpenFile(fname))
      {
        int bal = (int)stm.Length;
        ba = new byte[bal];
        stm.Read(ba, 0, bal);
      }
      return ba;
    }
    internal List<ModsListStructure> FileReadList() => ByteArr2ModsList(FileRead(ModsListFName));

    internal void FileWrite(List<ModsListStructure> mlsl) => FileWrite(ModsListFName, ModsList2ByteArr(mlsl));
    internal async void FileWrite(string fname, byte[] ba)
    {
      using (var stm = OpenFile(fname))
      {
        await stm.WriteAsync(ba, 0, ba.Length);
      }
    }
    internal void FileWrite(string fname, string s) => FileWrite(fname, Encoding.UTF8.GetBytes(s));
    internal void FileWrite(string fname, string[] sa)
    {
      string s = string.Empty;
      for (int i = 0; i < sa.Length; i++) s += sa[i] + Environment.NewLine;
      FileWrite(fname, s);
    }

    internal List<ModsListStructure> ByteArr2ModsList(byte[] ba) => String2ModsList(Encoding.UTF8.GetString(ba));
    internal List<ModsListStructure> String2ModsList(string s) => StringArr2ModsList(s?.Split(new char[] { '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries));
    internal List<ModsListStructure> StringArr2ModsList(string[] sa)
    {
      if (sa == null || sa.Length <= 0) return null;

      List<ModsListStructure> mlsl = new List<ModsListStructure>();

      for(int i=0;i< sa.Length; i++)
      {
        if (string.IsNullOrEmpty(sa[i])) continue;
        mlsl.Add(new ModsListStructure(sa[i]));
      }

      return mlsl;
    }

    internal string[] ModsList2StringArr(List<ModsListStructure> mlsl)
    {
      string[] sa = new string[mlsl.Count];

      Parallel.For(0, mlsl.Count, (i) => sa[i] = mlsl[i].GetString());

      return sa;
    }
    internal string ModsList2String(List<ModsListStructure> mlsl)
    {
      string[] sa = ModsList2StringArr(mlsl);
      string s = string.Empty;

      for (int i = 0; i < sa.Length; i++)
        s += sa[i] + Environment.NewLine;

      return s;
    }
    internal byte[] ModsList2ByteArr(List<ModsListStructure> mlsl) => Encoding.UTF8.GetBytes(ModsList2String(mlsl));
    
    private int FindIndex(List<ModsListStructure> mlsl, string ModName)
    {
      if (string.IsNullOrWhiteSpace(ModName) || !(mlsl?.Count >= 0)) return -1;

      int ind = -1;

      Parallel.For(0, mlsl.Count, (i) =>
      {
        if (ind < 0 && mlsl[i].ModName == ModName) ind = i;
      });

      return ind;
    }
  }

  public class ModsListStructure
  {
    public ModsListStructure() { }
    public ModsListStructure(string arg)
    {
      string[] sa = arg.Replace(" ", string.Empty).Split(',');
      if (sa.Length >= 5)
      {
        try
        {
          LocalUpdateTime = long.Parse(sa[3]);
        }
        catch (Exception) { LocalUpdateTime = 0; }
        LocalPath = sa[4];
      }
      if (sa.Length >= 3)
      {
        ModName = sa[0];
        try
        {
          RemoteUpdateTime = long.Parse(sa[1]);
        }
        catch (Exception) { RemoteUpdateTime = 0; }
        RemotePath = sa[2];
      }
    }
    
    public string GetString()
    {
      string restr = ModName;
      restr += ", ";
      restr += RemoteUpdateTime.ToString();
      restr += ", ";
      restr += RemotePath;
      restr += ", ";
      restr += LocalUpdateTime.ToString();
      restr += ", ";
      restr += LocalPath;

      return restr;
    }

    public string ModName = string.Empty;
    public long RemoteUpdateTime = 0;
    public string RemotePath = string.Empty;
    public long LocalUpdateTime = 0;
    public string LocalPath = string.Empty;
  }
}
