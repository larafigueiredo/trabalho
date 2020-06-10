using System;

namespace SistemaBancario.Armazenamento
{
    public class ContaPoupancaSalva : ContaSalva
    {
        public int IntervaloAniversarioEmMeses { get; set; }

        public DateTime DataUltimoAniversario { get; set; }

        public double TaxaDeRendimento { get; set; }
    }
}