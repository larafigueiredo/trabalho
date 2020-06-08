using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBancario
{
    public class  Titular
    {
        public enum Sexo
        {
            Masculino, 
            Feminino 
        }

        public string Nome { get; private set; }

        public int Nif { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public Sexo Genero { get; private set; }

        public string Morada { get; private set; }
       
        public void DefinirNome(string nome) {
            this.Nome = nome;
        }

        public void DefinirNif(int Nif)
        {
            this.Nif = Nif;
        }

        public void DefinirDataNascimento(DateTime DataNascimento)
        {
            this.DataNascimento = DataNascimento;
        }

        public void DefinirGenero (Sexo Genero)
        {
            this.Genero = Genero;
        }

        public void DefinirResidencia (string residencia)
        {
            this.Morada = residencia;
        }
    }
}
