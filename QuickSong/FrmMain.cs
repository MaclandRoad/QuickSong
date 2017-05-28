﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickSong.Core;

namespace QuickSong
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            PpTools.Setup();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void BtnIndexes_Click(object sender, EventArgs e)
        {
            new FrmIndexes().Show();
        }
    }
}
