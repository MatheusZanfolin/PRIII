create table Especialidade(
codEspecialidade int primary key identity(1,1),
nomeEspecialidade varchar(30) not null
)
create table Medico(
crm int primary key,
nome varchar(50) not null,
dataNascimento date not null,
email varchar(30)not null,
celular char(11) not null,
telefone varchar(11) not null,
foto image not null,
senha varchar(30) not null,
codEspecialidade int not null,
constraint fkEspecialidade foreign key(codEspecialidade) references Especialidade(codEspecialidade)
)
create table Paciente(
usuario  varchar(30) primary key,
nome varchar(50) not null,
endereco varchar(50) not null,
dataNascimento date not null,
email varchar(30)not null,
celular char(11) not null,
telefone varchar(11) not null,
foto image not null,
senha varchar(30) not null
)
create table StatusConsulta(
codStatus int primary key identity(1,1),
nomeStatus varchar(20) not null
)
create table Consulta(
codConsulta int primary key identity(1,1),
dataHoraConsulta datetime not null,
meiaHora bit not null,
codStatus int not null,
diagnostico text, 
codAvaliacao int ,
crm int not null,
usuario  varchar(30) not null,
constraint fkPaciente foreign key(usuario) references Paciente(usuario),
constraint fkMedico foreign key(crm) references Medico(crm),
constraint fkStatus foreign key(codStatus) references StatusConsulta(codStatus)
)
create table AvaliacaoConsulta(
codAvaliacao int primary key identity(1,1),
nivelSatisfacao int not null,
descricao text not null,
constraint fkAvaliacao foreign key(codAvaliacao) references Consulta(codConsulta)
)


create table Secretario(
usuario varchar(30) primary key,
senha varchar(30) not null
)