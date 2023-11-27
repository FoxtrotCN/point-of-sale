using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using SistemaVentas.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{
    public partial class FrmProducto : Form
    {
        private static DataTable dt = new DataTable();
        private static FrmProducto _instancia;
        public FrmProducto()
        {
            InitializeComponent();
        }

        public static FrmProducto GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new FrmProducto();

            }
            return _instancia;
        }


        public void SetFlag(string sValor)
        {
            txtFlag.Text = sValor;
        }

        public void SetCategoria(string id, string descripcion)
        {
            txtCategoriaId.Text = id;
            txtCategoriaDescripcion.Text = descripcion;
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
                        Producto producto = new Producto();

                        producto.Nombre = txtNombre.Text;
                        producto.Categoria.Id = Convert.ToInt32(txtCategoriaId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        producto.Stock = Convert.ToDouble(txtStock.Text);
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.FechaVencimiento = txtFechaDeVencimiento.Value;

                        MemoryStream ms = new MemoryStream();

                        if (Imagen.Image != null)
                        {
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        else
                        {
                            Imagen.Image = Resources.transparente;
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        producto.Imagen = ms.GetBuffer();

                        if (FProducto.Insertar(producto) > 0)
                        {
                            MessageBox.Show("Datos insertados correctamente!");
                            FrmProducto_Load(null, null);
                        }
                    }
                    else
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(txtId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Categoria.Id = Convert.ToInt32(txtCategoriaId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        producto.Stock = Convert.ToDouble(txtStock.Text);
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.FechaVencimiento = txtFechaDeVencimiento.Value;
                        MemoryStream ms = new MemoryStream();

                        if (Imagen.Image != null)
                        {
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        else
                        {
                            Imagen.Image = Resources.transparente;
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        producto.Imagen = ms.GetBuffer();

                        if (FProducto.Actualizar(producto) == 1)
                        {
                            MessageBox.Show("Datos Modificados correctamente!");
                            FrmProducto_Load(null, null);
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
        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado += "Nombre\n";
            }
            return Resultado;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtId.Text = "";
            txtNombre.Text = "";
            txtCategoriaId.Text = "";
            txtCategoriaDescripcion.Text = "";
            txtDescripcion.Text = "";
            txtStock.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtFechaDeVencimiento.Text = "";
            Imagen.BackgroundImage = Resources.transparente;
            Imagen.Image = null;
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            dgvProducto_CellClick(null, null);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Realmente deseas eliminar los productos seleccionados?", "Eliminacion de Producto",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvProducto.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FProducto.Eliminar(producto) != 1)
                            {
                                MessageBox.Show("El producto no pudo ser eliminado", "Eliminacion de producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    FrmProducto_Load(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCambiarImagen_Click(object sender, EventArgs e)
        {
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                Imagen.BackgroundImage = null;
                Imagen.Image = new Bitmap(dialogo.FileName);
                Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            Imagen.BackgroundImage = Resources.transparente;
            Imagen.Image = null;
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmCategoria frmcate = new frmCategoria();
            frmcate.SetFlag("1");
            frmcate.ShowDialog();
        }
        private void dgvProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 									
            if (dgvProducto.CurrentRow != null)
            {
                txtId.Text = dgvProducto.CurrentRow.Cells["Id"].Value.ToString();
                txtCategoriaId.Text = dgvProducto.CurrentRow.Cells["CategoriaId"].Value.ToString();
                txtCategoriaDescripcion.Text = dgvProducto.CurrentRow.Cells["CategoriaDescripcion"].Value.ToString();
                txtNombre.Text = dgvProducto.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = dgvProducto.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtStock.Text = dgvProducto.CurrentRow.Cells["Stock"].Value.ToString();
                txtPrecioCompra.Text = dgvProducto.CurrentRow.Cells["PrecioCompra"].Value.ToString();
                txtPrecioVenta.Text = dgvProducto.CurrentRow.Cells["PrecioVenta"].Value.ToString();
                txtFechaDeVencimiento.Text = dgvProducto.CurrentRow.Cells["FechaVencimiento"].Value.ToString();
                Imagen.BackgroundImage = null;
                byte[] b = (byte[])dgvProducto.CurrentRow.Cells["imagen"].Value;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(b);
                Imagen.Image = Image.FromStream(ms);
                Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void dgvProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProducto.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvProducto.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FProducto.GetAll();
                dt = ds.Tables[0];
                dgvProducto.DataSource = dt;


                if (dt.Rows.Count > 0)
                {
                    dgvProducto.Columns["Imagen"].Visible = false;
                    lblDatosNoEncontrados.Visible = false;
                    dgvProducto_CellClick(null, null);
                }
                else
                {
                    lblDatosNoEncontrados.Visible = true;
                }
                MostrarGuardarCancelar(false);
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
            btnNuevo.Visible = !b;
            btnEditar.Visible = !b;
            btnEliminar.Visible = !b;

            dgvProducto.Enabled = !b;

            txtNombre.Enabled = b;

            btnCambiarImagen.Visible = b;
            btnQuitarImagen.Visible = b;
            btnBuscarCategoria.Visible = b;

            txtNombre.Enabled = b;
            txtCategoriaId.Enabled = b;
            txtCategoriaDescripcion.Enabled = b;
            txtDescripcion.Enabled = b;
            txtStock.Enabled = b;
            txtPrecioCompra.Enabled = b;
            txtPrecioVenta.Enabled = b;
        }

        private void dgvProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtFlag.Text == "1")
            {
                FrmDetalleVenta frmDetVenta = FrmDetalleVenta.GetInstance();
                if (dgvProducto.CurrentRow != null)
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(dgvProducto.CurrentRow.Cells["Id"].Value.ToString());
                    producto.Nombre = dgvProducto.CurrentRow.Cells["Nombre"].Value.ToString();
                    producto.Stock = Convert.ToDouble(dgvProducto.CurrentRow.Cells["Stock"].Value.ToString());
                    producto.PrecioVenta = Convert.ToDouble(dgvProducto.CurrentRow.Cells["PrecioVenta"].Value.ToString());
                    frmDetVenta.SetProducto(producto);
                    frmDetVenta.Show();
                    Close();
                }
            }
        }
    }
}
