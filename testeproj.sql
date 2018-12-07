selectAll_sp
insert into Lote values(1,5,9)
insert into Fornecedor values('nome','11-1111-1111','usuario',null,null,null,null)                                                                                                                                                                                                                                                                              )
insert into Pedido values(1,8,0,'12/11/2016',0,'20/10/2016')
create proc dbccAll_sp
as
/*1*//*select * from insertOferta*/
/*sp_help insertOferta
delete from insertOferta
/*2*//*select * from insertproduto
sp_help insertproduto*/
delete from insertproduto
/*3select * from ProdutoFornecedor
sp_help produtofornecedor*/
delete from ProdutoFornecedor
*/DBCC Checkident(ProdutoFornecedor,reseed,0)
/*4select * from PedidoProduto
/*sp_help pedidoproduto*/
delete from PedidoProduto*/
DBCC Checkident(PedidoProduto,reseed,0)
/*5select * from LotePedido
/*sp_help lotepedido*/
delete from LotePedido/*
*/*/DBCC Checkident(LotePedido,reseed,0)
/*7select * from Boleto
/*sp_help boleto
*//*delete from Boleto*/*/
DBCC Checkident(Boleto,reseed,0)
/*8select * from TaxaLote
/*sp_help taxalote*/
*//*delete from TaxaLote
*/DBCC Checkident(TaxaLote,reseed,0)
/*9select * from OfertaProduto
/*sp_help ofertaproduto*/
delete from OfertaProduto
*/DBCC Checkident(OfertaProduto,reseed,0)
/*10select * from Pedido
/*sp_help pedido*/
delete from Pedido
*/DBCC Checkident(Pedido,reseed,0)
/*11select * from Lote
/*sp_help lote*/
delete from Lote
*/DBCC Checkident(Lote,reseed,0)
/*12select * from Produto
/*sp_help produto*/
delete from Produto
*/DBCC Checkident(Produto,reseed,0)
/*12select * from Oferta
/*sp_help oferta*/
delete from Oferta
*/DBCC Checkident(Oferta,reseed,0)
/*13select * from Fornecedor
/*sp_help fornecedor*/
/*delete from Fornecedor
*/*/DBCC Checkident(Fornecedor,reseed,0)
/*14select * from Taxa
/*sp_help taxa*/
delete from Taxa
*/DBCC Checkident(Taxa,reseed,0)
/*15select * from Usuario
/*sp_help usuario*/*/
/*delete from Usuario
 /*
Renomear
Coluna
sp_rename  'Banco.Tabela.coluna0','coluna','column' 

Tabela
sp_rename  'Banco.Tabela0','tabela','table' 


 */
Funções

create functionm FRetornaUsuario(@email varchar(100),@senha varchar(8))
 returns varchar(100)
 as
 begin
 declare @retorna varchar(100)
 
 select @retorna=login from Usuario where email=@email and senha=@senha
 return @retorna
 end                        

 */
 














/*



 /*sp_rename  'BDGRUPO4.OfertaProduto.valorOferta','valorProduto','column'*/
select * from Usuario
--insert into Oferta values(null,null)
--select codOferta from inserted
select * from Oferta
--delete from Fornecedor
--delete from OfertaProduto  DBCC Checkident(oferta,reseed,0)
--delete from Lote  DBCC Checkident(lote,reseed,0)
--delete from Usuario /* DBCC Checkident(usuario,reseed,0)*/
--delete from Oferta  DBCC Checkident(oferta,reseed,0)
--delete from Produto  DBCC Checkident(produto,reseed,0)
--delete from insertOferta/*  DBCC Checkident(insertOferta,reseed,0)*/
--delete from insertProduto  /*DBCC Checkident(insertProduto,reseed,0)*/
--delete from produtofornecedor ---
--delete from pedidoproduto ---
--delete from taxa
--select codproduto from deleted
--sp_help Oferta
--insert into Oferta /*(foiAceita, aceitaEm)*/values (null,null) 
--DBCC Checkident(oferta,reseed,0)

--sp_help Fornecedor
--insert into Fornecedor values('h  hr hrh','58-9689-5465',' ghervh')
select * from Produto
--insert into Produto values('Lápis','1.5')
--delete from insertproduto
--sp_help Produto
--sp_help OfertaFornecedor
select * from OfertaProduto
--sp_help OfertaProduto
--DELETE FROM OFERTAPRODUTO
--delete from Oferta where codOferta=2
--insert into Oferta values(null, null)
--select codOferta from deleted
--sp_help ProdutoFornecedor

--select nome,telefone select * from usuario where codUsuario
/*
alter proc insertOferta_sp 
@usuario varchar(50)
as
insert into Oferta values(null,null,@usuario)

create proc insertProduto_sp
@produto varchar(30),
@preco money
as
declare @codP int
declare @codO int
insert into Produto values(@produto,@preco)
set @codP = (select codProduto from insertproduto where data in(select max(data) from insertProduto))
set @codO = (select codOferta from insertoferta where data in(select max(data) from insertOferta ))
 insert into Ofertaproduto values(@codO,@codP)

 delete from Oferta
 delete from insertOferta
create table insertproduto (
codproduto int,
data datetime not null
) 
create trigger insertOferta_tg on Oferta for insert as
declare @cod int 
set @cod = (select codOferta from inserted)
insert into insertOferta values(@cod,Getdate())


create trigger insertProduto_tg on Produto for insert as
declare @cod int
set @cod = (select codproduto from inserted)
insert into insertproduto values(@cod,GetDate())
*/
SELECT * FROM INSERTOFERTA
select * from insertproduto
-- insertProduto_sp 'Lápis', '1'
-- insertOferta_sp 'fabercastell'
/*delete from insertproduto
delete from produto
delete from oferta
delete from ofertaroduto
delete from insertoferta
DBCC Checkident(oferta,reseed,0)
DBCC Checkident(produto,reseed,0)
DBCC Checkident(ofertaproduto,reseed,0)
insert into usuario values('usuario','senha','nome','telefone')
insert into produto values('sabonete','1.5')
insert into Fornecedor values('fornecedor','telefone','usuario',30,7,0.01,0.01)
insert into Oferta values(null,'usuario',null)
insert into Lote values(1,1,'1.5')
insert into OfertaProduto values(1,1)
insert into ProdutoFornecedor values(1,1)
insert into pedido values(1,2,0,'10/11/2016',0,'9/10/2016')

insert into LotePedido values(1,1)
insert into boleto values(1,0,'10/10/2016','10/10/2016')
insert into pedidoproduto values(2,2,1)
select codPedido from Pedido where codFornecedor in (select codFornecedor where usuario='usuario')
delete from pedido
sp_rename 'Boleto.RazaoPrecoFinal''column ','RazaoPrecoFinal','column' 

*/*/