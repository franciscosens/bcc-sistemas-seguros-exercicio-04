using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    [Table("arquivos_corretos")]
    public class ArquivoCorreto
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("nome_arquivo")]
        public string NomeArquivo { get; set; }

        [Column("nome_hash")]
        public string NomeHash { get; set; }
    }
}
