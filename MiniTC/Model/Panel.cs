using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MiniTC.Properties;

namespace MiniTC.Model
{
    class Panel
    {
        public List<string> Drives { get; set; }
        public string CurrentPath { get; set; }
        public List<string> Content { get; set; }

        public Panel()
        {
            UpdateDrives();
            Content = new List<string>();
            CurrentPath = "";
        }

        public void UpdateDrives()
        {
            this.Drives = Directory.GetLogicalDrives().ToList<string>();
        }

        public void UpdateContent()
        {
 
            List<string> directories = new List<string>();
            List<string> files = new List<string>();

            directories = (Directory.GetDirectories(CurrentPath).ToList<string>());
                for (int i = 0; i < directories.Count; i++)
                {
                    directories[i] = directories[i].Insert(0, Resources.DirectorySign);
                }
            files = (Directory.GetFiles(CurrentPath).ToList<string>());
            directories.AddRange(files);
            Content = directories;
                //Content.AddRange(files);
        }


    }
}
