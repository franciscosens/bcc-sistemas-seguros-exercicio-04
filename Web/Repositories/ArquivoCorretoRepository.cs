using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories
{
    public class ArquivoCorretoRepository : IArquivoCorretoRepository
    {
        private readonly SistemaContext _context;

        public ArquivoCorretoRepository(SistemaContext context)
        {
            _context = context;
        }

        public int Adicionar(ArquivoCorreto arquivo)
        {
            _context.ArquivosCorretos.Add(arquivo);
            _context.SaveChanges();
            return arquivo.Id;
        }

        public List<ArquivoCorreto> ObterTodos(string nome)
        {
            if(string.IsNullOrEmpty(nome))
            {
                return _context.ArquivosCorretos.ToList();
            }

            return _context.ArquivosCorretos.Where(x => x.NomeArquivo.Contains(nome.Trim())).ToList();
        }

        public ArquivoCorreto ObterPeloNomeHash(string hash)
        {
            return _context.ArquivosCorretos.FirstOrDefault(x => x.NomeHash == hash);
        }
    }
}
