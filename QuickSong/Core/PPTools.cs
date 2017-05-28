using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace QuickSong.Core
{
    public static class PpTools
    {
        private static void SetPpPath(string path)
        {
            Properties.Settings.Default.PpPath = path;
            Properties.Settings.Default.Save();
        }

        private static string GetPpPath()
        {
            // obtained via http://www.ryadel.com/en/microsoft-office-default-installation-folders-versions/
            // Iterates from top to bottom i think, so we want to grab the latest version
            // Untested: Click to run & non-365/2016
            var possiblePaths = new[]
            {
                @"C:\Program Files\Microsoft Office\root\Office16\",
                @"C:\Program Files (x86)\Microsoft Office\root\Office16\", // office 2016 / 365
                @"C:\Program Files\Microsoft Office\Office15\",
                @"C:\Program Files (x86)\Microsoft Office\Office15\", // office 2013
                @"C:\Program Files\Microsoft Office\Office14\",
                @"C:\Program Files (x86)\Microsoft Office\Office14\", // office 2010
                @"C:\Program Files\Microsoft Office\Office12\",
                @"C:\Program Files (x86)\Microsoft Office\Office12\", // office 2007
                @"C:\Program Files\Microsoft Office\Office11\",
                @"C:\Program Files (x86)\Microsoft Office\Office11\" // office 2003
            };
            // Don't think we really need to support office XP
            foreach (var path in possiblePaths)
            {
                var pptPath = path + "POWERPNT.exe";
                if(File.Exists(pptPath))
                {
                    return pptPath;
                }
            }
            return SelectPowerpointPath();
        }
        
        private static string SelectPowerpointPath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"Powerpoint |POWERPNT.exe";
            fileDialog.InitialDirectory = @"C:\Program Files\Microsoft Office";
            fileDialog.Title = @"Select your powerpoint exe (POWERPNT.exe)";
            return fileDialog.ShowDialog() == DialogResult.OK ? fileDialog.FileName : null;
        }

        public static void Setup()
        {
            var ppPath = GetPpPath();
            if(ppPath == null)
            {
                MessageBox.Show("Powerpoint exe was not automatically found or selected.\nIt is required.");
                Environment.Exit(0);
            }
            SetPpPath(ppPath);
        }

    }
}