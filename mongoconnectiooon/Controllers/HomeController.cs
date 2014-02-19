using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using mongoconnectiooon.Models;

namespace mongoconnectiooon.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private MongoCollection<Pessoa> _repository;

        public void conecao()
        {
            var connectiomongo = ConfigurationManager.AppSettings["MongoDbConnection"];
            var dados = MongoDatabase.Create(connectiomongo);

            _repository = dados.GetCollection<Pessoa>(typeof(Pessoa).Name);
        }

        public ActionResult Index()
        {
            

            //var pessoinha = new Pessoa
            //    {
            //        Nome = "Weslley",
            //        Sobrenome = "Neri"
            //    };

            //_repository.Insert(pessoinha);
            return View();
        }

        [HttpPost]
        public ActionResult Inserir(Pessoa pessoinha)
        {
            var novapessoa = new Pessoa
                {
                    Nome = pessoinha.Nome,
                    Sobrenome = pessoinha.Sobrenome
                };
            try
            {
                conecao();
                _repository.Insert(novapessoa);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return null;
        }
    }
}
