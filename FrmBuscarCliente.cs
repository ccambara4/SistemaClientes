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
    public partial class FrmBuscarCliente : Form
    {
        public FrmBuscarCliente()
        {
            InitializeComponent();
        }

        

        private void FrmBuscarCliente_Load(object sender, EventArgs e)
        {
            cmbTipoCliente.Items.Clear();

            cmbTipoCliente.Items.Add("TODOS");
            cmbTipoCliente.Items.Add("PERSONA INDIVIDUAL");
            cmbTipoCliente.Items.Add("PERSONA JURÍDICA");

            cmbTipoCliente.SelectedIndex = 0;

            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT
                    'PERSONA INDIVIDUAL' AS TipoCliente,
                    PI.IdPersona AS IdCliente,
                    PI.NombreCompleto AS Nombre,
                    PI.TipoIdentificacion,
                    PI.NumeroIdentificacion,
                    NULL AS NIT,
                    NULL AS Representante
                FROM PersonasIndividuales PI

                UNION ALL

                SELECT
                    'PERSONA JURÍDICA' AS TipoCliente,
                    PJ.IdPersonaJuridica AS IdCliente,
                    PJ.RazonSocial AS Nombre,
                    RL.TipoIdentificacion,
                    RL.NumeroIdentificacion,
                    PJ.NIT,
                    RL.NombreCompleto AS Representante
                FROM PersonasJuridicas PJ

                LEFT JOIN RepresentantesLegales RL
                    ON PJ.IdPersonaJuridica = RL.IdPersonaJuridica

                ORDER BY Nombre";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();

                            adaptador.Fill(tabla);

                            dgvClientes.DataSource = tabla;
                        }
                    }
                }

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los clientes.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }

        private void ConfigurarGrid()
        {
            dgvClientes.Columns["TipoCliente"].HeaderText = "Tipo de Cliente";
            dgvClientes.Columns["IdCliente"].HeaderText = "ID Cliente";
            dgvClientes.Columns["Nombre"].HeaderText = "Nombre / Razón Social";
            dgvClientes.Columns["TipoIdentificacion"].HeaderText = "Tipo de Identificación";
            dgvClientes.Columns["NumeroIdentificacion"].HeaderText = "Número de Identificación";
            dgvClientes.Columns["NIT"].HeaderText = "NIT";
            dgvClientes.Columns["Representante"].HeaderText = "Representante Legal";
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text.Trim();
            string tipoCliente = cmbTipoCliente.Text;

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    string consulta = @"
                SELECT
                    'PERSONA INDIVIDUAL' AS TipoCliente,
                    PI.IdPersona AS IdCliente,
                    PI.NombreCompleto AS Nombre,
                    PI.TipoIdentificacion,
                    PI.NumeroIdentificacion,
                    NULL AS NIT,
                    NULL AS Representante
                FROM PersonasIndividuales PI
                WHERE
                    (@TipoCliente = 'TODOS'
                     OR @TipoCliente = 'PERSONA INDIVIDUAL')
                    AND
                    (
                        @Busqueda = ''
                        OR PI.NombreCompleto LIKE @Texto
                        OR PI.NumeroIdentificacion LIKE @Texto
                        OR PI.NumeroEscritura LIKE @Texto
                        OR PI.ReferenciaTransferencia LIKE @Texto
                    )

                UNION ALL

                SELECT
                    'PERSONA JURÍDICA' AS TipoCliente,
                    PJ.IdPersonaJuridica AS IdCliente,
                    PJ.RazonSocial AS Nombre,
                    RL.TipoIdentificacion,
                    RL.NumeroIdentificacion,
                    PJ.NIT,
                    RL.NombreCompleto AS Representante
                FROM PersonasJuridicas PJ

                LEFT JOIN RepresentantesLegales RL
                    ON PJ.IdPersonaJuridica = RL.IdPersonaJuridica

                WHERE
                    (@TipoCliente = 'TODOS'
                     OR @TipoCliente = 'PERSONA JURÍDICA')
                    AND
                    (
                        @Busqueda = ''
                        OR PJ.RazonSocial LIKE @Texto
                        OR PJ.NombreComercial LIKE @Texto
                        OR PJ.NIT LIKE @Texto
                        OR RL.NombreCompleto LIKE @Texto
                        OR RL.NumeroIdentificacion LIKE @Texto
                    )

                ORDER BY Nombre";

                    using (SqlCommand comando =
                        new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@TipoCliente",
                            tipoCliente);

                        comando.Parameters.AddWithValue(
                            "@Busqueda",
                            busqueda);

                        comando.Parameters.AddWithValue(
                            "@Texto",
                            "%" + busqueda + "%");

                        using (SqlDataAdapter adaptador =
                            new SqlDataAdapter(comando))
                        {
                            DataTable tabla = new DataTable();

                            adaptador.Fill(tabla);

                            dgvClientes.DataSource = tabla;
                        }
                    }
                }

                ConfigurarGrid();

                if (dgvClientes.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron clientes con los criterios de búsqueda.",
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
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
            cmbTipoCliente.SelectedIndex = 0;

            CargarClientes();

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            // Verificar que haya un cliente seleccionado
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un cliente para ver sus datos.",
                    "Ver datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Obtener el tipo de cliente seleccionado
            string tipoCliente = dgvClientes.SelectedRows[0]
                .Cells["TipoCliente"]
                .Value
                .ToString();

            // Obtener el ID del cliente
            int idCliente = Convert.ToInt32(
                dgvClientes.SelectedRows[0]
                .Cells["IdCliente"]
                .Value);

            // Abrir el formulario correspondiente
            if (tipoCliente == "PERSONA INDIVIDUAL")
            {
                FrmPersonaIndividual formulario =
                    new FrmPersonaIndividual(idCliente);

                formulario.ShowDialog();
            }
            else if (tipoCliente == "PERSONA JURÍDICA")
            {
                FrmPersonasJuridicas formulario =
                    new FrmPersonasJuridicas(idCliente);

                formulario.ShowDialog();
            }
        }
    }
}
