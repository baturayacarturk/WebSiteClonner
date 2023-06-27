using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSC.Services;
using WSC.TreeStructure;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WSC
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            FormNodeSelecter frmNS = new FormNodeSelecter(url);
            frmNS.ShowDialog();
        }
        
        string RemoveForbiddenCharacters(string input)
        {
            string forbiddenCharacters = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string sanitizedString = new string(input
                .Where(c => !forbiddenCharacters.Contains(c))
                .ToArray());
            return sanitizedString;
        }

    }





}
