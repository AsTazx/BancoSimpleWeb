<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BancoSimpleWeb.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Banco Simple</title>
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="header">
                <h1>🏦 Banco Simple</h1>
                <p>Sistema de Gestión de Cuenta</p>
            </div>

            <!-- Tarjeta de saldo -->
            <div class="card saldo-card">
                <h2>👤 <asp:Label ID="lblTitular" runat="server" Text="Cliente" /></h2>
                <p class="saldo-label">Saldo disponible</p>
                <p class="saldo-monto">
                    <asp:Label ID="lblSaldo" runat="server" Text="$0.00" />
                </p>
            </div>

            <!-- Tarjeta de operación -->
            <div class="card">
                <h3>⚙️ Realizar Operación</h3>
                <div class="form-group">
                    <label>Tipo de operación:</label>
                    <asp:DropDownList ID="ddlOperacion" runat="server" CssClass="input-control">
                        <asp:ListItem Value="deposito">💰 Depósito</asp:ListItem>
                        <asp:ListItem Value="retiro">💸 Retiro</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Monto:</label>
                    <asp:TextBox ID="txtMonto" runat="server" CssClass="input-control" placeholder="Ej: 500.00" />
                </div>
                <asp:Button ID="btnOperar" runat="server" Text="Ejecutar Operación"
                    CssClass="btn-primary" OnClick="btnOperar_Click" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />
            </div>

            <!-- Tarjeta de historial -->
            <div class="card">
                <h3>📋 Historial de Movimientos</h3>
                <asp:GridView ID="gvHistorial" runat="server"
                    CssClass="tabla"
                    AutoGenerateColumns="true"
                    EmptyDataText="No hay movimientos aún." />
            </div>

        </div>
    </form>
</body>
</html>
