<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Tecnico.aspx.cs" Inherits="TallerElectronicos.CapaVista.Tecnico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>Tecnicos</h2>

       <br />
   <div>

       <asp:GridView ID="GridViewTecnico" runat="server"></asp:GridView>

   </div>
    <br />
<br />
<div>
    <asp:Label ID="LtecnicoID" runat="server" Text="ID Tecnico"></asp:Label>
    <asp:TextBox ID="TtecnicoID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LnombreTecnico" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="TnombreTecnico" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Lespecialidad" runat="server" Text="Especialidad"></asp:Label>
    <asp:TextBox ID="Tespecialidad" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="DropDownListTecnico" runat="server">
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
