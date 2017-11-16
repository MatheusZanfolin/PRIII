<%@ Page Language="C#"  MasterPageFile="~/Site.master"AutoEventWireup="true" CodeFile="AgendarConsultaSA.aspx.cs" Inherits="AgendarConsulta" %>

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
        <h1>Agendar Consulta</h1>
        <asp:Label ID="lblErro" runat="server" ForeColor="Red"></asp:Label>

        <table>
            <tr>
                <td> 
      
        <asp:Label ID="Label1" runat="server" Text="Especialidade: "></asp:Label>
                

                <td>
        <asp:ListBox ID="lsbEspec" runat="server" OnSelectedIndexChanged="lsbEspec_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True">Escolha a especialidade</asp:ListItem>
        </asp:ListBox>
      
        <asp:RequiredFieldValidator ID="rfvEspec" runat="server" ErrorMessage="Selecione uma especialidade" ValidationGroup="lsbEspec" ControlToValidate="lsbEspec"></asp:RequiredFieldValidator>
      
                </td>
            </tr>
            <tr>
                <td>
      
            <asp:Label ID="Label2" runat="server" Text="Médico:"></asp:Label>
                </td>
                <td>
            <asp:ListBox ID="lsbMedico" runat="server" OnSelectedIndexChanged="lsbMedico_SelectedIndexChanged" Visible="False" AutoPostBack="True">
                <asp:ListItem>Escolha o médico</asp:ListItem>
            </asp:ListBox>
      
            <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ErrorMessage="Selecione uma médico!" ValidationGroup="lsbMedico" ControlToValidate="lsbMedico"></asp:RequiredFieldValidator>
      
                </td>
            </tr>       
            <tr>
                <td> <asp:Label ID="Label3" runat="server" Text="Usuário(Paciente):"></asp:Label>


                </td></td>
                <td>
          

           
            <asp:TextBox ID="txtPaciente" runat="server" Visible="False" MaxLength="30" AutoPostBack="True"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPaciente" runat="server" ErrorMessage="Digite o usuário do paciente!" ValidationGroup="txtPaciente" ControlToValidate="txtPaciente"></asp:RequiredFieldValidator>
          

                </td>
            </tr>
            <tr>
                <td>
      
                    <asp:Label ID="Label4" runat="server" Text="Dia:"></asp:Label>
                </td>
                <td>
          

            
            <asp:TextBox ID="txtData" runat="server" OnTextChanged="txtData_TextChanged" TextMode="Date" AutoPostBack="True"></asp:TextBox>
          

            <asp:RequiredFieldValidator ID="rfvData" runat="server" ErrorMessage="Selecione um dia!" ValidationGroup="txtData" ControlToValidate="txtData"></asp:RequiredFieldValidator>
          

                </td>
            </tr>
            <tr>
                <td>
      
                    <asp:Label ID="Label5" runat="server" Text="Horários disponíveis:"></asp:Label>
                </td>
                <td>
                    <asp:ListBox ID="lsbHorarios" runat="server" Visible="False" AutoPostBack="True" Height="148px" OnSelectedIndexChanged="lsbHorarios_SelectedIndexChanged">
                        <asp:ListItem>Escolha o horário</asp:ListItem>
                    </asp:ListBox>

                    <asp:RequiredFieldValidator ID="rfvMedico2" runat="server" ErrorMessage="Selecione um horário!" ValidationGroup="lsbHorarios" ControlToValidate="lsbHorarios"></asp:RequiredFieldValidator>
            </tr>
        </table>

        <br />
        <p aria-busy="True">
          

            <asp:RadioButton ID="rbHora" runat="server" Text="Consulta dura 1 hora(se este campo não estiver preenchido, dura meia hora)" Visible="False" AutoPostBack="True" />
          

    </p>
        <p>
          

            <asp:Button ID="btnAgendar" runat="server" OnClick="btnAgendar_Click" Text="Agendar" />
          

    </p>
        <p>
        
    </p>


    </div>
   
    </asp:Content>