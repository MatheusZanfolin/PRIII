<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MedicaoEstatisticas.aspx.cs" Inherits="MedicaoEstatisticas" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        Consultas do médico
        <asp:DropDownList ID="ddlMedico" runat="server" DataSourceID="dsEstatistica" DataTextField="nome" DataValueField="crm" AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;</p>
    <ajaxToolkit:BarChart ID="grafConsultasMedico" runat="server" ChartHeight="100" ChartTitle="Consultas por mês" Height="386px">
    </ajaxToolkit:BarChart>
    <p>
        Atendimentos no dia
        <asp:TextBox ID="txtDiaConsultas" runat="server" AutoPostBack="True" TextMode="Date" ValidateRequestMode="Enabled"></asp:TextBox>
    </p>
    <ajaxToolkit:PieChart ID="grafAtendimentosEspecialidade" runat="server" ChartHeight="100" ChartTitle="Atendimentos por especialidade" Height="352px">
    </ajaxToolkit:PieChart>
    <p>
        </p>
    <ajaxToolkit:BarChart ID="grafConsultasPaciente" runat="server" ChartHeight="100" ChartTitle="Consultas por paciente" ChartType="Bar" Height="386px">
    </ajaxToolkit:BarChart>
    <p>
        Consultas canceladas no ano
        <asp:DropDownList ID="ddlAnoCancelamento" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <ajaxToolkit:BarChart ID="grafCancelamentosConsulta" runat="server" ChartHeight="100" ChartTitle="Cancelamentos por mês" Height="386px">
    </ajaxToolkit:BarChart>
    <p>
        <asp:SqlDataSource ID="dsEstatistica" runat="server" ConnectionString="<%$ ConnectionStrings:PRII16191ConnectionString %>" SelectCommand="SELECT [crm], [nome] FROM [Medico]"></asp:SqlDataSource>
&nbsp;</p>
</asp:Content>

