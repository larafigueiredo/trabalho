using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaBancario
{
    public class Conta : Titular
    {
        protected double _saldo;
        protected double _saldoFuturo;
        protected List<Movimento> _movimentos;
        protected List<Movimento> _movimentosFuturos;
        public DateTime _horaMovimento;

        public Conta()
        {
            this.Titulares = new List<Titular>();
            this._movimentos = new List<Movimento>();
            this._movimentosFuturos = new List<Movimento>();
          
        }

        public Conta(int numero) : this()
        {
            this.Numero = numero;
        }
        public int Numero { get; private set; }

        public List<Titular> Titulares { get; private set; }

        private void AdicionarMovimento(Movimento movimento)
        {
            //if (_movimentos.Count >= 50)
            //{
            //    var maisAntigo = this._movimentos.OrderBy(c => c.Data).First();
            //    this._movimentos.Remove(maisAntigo);
            //}
            this._horaMovimento = movimento.Data;
            this._movimentos.Add(movimento);
        }
        public void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }
        public double ConsultarSaldo()
        {
            return this._saldo;
        }

        public virtual void Deposito(double valor)
        {
            _saldo = _saldo + valor; 
            _horaMovimento = DateTime.Now;
            var movimento = new Movimento(valor);
            this.AdicionarMovimento(movimento);            
        }
    
        public virtual bool Levantar(double valor)
        {
            _saldo = _saldo - valor;
            _horaMovimento = DateTime.Now;
            var movimento = new Movimento(valor *- 1);
            this.AdicionarMovimento(movimento);
            return true;
        }

        public List<Movimento> ListarMovimentos(int pageSize = 50)
        {
            return this._movimentos
                .OrderByDescending(m=> m.Data)                
                .Take(pageSize)
                .ToList();
        }    
        
        public void AdicionarMovimentoFuturo(double movimento, DateTime data)
        {
            this._saldoFuturo += movimento;
            var novoMovimento = new Movimento(movimento, data);
            this._movimentos.Add(novoMovimento);
        }

        public List<Movimento> ListarMovimentosFuturos()
        {
            return this._movimentosFuturos.ToList();
        }        
    }
}
