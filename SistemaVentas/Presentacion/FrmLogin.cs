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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            DataSet ds = FLogin.ValidarLogin(txtUsuario.Text, txtPassword.Text);
            DataTable dt = ds.Tables[0];
            if(dt.Rows.Count > 0)
            {
                Usuario.Nombre = dt.Rows[0]["Nombre"].ToString();
                Usuario.Apellido = dt.Rows[0]["Apellido"].ToString();
                Usuario.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                Usuario.Dni = Convert.ToInt32(dt.Rows[0]["Dni"]);
                Usuario.NombreUsuario = dt.Rows[0]["Usuario"].ToString();
                Usuario.Tipo = dt.Rows[0]["Tipo"].ToString();
                Usuario.Telefono = dt.Rows[0]["Telefono"].ToString();
                Usuario.Direccion = dt.Rows[0]["Direccion"].ToString();

                FrmVenta.GetInstance().Show();
            }
            else
            {
                MessageBox.Show("Usuario y/o Contraseña incorrectos.");
                txtPassword.Text = "";
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
