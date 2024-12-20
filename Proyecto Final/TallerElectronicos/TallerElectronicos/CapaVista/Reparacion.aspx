<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Reparacion.aspx.cs" Inherits="TallerElectronicos.CapaVista.Reparacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>Reparacion</h2>

       <br />
   <div>

       <asp:GridView ID="GridViewReparacion" runat="server"></asp:GridView>

   </div>
    <br />
<br />
<div>
    <asp:Label ID="LreparacionID" runat="server" Text="ID Reparacion"></asp:Label>
    <asp:TextBox ID="TreparacionID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LequipoID" runat="server" Text="ID Equipo"></asp:Label>
    <asp:TextBox ID="TequipoID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LfechaSolicitud" runat="server" Text="Fecha Solicitud"></asp:Label>
    <asp:TextBox ID="TfechaSolicitud" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="DropDownListReparacion" runat="server">
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
