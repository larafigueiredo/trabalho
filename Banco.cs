using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBancario
{
    public class Banco
    {
        public const int MaxContas = 1000;
        public const int MaxTitulares = 1000;

        public Banco()
        {
            this.Contas = new List<Conta>();
            this.ContaPrazos = new List<ContaPrazo>();
            this.Titulares = new List<Titular>();
        }

        public List<Conta> Contas { get; private set; }

        public List<ContaPrazo> ContaPrazos { get; private set; }

        public List<Titular> Titulares { get; private set; }

        public void AdicionarConta(Conta conta)
        {
            this.Contas.Add(conta);
        }

        public void AdicionarContaPrazo(ContaPrazo conta)
        {
            this.ContaPrazos.Add(conta);
        }

        public void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }

        public List<Conta> ListarContas()
        {
            return this.Contas;
        }

        public List<ContaPrazo> ListarContasPrazo()
        {
            return this.ContaPrazos;
        }

        public List<Titular> ListarTitulares()
        {
            return this.Titulares;
        }

        public Titular CriarTitular(Titular titular)
        {
            if (this.Titulares.Count >= MaxTitulares)
            {
                return null;
            }

            this.Titulares.Add(titular);
            return titular;
        }
        public Conta CriarConta(Conta conta)
        {
            if (this.Contas.Count >= MaxContas)
            {
                return null;
            }

            this.Contas.Add(conta);
            return conta;
        }


        public ContaPrazo CriarContaPrazo(ContaPrazo conta)
        {
            if (this.ContaPrazos.Count >= MaxContas)
            {
                return null;
            }

            this.ContaPrazos.Add(conta);
            return conta;
        }
    } 
}