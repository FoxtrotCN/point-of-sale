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
    public partial class frmCategoria : Form
    {
        private static DataTable dt = new DataTable();
        public frmCategoria()
        {
            InitializeComponent();
        }

        public void SetFlag(string valor)
        {
            txtFlag.Text = valor;
        }
        private void frmCategoria_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FCategoria.GetAll();
                dt = ds.Tables[0];
                dgvCategorias.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    lblDatosNoEncontrados.Visible = false;
                    dgvCategorias_CellClick(null, null);
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();
                if (sResultado == "")
                {
                    if (txtId.Text == "")
                    {
                        Categoria categoria = new Categoria();
                        categoria.Descripcion = txtNombre.Text;


                        if (FCategoria.Insertar(categoria) > 0)
                        {
                            MessageBox.Show("Datos insertados correctamente!");
                            frmCategoria_Load(null, null);
                        }
                    }
                    else
                    {
                        Categoria categoria = new Categoria();
                        categoria.Id = Convert.ToInt32(txtId.Text);
                        categoria.Descripcion = txtNombre.Text;

                        if (FCategoria.Actualizar(categoria) == 1)
                        {
                            MessageBox.Show("Datos Modificados correctamente!");
                            frmCategoria_Load(null, null);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtId.Text = "";
            txtNombre.Text = "";

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            dgvCategorias_CellClick(null, null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Realmente deseas eliminar las categorias seleccionadas?", "Eliminacion de Categoria",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvCategorias.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Categoria categoria = new Categoria();
                            categoria.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FCategoria.Eliminar(categoria) != 1)
                            {
                                MessageBox.Show("La categoria no pudo ser eliminada", "Eliminacion de Categoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    frmCategoria_Load(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCategorias.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dgvCategorias.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.CurrentRow != null)
            {
                txtId.Text = dgvCategorias.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = dgvCategorias.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                dv.RowFilter = cmbBuscar.Text + " Like '" + txtBuscar.Text + "%'";

                dgvCategorias.DataSource = dv;
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
        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado += "Nombre\n";
            }
            return Resultado;
        }
        public void MostrarGuardarCancelar(bool b)
        {
            btnGuardar.Visible = b;
            btnCancelar.Visible = b;
            btnNuevo.Visible = !b;
            btnEditar.Visible = !b;
            btnEliminar.Visible = !b;

            dgvCategorias.Enabled = !b;

            txtNombre.Enabled = b;

        }

        private void dgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(txtFlag.Text == "1")
            {
                FrmProducto frmProd = FrmProducto.GetInstance();
                if (dgvCategorias.CurrentRow != null)
                {
                    frmProd.SetCategoria(dgvCategorias.CurrentRow.Cells[1].Value.ToString(),
                                        txtNombre.Text = dgvCategorias.CurrentRow.Cells[2].Value.ToString());
                    frmProd.Show();
                    Close();
                }
            }
            
        }
    }

 }    

