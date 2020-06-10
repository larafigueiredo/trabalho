using System;
using System.Collections.Generic;

namespace SistemaBancario.Model
{
    public class ContaOrdem : Conta
    {
        public ContaOrdem(int numero) : base(numero)
        {
        }

        public ContaOrdem(
            int numero,
            List<Titular> titulares,
            List<Movimento> movimentos,
            List<Movimento> movimentosFuturos,
            double saldo,
            double saldoFuturo) : base(
                numero, 
                titulares, 
                movimentos, 
                movimentosFuturos, 
                saldo, 
                saldoFuturo)
        {

        }

        public bool EfetuarPagamento(double valor)
        {
            // Só permite pagamento com saldo disponível
            if (this._saldo <= valor)
            {
                return false;
            }

            _saldo -= valor;
            // Movimentos de saída, tem valor negativo.
            var valorNegativo = valor * -1;
            var movimento = new Movimento(TipoMovimento.Pagamento, valorNegativo, DateTime.Now);
            this.AdicionarMovimento(movimento);
            return true;
        }
    }
}