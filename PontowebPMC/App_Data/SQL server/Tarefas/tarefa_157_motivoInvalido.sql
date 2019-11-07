--Desabilita trigger para update direto no banco
Disable trigger insere_frequencia_temp on TBFrequencia;
update TBFrequencia set IDMotivoFalta = 5 --(Outros)
--where IDFrequencia in(select f.IDFrequencia
from TBFrequencia f
left join TBMotivoFalta m on m.IDMotivoFalta = f.IDMotivoFalta
where f.IDMotivoFalta <> 0
and m.DSMotivoFalta is null;
--Habilita trigger
Enable trigger insere_frequencia_temp on TBFrequencia;



update TBFrequenciaJustificativa_Pedido set IDMotivoFalta = 5 --(Outros)
from TBFrequenciaJustificativa_Pedido f
left join TBMotivoFalta m on m.IDMotivoFalta = f.IDMotivoFalta
where f.IDMotivoFalta <> 0
and m.DSMotivoFalta is null;