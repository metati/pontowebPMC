create table TBPontoEletronico(
PontoEletronicoID int not null identity(1,1),
PontoEletronico_Nome varchar(200) not null,
PontoEletronico_Local varchar(200) not null,
PontoEletronico_Porta int not null,
PontoEletronico_Ip varchar(15) not null,
PontoEletronico_Usuario varchar(20) not null,
PontoEletronico_Senha varchar(20) not null,
DataCadastro datetime default getdate() not null
);
alter table TBPontoEletronico add constraint PK_TBPontoEletronico primary key (pontoEletronicoID);



create table TBPontoEletronicoSetor(
PontoEletronicoSetorID int not null identity(1,1),
PontoEletronicoID int not null,
IDSetor int not null,
DataCadastro datetime default getdate() not null
);
alter table TBPontoEletronicoSetor add constraint PK_TBPontoEletronicoSetor primary key (PontoEletronicoSetorID);
alter table TBPontoEletronicoSetor add constraint FK_TBPontoEletronicoSetor_TBPontoEletronico foreign key (PontoEletronicoID) references TBPontoEletronico (PontoEletronicoID);
alter table TBPontoEletronicoSetor add constraint FK_TBPontoEletronicoSetor_TBSetor foreign key (IDSetor) references TBSetor (IDSetor);


--Cria coluna tabela TBUsuario
alter table TBUsuario add PIS_GERADO varchar(12);