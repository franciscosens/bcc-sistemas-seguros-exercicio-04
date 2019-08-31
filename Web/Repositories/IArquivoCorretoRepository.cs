using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories
{
    public interface IArquivoCorretoRepository
    {
        List<ArquivoCorreto> ObterTodos(string nome);

        int Adicionar(ArquivoCorreto arquivo);

        ArquivoCorreto ObterPeloNomeHash(string hash);
    }
}
