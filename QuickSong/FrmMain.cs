using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickSong.Core;

namespace QuickSong
{
    public partial class FrmMain : Form
    {
        private List<PpFileItem> _ppFiles = new List<PpFileItem>();
        public FrmMain()
        {
            InitializeComponent();
            PpTools.Setup();

            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            foreach (var indx in Properties.Settings.Default.indexes)
            {
                if(string.IsNullOrEmpty(indx))
                    continue;
                foreach (var ppFile in Directory.GetFiles(indx, "*.pp?x", SearchOption.AllDirectories))
                {
                    PpFileItem fileitem = new PpFileItem
                    {
                        Directory = Path.GetDirectoryName(ppFile),
                        FileName = Path.GetFileName(ppFile),
                        FullPath = ppFile
                    };
                    _ppFiles.Add(fileitem);
                    var filename = Path.GetFileName(ppFile);
                    textBox1.AutoCompleteCustomSource.Add(filename);
                }
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void BtnIndexes_Click(object sender, EventArgs e)
        {
            new FrmIndexes().Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter )
                return;
            var myFile = _ppFiles.FirstOrDefault(x => x.FileName.ToLower() == textBox1.Text.ToLower());
            PpTools.LaunchPowerpoint(myFile?.FullPath);
        }
    }

    public class PpFileItem
    {
        public string FullPath;
        public string FileName;
        public string Directory;
    }
}
