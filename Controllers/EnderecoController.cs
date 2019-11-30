using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using consultaCep.DAO;
using consultaCep.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace consultaCep.Controllers
{

    /*
     *Robert Gustavo da Silva
     *Matrícula: 1726619
     *Faculdade Positivo Praça Osório   
     */

    [Route("api/Endereco")]
    [ApiController]
    public class EnderecoController : Controller
    {
        private readonly EnderecoDAO _enderecoDAO;
        public EnderecoController(EnderecoDAO enderecoDAO)
        {

            _enderecoDAO = enderecoDAO;
        }
        public IActionResult Index()
        {
            ViewBag.Data = DateTime.Now;
            return View(_enderecoDAO.MostrarTodos());
        }
        public IActionResult CadastrarEndereco()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarEndereco(Endereco e)
        {
            //string url = @"https://api.postmon.com.br/v1/cep/" + e.Cep ;
            string url = @"https://viacep.com.br/ws/" + e.Cep + "/json/";
            WebClient client = new WebClient();
            string resultado = client.DownloadString(url);
            Endereco enderecoNovo = JsonConvert.DeserializeObject<Endereco>(resultado);

            //_pedidoDAO.Cadastrar(pedido);
            _enderecoDAO.Cadastrar(enderecoNovo);

            // return View();
            return RedirectToAction("Index");
        }

        [HttpGet("Enderecos")]
        public String MostrarTodos()
        {
            return JsonConvert.SerializeObject(_enderecoDAO.MostrarTodos());
        }
        [HttpGet("Enderecos/{cep}")]
        public String MostrarPorCep(string cep)
        {


            List<Endereco> allEnderecos = _enderecoDAO.MostrarTodos();
            List<Endereco> enderecos = new List<Endereco>();
            foreach (var endereco in allEnderecos)
            {
                if (endereco.Cep.Equals(cep.Substring(0, 5) + "-" + cep.Substring(5, 3)))
                {
                    enderecos.Add(endereco);
                }
            }
            return JsonConvert.SerializeObject(enderecos);
        }

        [HttpGet("EnderecosPorEstado/{uf}")]
        public String ListEstado(string uf)
        {
            List<Endereco> TodosEnderecos = _enderecoDAO.MostrarTodos();
            List<Endereco> enderecos = new List<Endereco>();
            foreach (var endereco in TodosEnderecos)
            {
                if (endereco.Uf.Equals(uf))
                {
                    enderecos.Add(endereco);
                }
            }
            return JsonConvert.SerializeObject(enderecos);
        }
    } 
}