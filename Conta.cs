using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaBancario
{
    public class Conta
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

        public int Numero { get; private set; }

        public List<Titular> Titulares { get; private set; }

        public void DefinirNumero(int numero)
        {
            this.Numero = numero;
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
            _saldo = _saldo +valor; 
            _horaMovimento = DateTime.Now;
        }

        public virtual void Levantar(double valor)
        {
            _saldo = _saldo - valor;
            _horaMovimento = DateTime.Now;
        }

        public List<Movimento> ListarMovimentos()
        {
            return this._movimentos.ToList();
        }
         public void AdicionarMovimento(double movimento)
        {
            this._saldo += movimento;
            if (_movimentos.Count >= 50)
            {
                var maisAntigo = this._movimentos.First();
                this._movimentos.Remove(maisAntigo);
            }
            var novoMovimento = new Movimento(movimento);
            this._movimentos.Add(novoMovimento);
        }

        public List<Movimento> ListarMovimentosFuturos()
        {
            return this._movimentosFuturos.ToList();
        }

        public void AdicionarMovimentoFuturo(double movimento, DateTime data)
        {
            this._saldoFuturo += movimento;
            var novoMovimento = new Movimento(movimento, data);
            this._movimentos.Add(novoMovimento);
        }      
    }
}
