<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EstatisticaCancelamentosConsulta.aspx.cs" Inherits="EstatisticaCancelamentosConsulta" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="Label1" runat="server" Text="Consultas canceladas no ano:"></asp:Label>
        <asp:DropDownList ID="ddlAnoCancelamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnoCancelamento_SelectedIndexChanged">
            <asp:ListItem>Selecione um ano</asp:ListItem>
        </asp:DropDownList>
    <ajaxToolkit:BarChart ID="grafCancelamentosConsulta" runat="server" ChartHeight="200" ChartTitle="Cancelamentos por mês" Height="200px" ChartWidth="100%" Width="100%">
    </ajaxToolkit:BarChart>

        <div class="text-center"> <h4> <a href="MedicaoEstatisticas.aspx">Retornar</a> </h4> </div>
    </asp:Content>

