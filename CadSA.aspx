<%@ Page Language="C#"  MasterPageFile="~/Site.master"AutoEventWireup="true" CodeFile="CadSA.aspx.cs" Inherits="CadSA" %>


<asp:Content ID="ContentCadSA" ContentPlaceHolderID="MainContent" Runat="Server">

    
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
        <h1>
        Cadastro do secretário
        </h1>
    <p aria-busy="False" style="color: #FF0000; font-family: &quot;Times New Roman&quot;, Times, serif; font-size: 20px;">
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True"></asp:Label>
        </p>
    <p>
        Usuário:&nbsp;<asp:TextBox ID="txtUsuario" runat="server" CssClass="txtVerde" MaxLength="30"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Digite o usuário do secretário!"></asp:RequiredFieldValidator>
    </p>
        <p>
            Senha:&nbsp;
&nbsp;<asp:TextBox ID="txtSenha" runat="server" CssClass="txtVerde" MaxLength="30" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Digite a senha do secretário!"></asp:RequiredFieldValidator>
    </p>
        <p>
            <asp:Button ID="btnCadSA" runat="server" Text="Cadastrar" OnClick="btnCadSA_Click"  />
    </p>


    
   
    </asp:Content>