create table TBFechamentoFolha(
IDFechamento int not null identity(1,1),
IDEmpresa int not null,
DataProcessamento date not null,
Mes int not null,
Ano int not null 
);

alter table TBFechamentoFolha add constraint pk_TBFechamentoFolha primary key(IDFechamento);
alter table TBFechamentoFolha add constraint fk_TBFechamentoFolha_Setor foreign key (IDEmpresa) references TBEmpresa(IDEmpresa);
-------------------------------------------------------------------------------------------

create table TBFechamentoFolha_Setor(
IDFechamentoSetor int not null identity(1,1),
IDFechamento int not null,
IDSetor int not null,
DataProcessamento date not null
);

alter table TBFechamentoFolha_Setor add constraint pk_TBFechamentoFolhaSetor primary key(IDFechamentoSetor);
alter table TBFechamentoFolha_Setor add constraint fk_TBFechamentoFolha foreign key (IDFechamento) references TBFechamentoFolha(IDFechamento);
alter table TBFechamentoFolha_Setor add constraint fk_TBFechamentoFolhaSetor foreign key (IDSetor) references TBSetor(IDSetor);

----------------------------------------------------------
create table TBFechamentoFolha_Cargo(
IDFechamentoCargo int not null identity(1,1),
IDFechamento int not null,
IDCargo int not null,
DataProcessamento date not null
);

alter table TBFechamentoFolha_Cargo add constraint pk_TBFechamentoFolhaCargo primary key(IDFechamentoCargo);
alter table TBFechamentoFolha_Cargo add constraint fk_TBFechamentoCargoFolha foreign key (IDFechamento) references TBFechamentoFolha(IDFechamento);
alter table TBFechamentoFolha_Cargo add constraint fk_TBFechamentoFolhaCargo foreign key (IDCargo) references TBCargo(IDCargo);