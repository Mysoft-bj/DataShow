using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
namespace My.Core
{
    public interface ISingletonWatcher  { string WatcherFile { get; } }
    internal static class Watcher {

        public static void StartWatch(ISingletonWatcher inst) 
        {

          var   PyhsicalPath = PathHelper.GetDataPath(inst.WatcherFile);

          if (!File.Exists(PyhsicalPath))
          {
              PyhsicalPath = ResourceHelper.CopyToAppData(inst.GetType(), inst.WatcherFile);
            }
          Reload(inst);

          FileDepend.Watch(PyhsicalPath, () => Reload(inst));

        }
        public static void Reload(ISingletonWatcher inst) 
        {
            var PyhsicalPath = PathHelper.GetDataPath(inst.WatcherFile);
            if (!File.Exists(PyhsicalPath))
            {
                PyhsicalPath = ResourceHelper.CopyToAppData(inst.GetType(), inst.WatcherFile);
            }

            using (StreamReader reader = new StreamReader(PyhsicalPath, Encoding.UTF8))
            {
                var json = reader.ReadToEnd().Replace("\n", "").Replace("\r", "").Trim();
                var type = inst.GetType();
                var other=JsonConvert.DeserializeObject(json,type);
                MyReflectionHelper.InvokeMethod(typeof(Mapper), "Map", new Type[] { type, type }, null, new[] { other, inst });
               // Mapper.Map(, this);
            }
                
        }
       // var path = ResourceHelper.CopyToAppData<T1>();
    }

    internal static class FileDepend
    {
        public static void Watch(string path, Action onChanged)
        {

            FileSystemWatcher watcher = new FileSystemWatcher();
            if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                watcher.Path = fi.DirectoryName;
                watcher.Filter = fi.Name;
                watcher.IncludeSubdirectories = false;
            }
            else if (Directory.Exists(path))
            {
                watcher.Path = path;
                watcher.IncludeSubdirectories = true;
            }

            Action<object, FileSystemEventArgs> OnFileChanged = (obj, args) =>
            {
                var sender = obj as FileSystemWatcher;
                sender.EnableRaisingEvents = false;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    sender.EnableRaisingEvents = true;
                });
                
                onChanged();


            };
            watcher.Changed += new FileSystemEventHandler(OnFileChanged);
            watcher.Created += new FileSystemEventHandler(OnFileChanged);
            watcher.Deleted += new FileSystemEventHandler(OnFileChanged);
            watcher.Renamed += new RenamedEventHandler(OnFileChanged);
            watcher.EnableRaisingEvents = true;
        }
    }

}
