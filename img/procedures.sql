                   
create proc DiagnosticoC_sp 
@codConsulta int,
@diagnostico text
As
BEGIN
update Consulta set diagnostico = @diagnostico where codConsulta = @codConsulta
END

create proc CadastrarE_sp
@nomeEspecialidade varchar(30)
As
Begin
insert into Especialidade values(@nomeEspecialidade)
End

create proc MandarSMS_sp
@usuario varchar(30)
As
Begin
declare @celular char(11)
select @celular = celular from Paciente where usuario = @usuario  
End

create proc CadastrarM_sp
@crm int,
@nome varchar(50),
@dataNascimento date,
@email varchar(30),
@celular char(11),
@telefone varchar(11),
@foto image,
@senha varchar(30),
@codEspecialidade int
As
Begin
insert into Medico values(@crm,@nome,@dataNascimento,@email,@celular,@telefone,@foto,@senha,@codEspecialidade)
End

create proc statusC_sp
@codStatus int,
@codConsulta int
As
Begin
set @codStatus = (select codstatus from Consulta where codConsulta = @codConsulta)
select nomeStatus from StatusConsulta where codStatus = @codStatus 
End

create proc VerA_sp
@codConsulta int
AS
begin
declare @codAvaliacao int
set @codAvaliacao = (select codAvaliacao from Consulta where codConsulta = @codConsulta)
select * from AvaliacaoConsulta where codAvaliacao = @codAvaliacao
end

create proc ConsultarA_sp
@codConsulta int
as
begin
declare @codAvaliacao int
declare @codStatus int
declare @minutos int
set @codStatus = (select codstatus from Consulta where codConsulta = @codConsulta)
set @codAvaliacao = (select codAvaliacao from Consulta where codConsulta = @codConsulta)
if (select meiahora from Consulta where codConsulta = @codConsulta) = 0 set @minutos = 30 else set @minutos = 60
select a.*,sc.nomeStatus,c.diagnostico,dateadd(minute, @minutos ,c.datahoraconsulta )  from AvaliacaoConsulta a, consulta c,StatusConsulta sc where  a.codAvaliacao = @codAvaliacao
end

create proc ExibirD_sp
@codConsulta int
as
begin
select diagnostico from Consulta where codConsulta = @codConsulta
end

drop proc CadastrarE_sp

select * from Especialidade

