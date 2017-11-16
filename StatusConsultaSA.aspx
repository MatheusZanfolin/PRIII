<%@ Page Language="C#"  MasterPageFile="~/Site.master"AutoEventWireup="true" CodeFile="StatusConsultaSA.aspx.cs" Inherits="StatusConsultaSA" %>

<asp:Content ID="ContentAgendarConsultaSA" ContentPlaceHolderID="MainContent" Runat="Server">

    
    
        <asp:Menu ID="MenuSA" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Cadastrar Médico" Value="Cadastrar Médico" NavigateUrl="~/CadMedSa.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Paciente" Value="Paciente">
                    <asp:MenuItem Text="Cadastrar" Value="Cadastrar" NavigateUrl="~/CadPacienteSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Mandar SMS" Value="Mandar SMS" NavigateUrl="~/MandarSMSPacSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Visualizar avaliações" Value="Visualizar avaliações" NavigateUrl="~/VerAvaliacaoSA.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Cadastrar Especialidade" Value="Cadastrar Especialidade" NavigateUrl="~/especialidadeSA.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Consulta" Value="Consulta">
                    <asp:MenuItem Text="Agendar" Value="Agendar" NavigateUrl="~/AgendarConsultaSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Visualizar" Value="Visualizar" NavigateUrl="~/StatusConsultaSA.aspx"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/ConsultasSolicitadas.aspx" Text="Solicitadas" Value="Solicitadas"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Estatísticas" Value="Estatísticas" NavigateUrl="~/MedicaoEstatisticas.aspx"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/CadSA.aspx" Text="Cadastrar outro Secretário" Value="Cadastrar outro Secretário"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/logonSA.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
            </Items>
        </asp:Menu>
        
            <h1>Ver Status das Consultas
            </h1>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
        <br />

        <asp:Label ID="Label1" runat="server" Text="Selecione o dia que deseja visualizar as consultas:"></asp:Label>

        <p>
            <asp:TextBox ID="txtData" runat="server" TextMode="Date"></asp:TextBox>
            &nbsp;
            <asp:Button ID="btnGerarRel" runat="server" OnClick="btnGerarRel_Click" Text="Gerar Relatório" />
        &nbsp;<asp:RequiredFieldValidator ID="rfvDia" runat="server" ControlToValidate="txtData" ErrorMessage="Digite o dia das consultas!"></asp:RequiredFieldValidator>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Table ID="tabDados" runat="server" CellPadding="5">
            </asp:Table>
        </p>
       <p>
            <asp:Label ID="lblNum" runat="server" Text="Número de Consultas:" Visible="False"></asp:Label>
            <asp:Label ID="lblConsulta" runat="server" Visible="False"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
       <p>
            <asp:Label ID="Label3" runat="server" Text="Código da consulta para cancelar:"></asp:Label>
            <asp:TextBox ID="txtAlterarConsulta" runat="server" TextMode="Number"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Button ID="btnCancelarConsulta" runat="server" OnClick="btnAlterarConsulta_Click" Text="Cancelar Consulta" />
        &nbsp;<asp:Label ID="lblSemCodConsulta" runat="server" Text="Digite o código da consulta!" Visible="False"></asp:Label>
        </p>
        <p>
        
            <asp:Button ID="btnRedefinir" runat="server" OnClick="btnRedefinir_Click" Text="Escolher outro dia" OnClientClick="return false;"/>
        
    </p>


    
   
    </asp:Content>
