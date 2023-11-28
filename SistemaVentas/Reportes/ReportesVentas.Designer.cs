namespace SistemaVentas
{
    partial class ReportesVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.txtVentaId = new System.Windows.Forms.TextBox();
            this.dataSetPrincipal = new SistemaVentas.DataSetPrincipal();
            this.uspReportesGenerarReporteVentaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usp_Reportes_GenerarReporteVentaTableAdapter = new SistemaVentas.DataSetPrincipalTableAdapters.usp_Reportes_GenerarReporteVentaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uspReportesGenerarReporteVentaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.uspReportesGenerarReporteVentaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaVentas.Reportes.RptReporteVenta.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1058, 662);
            this.reportViewer1.TabIndex = 2;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load_1);
            // 
            // txtVentaId
            // 
            this.txtVentaId.Location = new System.Drawing.Point(722, 302);
            this.txtVentaId.Name = "txtVentaId";
            this.txtVentaId.Size = new System.Drawing.Size(166, 20);
            this.txtVentaId.TabIndex = 3;
            this.txtVentaId.Visible = false;
            // 
            // dataSetPrincipal
            // 
            this.dataSetPrincipal.DataSetName = "DataSetPrincipal";
            this.dataSetPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uspReportesGenerarReporteVentaBindingSource
            // 
            this.uspReportesGenerarReporteVentaBindingSource.DataMember = "usp_Reportes_GenerarReporteVenta";
            this.uspReportesGenerarReporteVentaBindingSource.DataSource = this.dataSetPrincipal;
            // 
            // usp_Reportes_GenerarReporteVentaTableAdapter
            // 
            this.usp_Reportes_GenerarReporteVentaTableAdapter.ClearBeforeFill = true;
            // 
            // ReportesVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 662);
            this.Controls.Add(this.txtVentaId);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportesVentas";
            this.Text = "ReportesVentas";
            this.Load += new System.EventHandler(this.ReportesVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uspReportesGenerarReporteVentaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.TextBox txtVentaId;
        private System.Windows.Forms.BindingSource uspReportesGenerarReporteVentaBindingSource;
        private DataSetPrincipal dataSetPrincipal;
        private DataSetPrincipalTableAdapters.usp_Reportes_GenerarReporteVentaTableAdapter usp_Reportes_GenerarReporteVentaTableAdapter;
    }
}