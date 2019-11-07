using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class ErroOperacao
    {
     public string RetornaErroOperacao(System.Data.OleDb.OleDbException ex)
        {
            string msg = "";

            if (ex.ErrorCode == -2146232060)
            {
                msg = "Registro já adicionado ao banco de dados. Tente outro registro.";
            }
            if (ex.ErrorCode == -2147483647 || ex.ErrorCode == -2147467263)
            {
                msg = "Código não implementado.";
            }
            if (ex.ErrorCode == -2147483646 || ex.ErrorCode == -2147024882)
            {
                msg = "A operação excedeu o uso de memória. Operação cancelada.";
            }
            if (ex.ErrorCode == -2147483645 || ex.ErrorCode == -2147024809)
            {
                msg = "Um ou mais argumentos de consulta inválidos. Tente novamente.";
            }
            if (ex.ErrorCode == -2147483644 || ex.ErrorCode == -2147467262)
            {
                msg = "Inteface não suportada. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147483643 || ex.ErrorCode == -2147467261)
            {
                msg = "Ponto de argumento inválido.";
            }
            if (ex.ErrorCode == -2147483642 || ex.ErrorCode == -2147024890)
            {
                msg = "Manipulador inválido.";
            }
            if (ex.ErrorCode == -2147483641 || ex.ErrorCode == -2147467260)
            {
                msg = "Operação abortada por razões desconhecidas. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147483640 || ex.ErrorCode == -2147467259)
            {
                msg = "Ocorreu um erro não especificado. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147483639 || ex.ErrorCode == -2147024891)
            {
                msg = "O acesso ao banco de dados foi negado. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147483638)
            {
                msg = "Os dados necessários para completar a operação não estão disponíveis. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467258)
            {
                msg = "Houve falha de armazenamento local. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467257)
            {
                msg = "Houve falha na locação de memórica física para este recurso. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467256)
            {
                msg = "Houve falha na tentativa de locar memórica para este recurso. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467255)
            {
                msg = "Não foi possível inicializar cache da classe usada. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467254)
            {
                msg = "Falha ao tentar iniciar os serviços de RPC. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467253)
            {
                msg = "Não foi possível inicializar o canal de serviços de armazenamento. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467252)
            {
                msg = "Não foi possível alocar o controle do canal de armazenamento. Tente novamente.";
            }
            if (ex.ErrorCode == -2147467251)
            {
                msg = "O usuário locador de memória fornecido é inaceitável. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467250)
            {
                msg = "O serviço de OLE solicitado já existe. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467249)
            {
                msg = "O mapeamento de serviços OLEDB já existe. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467248)
            {
                msg = "O serviço foi incapaz de ver o mapa de arquivo para o serviço OLE. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467247)
            {
                msg = "Falha ao tentar iniciar os serviços de OLE. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467246)
            {
                msg = "Houve falha na tentativa de chamar os serviços de operação uma segunda vez enquanto segmento único.";
            }
            if(ex.ErrorCode == -2147467245 )
            {
                msg = "Ativação remota de serviços foi negada. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467244)
            {
                msg = "O servidor para serviços remotos não foi encontrado. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467243)
            {
                msg = "A classe em execução está com a configuração de segurança diferente da do ID Chamador. Operação cancelada.";
            }
            if (ex.ErrorCode == -2147467242)
            {
                msg = "O uso de serviços de OLE1 DDE em janelas está desativado. Contate o administrador.";
            }
            if (ex.ErrorCode == -2147467241)
            {
                msg = "Uma especificação 'RunAs' deve ser <nome<nome_de_usuário>'\' ou simplesmente <nome_de_usuário>";
            }
            if(ex.ErrorCode == -2147467240)
            {
                msg = "O processo nçao pode ser iniciado no servidor. O caminho dos dados pode estar incorreto.";
            }
            if(ex.ErrorCode == -2147467239)
            {
                msg = "O processo não pode ser inicializado conforme configurado. O caminho pode estar incorreto ou ser inexistente.";
            }
            if(ex.ErrorCode == -2147467238)
            {
                msg = "O processo não pôde ser inicializado porque a identidade configurada está incorreta. Verifique o usuário e senha.";
            }
            if(ex.ErrorCode == -2147467237)
            {
                msg = "A parte cliente não tem permissão para acessar o servidor. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147467236)
            {
                msg = "A prestação dos serviços solicitados não pôde ser iniciado. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147467235)
            {
                msg = "Houve falha na comunicação com o servidor. Verifique sua internet.";
            }
            if(ex.ErrorCode == -2147467234)
            {
                msg = "Não há resposta do servidor após a comunicação ter sido lançada. Verifique sua internet.";
            }
            if(ex.ErrorCode == -2147467233)
            {
                msg = "A informação de serviços para este servidor é inconsistente ou incompleta.";
            }
            if(ex.ErrorCode == -2147467232)
            {
                msg = "A informação de registro para essa interface é inconsistente ou incompleta.";
            }
            if(ex.ErrorCode == -2147467231)
            {
                msg = "Operação executada não é suportada pelo servidor. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147418113)
            {
                msg = "Erro catastrófico. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217920 )
            {
                msg = "Requisitante de acesso inválido. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217919)
            {
                msg = "Número total de linhas criadas não suportada pelo banco de dados.";
            }
            if(ex.ErrorCode == -2147217918)
            {
                msg = "Não foi possível incluir com o tipo de acesso read-only";
            }

            if(ex.ErrorCode == -2147217917)
            {
                msg = "Valores indicado violam o esquema do banco de dados. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217916)
            {
                msg = "Linha manipulada inválida. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217915)
            {
                msg = "Um objeto foi aberto. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217914)
            {
                msg = "Caracter inválido para esta operação. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217913)
            {
                msg = "Algum valor informado não pôde ser convertido para o tipo correto. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217912)
            {
                msg = "Informação vinculada inválida. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217911)
            {
                msg = "Permissão de acesso negada. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217910)
            {
                msg = "Coluna especificada não contém marcadores ou capítulos. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217909)
            {
                msg = "Alguns limites de custo foram rejeitados. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217908)
            {
                msg = "Nenhum comando foi definido para o objeto de comando. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217907)
            {
                msg = "Não foi possível encontrar um plano de consulta dentro do limite de dados. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217906)
            {
                msg = "Livro de marcações inválido.";
            }
            if(ex.ErrorCode == -2147217905)
            {
                msg = "Modo de trava inválido.";
            }
            if(ex.ErrorCode == -2147217904)
            {
                msg = "Nehum valor encontrado para um ou mais parâmetros de consulta. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217903)
            {
                msg = "Coluna de indentificação inválida.";
            }
            if(ex.ErrorCode == -2147217902)
            {
                msg = "Relação inválida. Tente novamente.";
            }
            if(ex.ErrorCode == -2147217901)
            {
                msg = "Valor de argumento inválido.";
            }
            if(ex.ErrorCode == -2147217900)
            {
                msg ="O comando executado possui um ou mais erros. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217899)
            {
                msg = "O comando executado não pode ser cancelado.";
            }
            if(ex.ErrorCode == -2147217898)
            {
                msg = "O provedor de dados não suporta o dialéto usado. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217897)
            {
                msg = "O 'Data Source' especificado já existe no contexto atual. Contate o administrador.";
            }
            if(ex.ErrorCode == -2147217896)
            {
                msg = "Falha na inclusão do dado.";
            }
            if(ex.ErrorCode == -2147217895 )
            {
                msg = "Sem faixa correspondente as características descritas podem ser encontradas dentro da faixa atual.";
            }
            if(ex.ErrorCode == -2147217894)
            {
                msg = "Propriedade desta arvore foi entregue ao provedor de acesso.";
            }
            return msg;
        }
    }
}
