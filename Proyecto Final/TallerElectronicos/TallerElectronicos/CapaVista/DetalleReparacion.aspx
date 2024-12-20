<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="DetalleReparacion.aspx.cs" Inherits="TallerElectronicos.CapaVista.DetalleReparacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <h2>Detalle de Reparacion</h2>

       <br />
   <div>

       <asp:GridView ID="GridViewDetalleReparacion" runat="server"></asp:GridView>

   </div>
    <br />
<br />
<div>
    <asp:Label ID="LDetalleID" runat="server" Text="ID Detalle"></asp:Label>
    <asp:TextBox ID="TDetalleID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LReparacionID" runat="server" Text=" ID Reparacion"></asp:Label>
    <asp:TextBox ID="TReparacionID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LDescripcion" runat="server" Text="Descripcion"></asp:Label>
    <asp:TextBox ID="TDescripcion" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LFechaInicio" runat="server" Text="Fecha Inicio"></asp:Label>
    <asp:TextBox ID="TFechaInicio" runat="server"></asp:TextBox>
    <br />
     <asp:Label ID="LfechaFin" runat="server" Text="Fecha Fin"></asp:Label>
    <asp:TextBox ID="TfechaFin" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="DropDownListDetalleReparacion" runat="server">
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
