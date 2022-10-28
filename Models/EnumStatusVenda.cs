using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagamentoApi.Models
{
    public enum EnumStatusVenda
    {
        Aguardando_Pagamento,
        Pagamento_Aprovado,
        Enviado_para_Transportadora,
        Entregue,
        Cancelada,
        
    }
}