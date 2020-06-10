using System;
using System.Collections.Generic;

namespace SistemaBancario.Model
{
    public class ContaPrazo : Conta
    {
        public DateTime DataDeDisponibilizacaoDeSaque { get; private set; }

        public DateTime DataUltimoAniversario { get; protected set; }

        public double TaxaDeRendimento { get; protected set; }

        public ContaPrazo(int numeroConta) : base(numeroConta)
        {
            this.DataUltimoAniversario = DateTime.Now;
            this.TaxaDeRendimento = 0.0036;
        }

        public ContaPrazo(
            int numero,
            List<Titular> titulares,
            List<Movimento> movimentos,
            List<Movimento> movimentosFuturos,
            double saldo,
            double saldoFuturo,
            DateTime dataDeDisponibilizacaoDeSaque,
            DateTime dataUltimoAniversario,
            double taxaRendimento) : base(
                numero,
                titulares,
                movimentos,
                movimentosFuturos,
                saldo,
                saldoFuturo)
        {
            this.DataDeDisponibilizacaoDeSaque = dataDeDisponibilizacaoDeSaque;
            this.DataUltimoAniversario = dataUltimoAniversario;
            this.TaxaDeRendimento = taxaRendimento;
        }

        public void DefinirDataDisponilizacaoDeSaque(DateTime data)
        {
            this.DataDeDisponibilizacaoDeSaque = data;
        }

        public override bool EfetuarLevantamento(double valor)
        {
            // Caso a data não permita o saque, cancela operação
            if (DateTime.Now <= this.DataDeDisponibilizacaoDeSaque)
            {
                return false;
            }

            return base.EfetuarLevantamento(valor);
        }

        public bool ProcessarRendimento()
        {
            // Caso a data não permita o saque, cancela operação
            if (DateTime.Now <= this.DataDeDisponibilizacaoDeSaque)
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