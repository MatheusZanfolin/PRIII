<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MedicaoEstatisticas.aspx.cs" Inherits="MedicaoEstatisticas" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Relatórios estatísticos</h2>

    <asp:Menu ID="Menu1" runat="server" BackColor="White">
        <Items>
            <asp:MenuItem NavigateUrl="~/EstatisticaAtendimentosEspecialidade.aspx" Text="Atendimentos por especialidade" Value="Atendimentos por especialidade"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/EstatisticaCancelamentosConsulta.aspx" Text="Cancelamentos de consultas" Value="Cancelamentos de consultas"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/EstatisticaConsultasMedico.aspx" Text="Consultas dos médicos" Value="Consultas dos médicos"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/EstatisticaConsultasPaciente.aspx" Text="Consultas por paciente" Value="Consultas por paciente"></asp:MenuItem>
            <asp:MenuItem NavigateUrl="~/Principal.aspx" Text="Retornar"></asp:MenuItem>
        </Items>
        <StaticMenuItemStyle VerticalPadding="5px" />
    </asp:Menu>
    <p>
        &nbsp;</p>
</asp:Content>

