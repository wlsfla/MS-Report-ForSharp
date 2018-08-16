using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace MS_Report_ForSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			List<SummaryData> list = new List<SummaryData> ();
			list.Add(new SummaryData("가가가", "bb", "cc"));
			list.Add(new SummaryData("aa", "bb", "cc"));
			list.Add(new SummaryData("aa", "bb", "cc"));

			ReportManager rm = new MS_Report_ForSharp.ReportManager(ProcessingMode.Local, PageCountMode.Actual, "MS_Report_ForSharp.Report.rdlc");
			rm.SetBindingSource("DataSet1", list);
			rm.Export(rm.GetByte("PDF"), "result.pdf", FileMode.Create);
			MessageBox.Show("Done");
		}
    }

    public class SummaryData
    {
		public SummaryData(string fileName1, string fileName2, string fileName3) {
			this.FileName1 = fileName1;
			this.FileName2 = fileName2;
			this.FileName3 = fileName3;
		}
		public string FileName1 { get; set; }
        public string FileName2 { get; set; }
        public string FileName3 { get; set; }
    }
}