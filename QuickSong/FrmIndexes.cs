﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;
using Squirrel;

namespace QuickSong
{
    public partial class FrmIndexes : Form
    {
        public FrmIndexes()
        {
            InitializeComponent();
            if(Properties.Settings.Default.indexes == null)
                Properties.Settings.Default.indexes = new StringCollection();
            foreach (string path in Properties.Settings.Default.indexes)
            {
                listBox1.Items.Add(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Add a location to search for Powerpoint files";
            dialog.ShowDialog();
            if(listBox1.Items.Contains(dialog.SelectedPath))
            {
                MessageBox.Show("Already added!");
                return;
            }
            listBox1.Items.Add(dialog.SelectedPath);
        }

        private void SaveItems()
        {
            Properties.Settings.Default.indexes = new StringCollection(); // clear to prevent duplication of list
            foreach (string s in listBox1.Items)
            {
                Properties.Settings.Default.indexes.Add(s);
            }
            Properties.Settings.Default.Save();
        }

        private void FrmIndexes_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveItems();
            Application.Restart();
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
    }
}
