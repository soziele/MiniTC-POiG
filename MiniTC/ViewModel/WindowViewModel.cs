using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows;


namespace MiniTC.ViewModel
{
    using Model;
    using Base;
    class WindowViewModel
    {
        public PanelViewModel Left { get; set; }
        public PanelViewModel Right { get; set; }

        public WindowViewModel()
        {
            Left = new PanelViewModel();
            Right = new PanelViewModel();
        }

        private ICommand copy = null;
        public ICommand Copy
        {
            get
            {
                if (copy == null)
                {
                    copy = new RelayCommand(
                        arg =>
                        {
                            string item = Left.SelectedItem.Substring(Left.SelectedItem.LastIndexOf(Path.DirectorySeparatorChar));
                            string source = Left.SelectedItem;
                            string dest = "";
                            if (!string.IsNullOrEmpty(Right.SelectedItem)) dest = Right.CurrentPath.Remove(Right.CurrentPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                            else dest = Right.CurrentPath;
                            string destination = dest + item;
                            try
                            {
                                File.Copy(@source, @destination);
                                Right.ShowDirectoryContent();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Błąd kopiowania! "+e.Message);
                            }
                        },
                        arg => !string.IsNullOrEmpty(Left.SelectedItem) && !Left.SelectedItem.Contains("<D>") && !string.IsNullOrEmpty(Right.SelectedDrive)
                        );
                }
                return copy;
            }
        }
        

    }
}
