using SistemaClientes.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaClientes
{
    public partial class FrmConsultarPersonasJuridicas : Form
    {
        public FrmConsultarPersonasJuridicas()
        {
            InitializeComponent();
        }

        private void pnlBotones_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmConsultarPersonasJuridicas_Load(object sender, EventArgs e)
        {
            CargarPersonasJuridicas();

        }

        private void CargarPersonasJuridicas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando =
                        new SqlCommand("sp_PersonaJuridica_Obtener", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();
                            adaptador.Fill(tabla);

                            dgvPersonasJuridicas.DataSource = tabla;
                        }
                    }
                }

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar las personas jurídicas.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            if (dgvPersonasJuridicas.Columns.Count == 0)
                return;

            dgvPersonasJuridicas.Columns["IdPersonaJuridica"].HeaderText = "ID";
            dgvPersonasJuridicas.Columns["TipoEntidad"].HeaderText = "Tipo de entidad";
            dgvPersonasJuridicas.Columns["RazonSocial"].HeaderText = "Razón social";
            dgvPersonasJuridicas.Columns["NombreComercial"].HeaderText = "Nombre comercial";
            dgvPersonasJuridicas.Columns["NIT"].HeaderText = "NIT";
            dgvPersonasJuridicas.Columns["NombreRepresentante"].HeaderText = "Representante legal";
            dgvPersonasJuridicas.Columns["TipoIdentificacion"].HeaderText = "Tipo identificación";
            dgvPersonasJuridicas.Columns["NumeroIdentificacion"].HeaderText = "No. identificación";

            // Ocultar información que utilizaremos posteriormente para editar
            dgvPersonasJuridicas.Columns["DatosInscripcionRegistral"].Visible = false;
            dgvPersonasJuridicas.Columns["FormaLugarConstitucion"].Visible = false;
            dgvPersonasJuridicas.Columns["DomicilioFiscal"].Visible = false;

            dgvPersonasJuridicas.Columns["IdRepresentante"].Visible = false;
            dgvPersonasJuridicas.Columns["LugarNacimiento"].Visible = false;
            dgvPersonasJuridicas.Columns["FechaNacimiento"].Visible = false;
            dgvPersonasJuridicas.Columns["Nacionalidad"].Visible = false;
            dgvPersonasJuridicas.Columns["Genero"].Visible = false;
            dgvPersonasJuridicas.Columns["EstadoCivil"].Visible = false;
            dgvPersonasJuridicas.Columns["ProfesionOficio"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                CargarPersonasJuridicas();
                return;
            }

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT
                    PJ.IdPersonaJuridica,
                    PJ.TipoEntidad,
                    PJ.RazonSocial,
                    PJ.NombreComercial,
                    PJ.DatosInscripcionRegistral,
                    PJ.NIT,
                    PJ.FormaLugarConstitucion,
                    PJ.DomicilioFiscal,

                    RL.IdRepresentante,
                    RL.NombreCompleto AS NombreRepresentante,
                    RL.LugarNacimiento,
                    RL.FechaNacimiento,
                    RL.Nacionalidad,
                    RL.Genero,
                    RL.EstadoCivil,
                    RL.ProfesionOficio,
                    RL.TipoIdentificacion,
                    RL.NumeroIdentificacion

                FROM PersonasJuridicas PJ

                LEFT JOIN RepresentantesLegales RL
                    ON PJ.IdPersonaJuridica = RL.IdPersonaJuridica

                WHERE
                    PJ.RazonSocial LIKE @Busqueda
                    OR PJ.NombreComercial LIKE @Busqueda
                    OR PJ.NIT LIKE @Busqueda
                    OR RL.NombreCompleto LIKE @Busqueda
                    OR RL.NumeroIdentificacion LIKE @Busqueda

                ORDER BY PJ.IdPersonaJuridica DESC";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@Busqueda",
                            "%" + busqueda + "%");

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();
                            adaptador.Fill(tabla);

                            dgvPersonasJuridicas.DataSource = tabla;
                        }
                    }
                }

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo realizar la búsqueda.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarPersonasJuridicas();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmPersonasJuridicas formulario = new FrmPersonasJuridicas();
            formulario.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verificar que haya una empresa seleccionada
            if (dgvPersonasJuridicas.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una persona jurídica para editar.",
                    "Editar empresa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Obtener el ID de la empresa seleccionada
            int idPersonaJuridica = Convert.ToInt32(
                dgvPersonasJuridicas
                    .SelectedRows[0]
                    .Cells["IdPersonaJuridica"]
                    .Value);

            // Abrir formulario en modo edición
            FrmPersonasJuridicas formulario =
                new FrmPersonasJuridicas(idPersonaJuridica);

            formulario.ShowDialog();

            // Actualizar la tabla después de cerrar el formulario
            CargarPersonasJuridicas();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPersonasJuridicas.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una persona jurídica para eliminar.",
                    "Eliminar empresa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idPersonaJuridica = Convert.ToInt32(
                dgvPersonasJuridicas.SelectedRows[0]
                .Cells["IdPersonaJuridica"]
                .Value);

            string razonSocial =
                dgvPersonasJuridicas.SelectedRows[0]
                .Cells["RazonSocial"]
                .Value
                .ToString();

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar la empresa?\n\n" +
                "Empresa: " + razonSocial +
                "\n\nTambién se eliminará su representante legal.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(
                        "sp_PersonaJuridica_Eliminar",
                        conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue(
                            "@IdPersonaJuridica",
                            idPersonaJuridica);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "La persona jurídica y su representante legal fueron eliminados correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarPersonasJuridicas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar la persona jurídica.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
}
}

