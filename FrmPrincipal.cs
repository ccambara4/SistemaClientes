using Guna.UI2.WinForms;
using SistemaClientes.Clases;
using System.Data;
using System.Data.SqlClient;

namespace SistemaClientes
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNuevaPersona_Click(object sender, EventArgs e)
        {
            FrmPersonaIndividual formulario = new FrmPersonaIndividual();
            formulario.ShowDialog();

        }

        private void btnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            FrmPersonasJuridicas formulario = new FrmPersonasJuridicas();
            formulario.ShowDialog();

            // Actualizar estadísticas después de registrar
            CargarEstadisticas();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmBuscarCliente formulario = new FrmBuscarCliente();
            formulario.ShowDialog();

        }

        private void CargarEstadisticas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(
                        "sp_Dashboard_Estadisticas",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblTotalIndividuales.Text =
                                    reader["PersonasIndividuales"].ToString();

                                lblTotalJuridicas.Text =
                                    reader["PersonasJuridicas"].ToString();

                                lblTotalClientes.Text =
                                    reader["TotalClientes"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar las estadísticas.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CargarEstadisticas();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            FrmBuscarCliente formulario = new FrmBuscarCliente();
            formulario.ShowDialog();
        }

        private void pnlContenido_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPersonasJuridicas_Click(object sender, EventArgs e)
        {
            FrmConsultarPersonasJuridicas formulario = new FrmConsultarPersonasJuridicas();
            formulario.ShowDialog();
        }

        private void btnPersonasIndividuales_Click(object sender, EventArgs e)
        {
            FrmConsultarPersonas formulario = new FrmConsultarPersonas();
            formulario.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes formulario = new FrmReportes();
            formulario.ShowDialog();
        }
    }


}
