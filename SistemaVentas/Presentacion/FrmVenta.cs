﻿using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{
    public partial class FrmVenta : Form
    {
        private static DataTable dt = new DataTable();
        private static FrmVenta _instancia = null;
        public FrmVenta()
        {
            InitializeComponent();
        }
        public static FrmVenta GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new FrmVenta();

            }
            return _instancia;
        }
        private void FrmVenta_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FVenta.GetAll();
                dt = ds.Tables[0];
                dgvVentas.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    lblDatosNoEncontrados.Visible = false;
                    dgvVentas_CellClick(null, null);
                }
                else
                {
                    lblDatosNoEncontrados.Visible = true;
                }
                MostrarGuardarCancelar(false);
                lblUsuario.Text = Usuario.Nombre + " " + Usuario.Apellido;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        public void MostrarGuardarCancelar(bool b)
        {
            btnGuardar.Visible = b;
            btnCancelar.Visible = b;
            btnBuscarCliente.Visible = b;  
            btnNuevo.Visible = !b;
            btnEditar.Visible = !b;

            dgvVentas.Enabled = !b;
            txtFecha.Enabled = b;
            cmbTipoDoc.Enabled = b;
            txtNumeroDoc.Enabled = b;


        }
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmCliente frmcliente = new frmCliente();
            frmcliente.SetFlag("1");
            frmcliente.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();
                if (sResultado == "")
                {
                    if (txtId.Text == "")
                    {
                        Venta venta = new Venta();
                        venta.Cliente.Id = Convert.ToInt32(txtClienteId.Text);
                        venta.FechaVenta = txtFecha.Value;
                        venta.TipoDocumento = cmbTipoDoc.Text;
                        venta.NumeroDocumento = txtNumeroDoc.Text;

                        venta.Cliente.Nombre = txtClienteNombre.Text;

                        int iVentaId = FVenta.Insertar(venta);
                        if (iVentaId > 0)
                        {
                            FrmVenta_Load(null, null);
                            venta.Id= iVentaId;
                            CargarDetalle(venta);
                        }
                    }
                    else
                    {
                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(txtId.Text);
                        venta.Cliente.Id = Convert.ToInt32(txtClienteId.Text);
                        venta.FechaVenta = txtFecha.Value;
                        venta.TipoDocumento = cmbTipoDoc.Text;
                        venta.NumeroDocumento = txtNumeroDoc.Text;

                        if (FVenta.Actualizar(venta) == 1)
                        {
                            MessageBox.Show("Datos Modificados correctamente!");
                            FrmVenta_Load(null, null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Faltan cargar Datos: \n" + sResultado);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void CargarDetalle(Venta venta)
        {
            FrmDetalleVenta frmDetVenta = FrmDetalleVenta.GetInstance();
            frmDetVenta.SetVenta(venta);
            frmDetVenta.ShowDialog();
        }

        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtClienteId.Text == "")
            {
                Resultado += "Cliente\n";
            }
            if (txtNumeroDoc.Text == "")
            {
                Resultado += "Numero Documento\n";
            }
            return Resultado;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtId.Text = "";
            txtClienteId.Text = "";
            txtClienteNombre.Text = "";
            txtNumeroDoc.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            dgvVentas_CellClick(null, null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                dv.RowFilter = cmbBuscar.Text + " Like '" + txtBuscar.Text + "%'";

                dgvVentas.DataSource = dv;
                if (dv.Count == 0)
                {
                    lblDatosNoEncontrados.Visible = true;
                }
                else
                {
                    lblDatosNoEncontrados.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                txtId.Text = dgvVentas.CurrentRow.Cells["Id"].Value.ToString();
                txtClienteId.Text = dgvVentas.CurrentRow.Cells["ClienteId"].Value.ToString();
                txtClienteNombre.Text = dgvVentas.CurrentRow.Cells["Nombre"].Value.ToString() + " " +
                                        dgvVentas.CurrentRow.Cells["Apellido"].Value.ToString();
                txtFecha.Text = dgvVentas.CurrentRow.Cells["FechaVenta"].Value.ToString();
                cmbTipoDoc.Text = dgvVentas.CurrentRow.Cells["TipoDocumento"].Value.ToString();
                txtNumeroDoc.Text = dgvVentas.CurrentRow.Cells["NumeroDocumento"].Value.ToString();
            }

        }
        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           /* if (e.ColumnIndex == dgvVentas.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvVentas.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }*/
        }

        internal void SetCliente(string sIdCliente, string sNombreCliente)
        {
            txtClienteId.Text = sIdCliente;
            txtClienteNombre.Text = sNombreCliente;
        }

        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                Venta venta = new Venta();

                venta.Id = Convert.ToInt32(dgvVentas.CurrentRow.Cells["Id"].Value.ToString());
                venta.Cliente.Id = Convert.ToInt32(dgvVentas.CurrentRow.Cells["ClienteId"].Value.ToString());
                venta.Cliente.Nombre = dgvVentas.CurrentRow.Cells["Nombre"].Value.ToString() + " " +
                                        dgvVentas.CurrentRow.Cells["Apellido"].Value.ToString();
                venta.FechaVenta = Convert.ToDateTime(dgvVentas.CurrentRow.Cells["FechaVenta"].Value.ToString());
                venta.TipoDocumento = dgvVentas.CurrentRow.Cells["TipoDocumento"].Value.ToString();
                venta.NumeroDocumento= dgvVentas.CurrentRow.Cells["NumeroDocumento"].Value.ToString();

                CargarDetalle(venta);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
