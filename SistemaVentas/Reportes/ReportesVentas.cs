using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class ReportesVentas : Form
    {

        public ReportesVentas()
        {
            InitializeComponent();
        }

        public void SetVentaId(int ventaId)
        {
            txtVentaId.Text = ventaId.ToString();
        }

        private void ReportesVentas_Load(object sender, EventArgs e)
        {
            this.usp_Reportes_GenerarReporteVentaTableAdapter.Fill(this.dataSetPrincipal.usp_Reportes_GenerarReporteVenta, Convert.ToInt32(txtVentaId.Text));    
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
