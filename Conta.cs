using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaBancario
{
    public abstract class Conta
    {
        public double _saldo;
        public double _saldoFuturo;
        public int _numero;
        public List<Movimento> _movimentos;
        public List<Movimento> _movimentosFuturos;
        public DateTime _horaMovimento;

        public Conta()
        {
            this.Titulares = new List<Titular>();
            this._movimentos = new List<Movimento>();
            this._movimentosFuturos = new List<Movimento>();
          
        }

        public List<Titular> Titulares { get; set; }

        public abstract void DefinirNumero(int numerom);

        public abstract void AdicionarTitular(Titular titular);

        public abstract double ConsultarSaldo();

        public abstract void Deposito(double valor);

        public abstract void Levantar(double valor);

        public abstract void AdicionarRendimento();

        public abstract List<Movimento> ListarMovimentos();

        public abstract void AdicionarMovimento(double movimento);

        public abstract List<Movimento> ListarMovimentosFuturos();

        public abstract void AdicionarMovimentoFuturo(double movimento, DateTime data);   
    }
}
