using Guna.UI2.WinForms;
using SistemaClientes.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace SistemaClientes
{
    public partial class FrmPersonasJuridicas : Form
    {
        private int idPersonaJuridica = 0;
        public FrmPersonasJuridicas()
        {
            InitializeComponent();
        }

        public FrmPersonasJuridicas(int id)
        {
            InitializeComponent();

            idPersonaJuridica = id;

        }

        private void FrmPersonasJuridicas_Load(object sender, EventArgs e)
        {
            CargarTipoEntidad();
            CargarTipoIdentificacionRepresentante();
            CargarNacionalidadRepresentante();
            CargarGeneroRepresentante();
            CargarEstadoCivilRepresentante();

            if (idPersonaJuridica > 0)
            {
                CargarPersonaJuridica(idPersonaJuridica);
            }
        }

        private void CargarPersonaJuridica(int id)
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

                        comando.Parameters.AddWithValue(
                            "@IdPersonaJuridica",
                            id);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // =========================
                                // DATOS DE LA EMPRESA
                                // =========================

                                txtRazonSocial.Text =
                                    reader["RazonSocial"].ToString();

                                txtNombreComercial.Text =
                                    reader["NombreComercial"].ToString();

                                txtNIT.Text =
                                    reader["NIT"].ToString();

                                txtInscripcionRegistral.Text =
                                    reader["DatosInscripcionRegistral"].ToString();

                                txtFormaLugarConstitucion.Text =
                                    reader["FormaLugarConstitucion"].ToString();

                                txtDomicilioFiscal.Text =
                                    reader["DomicilioFiscal"].ToString();

                                SeleccionarComboPorTexto(
                                    cmbTipoEntidad,
                                    reader["TipoEntidad"].ToString());

                                // =========================
                                // REPRESENTANTE LEGAL
                                // =========================

                                txtNombreRepresentante.Text =
                                    reader["NombreRepresentante"].ToString();

                                txtLugarNacimientoRepresentante.Text =
                                    reader["LugarNacimiento"].ToString();

                                txtProfesionRepresentante.Text =
                                    reader["ProfesionOficio"].ToString();

                                txtNumeroIdentificacionRepresentante.Text =
                                    reader["NumeroIdentificacion"].ToString();

                                SeleccionarComboPorTexto(
                                    cmbTipoIdentificacionRepresentante,
                                    reader["TipoIdentificacion"].ToString());

                                SeleccionarComboPorTexto(
                                    cmbNacionalidadRepresentante,
                                    reader["Nacionalidad"].ToString());

                                SeleccionarComboPorTexto(
                                    cmbGeneroRepresentante,
                                    reader["Genero"].ToString());

                                SeleccionarComboPorTexto(
                                    cmbEstadoCivilRepresentante,
                                    reader["EstadoCivil"].ToString());

                                if (reader["FechaNacimiento"] != DBNull.Value)
                                {
                                    dtpFechaNacimientoRepresentante.Value =
                                        Convert.ToDateTime(
                                            reader["FechaNacimiento"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la persona jurídica.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SeleccionarComboPorTexto(
            Guna.UI2.WinForms.Guna2ComboBox combo,
            string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                combo.SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < combo.Items.Count; i++)
            {
                DataRowView fila = combo.Items[i] as DataRowView;

                if (fila != null &&
                    fila["Nombre"].ToString()
                        .Equals(texto, StringComparison.OrdinalIgnoreCase))
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }

            combo.SelectedIndex = -1;
        }
        private void lblFechaNacimiento_Click(object sender, EventArgs e)
        {

        }

        private void CargarTipoEntidad()
        {
            CargarCombo(
                cmbTipoEntidad,
                "SELECT IdTipoEntidad, Nombre FROM TiposEntidades WHERE Activo = 1 ORDER BY Nombre",
                "IdTipoEntidad");
        }

        private void CargarTipoIdentificacionRepresentante()
        {
            CargarCombo(
                cmbTipoIdentificacionRepresentante,
                "SELECT IdTipoIdentificacion, Nombre FROM TiposIdentificacion WHERE Activo = 1 ORDER BY Nombre",
                "IdTipoIdentificacion");
        }

        private void CargarNacionalidadRepresentante()
        {
            CargarCombo(
                cmbNacionalidadRepresentante,
                "SELECT IdNacionalidad, Nombre FROM Nacionalidades WHERE Activo = 1 ORDER BY Nombre",
                "IdNacionalidad");
        }

        private void CargarGeneroRepresentante()
        {
            CargarCombo(
                cmbGeneroRepresentante,
                "SELECT IdGenero, Nombre FROM Generos WHERE Activo = 1 ORDER BY Nombre",
                "IdGenero");
        }

        private void CargarEstadoCivilRepresentante()
        {
            CargarCombo(
                cmbEstadoCivilRepresentante,
                "SELECT IdEstadoCivil, Nombre FROM EstadosCiviles WHERE Activo = 1 ORDER BY Nombre",
                "IdEstadoCivil");
        }

        private void CargarCombo(
            Guna2ComboBox combo,
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

        private void pnlEmpresa_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // ==========================================
            // VALIDACIONES
            // ==========================================

            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                MessageBox.Show(
                    "Ingrese la razón o denominación social.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRazonSocial.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreRepresentante.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre completo del representante legal.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombreRepresentante.Focus();
                return;
            }

            if (cmbTipoIdentificacionRepresentante.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el tipo de identificación del representante legal.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbTipoIdentificacionRepresentante.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroIdentificacionRepresentante.Text))
            {
                MessageBox.Show(
                    "Ingrese el número de identificación del representante legal.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNumeroIdentificacionRepresentante.Focus();
                return;
            }

            // ==========================================
            // GUARDAR / ACTUALIZAR
            // ==========================================

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string procedimiento;

                    if (idPersonaJuridica == 0)
                    {
                        procedimiento =
                            "sp_PersonaJuridica_RegistrarCompleta";
                    }
                    else
                    {
                        procedimiento =
                            "sp_PersonaJuridica_ActualizarCompleta";
                    }

                    using (SqlCommand comando =
                        new SqlCommand(procedimiento, conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        // ==========================================
                        // ID PARA ACTUALIZAR
                        // ==========================================

                        if (idPersonaJuridica > 0)
                        {
                            comando.Parameters.AddWithValue(
                                "@IdPersonaJuridica",
                                idPersonaJuridica);
                        }

                        // ==========================================
                        // DATOS DE LA EMPRESA
                        // ==========================================

                        comando.Parameters.AddWithValue(
                            "@TipoEntidad",
                            cmbTipoEntidad.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbTipoEntidad.Text);

                        comando.Parameters.AddWithValue(
                            "@RazonSocial",
                            txtRazonSocial.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@NombreComercial",
                            string.IsNullOrWhiteSpace(
                                txtNombreComercial.Text)
                                ? (object)DBNull.Value
                                : txtNombreComercial.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@DatosInscripcionRegistral",
                            string.IsNullOrWhiteSpace(
                                txtInscripcionRegistral.Text)
                                ? (object)DBNull.Value
                                : txtInscripcionRegistral.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@NIT",
                            string.IsNullOrWhiteSpace(txtNIT.Text)
                                ? (object)DBNull.Value
                                : txtNIT.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@FormaLugarConstitucion",
                            string.IsNullOrWhiteSpace(
                                txtFormaLugarConstitucion.Text)
                                ? (object)DBNull.Value
                                : txtFormaLugarConstitucion.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@DomicilioFiscal",
                            string.IsNullOrWhiteSpace(
                                txtDomicilioFiscal.Text)
                                ? (object)DBNull.Value
                                : txtDomicilioFiscal.Text.Trim());

                        // ==========================================
                        // DATOS DEL REPRESENTANTE
                        // ==========================================

                        comando.Parameters.AddWithValue(
                            "@NombreRepresentante",
                            txtNombreRepresentante.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@LugarNacimientoRepresentante",
                            string.IsNullOrWhiteSpace(
                                txtLugarNacimientoRepresentante.Text)
                                ? (object)DBNull.Value
                                : txtLugarNacimientoRepresentante.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@FechaNacimientoRepresentante",
                            dtpFechaNacimientoRepresentante.Value.Date);

                        comando.Parameters.AddWithValue(
                            "@NacionalidadRepresentante",
                            cmbNacionalidadRepresentante.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbNacionalidadRepresentante.Text);

                        comando.Parameters.AddWithValue(
                            "@GeneroRepresentante",
                            cmbGeneroRepresentante.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbGeneroRepresentante.Text);

                        comando.Parameters.AddWithValue(
                            "@EstadoCivilRepresentante",
                            cmbEstadoCivilRepresentante.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbEstadoCivilRepresentante.Text);

                        comando.Parameters.AddWithValue(
                            "@ProfesionRepresentante",
                            string.IsNullOrWhiteSpace(
                                txtProfesionRepresentante.Text)
                                ? (object)DBNull.Value
                                : txtProfesionRepresentante.Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@TipoIdentificacionRepresentante",
                            cmbTipoIdentificacionRepresentante.SelectedIndex == -1
                                ? (object)DBNull.Value
                                : cmbTipoIdentificacionRepresentante.Text);

                        comando.Parameters.AddWithValue(
                            "@NumeroIdentificacionRepresentante",
                            txtNumeroIdentificacionRepresentante.Text.Trim());

                        // ==========================================
                        // EJECUTAR
                        // ==========================================

                        comando.ExecuteNonQuery();
                    }
                }

                // ==========================================
                // MENSAJE
                // ==========================================

                if (idPersonaJuridica == 0)
                {
                    MessageBox.Show(
                        "La persona jurídica y su representante legal fueron registrados correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "La persona jurídica y su representante legal fueron actualizados correctamente.",
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

        private void LimpiarFormulario()
        {
            // Empresa
            cmbTipoEntidad.SelectedIndex = -1;
            txtRazonSocial.Clear();
            txtNombreComercial.Clear();
            txtNIT.Clear();
            txtInscripcionRegistral.Clear();
            txtFormaLugarConstitucion.Clear();
            txtDomicilioFiscal.Clear();

            // Representante
            txtNombreRepresentante.Clear();
            txtLugarNacimientoRepresentante.Clear();
            dtpFechaNacimientoRepresentante.Value = DateTime.Today;

            cmbNacionalidadRepresentante.SelectedIndex = -1;
            cmbGeneroRepresentante.SelectedIndex = -1;
            cmbEstadoCivilRepresentante.SelectedIndex = -1;

            txtProfesionRepresentante.Clear();

            cmbTipoIdentificacionRepresentante.SelectedIndex = -1;
            txtNumeroIdentificacionRepresentante.Clear();

            txtRazonSocial.Focus();
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

