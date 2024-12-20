<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Asignacion.aspx.cs" Inherits="TallerElectronicos.CapaVista.Asignacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


            <h2>Asignacion</h2>

       <br />
   <div>

       <asp:GridView ID="GridViewAsignacion" runat="server"></asp:GridView>

   </div>
    <br />
<br />
<div>
    <asp:Label ID="LAsignacionID" runat="server" Text="ID Asignacion"></asp:Label>
    <asp:TextBox ID="TAsignacionID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LReparacionID" runat="server" Text="ID Reparacion"></asp:Label>
    <asp:TextBox ID="TReparacionID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LTecnico" runat="server" Text="ID Tecnico"></asp:Label>
    <asp:TextBox ID="TTecnico" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LFechaAsignacion" runat="server" Text="Fecha de Asignacion"></asp:Label>
    <asp:TextBox ID="TFechaAsignacion" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="DropDownListAsignacion" runat="server">
        <asp:ListItem>Inactivo</asp:ListItem>
        <asp:ListItem>Activo</asp:ListItem>
    </asp:DropDownList>

</div>
<div>
    <br />
    <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" />
    <asp:Button ID="BconsultarFiltro" runat="server" Text="Consultar" OnClick="BconsultarFiltro_Click" />
    <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
    <asp:Button ID="Bmodificar" runat="server" Text="Modificar" OnClick="Bmodificar_Click1" />
</div>

</asp:Content>
