 <%@ Page Language="C#"  MasterPageFile="~/Site.master"AutoEventWireup="true" CodeFile="ConsultasSolicitadas.aspx.cs" Inherits="ConsultasSolicitadas" %>

<asp:Content ID="ContentAgendarConsultaSA" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="divErro">
    
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
        <h1>Consultas Solicitadas</h1>
        <asp:Label ID="lblErro" runat="server" ForeColor="Red"></asp:Label>
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Selecione a consulta:"></asp:Label></td>
                <td>
        
        <asp:ListBox ID="lsbConsulta" runat="server" OnSelectedIndexChanged="lsbConsulta_SelectedIndexChanged"></asp:ListBox>
      
            <asp:RequiredFieldValidator ID="rfvMedico3" runat="server" ErrorMessage="Selecione uma consulta!" ValidationGroup="lsbMedico" ControlToValidate="lsbConsulta"></asp:RequiredFieldValidator>
      
                </td>
            </tr>
            <tr>
                <td><h4>Dados da consulta:</h4></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Médico:"></asp:Label>
                </td>
                <td>
            <asp:ListBox ID="lsbMedico" runat="server" OnSelectedIndexChanged="lsbMedico_SelectedIndexChanged" Visible="False" AutoPostBack="True"></asp:ListBox>
      
            <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ErrorMessage="Selecione uma médico!" ValidationGroup="lsbMedico" ControlToValidate="lsbMedico"></asp:RequiredFieldValidator>
      
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Usuário(Paciente):"></asp:Label>
                </td>
                <td><asp:TextBox ID="txtPaciente" runat="server" MaxLength="30" ReadOnly="True"></asp:TextBox>
          

                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtPaciente" ErrorMessage="Selecione um usuário!"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Dia:"></asp:Label>
                    <%--<asp:Calendar> ID="cldData" runat="server" OnSelectionChanged="cldData_SelectionChanged"></asp:Calendar>--%>
                </td>
                <td>
          

                    <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
          

            <asp:RequiredFieldValidator ID="rfvData" runat="server" ErrorMessage="Selecione um dia!" ControlToValidate="txtData"></asp:RequiredFieldValidator>
          

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Horários disponíveis:"></asp:Label>
                </td>
                <td>
            <asp:ListBox ID="lsbHorarios" runat="server" Visible="False"></asp:ListBox>
          

            <asp:RequiredFieldValidator ID="rfvMedico2" runat="server" ErrorMessage="Selecione um horário!" ValidationGroup="lsbHorarios" ControlToValidate="lsbHorarios"></asp:RequiredFieldValidator>
          

                </td>
            </tr>
        </table>
        <p aria-busy="True">
          

            <asp:RadioButton ID="rbHora" runat="server" Text="Consulta dura 1 hora(se este campo não estiver preenchido, dura meia hora)" Visible="False" />
          

    </p>
        <p>
          

            <asp:Button ID="btnAgendar" runat="server" OnClick="btnAgendar_Click" Text="Alterar Consulta" />
          

    </p>
        <p>
        
    </p>


        <br />
    


    </div>
   
    </asp:Content>