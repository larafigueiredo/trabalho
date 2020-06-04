using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBancario
{
    public class Movimento
    {
        public double Valor{ get; private set; }
        public DateTime Data{ get; private set; }

       

        //Construtor
        public Movimento(double valor)
        {
            this.Valor = valor;
            this.Data = DateTime.Now;
        }

        public Movimento(double valor, DateTime data)
        {
            this.Valor = valor;
            this.Data = data;
        }
    }
}

