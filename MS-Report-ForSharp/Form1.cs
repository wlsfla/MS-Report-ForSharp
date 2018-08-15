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
        ReportViewer rv;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GetRpt();
            var b = this.RptExport(this.rv);
            this.ExportFile(b, "result.pdf", FileMode.Create);
        }

        private void GetRpt()
        {
            this.rv = new ReportViewer();
            this.rv.ProcessingMode = ProcessingMode.Local;
            rv.PageCountMode = PageCountMode.Actual;
            this.rv.LocalReport.ReportEmbeddedResource = "MS_Report_ForSharp.Report.rdlc";
            //DataTable dt = new DataTable();
            //for (int i = 1; i < 4; i++)
            //{
            //    dt.Columns.Add(string.Format("FileName{0}", i.ToString()));
            //    dt.Columns.Add(string.Format("Hash{0}", i.ToString()));
            //}

            //dt.Rows.Add(new object[] {
            //    "fileName1","fileName2","fileName3",
            //    "Hash1","Hash2","Hash3"
            //});

            SummaryData s = new SummaryData();
            s.FileName1 = "name1";
            s.FileName2 = "name2";
            s.FileName3 = "name3";
            s.Hash1 = "hash1";
            s.Hash2 = "hash2";
            s.Hash3 = "hash3";

            List<SummaryData> list = new List<SummaryData>();
            list.Add(s);

            this.rv.LocalReport.DataSources.Clear();
            BindingSource bs = new BindingSource();
            bs.DataSource = list;

            ReportDataSource source = new ReportDataSource();
            source.Name = "DataSet1";
            source.Value = bs;

            this.rv.LocalReport.DataSources.Add(source);
            this.RptRefresh(this.rv);
        }

        private void ExportFile(byte[] b, string path, FileMode fileMode)
        {
            using (FileStream fs = new FileStream(path, fileMode))
            {
                fs.Write(b, 0, b.Length);
            }
        }

        private byte[] RptExport(ReportViewer rpt)
        {
            Warning[] warning;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bb = rpt.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension
                , out streamids, out warning);
            return bb;
        }

        private void RptRefresh(ReportViewer rpt)
        {
            rpt.Refresh();
            rpt.RefreshReport();
        }
    }

    public class SummaryData
    {
        public string FileName1 { get; set; }
        public string FileName2 { get; set; }
        public string FileName3 { get; set; }
        public string Hash1 { get; set; }
        public string Hash2 { get; set; }
        public string Hash3 { get; set; }
    }
}