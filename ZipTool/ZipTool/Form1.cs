using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZipHelper;
using System.Threading;


namespace ZipTool
{
    public delegate void MyInvokeFunc(ZipReportProgressEventArgs ea);
    public partial class MainForm : Form
    {

        private CZipHelper m_ZipHelper;

        private FolderBrowserDialog m_FolderBrowserDialog;

        private OpenFileDialog m_OpenFileDialog;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void button_unzip_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(new ThreadStart(ThreadFunc));
            thr.IsBackground = true;
            thr.Start();
        }

        private void ThreadFunc()
        {
            m_ZipHelper = new CZipHelper();
            EventHandler < ZipReportEventArgs > newHandler = new EventHandler<ZipReportEventArgs>(ReportEvent);
            m_ZipHelper.SetDecompressEvent(newHandler);

            string src = this.textBox_src.Text;
            string tar = this.textBox_tar.Text;

            m_ZipHelper.Decompress(src, tar, false);
        }

        public void ReportEvent(object sender, ZipReportEventArgs e)
        {
            if (e is ZipReportProgressEventArgs)
            {
                var ea = e as ZipReportProgressEventArgs;
                UpdateProgress(ea);
            }
            else if (e is ZipReportErrorEventArgs)
            {
                var ea = e as ZipReportErrorEventArgs;
                Console.WriteLine(ea.strError);
            }
            else if (e is ZipReportFinishEventArgs)
            {
                var ea = e as ZipReportFinishEventArgs;
                Finished();
            }
        }

        public void Finished()
        {
            MessageBox.Show("UnZip Finished","Successfuuly");
        }


        public void UpdateProgress(ZipReportProgressEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                MyInvokeFunc call = delegate (ZipReportProgressEventArgs e)
                {
                    this.progressBar_unzip.Value = e.perc;
                    string strvalue = Convert.ToString(e.speed)+"bit/s " + Convert.ToString(e.resttime) + "s " + Convert.ToString(e.perc) + "%";
                    this.label_progressbar.Text = strvalue;
                };

                this.Invoke(call, ea); 
            }
            else
            {
                this.progressBar_unzip.Value = ea.perc;
                string strvalue = Convert.ToString(ea.perc) + "%";
                this.label_progressbar.Text = strvalue;
            }

            
        }

        private void button_src_Click(object sender, EventArgs e)
        {
            m_OpenFileDialog = new OpenFileDialog();
            m_OpenFileDialog.Filter = "Zip Files(*.zip)|*.zip";
            if (m_OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_src.Text = m_OpenFileDialog.FileName.ToString().Trim();
            }
        }

        private void button_tar_Click(object sender, EventArgs e)
        {
            m_FolderBrowserDialog = new FolderBrowserDialog();

            if (m_FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_tar.Text = m_FolderBrowserDialog.SelectedPath.ToString().Trim();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            m_ZipHelper.DoCancle();
        }

        private void button_zip_Click(object sender, EventArgs e)
        {
            CZipHelper test = new CZipHelper();         
            string src = @"D:\Tool\Git";
            string tar = @"D:\test\test.zip";
            test.Compress(src,tar);
        }
    }
}
