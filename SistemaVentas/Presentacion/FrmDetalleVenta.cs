using SistemaVentas.Datos;
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
    public partial class FrmDetalleVenta : Form
    {
        private static DataTable dt = new DataTable();
        private static FrmDetalleVenta _instacia = null;
        public FrmDetalleVenta()
        {
            InitializeComponent();
        }

        public static FrmDetalleVenta GetInstance()
        {
            if (_instacia == null)
            {
                _instacia = new FrmDetalleVenta();


            
            }
            return _instacia;
        }

        internal void SetProducto(Producto producto)
        {
            txtProductoId.Text = producto.Id.ToString();
            txtProductoDescripcion.Text = producto.Nombre;
            txtStock.Text = producto.Stock.ToString();
            txtPrecioUnitario.Text = producto.PrecioVenta.ToString();
        }

        internal void SetVenta(Venta venta)
        {
            txtVentaId.Text = venta.Id.ToString();
            txtClienteId.Text = venta.Cliente.Id.ToString();
            txtClienteNombre.Text = venta.Cliente.Nombre;
            txtFecha.Text = venta.FechaVenta.ToShortDateString();
            cmbTipoDoc.Text = venta.TipoDocumento;
            txtNumeroDoc.Text = venta.NumeroDocumento.ToString();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            FrmProducto frmProd = FrmProducto.GetInstance();
            frmProd.SetFlag("1");
            frmProd.ShowDialog();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();
                if (sResultado == "")
                {

                    DetalleVenta detventa = new DetalleVenta();
                    detventa.Venta.Id = Convert.ToInt32(txtVentaId.Text);
                    detventa.Producto.Id = Convert.ToInt32(txtProductoId.Text);
                    detventa.Cantidad = Convert.ToDouble(txtCantidad.Text);
                    detventa.PrecioUnitario = Convert.ToDouble(txtPrecioUnitario.Text);

                    int iDetVentaId = FDetalleVenta.Insertar(detventa);

                    if (iDetVentaId > 0)
                    {
                        FDetalleVenta.DisminuirStock(detventa);
                        MessageBox.Show("El Producto se agrego correctamente.");
                        FrmDetalleVenta_Load(null, null);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El Producto no se pudo agregar, intente nuevamente.");
                    }
                }
                else
                {
                    MessageBox.Show(sResultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Limpiar()
        {
            txtProductoId.Text = "";
            txtProductoDescripcion.Text = "";
            txtCantidad.Text = "1";
            txtStock.Text = "0";
            txtPrecioUnitario.Text = "";

        }

        private string ValidarDatos()
        {
            string Resultado = "";
            if (txtProductoId.Text == "")
            {
                Resultado += "Debe Seleccionar un Producto\n";
            }
            if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtStock.Text))
            {
                Resultado += "La cantidad que intenta vender supera el stock.\n";
            }
            return Resultado;
        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FDetalleVenta.GetAll(Convert.ToInt32(txtVentaId.Text));
                dt = ds.Tables[0];
                dgvVentas.DataSource = dt;
                dgvVentas.Columns["VentaId"].Visible = false;
                dgvVentas.Columns["Id"].Visible = false;
                dgvVentas.Columns["ProductoId"].Visible = false;
                dgvVentas.Columns["PrecioVenta"].Visible = false;

                if (dt.Rows.Count > 0)
                {
                    lblDatosNoEncontrados.Visible = false;
                    // dgvVentas_CellClick(null, null);
                }
                else
                {
                    lblDatosNoEncontrados.Visible = true;

                }
                // MostrarGuardarCancelar(false);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvVentas.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvVentas.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Realmente deseas eliminar los productos seleccionados?", "Eliminacion de Producto",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvVentas.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            DetalleVenta detVenta = new DetalleVenta();
                            detVenta.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            detVenta.Producto.Id = Convert.ToInt32(row.Cells["ProductoId"].Value);
                            detVenta.Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                           if (FDetalleVenta.Eliminar(detVenta) > 0)
                            {
                                if (FDetalleVenta.AumentarStock(detVenta) != 1)
                                {
                                    MessageBox.Show("No se pudo actualizar el stock", "Eliminacion de Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("El Producto no pudo ser eliminado de la venta. Intente nuevamente", "Eliminacion de Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            
                        }
                    }
                    FrmDetalleVenta_Load(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
