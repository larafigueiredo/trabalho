using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBancario.Armazenamento
{
    public class ContaPrazoSalva : ContaSalva
    {
        public DateTime DataDeDisponibilizacaoDeSaque { get; set; }

        public DateTime DataUltimoAniversario { get; set; }

        public double TaxaDeRendimento { get; set; }
    }
}
