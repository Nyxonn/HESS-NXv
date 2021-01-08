using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace harvester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool chosen = false;
        public string paczka;
        public string filetoload;
        public string searchingfor;
        public int ilosc = 0;
        public int ilosctrg = 0;

        public void Log(string text)
        {
            richTextBox1.Invoke(new MethodInvoker(delegate () { richTextBox1.AppendText(text + "\r\n"); richTextBox1.ScrollToCaret(); }));
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                richTextBox1.Text = "";
                Log("➤ Folder path has been set to: " + dialog.FileName);
                chosen = true;
                paczka = dialog.FileName;
            }
        }

        private void xuiSuperButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Nyxonn");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            ilosctrg = 0;
            ilosc = 0;
            if (chosen == false)
            {
                richTextBox1.Text = "";
                Log("⚠️ Select folder with .lua file before extraction!");
            }
            else
            {
                listBox1.Items.Clear();
                richTextBox2.Text = "";
                searchingfor = richTextBox3.Text;

                DirectoryInfo dinfo = new DirectoryInfo(paczka);

                Regex FileRegex = new Regex(@"\*\.\w\w\w");
                FileInfo[] Files;
                foreach (Match match in FileRegex.Matches("*.lua"))
                {
                    Files = dinfo.GetFiles(match.Value);

                    tabControl1.SelectTab(1);


                    foreach (FileInfo file in Files)
                    {
                        listBox1.Items.Add(file.Name);
                        filetoload = paczka + "\\" + file.Name;
                        string pliczekNazwa = file.Name;
                        foreach (var line in File.ReadAllLines(filetoload))
                        {
                            if (line.Contains("--"))
                            {

                            }
                            else
                            {
                                if (line.Contains(searchingfor.ToString()))
                                {
                                    richTextBox2.AppendText(pliczekNazwa + ": " + line.TrimStart() + "\n\n");
                                    richTextBox2.ScrollToCaret();
                                    ilosctrg = ilosctrg + 1;
                                }
                            }
                            ilosc = ilosc + 1;
                        }
                      



                    }
                    Log("★ Readed " + ilosc + " lines and found " + ilosctrg + " potential strings!");
                }
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilosctrg = 0;
            ilosc = 0;
            filetoload = paczka + @"\" + listBox1.SelectedItem;
            string pliczekNazwa = listBox1.SelectedItem.ToString();
            string pliczek = File.ReadAllText(filetoload);
            searchingfor = richTextBox3.Text;
            if (pliczek.Contains(searchingfor))
            {
                richTextBox2.AppendText("\n\n");
                foreach (var line in File.ReadAllLines(filetoload))
                {
                    if (line.Contains("--")) { 
                        
                    } else {
                        if (line.Contains(searchingfor))
                        {
                            richTextBox2.AppendText(pliczekNazwa + ": " + line.TrimStart() + "\n\n");
                            richTextBox2.ScrollToCaret();
                            ilosctrg = ilosctrg + 1;
                        }
                    }
                    ilosc = ilosc + 1;
                }
                Log("★ " + pliczekNazwa + ": Readed " + ilosc + " lines and found " + ilosctrg + " potential strings!");
            } else
            {
                richTextBox2.AppendText("\n" + pliczekNazwa + ": " + "No triggers were found\n");
                richTextBox2.ScrollToCaret();
                Log("✫ " + pliczekNazwa + ": Readed " + ilosc + " lines and found none potential strings!");
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
