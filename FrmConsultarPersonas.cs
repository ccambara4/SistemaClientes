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
    public partial class FrmConsultarPersonas : Form
    {
       
        public FrmConsultarPersonas()
        {
            InitializeComponent();

        }



        private void FrmConsultarPersonas_Load(object sender, EventArgs e)
        {
            CargarPersonas();
        }

        private void CargarPersonas()
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(
                        "sp_PersonaIndividual_Obtener",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();
                            adaptador.Fill(tabla);

                            dgvPersonas.DataSource = tabla;

                            if (dgvPersonas.Columns.Contains("IdPersona"))
                                dgvPersonas.Columns["IdPersona"].HeaderText = "ID";

                            if (dgvPersonas.Columns.Contains("NombreCompleto"))
                                dgvPersonas.Columns["NombreCompleto"].HeaderText = "Nombre Completo";

                            if (dgvPersonas.Columns.Contains("LugarNacimiento"))
                                dgvPersonas.Columns["LugarNacimiento"].HeaderText = "Lugar Nacimiento";

                            if (dgvPersonas.Columns.Contains("FechaNacimiento"))
                                dgvPersonas.Columns["FechaNacimiento"].HeaderText = "Fecha Nacimiento";

                            if (dgvPersonas.Columns.Contains("Nacionalidad"))
                                dgvPersonas.Columns["Nacionalidad"].HeaderText = "Nacionalidad";

                            if (dgvPersonas.Columns.Contains("Genero"))
                                dgvPersonas.Columns["Genero"].HeaderText = "Género";

                            if (dgvPersonas.Columns.Contains("EstadoCivil"))
                                dgvPersonas.Columns["EstadoCivil"].HeaderText = "Estado Civil";

                            if (dgvPersonas.Columns.Contains("ProfesionOficio"))
                                dgvPersonas.Columns["ProfesionOficio"].HeaderText = "Profesion/Oficio";

                            if (dgvPersonas.Columns.Contains("TipoIdentificacion"))
                                dgvPersonas.Columns["TipoIdentificacion"].HeaderText = "Tipo Identificación";

                            if (dgvPersonas.Columns.Contains("NumeroIdentificacion"))
                                dgvPersonas.Columns["NumeroIdentificacion"].HeaderText = "No. Identificación";

                            if (dgvPersonas.Columns.Contains("DireccionResidencia"))
                                dgvPersonas.Columns["DireccionResidencia"].HeaderText = "Direccion de Residencia";

                            if (dgvPersonas.Columns.Contains("CondicionMigratoria"))
                                dgvPersonas.Columns["CondicionMigratoria"].HeaderText = "Condicion Migratoria";

                            if (dgvPersonas.Columns.Contains("NumeroEscritura"))
                                dgvPersonas.Columns["NumeroEscritura"].HeaderText = "No. Escritura";

                            if (dgvPersonas.Columns.Contains("CalidadActua"))
                                dgvPersonas.Columns["CalidadActua"].HeaderText = "Calidad";

                            if (dgvPersonas.Columns.Contains("ReferenciaTransferencia"))
                                dgvPersonas.Columns["ReferenciaTransferencia"].HeaderText = "Referencia Transferencia";

                            if (dgvPersonas.Columns.Contains("Monto"))
                                dgvPersonas.Columns["Monto"].HeaderText = "Monto";

                            if (dgvPersonas.Columns.Contains("Monto"))
                            {
                                dgvPersonas.Columns["Monto"].DefaultCellStyle.Format = "Q #,##0.00";
                                dgvPersonas.Columns["Monto"].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleRight;
                            }

                            if (dgvPersonas.Columns.Contains("FechaRegistro"))
                                dgvPersonas.Columns["FechaRegistro"].HeaderText = "Fecha Registro";


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar las personas.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                CargarPersonas();
                return;
            }

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT
                    *
                FROM PersonasIndividuales
                WHERE
                    NombreCompleto LIKE @Busqueda
                    OR NumeroIdentificacion LIKE @Busqueda
                    OR NumeroEscritura LIKE @Busqueda
                    OR ReferenciaTransferencia LIKE @Busqueda
                ORDER BY IdPersona DESC";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@Busqueda",
                            "%" + texto + "%");

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();
                            adaptador.Fill(tabla);

                            dgvPersonas.DataSource = tabla;

                            if (tabla.Rows.Count == 0)
                            {
                                MessageBox.Show(
                                    "No se encontraron personas con ese criterio.",
                                    "Búsqueda",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error durante la búsqueda.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarPersonas();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmPersonaIndividual formulario = new FrmPersonaIndividual();

            formulario.ShowDialog();

            // Actualizar la tabla al regresar
            CargarPersonas();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPersonas.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una persona para editar.",
                    "Editar persona",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idPersona = Convert.ToInt32(
                dgvPersonas.SelectedRows[0].Cells["IdPersona"].Value);

            FrmPersonaIndividual formulario =
                new FrmPersonaIndividual(idPersona);

            formulario.ShowDialog();

            CargarPersonas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPersonas.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una persona para eliminar.",
                    "Eliminar persona",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idPersona = Convert.ToInt32(
                dgvPersonas.SelectedRows[0].Cells["IdPersona"].Value);

            string nombrePersona =
                dgvPersonas.SelectedRows[0].Cells["NombreCompleto"].Value.ToString();

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar a:\n\n" +
                nombrePersona +
                "\n\nEsta acción no se puede deshacer.",
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
                        "sp_PersonaIndividual_Eliminar",
                        conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue(
                            "@IdPersona",
                            idPersona);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "La persona fue eliminada correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarPersonas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar la persona.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
