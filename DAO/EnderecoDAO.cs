using consultaCep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consultaCep.DAO
{
    public class EnderecoDAO
    {
        private readonly Context _context;
        private object[] Id;

        public EnderecoDAO(Context context)
        {
            _context = context;
        }

        public void Cadastrar(Endereco e)
        {
            _context.Enderecos.Add(e);
            _context.SaveChanges();
        }
        public List<Endereco> MostrarTodos()
        {
            return _context.Enderecos.ToList();
        }


    }
}
