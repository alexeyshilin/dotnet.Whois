using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// using NameWhoIs;
using System.IO;

namespace WindowsFormsApplication1Whois
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (System.IO.File.Exists(TemporaryFilename))
            {
                System.IO.File.Delete(TemporaryFilename);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //NameWhoIs.WhoIs srv = new NameWhoIs.WhoIs();
            //txtResult.Text = srv.WhoIsWho(txtDomain.Text);

            Krysalix.DomainChecker srv = new Krysalix.DomainChecker();
            Krysalix.WhoisInfo info = srv.GetInfo(txtDomain.Text);
            txtResult.Text = info.Info;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string topdomains = "kz tel su ru com bz mobi org net biz info tv me рф";

            char[] arr = { ' ' };

            string[] domains = topdomains.Split(arr);

            txtResult.Text = string.Empty;

            string data = string.Empty;

            /*
            string[] names = txtDomain.Text.Split(arr);

            for (int j = 0; j < names.Length; ++j)
            {

            }
            */

            if (!System.IO.File.Exists(TemporaryFilename))
            {
                data += "Домены;";
                for (int i = 0; i < domains.Length; ++i)
                {
                    data += domains[i] + ";";
                }
                data += "\r\n";
            }

            data += txtDomain.Text + ";";

            for (int i = 0; i < domains.Length; ++i)
            {
                /*
                Krysalix.DomainChecker srv = new Krysalix.DomainChecker();
                string domain = txtDomain.Text + "." + domains[i];
                txtResult.Text += domain + " " + srv.Exists(domain).ToString() + "\r\n";
                */

                Krysalix.DomainChecker srv = new Krysalix.DomainChecker();
                string domain = txtDomain.Text + "." + domains[i];
                Krysalix.WhoisInfo info = srv.GetInfo(domain);
                txtResult.Text += domain + " " + info.IsExists().ToString() + "\r\n";
                txtResult.Text += "\t" + "state: " + info.GetValue("state") + "\r\n";
                txtResult.Text += "\t" + "status: " + info.GetValue("status") + "\r\n";
                txtResult.Text += "\t" + "domain:" + info.GetValue("domain") + "\r\n";
                txtResult.Text += "\t" + "domain name: " + info.GetValue("domain name") + "\r\n";
                txtResult.Text += "\t" + "server name:" + info.GetValue("server name") + "\r\n";
                txtResult.Text += "\t" + "created:" + info.GetValue("created") + "\r\n";
                txtResult.Text += "\r\n";

                data += info.IsExists().ToString() + ";";
            }

            data += "\r\n";

            //FileStream sr = File.Create(TemporaryFilename);
            FileStream sr = new FileStream(TemporaryFilename, FileMode.Append); // FileMode.OpenOrCreate
            StreamWriter writer = new StreamWriter(sr, Encoding.GetEncoding("utf-8"));

            writer.Write(data);

            writer.Close();
            sr.Close();
        }


        private string TemporaryFilename
        {
            get
            {

                //return System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + "out.xml";
                return Application.StartupPath + "/" + "out.csv";
            }
        }

        private void ShowSaveDialog()
        {
            if (!System.IO.File.Exists(TemporaryFilename))
            {
                MessageBox.Show("Данные не сохранены");
                return;
            }

            System.Windows.Forms.SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.csv)|*.csv|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(dlg.FileName)) System.IO.File.Delete(dlg.FileName);
                System.IO.File.Copy(TemporaryFilename, dlg.FileName);
            }

            System.IO.File.Delete(TemporaryFilename);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ShowSaveDialog();
            MessageBox.Show("Файл сохранен.");
        }

    }
}
