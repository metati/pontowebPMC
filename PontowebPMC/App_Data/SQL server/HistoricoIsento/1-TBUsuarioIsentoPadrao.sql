create table TBUsuarioIsentoPadrao(
IDUsuarioIsentoPadrao integer identity(1,1) not null,
IDUsuario integer not null,
IDVinculoUsuario bigint not null,
DTInicio date not null,
DTFim date,
Isento bit not null,
PadraoAtual bit not null,
Acesso datetime default getdate()
);

alter table TBUsuarioIsentoPadrao add constraint PK_TBUsuarioIsentoPadrao primary key (IDUsuarioIsentoPadrao);
alter table TBUsuarioIsentoPadrao add constraint FK_TBUsuarioIsentoPadraoUsuario foreign key (IDUsuario) 
references TBUsuario (IDUsuario);
alter table TBUsuarioIsentoPadrao add constraint FK_TBUsuarioIsentoPadraoVinculoUsuario foreign key (IDVinculoUsuario)
references TBVinculoUsuario (IDVinculoUsuario);





