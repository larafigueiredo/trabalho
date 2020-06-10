using System;

namespace SistemaBancario.Model
{
    public class Movimento
    {
        // Essas informações são preenchidas apenas na construção,
        // se tornando imutáveis após a construção do objeto,
        // garantindo que nenhum movimento pode ser alterado.
        public readonly DateTime Data;

        public readonly TipoMovimento Tipo;

        public readonly double Valor;

        public Movimento(TipoMovimento tipo, double valor) : this(tipo, valor, DateTime.Now)
        {
        }

        public Movimento(TipoMovimento tipo, double valor, DateTime data)
        {
            this.Tipo = tipo;
            this.Valor = valor;
            this.Data = data;
        }
    }
}