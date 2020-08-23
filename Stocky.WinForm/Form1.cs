using Stocky.Data;
using Stocky.WinForm.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stocky.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSaveHistoricalData_Click(object sender, EventArgs e)
        {
            //HistoricalDataHelper.SaveHistoricalData();
            EodData.DownloadAsync();
            MessageBox.Show("done!");
        }

        private void btnShowChart_Click(object sender, EventArgs e)
         {
            string txt = txtStocks.Text;
            string[] symbols = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            

            foreach (var symbol in symbols)
            {
                string url = $"https://finviz.com/quote.ashx?t={symbol}";
                System.Diagnostics.Process.Start(url);

            }


        }

        private void btnSaveEODData_Click(object sender, EventArgs e)
        {
            EodData.DownloadEodAsync();
        }

        private void btnReadTweets_Click(object sender, EventArgs e)
        {
            txtStocks.Text = EodData.GetUserTimeLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OptionData.DownloadAsync();
        }
    }
}
