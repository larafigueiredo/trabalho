using System;
using System.Collections.Generic;

namespace SistemaBancario.Model
{
    public class ContaPoupanca : Conta
    {
        public int IntervaloAniversarioEmMeses { get; protected set; }

        public DateTime DataUltimoAniversario { get; protected set; }

        public double TaxaDeRendimento { get; protected set; }

        public ContaPoupanca(int numeroConta) : base(numeroConta)
        {
            this.IntervaloAniversarioEmMeses = 3;
            this.DataUltimoAniversario = DateTime.Now;
            this.TaxaDeRendimento = 0.0036;
        }

        public ContaPoupanca(
            int numero,
            List<Titular> titulares,
            List<Movimento> movimentos,
            List<Movimento> movimentosFuturos,
            double saldo,
            double saldoFuturo,
            int intervaloAniversarioEmMeses,
            DateTime dataUltimoAniversario,
            double taxaRendimento) : base(
                numero,
                titulares,
                movimentos,
                movimentosFuturos,
                saldo,
                saldoFuturo)
        {
            this.IntervaloAniversarioEmMeses = intervaloAniversarioEmMeses;
            this.DataUltimoAniversario = dataUltimoAniversario;
            this.TaxaDeRendimento = taxaRendimento;
        }

        // False = Não foi possível realizar a operação
        // True = Operação realizada com sucesso.
        public bool DefinirIntervaloAniversario(int intervaloEmMeses)
        {
            if (intervaloEmMeses >= 0 && intervaloEmMeses <= 12)
            {
                this.IntervaloAniversarioEmMeses = intervaloEmMeses;
                return true;
            }

            return false;
        }

        // False = Não foi possível realizar a operação
        // True = Operação realizada com sucesso.
        public bool DefinirDataUltimoAniversario(DateTime dataAniversario)
        {
            // Deve ser uma data do passado
            if (dataAniversario > DateTime.Now)
            {
                return false;
            }

            this.DataUltimoAniversario = dataAniversario;
            return true;
        }

        public bool DefinirTaxaDeRendimento(double taxaDeRendimento)
        {
            if (taxaDeRendimento <= 0 || taxaDeRendimento >= 1)
            {
                return false;
            }

            this.TaxaDeRendimento = taxaDeRendimento;
            return true;
        }

        // False = Não foi possível realizar a operação
        // True = Operação realizada com sucesso.
        public bool ProcessarRendimentos()
        {
            var dataProximoAniversario = this.DataUltimoAniversario.AddMonths(this.IntervaloAniversarioEmMeses);

            // Ainda não.
            if (DateTime.Now < dataProximoAniversario)
            {
                return false;
            }

            var inicioPeriodo = this.DataUltimoAniversario;
            var fimPeriodo = DateTime.Now;
            var mesesEmAtrasoDeCalculo = this.CalcularDiferencaEmMeses(inicioPeriodo, fimPeriodo);

            while (mesesEmAtrasoDeCalculo > 0)
            {
                // Calcula projeção da data
                var dataMovimento = this.DataUltimoAniversario.AddMonths(1);
                // Calcula rendimento do mês
                var rendimento = this._saldo * this.TaxaDeRendimento;

                // Registra movimento e afeta saldo.
                var movimento = new Movimento(TipoMovimento.Rendimento, rendimento, dataMovimento);
                this.AdicionarMovimento(movimento);
                this._saldo += rendimento;

                // Atualiza data do ultimo aniversário calculado
                this.DataUltimoAniversario = dataMovimento;

                // Reduz o contador do while
                mesesEmAtrasoDeCalculo--;
            }

            return true;
        }

        
    }
}