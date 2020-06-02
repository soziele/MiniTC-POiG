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
            this.selectedItem = null;
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

        public void Guiding()
        {
            if (string.IsNullOrEmpty(SelectedItem))
            {
                return;
            }
            else if (SelectedItem.Contains(Resources.DirectorySign))
            {
                this.CurrentPath = selectedItem.Substring(3);
                this.selectedItem=null;
                ShowDirectoryContent();
            }
            else if (SelectedItem == Resources.GoBack)
            {
                CurrentPath = Directory.GetParent(currentPath).ToString();
                this.selectedItem = null;
                ShowDirectoryContent();
            }
            else this.CurrentPath = SelectedItem;
            
        }

        public void ShowDirectoryContent()
        {
            try
            {
                currentPanel.UpdateContent(CurrentPath);
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("Brak dostępu do ścieżki!");
                this.SelectedItem = null;
            }

            bool root = false;
            foreach(var drive in DrivesList)
            {
                if (this.currentPath == drive) root = true;
            }
            if (!root)
            {
                List<string> tmp = currentPanel.Content;
                tmp.Insert(0, Resources.GoBack);
                this.ContentList = tmp;
                this.SelectedItem = null;
            }
            else this.ContentList = currentPanel.Content;
        }


    }

}
