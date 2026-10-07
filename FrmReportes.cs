using SistemaClientes.Clases;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ClosedXML.Excel;
using System.IO;

namespace SistemaClientes
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            CargarResumenMensual();

            CargarGraficaClientesPorMes();

            CargarGraficaClientesPorTipo();
        }

        // =========================================================
        // RESUMEN MENSUAL
        // =========================================================
        private void CargarResumenMensual()
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando =
                        new SqlCommand(
                            "sp_Reportes_ResumenMensual",
                            conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        using (SqlDataReader lector =
                            comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                int mesActual =
                                    Convert.ToInt32(
                                        lector["ClientesMesActual"]);

                                int mesAnterior =
                                    Convert.ToInt32(
                                        lector["ClientesMesAnterior"]);

                                int diferencia =
                                    Convert.ToInt32(
                                        lector["Diferencia"]);

                                decimal crecimiento =
                                    Convert.ToDecimal(
                                        lector["PorcentajeCrecimiento"]);

                                lblMesActual.Text =
                                    mesActual.ToString();

                                lblMesAnterior.Text =
                                    mesAnterior.ToString();

                                lblDiferencia.Text =
                                    diferencia > 0
                                    ? "+" + diferencia
                                    : diferencia.ToString();

                                lblCrecimiento.Text =
                                    crecimiento.ToString("0.00") + "%";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los reportes.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // GRÁFICA DE CLIENTES POR MES
        // =========================================================
        private void CargarGraficaClientesPorMes()
        {
            try
            {
                using (SqlConnection conexion =
                    Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando =
                        new SqlCommand(
                            "sp_Reportes_ClientesPorMes",
                            conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        using (SqlDataReader lector =
                            comando.ExecuteReader())
                        {
                            chartClientes.Series.Clear();
                            chartClientes.ChartAreas.Clear();
                            chartClientes.Titles.Clear();
                            chartClientes.Legends.Clear();

                            // Área de la gráfica
                            ChartArea area =
                                new ChartArea("AreaPrincipal");

                            area.AxisX.Title = "Mes";
                            area.AxisY.Title =
                                "Cantidad de clientes";

                            area.AxisX.MajorGrid.Enabled = false;

                            chartClientes.ChartAreas.Add(area);

                            // Crear serie
                            Series serie =
                                new Series("Clientes");

                            serie.ChartType =
                                SeriesChartType.Column;

                            serie.IsValueShownAsLabel = true;

                            while (lector.Read())
                            {
                                string nombreMes =
                                    lector["NombreMes"].ToString();

                                int totalClientes =
                                    Convert.ToInt32(
                                        lector["TotalClientes"]);

                                int anio =
                                    Convert.ToInt32(
                                        lector["Anio"]);

                                if (!string.IsNullOrEmpty(nombreMes))
                                {
                                    nombreMes =
                                        char.ToUpper(
                                            nombreMes[0]) +
                                        nombreMes.Substring(1);
                                }

                                serie.Points.AddXY(
                                    nombreMes + " " + anio,
                                    totalClientes);
                            }

                            chartClientes.Series.Add(serie);

                            chartClientes.Titles.Add(
                                "Clientes registrados por mes");


                            area.AxisX.LabelStyle.Angle = -45;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la gráfica.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void CargarGraficaClientesPorTipo()
        {
            try
            {
                using (SqlConnection conexion =
                    Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando =
                        new SqlCommand(
                            "sp_Reportes_ClientesPorTipoMes",
                            conexion))
                    {
                        comando.CommandType =
                            CommandType.StoredProcedure;

                        using (SqlDataReader lector =
                            comando.ExecuteReader())
                        {
                            chartTipoCliente.Series.Clear();
                            chartTipoCliente.ChartAreas.Clear();
                            chartTipoCliente.Titles.Clear();
                            chartTipoCliente.Legends.Clear();

                            // Área de la gráfica
                            ChartArea area =
                                new ChartArea("AreaPrincipal");

                            area.AxisX.Title = "Mes";
                            area.AxisY.Title =
                                "Cantidad de clientes";

                            area.AxisX.MajorGrid.Enabled = false;

                            chartTipoCliente.ChartAreas.Add(area);

                            // Personas individuales
                            Series individuales =
                                new Series("Personas Individuales");

                            individuales.ChartType =
                                SeriesChartType.Column;

                            individuales.IsValueShownAsLabel =
                                true;

                            // Personas jurídicas
                            Series juridicas =
                                new Series("Personas Jurídicas");

                            juridicas.ChartType =
                                SeriesChartType.Column;

                            juridicas.IsValueShownAsLabel =
                                true;

                            while (lector.Read())
                            {
                                string nombreMes =
                                    lector["NombreMes"].ToString();

                                int anio =
                                    Convert.ToInt32(
                                        lector["Anio"]);

                                int totalIndividuales =
                                    Convert.ToInt32(
                                        lector["PersonasIndividuales"]);

                                int totalJuridicas =
                                    Convert.ToInt32(
                                        lector["PersonasJuridicas"]);

                                if (!string.IsNullOrEmpty(nombreMes))
                                {
                                    nombreMes =
                                        char.ToUpper(
                                            nombreMes[0]) +
                                        nombreMes.Substring(1);
                                }

                                string etiqueta =
                                    nombreMes + " " + anio;

                                individuales.Points.AddXY(
                                    etiqueta,
                                    totalIndividuales);

                                juridicas.Points.AddXY(
                                    etiqueta,
                                    totalJuridicas);
                            }

                            chartTipoCliente.Series.Add(
                                individuales);

                            chartTipoCliente.Series.Add(
                                juridicas);

                            chartTipoCliente.Titles.Add(
                                "Clientes por tipo y mes");

                            area.AxisX.LabelStyle.Angle = -45;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cargar la gráfica de clientes por tipo.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CERRAR
        // =========================================================

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog guardar = new SaveFileDialog())
                {
                    guardar.Filter = "Archivo Excel (*.xlsx)|*.xlsx";
                    guardar.Title = "Guardar reporte de clientes";
                    guardar.FileName =
                        "Reporte_Clientes_" +
                        DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                        ".xlsx";

                    if (guardar.ShowDialog() != DialogResult.OK)
                        return;

                    using (XLWorkbook libro = new XLWorkbook())
                    {
                        // =====================================================
                        // 1. OBTENER TOTALES
                        // =====================================================

                        int totalIndividuales = 0;
                        int totalJuridicas = 0;

                        using (SqlConnection conexion =
                            Conexion.ObtenerConexion())
                        {
                            conexion.Open();

                            string consulta =
                                @"SELECT
                            (SELECT COUNT(*) FROM PersonasIndividuales),
                            (SELECT COUNT(*) FROM PersonasJuridicas)";

                            using (SqlCommand comando =
                                new SqlCommand(consulta, conexion))
                            {
                                using (SqlDataReader lector =
                                    comando.ExecuteReader())
                                {
                                    if (lector.Read())
                                    {
                                        totalIndividuales =
                                            Convert.ToInt32(lector.GetValue(0));

                                        totalJuridicas =
                                            Convert.ToInt32(lector.GetValue(1));
                                    }
                                }
                            }
                        }

                        int totalClientes =
                            totalIndividuales + totalJuridicas;


                        // =====================================================
                        // 2. HOJA RESUMEN
                        // =====================================================

                        IXLWorksheet hojaResumen =
                            libro.Worksheets.Add("Resumen");

                        hojaResumen.Cell("A1").Value =
                            "REPORTE DE CLIENTES";

                        hojaResumen.Cell("A1").Style.Font.Bold = true;
                        hojaResumen.Cell("A1").Style.Font.FontSize = 20;

                        hojaResumen.Cell("A2").Value =
                            "Sistema de Gestión de Clientes";

                        hojaResumen.Cell("A2").Style.Font.Italic = true;

                        hojaResumen.Cell("A4").Value =
                            "Fecha de generación";

                        hojaResumen.Cell("B4").Value =
                            DateTime.Now;

                        hojaResumen.Cell("B4")
                            .Style.DateFormat.Format =
                            "dd/MM/yyyy HH:mm";

                        hojaResumen.Cell("A6").Value =
                            "RESUMEN MENSUAL";

                        hojaResumen.Cell("A6").Style.Font.Bold = true;
                        hojaResumen.Cell("A6").Style.Font.FontSize = 14;

                        hojaResumen.Cell("A8").Value =
                            "Clientes este mes";

                        hojaResumen.Cell("B8").Value =
                            lblMesActual.Text;

                        hojaResumen.Cell("A9").Value =
                            "Mes anterior";

                        hojaResumen.Cell("B9").Value =
                            lblMesAnterior.Text;

                        hojaResumen.Cell("A10").Value =
                            "Diferencia";

                        hojaResumen.Cell("B10").Value =
                            lblDiferencia.Text;

                        hojaResumen.Cell("A11").Value =
                            "Crecimiento";

                        hojaResumen.Cell("B11").Value =
                            lblCrecimiento.Text;

                        hojaResumen.Cell("A13").Value =
                            "TOTAL DE CLIENTES";

                        hojaResumen.Cell("A13").Style.Font.Bold = true;
                        hojaResumen.Cell("A13").Style.Font.FontSize = 14;

                        hojaResumen.Cell("A15").Value =
                            "Total clientes";

                        hojaResumen.Cell("B15").Value =
                            totalClientes;

                        hojaResumen.Cell("A16").Value =
                            "Personas individuales";

                        hojaResumen.Cell("B16").Value =
                            totalIndividuales;

                        hojaResumen.Cell("A17").Value =
                            "Personas jurídicas";

                        hojaResumen.Cell("B17").Value =
                            totalJuridicas;

                        // Bordes
                        hojaResumen.Range("A8:B11")
                            .Style.Border.OutsideBorder =
                            XLBorderStyleValues.Thin;

                        hojaResumen.Range("A15:B17")
                            .Style.Border.OutsideBorder =
                            XLBorderStyleValues.Thin;

                        // Negrita en etiquetas
                        hojaResumen.Range("A8:A11")
                            .Style.Font.Bold = true;

                        hojaResumen.Range("A15:A17")
                            .Style.Font.Bold = true;

                        hojaResumen.Columns()
                            .AdjustToContents();


                        // =====================================================
                        // 3. CLIENTES POR MES
                        // =====================================================

                        IXLWorksheet hojaMes =
                            libro.Worksheets.Add("Clientes por mes");

                        hojaMes.Cell("A1").Value = "Año";
                        hojaMes.Cell("B1").Value = "Mes";
                        hojaMes.Cell("C1").Value =
                            "Total Clientes";

                        int filaMes = 2;

                        using (SqlConnection conexion =
                            Conexion.ObtenerConexion())
                        {
                            conexion.Open();

                            using (SqlCommand comando =
                                new SqlCommand(
                                    "sp_Reportes_ClientesPorMes",
                                    conexion))
                            {
                                comando.CommandType =
                                    CommandType.StoredProcedure;

                                using (SqlDataReader lector =
                                    comando.ExecuteReader())
                                {
                                    while (lector.Read())
                                    {
                                        hojaMes.Cell(filaMes, 1).Value =
                                            Convert.ToInt32(
                                                lector["Anio"]);

                                        hojaMes.Cell(filaMes, 2).Value =
                                            lector["NombreMes"].ToString();

                                        hojaMes.Cell(filaMes, 3).Value =
                                            Convert.ToInt32(
                                                lector["TotalClientes"]);

                                        filaMes++;
                                    }
                                }
                            }
                        }

                        if (filaMes > 2)
                        {
                            var tablaMes =
                                hojaMes.Range(
                                    1, 1,
                                    filaMes - 1, 3)
                                .CreateTable();

                            tablaMes.Theme =
                                XLTableTheme.TableStyleMedium2;
                        }

                        hojaMes.SheetView.FreezeRows(1);
                        hojaMes.Columns()
                            .AdjustToContents();


                        // =====================================================
                        // 4. CLIENTES POR TIPO
                        // =====================================================

                        IXLWorksheet hojaTipo =
                            libro.Worksheets.Add("Clientes por tipo");

                        hojaTipo.Cell("A1").Value = "Año";
                        hojaTipo.Cell("B1").Value = "Mes";
                        hojaTipo.Cell("C1").Value =
                            "Personas Individuales";
                        hojaTipo.Cell("D1").Value =
                            "Personas Jurídicas";

                        int filaTipo = 2;

                        using (SqlConnection conexion =
                            Conexion.ObtenerConexion())
                        {
                            conexion.Open();

                            using (SqlCommand comando =
                                new SqlCommand(
                                    "sp_Reportes_ClientesPorTipoMes",
                                    conexion))
                            {
                                comando.CommandType =
                                    CommandType.StoredProcedure;

                                using (SqlDataReader lector =
                                    comando.ExecuteReader())
                                {
                                    while (lector.Read())
                                    {
                                        hojaTipo.Cell(filaTipo, 1).Value =
                                            Convert.ToInt32(
                                                lector["Anio"]);

                                        hojaTipo.Cell(filaTipo, 2).Value =
                                            lector["NombreMes"].ToString();

                                        hojaTipo.Cell(filaTipo, 3).Value =
                                            Convert.ToInt32(
                                                lector["PersonasIndividuales"]);

                                        hojaTipo.Cell(filaTipo, 4).Value =
                                            Convert.ToInt32(
                                                lector["PersonasJuridicas"]);

                                        filaTipo++;
                                    }
                                }
                            }
                        }

                        if (filaTipo > 2)
                        {
                            var tablaTipo =
                                hojaTipo.Range(
                                    1, 1,
                                    filaTipo - 1, 4)
                                .CreateTable();

                            tablaTipo.Theme =
                                XLTableTheme.TableStyleMedium4;
                        }

                        hojaTipo.SheetView.FreezeRows(1);
                        hojaTipo.Columns()
                            .AdjustToContents();


                        // =====================================================
                        // 5. PERSONAS INDIVIDUALES
                        // =====================================================

                        IXLWorksheet hojaIndividuales =
                            libro.Worksheets.Add(
                                "Personas individuales");

                        using (SqlConnection conexion =
                            Conexion.ObtenerConexion())
                        {
                            conexion.Open();

                            string consulta =
                                @"SELECT
                            IdPersona,
                            NombreCompleto,
                            LugarNacimiento,
                            FechaNacimiento,
                            Nacionalidad,
                            Genero,
                            EstadoCivil,
                            ProfesionOficio,
                            TipoIdentificacion,
                            NumeroIdentificacion,
                            DireccionResidencia,
                            CondicionMigratoria,
                            CalidadActua,
                            NumeroEscritura,
                            ReferenciaTransferencia,
                            Monto,
                            FechaRegistro
                          FROM PersonasIndividuales
                          ORDER BY IdPersona DESC";

                            using (SqlCommand comando =
                                new SqlCommand(
                                    consulta,
                                    conexion))
                            {
                                using (SqlDataReader lector =
                                    comando.ExecuteReader())
                                {
                                    // Encabezados
                                    for (int i = 0;
                                         i < lector.FieldCount;
                                         i++)
                                    {
                                        hojaIndividuales
                                            .Cell(1, i + 1)
                                            .Value =
                                            lector.GetName(i);
                                    }

                                    int fila = 2;

                                    while (lector.Read())
                                    {
                                        for (int i = 0;
                                             i < lector.FieldCount;
                                             i++)
                                        {
                                            object valor =
                                                lector.IsDBNull(i)
                                                ? null
                                                : lector.GetValue(i);

                                            hojaIndividuales
                                                .Cell(fila, i + 1)
                                                .Value =
                                                valor?.ToString() ?? "";
                                        }

                                        fila++;
                                    }

                                    if (fila > 2)
                                    {
                                        var tabla =
                                            hojaIndividuales.Range(
                                                1, 1,
                                                fila - 1,
                                                lector.FieldCount)
                                            .CreateTable();

                                        tabla.Theme =
                                            XLTableTheme.TableStyleMedium2;
                                    }
                                }
                            }
                        }

                        // Formato de fechas
                        hojaIndividuales.Column(4)
                            .Style.DateFormat.Format =
                            "dd/MM/yyyy";

                        hojaIndividuales.Column(17)
                            .Style.DateFormat.Format =
                            "dd/MM/yyyy";

                        // Formato de monto
                        hojaIndividuales.Column(16)
                            .Style.NumberFormat.Format =
                            "#,##0.00";

                        hojaIndividuales.SheetView
                            .FreezeRows(1);

                        hojaIndividuales.Columns()
                            .AdjustToContents();


                        // =====================================================
                        // 6. PERSONAS JURÍDICAS
                        // =====================================================

                        IXLWorksheet hojaJuridicas =
                            libro.Worksheets.Add(
                                "Personas jurídicas");

                        using (SqlConnection conexion =
                            Conexion.ObtenerConexion())
                        {
                            conexion.Open();

                            string consulta =
                                @"SELECT
                            PJ.IdPersonaJuridica,
                            PJ.TipoEntidad,
                            PJ.RazonSocial,
                            PJ.NombreComercial,
                            PJ.DatosInscripcionRegistral,
                            PJ.NIT,
                            PJ.FormaLugarConstitucion,
                            PJ.DomicilioFiscal,
                            RL.NombreCompleto AS RepresentanteLegal,
                            RL.LugarNacimiento AS LugarNacimientoRepresentante,
                            RL.FechaNacimiento AS FechaNacimientoRepresentante,
                            RL.Nacionalidad AS NacionalidadRepresentante,
                            RL.Genero AS GeneroRepresentante,
                            RL.EstadoCivil AS EstadoCivilRepresentante,
                            RL.ProfesionOficio AS ProfesionRepresentante,
                            RL.TipoIdentificacion AS TipoIdentificacionRepresentante,
                            RL.NumeroIdentificacion AS NumeroIdentificacionRepresentante,
                            PJ.FechaRegistro
                          FROM PersonasJuridicas PJ
                          LEFT JOIN RepresentantesLegales RL
                            ON PJ.IdPersonaJuridica =
                               RL.IdPersonaJuridica
                          ORDER BY
                            PJ.IdPersonaJuridica DESC";

                            using (SqlCommand comando =
                                new SqlCommand(
                                    consulta,
                                    conexion))
                            {
                                using (SqlDataReader lector =
                                    comando.ExecuteReader())
                                {
                                    // Encabezados
                                    for (int i = 0;
                                         i < lector.FieldCount;
                                         i++)
                                    {
                                        hojaJuridicas
                                            .Cell(1, i + 1)
                                            .Value =
                                            lector.GetName(i);
                                    }

                                    int fila = 2;

                                    while (lector.Read())
                                    {
                                        for (int i = 0;
                                             i < lector.FieldCount;
                                             i++)
                                        {
                                            object valor =
                                                lector.IsDBNull(i)
                                                ? null
                                                : lector.GetValue(i);

                                            hojaJuridicas
                                                .Cell(fila, i + 1)
                                                .Value =
                                                valor?.ToString() ?? "";
                                        }

                                        fila++;
                                    }

                                    if (fila > 2)
                                    {
                                        var tabla =
                                            hojaJuridicas.Range(
                                                1, 1,
                                                fila - 1,
                                                lector.FieldCount)
                                            .CreateTable();

                                        tabla.Theme =
                                            XLTableTheme.TableStyleMedium4;
                                    }
                                }
                            }
                        }

                        // Formato de fechas
                        hojaJuridicas.Column(11)
                            .Style.DateFormat.Format =
                            "dd/MM/yyyy";

                        hojaJuridicas.Column(18)
                            .Style.DateFormat.Format =
                            "dd/MM/yyyy";

                        hojaJuridicas.SheetView
                            .FreezeRows(1);

                        hojaJuridicas.Columns()
                            .AdjustToContents();


                        // =====================================================
                        // GUARDAR
                        // =====================================================

                        libro.SaveAs(guardar.FileName);
                    }

                    MessageBox.Show(
                        "El reporte se exportó correctamente.\n\n" +
                        "Se generaron 5 hojas con información " +
                        "estadística y todos los clientes.",
                        "Exportación completada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo exportar el reporte.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }

    }
}
