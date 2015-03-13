using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DowngradeSSISPackage
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

          
        }

        private void btnDowngrade_Click(object sender, EventArgs e)
        {
            if (txtTargetPackage.Text == "")
                txtTargetPackage.Text = Path.GetFileNameWithoutExtension(txtSourcePackage.Text ) + "_Yukon.dtsx";

            try
            {
                txtStatus.Text = "Package downgraded Started";
                Application.DoEvents();
                Downgrade.ProcessPackage(txtSourcePackage.Text, txtTargetPackage.Text);

                txtStatus.Text = "Package downgraded Completed";
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.Message ;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectSourcePackage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckFileExists = true;
            fd.FileName = txtSourcePackage.Text;
            fd.Filter = "SSIS Package (*.dtsx)|*.dtsx|All Files (*.*)|*.*";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSourcePackage.Text = fd.FileName;
            }
                
        }

        private void btnSelectTargetPackage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckFileExists = false;
            fd.FileName = txtTargetPackage.Text;
            fd.Filter = "SSIS Package (*.dtsx)|*.dtsx|All Files (*.*)|*.*";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTargetPackage.Text = fd.FileName;
            }
        }

        private void linkCodeplex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://SSISDowngrade.codeplex.com");
        }

        
      
    }
}
