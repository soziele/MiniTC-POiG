using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using MiniTC.Properties;

namespace MiniTC.ViewModel
{
    using Base;
    using Model;
    class PanelViewModel : ViewModelBase
    {
        private Panel currentPanel = new Panel();
        private List<string> drivesList;
        private string currentPath;
        private List<string> contentList;
        private string selectedDrive;
        private string selectedItem;

        public PanelViewModel()
        {
            this.drivesList = currentPanel.Drives;
            this.currentPath = "";
            this.contentList = new List<string>();
            this.selectedDrive = "";
            this.selectedItem = "";
        }

        public List<string> DrivesList
        {
            get => this.drivesList;
            set
            {
                this.drivesList = value;
                this.OnPropertyChanged();
            }
        }

        public string CurrentPath
        {
            get => this.currentPath;
            set
            {
                this.currentPath = value;
                this.OnPropertyChanged();
            }
        }
        public string SelectedDrive
        {
            get => this.selectedDrive;
            set
            {
                
                this.selectedDrive = value;
                this.CurrentPath = value;
                this.ShowDirectoryContent();
                this.OnPropertyChanged();
            }
        }

        public string SelectedItem
        {
            get=> this.selectedItem;
            set
            {
                this.selectedItem = value;
                Guiding();
                this.OnPropertyChanged();
            }
        }
        public List<string> ContentList
        {
            get => this.contentList;
            set
            {
                this.contentList = value;
                this.OnPropertyChanged();
            }
        }

        #region commands

        /*private ICommand guide=null;

        public ICommand Guide
        {
            get
            {
                if (guide == null)
                {
                    guide = new RelayCommand(
                        arg =>
                        {
                            Guiding();
                        },
                        arg => true
                        );
                }
                return guide;
            }
        }*/
 

        public void Guiding()
        {
            if (string.IsNullOrEmpty(SelectedItem))
            {
                return;
            }
            else if (SelectedItem.Contains(Resources.DirectorySign))
            {
                this.CurrentPath = SelectedItem.Substring(3);
                ShowDirectoryContent();
            }
            else if (SelectedItem == Resources.GoBack)
            {
                string removed = this.CurrentPath.Substring(CurrentPath.LastIndexOf(Path.DirectorySeparatorChar)+1);
                this.CurrentPath=this.CurrentPath.TrimEnd(removed.ToCharArray());
                ShowDirectoryContent();
            }
            else this.CurrentPath = SelectedItem;
            
        }

        public void ShowDirectoryContent()
        {
            currentPanel.CurrentPath = this.CurrentPath;
            currentPanel.UpdateContent();
            this.ContentList=currentPanel.Content;

            bool root = false;
            foreach(var drive in DrivesList)
            {
                if (this.CurrentPath == drive) root = true;
            }
            if(!root) this.ContentList.Insert(0, Resources.GoBack);
            this.OnPropertyChanged();
        }


        #endregion

    }

}
