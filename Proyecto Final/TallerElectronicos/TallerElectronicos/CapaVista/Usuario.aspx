<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="TallerElectronicos.CapaVista.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Usuario</h2>

       <br />
   <div>

       <asp:GridView ID="GridViewUsuario" runat="server"></asp:GridView>

   </div>
    <br />
<br />
<div>
    <asp:Label ID="LusuarioID" runat="server" Text="ID Usuario"></asp:Label>
    <asp:TextBox ID="TusuarioID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LnombreUsuario" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="TnombreUsuario" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Lcorreo" runat="server" Text="Correo Electronico"></asp:Label>
    <asp:TextBox ID="Tcorreo" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Ltelefono" runat="server" Text="Telefono"></asp:Label>
    <asp:TextBox ID="Ttelefono" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="DropDownListUsuario" runat="server">
        <asp:ListItem>Inactivo</asp:ListItem>
        <asp:ListItem>Activo</asp:ListItem>
    </asp:DropDownList>

</div>
<div>
    <br />
    <div class="form-buttons">
    <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" />
    <asp:Button ID="BconsultarFiltro" runat="server" Text="Consultar" OnClick="BconsultarFiltro_Click" />
    <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
    <asp:Button ID="Bmodificar" runat="server" Text="Modificar" OnClick="Bmodificar_Click1" />
</div>
</asp:Content>
