using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
namespace RandGur
{
    public partial class aboutForm : Form
    {
        public aboutForm()
        {
            InitializeComponent();
        }

        private void aboutForm_Load(object sender, EventArgs e)
        {
            Assembly assembly;
            StreamReader textStreamReader;
            try
            {
                assembly = Assembly.GetExecutingAssembly();
                textStreamReader = new StreamReader(assembly.GetManifestResourceStream("RandGur.about.txt"));
                string about = textStreamReader.ReadToEnd();
                textBox1.Text = about;
            }
            catch
            {
                MessageBox.Show("Error Reading About Text");
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.mikerosenberger.com/blog");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            donate dn = new donate();
            dn.ShowDialog();
        }        
    }
}
