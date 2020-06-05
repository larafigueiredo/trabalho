using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SistemaBancario
{
    public class ContaPrazo : Conta

    {
        // ESTA SETADA UMA DATA Q PERMITE SACAR NOS TESTES
        
        public DateTime _PermiteLevantar = new DateTime(2019, 1, 18);
        

        public override void DefinirNumero(int numerom)
        {
            this._numero = numerom;

        }


        public override void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }
        //test
        public override void Levantar(double valor)
        {
            Console.WriteLine("Dasdasdasdar");
            DateTime date1 = DateTime.Now;
            //quiser testar sete aqui a data de agora e ele nao permite sacar
            //_PermiteLevantar = DateTime.Now;
           DateTime date2 = _PermiteLevantar;
            int result = DateTime.Compare(date1, date2);
            
            if (result < 0)
            {
                Console.WriteLine("Data mais cedo = nao possivel sacar");

            }
                
            else
            {
                Console.WriteLine("possivel sacar");
                _saldo = _saldo - valor;
                _horaMovimento = DateTime.Now;

            }

         }
        public override double ConsultarSaldo()
        {
            return this._saldo;
        }

        public override void AdicionarRendimento()
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
