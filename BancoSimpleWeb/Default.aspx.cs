using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace BancoSimpleWeb
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarCuenta();
                ActualizarUI();
            }
        }

        private void InicializarCuenta()
        {
            Session["Saldo"]     = 1000m;
            Session["Titular"]   = "Juan Pérez";
            Session["Historial"] = new List<Movimiento>
            {
                new Movimiento("INICIO", 0, 1000m, "Saldo inicial de cuenta")
            };
        }

        protected void btnOperar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto) || monto <= 0)
            {
                MostrarMensaje("❌ Ingresa un monto válido mayor a 0.", "error");
                return;
            }

            decimal saldo = (decimal)Session["Saldo"];
            var historial = (List<Movimiento>)Session["Historial"];
            string tipo = ddlOperacion.SelectedValue;

            if (tipo == "deposito")
            {
                saldo += monto;
                historial.Add(new Movimiento("DEPÓSITO", monto, saldo, ""));
                MostrarMensaje($"✅ Depósito de ${monto:F2} realizado con éxito.", "exito");
            }
            else
            {
                if (monto > saldo)
                {
                    MostrarMensaje("❌ Saldo insuficiente para realizar el retiro.", "error");
                    return;
                }
                saldo -= monto;
                historial.Add(new Movimiento("RETIRO", monto, saldo, ""));
                MostrarMensaje($"✅ Retiro de ${monto:F2} realizado con éxito.", "exito");
            }

            Session["Saldo"]     = saldo;
            Session["Historial"] = historial;
            txtMonto.Text        = "";
            ActualizarUI();
        }

        private void ActualizarUI()
        {
            lblTitular.Text = Session["Titular"].ToString();
            lblSaldo.Text   = $"${(decimal)Session["Saldo"]:F2}";

            var historial = (List<Movimiento>)Session["Historial"];

            DataTable dt = new DataTable();
            dt.Columns.Add("#");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Monto");
            dt.Columns.Add("Saldo Resultante");
            dt.Columns.Add("Descripción");

            for (int i = 0; i < historial.Count; i++)
            {
                var m = historial[i];
                dt.Rows.Add(
                    i + 1,
                    m.Tipo,
                    m.Monto > 0 ? $"${m.Monto:F2}" : "-",
                    $"${m.SaldoResultante:F2}",
                    m.Descripcion
                );
            }

            gvHistorial.DataSource = dt;
            gvHistorial.DataBind();
        }

        private void MostrarMensaje(string texto, string clase)
        {
            lblMensaje.Text     = texto;
            lblMensaje.CssClass = $"mensaje {clase}";
        }
    }

    // Clase auxiliar para los movimientos
    public class Movimiento
    {
        public string  Tipo             { get; set; }
        public decimal Monto            { get; set; }
        public decimal SaldoResultante  { get; set; }
        public string  Descripcion      { get; set; }

        public Movimiento(string tipo, decimal monto, decimal saldo, string desc)
        {
            Tipo            = tipo;
            Monto           = monto;
            SaldoResultante = saldo;
            Descripcion     = desc;
        }
    }
}
