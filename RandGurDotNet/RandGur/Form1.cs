using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace RandGur
{
    public partial class Form1 : Form
    {
        public string prevURL, curURL, tmp, imgurSha = "B2-C9-B9-09-EB-C9-58-6E-D6-81-3E-88-38-D5-46-D9-89-7E-52-81", characters = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        public bool isFullScreen = false;
        FormState fs = new FormState();
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(tmp);
            }
            catch (System.IO.FileNotFoundException fne) { Console.Write(fne); }
            Close();
            Environment.Exit(0);
        }



        private void LoadImage()
        {
            char[] gur = new char[5];
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                gur[i] = characters[rand.Next(0, characters.Length)];
            }
            string gururl = new string(gur);
            string jpeg = ".jpg", img = "http://i.imgur.com/";
            pictureBox1.Visible = false;
            pictureBox1.ImageLocation = img + gururl + jpeg;
            pictureBox1.Load();
            tmp = System.IO.Path.GetTempFileName();
            pictureBox1.Image.Save(tmp, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] data = System.IO.File.ReadAllBytes(tmp);
            SHA1CryptoServiceProvider cryptoProvider = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoProvider.ComputeHash(data));
            if (hash == imgurSha)
            {
                LoadImage();
            }
            else
            {
                
            if (pictureBox1.Image.Size.Width > this.Size.Width || pictureBox1.Image.Size.Height > this.Size.Height)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.Size = pictureBox1.Image.Size;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
                prevURL = curURL;
                curURL = img + gururl + jpeg;
                pictureBox1.Refresh();
                pictureBox1.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            LoadImage();
        }

        private void nextImageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(tmp);
            }
            catch (System.IO.FileNotFoundException fne) { Console.Write(fne); }
            LoadImage();
        }

        private void getCurrentImageURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(curURL);
            MessageBox.Show("URL Copied To Your Clipboard", "Copied!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        private void saveCurrentImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".jpg";
            sfd.Filter = "JPEG Images|*.jpg";
            sfd.InitialDirectory = Environment.CurrentDirectory;
            sfd.Title = "Save Image";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void reloadCurrentImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }


        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                nextImageToolStripMenuItem1.PerformClick();
            }
            if (e.KeyCode == Keys.Right)
            {
                nextImageToolStripMenuItem1.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                fs.Restore(this);
                isFullScreen = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                pictureBox1.ImageLocation = prevURL;
                pictureBox1.Load();
                pictureBox1.Refresh();
            }
        }

        private void nextImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isFullScreen)
            {
                fs.Maximize(this);
                isFullScreen = true;
            }
            else
            {
                fs.Restore(this);
                isFullScreen = false;
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutForm af = new aboutForm();
            af.ShowDialog();
        }

    }
}
