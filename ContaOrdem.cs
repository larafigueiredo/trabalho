using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaBancario
{
    class ContaOrdem: Conta
    {

        public ContaOrdem()
        {
            this.Titulares = new List<Titular>();
            this._movimentos = new List<Movimento>();
            this._movimentosFuturos = new List<Movimento>();

        }

        public override void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }

        public override void DefinirNumero(int numerom)
        {
            this._numero = numerom;

        }

        public override void Levantar(double valor)
        {
            _saldo = _saldo - valor;
            _horaMovimento = DateTime.Now;
            Console.WriteLine("possivel sacar");

        }

        public override double ConsultarSaldo()
        {
            return this._saldo;
        }

        public override void AdicionarRendimento()
        {

        }


        public override void Deposito(double valor)
        {
            _saldo = _saldo + valor;
            _horaMovimento = DateTime.Now;
        }


        public override List<Movimento> ListarMovimentos()
        {
            return this._movimentos.ToList();
        }

        public override void AdicionarMovimento(double movimento)
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

        public override List<Movimento> ListarMovimentosFuturos()
        {
            return this._movimentosFuturos.ToList();
        }

        public override void AdicionarMovimentoFuturo(double movimento, DateTime data)
        {
            this._saldoFuturo += movimento;
            var novoMovimento = new Movimento(movimento, data);
            this._movimentos.Add(novoMovimento);
        }
    }
}
