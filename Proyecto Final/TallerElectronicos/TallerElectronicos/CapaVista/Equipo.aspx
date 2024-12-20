<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Equipo.aspx.cs" Inherits="TallerElectronicos.CapaVista.Equipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>Equipos</h2>

       <br />
   <div>

       <asp:GridView ID="GridViewEquipos" runat="server"></asp:GridView>

   </div>
    <br />
<br />
<div>
    <asp:Label ID="LequipoID" runat="server" Text="ID Equipo"></asp:Label>
    <asp:TextBox ID="TequipoID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LtipoEquipo" runat="server" Text="Tipo Equipo"></asp:Label>
    <asp:TextBox ID="TtipoEquipo" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Lmodelo" runat="server" Text="Modelo"></asp:Label>
    <asp:TextBox ID="Tmodelo" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LusuarioID" runat="server" Text="ID Usuario"></asp:Label>
    <asp:TextBox ID="TUsuarioID" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="DropDownListEquipo" runat="server">
        <asp:ListItem>Inactivo</asp:ListItem>
        <asp:ListItem>Activo</asp:ListItem>
    </asp:DropDownList>

</div>
<div>
    <br />
    <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" style="height: 29px" />
    <asp:Button ID="BconsultarFiltro" runat="server" Text="Consultar" OnClick="BconsultarFiltro_Click" />
    <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
    <asp:Button ID="Bmodificar" runat="server" Text="Modificar" OnClick="Bmodificar_Click1" />
</div>
</asp:Content>
