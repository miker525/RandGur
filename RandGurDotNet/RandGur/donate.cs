using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
namespace RandGur
{
    public partial class donate : Form
    {
        public donate()
        {
            InitializeComponent();
        }

        private void donate_Load(object sender, EventArgs e)
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.GetExecutingAssembly();
                webBrowser1.DocumentStream = assembly.GetManifestResourceStream("RandGur.donation.htm");
            }
            catch
            {
                MessageBox.Show("Error Loading Donation Page");
            }
        }
    }
}
