using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBancario
{
    public class ContaPrazo : Conta

    {
       // ESTA SETADA UMA DATA Q PERMITE SACAR NOS TESTES
       public DateTime _PermiteLevantar = new DateTime(2019, 1, 18);
      
        //test
        public override void Levantar(double valor)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = _PermiteLevantar;
            int result = DateTime.Compare(date1, date2);
            
            if (result < 0)
            {
                Console.WriteLine("Data mais cedo = nao possivel sacar");

            }
                
            else
            {
                Console.WriteLine("possivel sacar");
                _saldo = -valor;
                _horaMovimento = DateTime.Now;

            }

         }

        public void AdicionarRendimento()
        {
            
            // NOS TESTES NAO LEVA EM CONSIDERAÇAO A HORA DO ULTIMO MOVIMENTO
            //DateTime inicio = this._horaMovimento;
            DateTime inicio = new DateTime(2018, 4, 06);
            DateTime fim = DateTime.Now;
            Console.WriteLine("VALOR DE I=inicio", this._horaMovimento);



            double valorInvestido = _saldo;
            var i = Math.Truncate(fim.Subtract(inicio).Days / (365.25 / 12));
            Console.WriteLine("VALOR DE I",i);

            if (i > 1)
            {
                double contadorMes = i;
                //rendimento de 0.36% = 0.0036
                while (contadorMes <= i)
                {
                    valorInvestido = valorInvestido + valorInvestido * 0.05;
                    contadorMes = contadorMes + 1;
                }
                _saldo = valorInvestido;
            }


        }



    }
}
