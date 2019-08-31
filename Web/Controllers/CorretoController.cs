using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Repositories;

namespace Web.Controllers
{
    [Route("correto")]
    public class CorretoController : Controller
    {
        private readonly string _nomePasta;
        private readonly string _caminho;
        private readonly IArquivoCorretoRepository _repository;

        public CorretoController(IArquivoCorretoRepository repository, IHostingEnvironment env)
        {
            _repository = repository;

            string wwwroot = env.WebRootPath;
            _nomePasta = "arquivos-corretos";
            _caminho = Path.Combine(wwwroot, _nomePasta);

            if (!Directory.Exists(_caminho))
            {
                Directory.CreateDirectory(_caminho);
            }
        }

        public IActionResult Index(string nome = "")
        {
            var arquivos = _repository.ObterTodos(nome);

            ViewBag.Nome = nome;
            ViewBag.Caminho = _nomePasta;
            ViewBag.Arquivos = arquivos;
            return View();
        }

        [HttpPost, Route("Uploade")]
        public ActionResult Upload(IFormFile arquivo)
        {

            var nomeArquivo = arquivo.FileName;
            var nomeHash = ObterHashDoNomeDoArquivo(nomeArquivo);

            var caminhoArquivo = Path.Combine(_caminho, nomeHash);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                arquivo.CopyTo(stream);
                _repository.Adicionar(new Models.ArquivoCorreto()
                {
                    NomeArquivo = nomeArquivo,
                    NomeHash = nomeHash
                });
            }
            return RedirectToAction("Index");
        }

        [HttpGet, Route("download")]
        public ActionResult Download(string hash)
        {
            var arquivo = _repository.ObterPeloNomeHash(hash);

            string caminhoArquivo = Path.Combine(_caminho, hash);
            if(arquivo == null || !System.IO.File.Exists(caminhoArquivo))
            {
                return NotFound("Arquivo não existe");
            }

            byte[] bytes = System.IO.File.ReadAllBytes(caminhoArquivo);
            return File(bytes, "application/force-download", arquivo.NomeArquivo);
        }

        public static string ObterHashDoNomeDoArquivo(string nome)
        {
            FileInfo info = new FileInfo(nome);

            var crypt = new SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(info.Name.Replace(info.Extension, "") + DateTime.Now));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return (hash + info.Extension).ToUpper();
        }
    }
}