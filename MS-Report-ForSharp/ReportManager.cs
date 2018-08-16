using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace MS_Report_ForSharp
{
    class ReportManager
    {
        public ReportManager() {
            this.rpt = new ReportViewer();
        }

		public ReportManager(ProcessingMode p) : this() {
			this.processingMode = p;
		}

		public ReportManager(ProcessingMode p, PageCountMode count): this(p) {
			this.pageCountMode = count;
		}

		public ReportManager(ProcessingMode p, PageCountMode count, string local_EmbeddedResource_Path) : this(p, count) {
			this.Local_EmbeddedResource = local_EmbeddedResource_Path;
		}

		private ReportViewer rpt;

        public ProcessingMode processingMode {
            set {
                this.rpt.ProcessingMode = value;
            }
        }

        public PageCountMode pageCountMode {
            set {
                this.rpt.PageCountMode = value;
            }
        }

        public string Local_EmbeddedResource {
            set {
                this.rpt.LocalReport.ReportEmbeddedResource = value;
            }
        }

		public void ExportPDF(string path, string local_EmbeddedResource_Path) {
			this.Local_EmbeddedResource = local_EmbeddedResource_Path;
			this.Export(this.GetByte("PDF"), path, FileMode.Create);

		}

		/// <summary>
		/// Report 새로고침
		/// </summary>
        public void RefreshRport() {
			this.rpt.Refresh();
			this.rpt.RefreshReport();
		}

		/// <summary>
		/// 특정 형식으로 Byte[]를 만든다
		/// </summary>
		public byte[] GetByte(string sType) {
			Warning[] warning;
			string[] streamids;
			string mimeType;
			string encoding;
			string filenameExtension;
			byte[] bb = rpt.LocalReport.Render(
				sType, null, out mimeType, out encoding, out filenameExtension
				, out streamids, out warning);

			return bb;
		}

		/// <summary>
		/// byte[]를 파일에 쓴다.
		/// </summary>
		public void Export(byte[] b, string path, FileMode fileMode) {
			using (FileStream fs = new FileStream(path, fileMode)) {
				fs.Write(b, 0, b.Length);
			}
		}

		/// <summary>
		/// Source Data 바인딩
		/// </summary>
		public void SetBindingSource(string dataSetName, object list) {
			//BindingSource bs = new BindingSource();
			//bs.DataSource = list;

			ReportDataSource source = new ReportDataSource(dataSetName, list);
			this.rpt.LocalReport.DataSources.Add(source);
		}

	}
}
