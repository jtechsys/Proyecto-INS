<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogeo.aspx.cs" Inherits="slnPryINS.Calidad.frmLogeo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Style/StyleINS.css" rel="stylesheet" />
    <title></title>

</head>

<body class="bg_login">

    <form id="frmLogeo" runat="server">
    
        <div class="login_center-mtc">
           <img src="../Images/Logo_MinisterioSalud.png">
        </div>

        <div class="login_center">

             <h2>Sistema de Seguimiento de Expedientes</h2>            

             <div class="editor-label">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
             </div>

             <div class="editor-field">
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
             </div>

             <div class="editor-label">
               <asp:Label ID="lblClave" runat="server" Text="Password"></asp:Label>
             </div>

             <div class="editor-field">
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox>
             </div>

             <asp:Button ID="btnAcceder" runat="server" Text="Acceder" CssClass="acceder" OnClick="btnAcceder_Click"/>
             <br />
             <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
    
        </div>

    </form>

</body>
</html>
