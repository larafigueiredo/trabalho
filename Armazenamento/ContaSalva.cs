using System.Collections.Generic;

namespace SistemaBancario.Armazenamento
{
    public class ContaSalva
    {
        public int Numero { get; set; }

        public List<int> Titulares { get; set; }

        public List<MovimentoSalvo> Movimentos { get; set; }

        public List<MovimentoSalvo> MovimentosFuturos { get; set; }

        public double Saldo { get; set; }

        public double SaldoFuturo { get; set; }
    }
}