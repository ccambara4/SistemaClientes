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
    public partial class FrmPersonaIndividual : Form
    {
        private int idPersona = 0;

        public FrmPersonaIndividual()
        {
            InitializeComponent();
        }

        public FrmPersonaIndividual(int id)
        {
            InitializeComponent();
            idPersona = id;
        }
        private void FrmPersonaIndividual_Load(object sender, EventArgs e)
        {
            CargarNacionalidades();
            CargarGeneros();
            CargarEstadosCiviles();
            CargarTiposIdentificacion();
            CargarCalidades();

            if (idPersona > 0)
            {
                CargarPersona(idPersona);
            }
        }

        private void CargarPersona(int id)
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

                        comando.Parameters.AddWithValue("@IdPersona", id);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNombreCompleto.Text =
                                    reader["NombreCompleto"].ToString();

                                txtLugarNacimiento.Text =
                                    reader["LugarNacimiento"].ToString();

                                if (reader["FechaNacimiento"] != DBNull.Value)
                                {
                                    dtpFechaNacimiento.Value =
                                        Convert.ToDateTime(
                                            reader["FechaNacimiento"]);
                                }

                                cmbNacionalidad.Text =
                                    reader["Nacionalidad"].ToString();

                                cmbGenero.Text =
                                    reader["Genero"].ToString();

                                cmbEstadoCivil.Text =
                                    reader["EstadoCivil"].ToString();

                                txtProfesionOficio.Text =
                                    reader["ProfesionOficio"].ToString();

                                cmbTipoIdentificacion.Text =
                                    reader["TipoIdentificacion"].ToString();

                                txtNumeroIdentificacion.Text =
                                    reader["NumeroIdentificacion"].ToString();

                                txtDireccion.Text =
                                    reader["DireccionResidencia"].ToString();

                                txtCondicionMigratoria.Text =
                                    reader["CondicionMigratoria"].ToString();

                                cmbCalidadActua.Text =
                                    reader["CalidadActua"].ToString();

                                txtNumeroEscritura.Text =
                                    reader["NumeroEscritura"].ToString();

                                txtReferenciaTransferencia.Text =
                                    reader["ReferenciaTransferencia"].ToString();

                                if (reader["Monto"] != DBNull.Value)
                                {
                                    txtMonto.Text =
                                        Convert.ToDecimal(
                                            reader["Monto"]).ToString("0.00");
                                }
                            }
                            else
                            {
                                MessageBox.Show(
                                    "No se encontró la persona.",
                                    "Editar persona",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la información de la persona.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarNacionalidades()
        {
            CargarCombo(
                cmbNacionalidad,
                "SELECT IdNacionalidad, Nombre FROM Nacionalidades WHERE Activo = 1 ORDER BY Nombre",
                "IdNacionalidad");
        }

        private void CargarGeneros()
        {
            CargarCombo(
                cmbGenero,
                "SELECT IdGenero, Nombre FROM Generos WHERE Activo = 1 ORDER BY Nombre",
                "IdGenero");
        }

        private void CargarEstadosCiviles()
        {
            CargarCombo(
                cmbEstadoCivil,
                "SELECT IdEstadoCivil, Nombre FROM EstadosCiviles WHERE Activo = 1 ORDER BY Nombre",
                "IdEstadoCivil");
        }

        private void CargarTiposIdentificacion()
        {
            CargarCombo(
                cmbTipoIdentificacion,
                "SELECT IdTipoIdentificacion, Nombre FROM TiposIdentificacion WHERE Activo = 1 ORDER BY Nombre",
                "IdTipoIdentificacion");
        }

        private void CargarCalidades()
        {
            CargarCombo(
                cmbCalidadActua,
                "SELECT IdCalidad, Nombre FROM Calidades WHERE Activo = 1 ORDER BY Nombre",
                "IdCalidad");
        }

        private void CargarCombo(
            Guna.UI2.WinForms.Guna2ComboBox combo,
            string consulta,
            string campoId)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataReader reader =
                            comando.ExecuteReader())
                        {
                            DataTable tabla = new DataTable();
                            tabla.Load(reader);

                            combo.DataSource = tabla;
                            combo.DisplayMember = "Nombre";
                            combo.ValueMember = campoId;
                            combo.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la información.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombreCompleto.Clear();
            txtLugarNacimiento.Clear();
            txtProfesionOficio.Clear();
            txtNumeroIdentificacion.Clear();
            txtDireccion.Clear();
            txtCondicionMigratoria.Clear();
            txtReferenciaTransferencia.Clear();
            txtMonto.Clear();
            txtNumeroEscritura.Clear();

            cmbNacionalidad.SelectedIndex = -1;
            cmbGenero.SelectedIndex = -1;
            cmbEstadoCivil.SelectedIndex = -1;
            cmbTipoIdentificacion.SelectedIndex = -1;
            cmbCalidadActua.SelectedIndex = -1;

            dtpFechaNacimiento.Value = DateTime.Today;

            txtNombreCompleto.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre completo.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombreCompleto.Focus();
                return;
            }

            if (cmbTipoIdentificacion.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el tipo de identificación.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbTipoIdentificacion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroIdentificacion.Text))
            {
                MessageBox.Show(
                    "Ingrese el número de identificación.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNumeroIdentificacion.Focus();
                return;
            }

            // Validar monto
            decimal monto;

            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                monto = 0;
            }
            else if (!decimal.TryParse(txtMonto.Text, out monto))
            {
                MessageBox.Show(
                    "El monto ingresado no es válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMonto.Focus();
                return;
            }

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string procedimiento;

                    if (idPersona == 0)
                    {
                        procedimiento = "sp_PersonaIndividual_Insertar";
                    }
                    else
                    {
                        procedimiento = "sp_PersonaIndividual_Actualizar";
                    }

                    using (SqlCommand comando =
                        new SqlCommand(procedimiento, conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        // ID solamente cuando estamos editando
                        if (idPersona > 0)
                        {
                            comando.Parameters.AddWithValue(
                                "@IdPersona",
                                idPersona);
                        }

                        comando.Parameters.AddWithValue(
                            "@NombreCompleto",
                            txtNombreCompleto.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@LugarNacimiento",
                            string.IsNullOrWhiteSpace(txtLugarNacimiento.Text)
                                ? (object)DBNull.Value
                                : txtLugarNacimiento.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@FechaNacimiento",
                            dtpFechaNacimiento.Value.Date);

                        comando.Parameters.AddWithValue(
                            "@Nacionalidad",
                            cmbNacionalidad.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbNacionalidad.Text);

                        comando.Parameters.AddWithValue(
                            "@Genero",
                            cmbGenero.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbGenero.Text);

                        comando.Parameters.AddWithValue(
                            "@EstadoCivil",
                            cmbEstadoCivil.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbEstadoCivil.Text);

                        comando.Parameters.AddWithValue(
                            "@ProfesionOficio",
                            string.IsNullOrWhiteSpace(txtProfesionOficio.Text)
                                ? (object)DBNull.Value
                                : txtProfesionOficio.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@TipoIdentificacion",
                            cmbTipoIdentificacion.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbTipoIdentificacion.Text);

                        comando.Parameters.AddWithValue(
                            "@NumeroIdentificacion",
                            txtNumeroIdentificacion.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@DireccionResidencia",
                            string.IsNullOrWhiteSpace(txtDireccion.Text)
                                ? (object)DBNull.Value
                                : txtDireccion.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@CondicionMigratoria",
                            string.IsNullOrWhiteSpace(txtCondicionMigratoria.Text)
                                ? (object)DBNull.Value
                                : txtCondicionMigratoria.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@CalidadActua",
                            cmbCalidadActua.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbCalidadActua.Text);

                        comando.Parameters.AddWithValue(
                            "@NumeroEscritura",
                            string.IsNullOrWhiteSpace(txtNumeroEscritura.Text)
                                ? (object)DBNull.Value
                                : txtNumeroEscritura.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@ReferenciaTransferencia",
                            string.IsNullOrWhiteSpace(txtReferenciaTransferencia.Text)
                                ? (object)DBNull.Value
                                : txtReferenciaTransferencia.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@Monto",
                            monto);

                        comando.ExecuteNonQuery();
                    }
                }

                if (idPersona == 0)
                {
                    MessageBox.Show(
                        "La persona fue registrada correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "La persona fue actualizada correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar la información.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
