<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EstatisticaConsultasPaciente.aspx.cs" Inherits="EstatisticaConsultasPaciente" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">    
    <br />
    <ajaxToolkit:BarChart ID="grafConsultasPaciente" runat="server" ChartHeight="" ChartTitle="Consultas por paciente" ChartWidth="100%" Width="100%">
    </ajaxToolkit:BarChart>
    <br />

    <div class="text-center"> <h4> <a href="MedicaoEstatisticas.aspx">Retornar</a> </h4> </div>
</asp:Content>

