using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBancario
{
    class ContaPoupanca : Conta
    {

        public void AdicionarRendimento()
        {
            
            double valorInvestido = _saldo;
            DateTime inicio = this._horaMovimento;
            DateTime fim = DateTime.Now;

            var i = Math.Truncate(fim.Subtract(inicio).Days / (365.25 / 12));
           
            if (i > 1) {
                double contadorMes = i;
                //rendimento de 0.36% = 0.0036
                while (contadorMes <= i)
                {
                    valorInvestido = valorInvestido + valorInvestido * 0.0036;
                    contadorMes = contadorMes + 1;
                }
                _saldo = valorInvestido;
                _saldo = valorInvestido;
            }
       
        }

    }
}
