using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("vulneravel")]
    public class VulneravelController : Controller
    {
        private readonly string _caminho;

        public VulneravelController()
        {
            _caminho = Path.Combine(Directory.GetCurrentDirectory(), "arquivos-vulneraveis");

            if (!Directory.Exists(_caminho))
            {
                Directory.CreateDirectory(_caminho);
            }
        }

        public IActionResult Index()
        {
            List<ArquivoVulneravel> arquivosVulneraveis = new List<ArquivoVulneravel>();

            List<string> arquivos = Directory.GetFiles(_caminho, "").ToList();

            foreach (var arquivo in arquivos)
            {
                FileInfo info = new FileInfo(arquivo);
                arquivosVulneraveis.Add(new ArquivoVulneravel()
                {
                    Nome = info.Name,
                    Caminho = arquivo
                });
            }

            ViewBag.Caminho = _caminho;
            ViewBag.Arquivos = arquivosVulneraveis;
            return View();
        }

        [HttpPost, Route("upload")]
        public ActionResult Upload(IFormFile arquivo)
        {
            string nome = arquivo.FileName;

            string caminhoArquivo = Path.Combine(_caminho, nome);
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }
            return RedirectToAction("Index");
        }

        [HttpGet, Route("download")]
        public ActionResult Download(string nome)
        {
            string caminhoArquivo = Path.Combine(_caminho, nome);
            if (System.IO.File.Exists(caminhoArquivo))
            {
                return NotFound("Arquivo não existe");
            }

            byte[] bytes = System.IO.File.ReadAllBytes(caminhoArquivo);
            return File(bytes, "application/force-download", nome);
        }
    }
}