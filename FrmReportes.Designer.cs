namespace SistemaClientes
{
    partial class FrmReportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            chartClientes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            lblSubtitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCardMesActual = new Guna.UI2.WinForms.Guna2Panel();
            lblMesActual = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTituloMesActual = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCardMesAnterior = new Guna.UI2.WinForms.Guna2Panel();
            lblMesAnterior = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTituloMesAnterior = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCardDiferencia = new Guna.UI2.WinForms.Guna2Panel();
            lblDiferencia = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTituloDiferencia = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCardCrecimiento = new Guna.UI2.WinForms.Guna2Panel();
            lblCrecimiento = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTituloCrecimiento = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCerrar = new Guna.UI2.WinForms.Guna2Button();
            chartTipoCliente = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btnExportarExcel = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)chartClientes).BeginInit();
            pnlTop.SuspendLayout();
            pnlCardMesActual.SuspendLayout();
            pnlCardMesAnterior.SuspendLayout();
            pnlCardDiferencia.SuspendLayout();
            pnlCardCrecimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartTipoCliente).BeginInit();
            SuspendLayout();
            // 
            // chartClientes
            // 
            chartClientes.BorderlineWidth = 0;
            chartArea1.Name = "ChartArea1";
            chartClientes.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartClientes.Legends.Add(legend1);
            chartClientes.Location = new Point(25, 250);
            chartClientes.Name = "chartClientes";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartClientes.Series.Add(series1);
            chartClientes.Size = new Size(535, 330);
            chartClientes.TabIndex = 0;
            chartClientes.Text = "Grafica";
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblSubtitulo);
            pnlTop.Controls.Add(lblTitulo);
            pnlTop.CustomizableEdges = customizableEdges1;
            pnlTop.FillColor = Color.FromArgb(31, 41, 55);
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlTop.Size = new Size(1150, 70);
            pnlTop.TabIndex = 1;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.BackColor = Color.Transparent;
            lblSubtitulo.ForeColor = Color.White;
            lblSubtitulo.Location = new Point(27, 42);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(204, 17);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Análisis y comportamiento de clientes";
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(25, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(313, 34);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "REPORTES Y ESTADÍSTICAS";
            // 
            // pnlCardMesActual
            // 
            pnlCardMesActual.BorderRadius = 12;
            pnlCardMesActual.Controls.Add(lblMesActual);
            pnlCardMesActual.Controls.Add(lblTituloMesActual);
            pnlCardMesActual.CustomizableEdges = customizableEdges3;
            pnlCardMesActual.FillColor = Color.White;
            pnlCardMesActual.Location = new Point(25, 95);
            pnlCardMesActual.Name = "pnlCardMesActual";
            pnlCardMesActual.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlCardMesActual.Size = new Size(250, 130);
            pnlCardMesActual.TabIndex = 2;
            // 
            // lblMesActual
            // 
            lblMesActual.BackColor = Color.Transparent;
            lblMesActual.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMesActual.Location = new Point(20, 48);
            lblMesActual.Name = "lblMesActual";
            lblMesActual.Size = new Size(24, 52);
            lblMesActual.TabIndex = 1;
            lblMesActual.Text = "0";
            // 
            // lblTituloMesActual
            // 
            lblTituloMesActual.BackColor = Color.Transparent;
            lblTituloMesActual.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloMesActual.Location = new Point(20, 18);
            lblTituloMesActual.Name = "lblTituloMesActual";
            lblTituloMesActual.Size = new Size(111, 17);
            lblTituloMesActual.TabIndex = 0;
            lblTituloMesActual.Text = "CLIENTES ESTE MES";
            // 
            // pnlCardMesAnterior
            // 
            pnlCardMesAnterior.BorderRadius = 12;
            pnlCardMesAnterior.Controls.Add(lblMesAnterior);
            pnlCardMesAnterior.Controls.Add(lblTituloMesAnterior);
            pnlCardMesAnterior.CustomizableEdges = customizableEdges5;
            pnlCardMesAnterior.FillColor = Color.White;
            pnlCardMesAnterior.Location = new Point(295, 95);
            pnlCardMesAnterior.Name = "pnlCardMesAnterior";
            pnlCardMesAnterior.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlCardMesAnterior.Size = new Size(250, 130);
            pnlCardMesAnterior.TabIndex = 3;
            // 
            // lblMesAnterior
            // 
            lblMesAnterior.BackColor = Color.Transparent;
            lblMesAnterior.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMesAnterior.Location = new Point(20, 48);
            lblMesAnterior.Name = "lblMesAnterior";
            lblMesAnterior.Size = new Size(24, 52);
            lblMesAnterior.TabIndex = 1;
            lblMesAnterior.Text = "0";
            // 
            // lblTituloMesAnterior
            // 
            lblTituloMesAnterior.BackColor = Color.Transparent;
            lblTituloMesAnterior.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloMesAnterior.Location = new Point(20, 18);
            lblTituloMesAnterior.Name = "lblTituloMesAnterior";
            lblTituloMesAnterior.Size = new Size(89, 17);
            lblTituloMesAnterior.TabIndex = 0;
            lblTituloMesAnterior.Text = "MES ANTERIOR";
            // 
            // pnlCardDiferencia
            // 
            pnlCardDiferencia.BorderRadius = 12;
            pnlCardDiferencia.Controls.Add(lblDiferencia);
            pnlCardDiferencia.Controls.Add(lblTituloDiferencia);
            pnlCardDiferencia.CustomizableEdges = customizableEdges7;
            pnlCardDiferencia.FillColor = Color.White;
            pnlCardDiferencia.Location = new Point(595, 95);
            pnlCardDiferencia.Name = "pnlCardDiferencia";
            pnlCardDiferencia.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlCardDiferencia.Size = new Size(250, 130);
            pnlCardDiferencia.TabIndex = 4;
            // 
            // lblDiferencia
            // 
            lblDiferencia.BackColor = Color.Transparent;
            lblDiferencia.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDiferencia.Location = new Point(20, 48);
            lblDiferencia.Name = "lblDiferencia";
            lblDiferencia.Size = new Size(24, 52);
            lblDiferencia.TabIndex = 1;
            lblDiferencia.Text = "0";
            // 
            // lblTituloDiferencia
            // 
            lblTituloDiferencia.BackColor = Color.Transparent;
            lblTituloDiferencia.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloDiferencia.Location = new Point(20, 18);
            lblTituloDiferencia.Name = "lblTituloDiferencia";
            lblTituloDiferencia.Size = new Size(70, 17);
            lblTituloDiferencia.TabIndex = 0;
            lblTituloDiferencia.Text = "DIFERENCIA";
            // 
            // pnlCardCrecimiento
            // 
            pnlCardCrecimiento.BorderRadius = 12;
            pnlCardCrecimiento.Controls.Add(lblCrecimiento);
            pnlCardCrecimiento.Controls.Add(lblTituloCrecimiento);
            pnlCardCrecimiento.CustomizableEdges = customizableEdges9;
            pnlCardCrecimiento.FillColor = Color.White;
            pnlCardCrecimiento.Location = new Point(860, 95);
            pnlCardCrecimiento.Name = "pnlCardCrecimiento";
            pnlCardCrecimiento.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlCardCrecimiento.Size = new Size(262, 130);
            pnlCardCrecimiento.TabIndex = 5;
            // 
            // lblCrecimiento
            // 
            lblCrecimiento.BackColor = Color.Transparent;
            lblCrecimiento.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCrecimiento.Location = new Point(20, 48);
            lblCrecimiento.Name = "lblCrecimiento";
            lblCrecimiento.Size = new Size(24, 52);
            lblCrecimiento.TabIndex = 1;
            lblCrecimiento.Text = "0";
            // 
            // lblTituloCrecimiento
            // 
            lblTituloCrecimiento.BackColor = Color.Transparent;
            lblTituloCrecimiento.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloCrecimiento.Location = new Point(20, 18);
            lblTituloCrecimiento.Name = "lblTituloCrecimiento";
            lblTituloCrecimiento.Size = new Size(81, 17);
            lblTituloCrecimiento.TabIndex = 0;
            lblTituloCrecimiento.Text = "CRECIMIENTO";
            // 
            // btnCerrar
            // 
            btnCerrar.BorderRadius = 8;
            btnCerrar.CustomizableEdges = customizableEdges11;
            btnCerrar.DisabledState.BorderColor = Color.DarkGray;
            btnCerrar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCerrar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCerrar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCerrar.FillColor = Color.FromArgb(107, 114, 128);
            btnCerrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(945, 604);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCerrar.Size = new Size(120, 40);
            btnCerrar.TabIndex = 6;
            btnCerrar.Text = "CERRAR";
            btnCerrar.Click += btnCerrar_Click_1;
            // 
            // chartTipoCliente
            // 
            chartTipoCliente.BorderlineWidth = 0;
            chartArea2.Name = "ChartArea1";
            chartTipoCliente.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartTipoCliente.Legends.Add(legend2);
            chartTipoCliente.Location = new Point(590, 250);
            chartTipoCliente.Name = "chartTipoCliente";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartTipoCliente.Series.Add(series2);
            chartTipoCliente.Size = new Size(535, 330);
            chartTipoCliente.TabIndex = 7;
            chartTipoCliente.Text = "Grafica";
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.BorderRadius = 8;
            btnExportarExcel.CustomizableEdges = customizableEdges13;
            btnExportarExcel.DisabledState.BorderColor = Color.DarkGray;
            btnExportarExcel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExportarExcel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExportarExcel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExportarExcel.FillColor = Color.SeaGreen;
            btnExportarExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportarExcel.ForeColor = Color.White;
            btnExportarExcel.Location = new Point(797, 604);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnExportarExcel.Size = new Size(120, 40);
            btnExportarExcel.TabIndex = 8;
            btnExportarExcel.Text = "EXPORTAR A EXCEL";
            btnExportarExcel.Click += btnExportarExcel_Click;
            // 
            // FrmReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(1134, 661);
            Controls.Add(btnExportarExcel);
            Controls.Add(chartTipoCliente);
            Controls.Add(btnCerrar);
            Controls.Add(pnlCardCrecimiento);
            Controls.Add(pnlCardDiferencia);
            Controls.Add(pnlCardMesAnterior);
            Controls.Add(pnlCardMesActual);
            Controls.Add(pnlTop);
            Controls.Add(chartClientes);
            Name = "FrmReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes y Estadísticas";
            Load += FrmReportes_Load;
            ((System.ComponentModel.ISupportInitialize)chartClientes).EndInit();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlCardMesActual.ResumeLayout(false);
            pnlCardMesActual.PerformLayout();
            pnlCardMesAnterior.ResumeLayout(false);
            pnlCardMesAnterior.PerformLayout();
            pnlCardDiferencia.ResumeLayout(false);
            pnlCardDiferencia.PerformLayout();
            pnlCardCrecimiento.ResumeLayout(false);
            pnlCardCrecimiento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartTipoCliente).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartClientes;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Panel pnlCardMesActual;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMesActual;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloMesActual;
        private Guna.UI2.WinForms.Guna2Panel pnlCardMesAnterior;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMesAnterior;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloMesAnterior;
        private Guna.UI2.WinForms.Guna2Panel pnlCardDiferencia;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiferencia;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloDiferencia;
        private Guna.UI2.WinForms.Guna2Panel pnlCardCrecimiento;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCrecimiento;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloCrecimiento;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTipoCliente;
        private Guna.UI2.WinForms.Guna2Button btnExportarExcel;
    }
}