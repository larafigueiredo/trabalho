using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SistemaBancario
{
    public class ContaPoupanca : Conta
    {
        public override void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }

        public override void AdicionarRendimento()
        {
            
            double saldoAtualizado = _saldo;
            DateTime inicio = this._horaMovimento;
            DateTime fim = DateTime.Now;

            var i = Math.Truncate(fim.Subtract(inicio).Days / (365.25 / 12));
           
            if (i > 1) {
                double contadorMes = i;
                //rendimento de 0.36% = 0.0036
                while (contadorMes <= i)
                {
                    saldoAtualizado += saldoAtualizado * 0.0036;
                    contadorMes ++;
                }
                var rendimentoNoPeriodo = saldoAtualizado - _saldo;
                var movimentoRendimento = new Movimento(rendimentoNoPeriodo);
                this._movimentos.Add(movimentoRendimento);
                _saldo = saldoAtualizado;
                _horaMovimento = DateTime.Now;
            }
       
        }

        public override double ConsultarSaldo()
        {
            return this._saldo;
        }

        public override void Levantar(double valor)
        {
            _saldo = _saldo - valor;
            _horaMovimento = DateTime.Now;
            Console.WriteLine("possivel sacar");

        }

        public override void DefinirNumero(int numerom)
        {
            this._numero = numerom;

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
