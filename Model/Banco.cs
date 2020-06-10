using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaBancario.Model
{
    public class Banco
    {
        public const int MaxContas = 1000;
        public const int MaxTitulares = 1000;

        public Banco()
        {
            this.Contas = new List<Conta>();
            this.Titulares = new List<Titular>();
        }

        public List<Conta> Contas { get; private set; }

        public List<Titular> Titulares { get; private set; }

        public void AdicionarConta(Conta conta)
        {
            this.Contas.Add(conta);
        }

        public void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }

        public ContaOrdem CriarContaAOrdem(Titular titular)
        {
            if (this.Contas.Count >= MaxContas)
            {
                return null;
            }

            // Gera numero novo de conta, se a lista estiver vazia, retorna 1
            var novoNumero = this.Contas.Any() ? 
                this.Contas.Max(c => c.Numero) + 1 
                : 1;

            var novaConta = new ContaOrdem(novoNumero);
            novaConta.AdicionarTitular(titular);

            this.Contas.Add(novaConta);

            return novaConta;
        }

        public ContaPoupanca CriarContaPoupanca(Titular titular)
        {
            if (this.Contas.Count >= MaxContas)
            {
                return null;
            }

            // Gera numero novo de conta
            var novoNumero = this.Contas.Max(c => c.Numero) + 1;

            var novaConta = new ContaPoupanca(novoNumero);
            novaConta.AdicionarTitular(titular);

            this.Contas.Add(novaConta);

            return novaConta;
        }

        public ContaPrazo CriarContaPrazo(Titular titular)
        {
            if (this.Contas.Count >= MaxContas)
            {
                return null;
            }

            // Gera numero novo de conta
            var novoNumero = this.Contas.Max(c => c.Numero) + 1;

            var novaConta = new ContaPrazo(novoNumero);
            novaConta.AdicionarTitular(titular);

            this.Contas.Add(novaConta);

            return novaConta;
        }

        public bool CriarTitular(Titular titular)
        {
            if (this.Titulares.Count >= MaxTitulares)
            {
                return false;
            }

            this.Titulares.Add(titular);
            return true;
        }

        public List<Conta> ListarContasCliente(Titular cliente)
        {
            return this.ListarContasCliente(cliente.Nif);
        }

        public List<Conta> ListarContasCliente(int nifCliente)
        {
            // retorna todas as contas que tenham um titular com esse nif na lista.
            return this.Contas
                .Where(c => c.Titulares.Any(t => t.Nif == nifCliente))
                .ToList();
        }

        public List<Conta> ListarContasNegativas()
        {
            return this.Contas
                .Where(c => c.ConsultarSaldo() < 0)
                .ToList();
        }

        public List<ContaOrdem> ListarContasOrdem()
        {
            return this.Contas
                .OfType<ContaOrdem>()
                .ToList();
        }

        public List<ContaPoupanca> ListarContasPoupanca()
        {
            return this.Contas
                .OfType<ContaPoupanca>()
                .ToList();
        }

        public List<ContaPrazo> ListarContasPrazo()
        {
            return this.Contas
                .OfType<ContaPrazo>()
                .ToList();
        }

        public List<Titular> ListarTitulares()
        {
            return this.Titulares;
        }

        public bool TransferirFundos(Conta origem, Conta destino, double valor)
        {
            // Se a operação de retirar o valor da conta de origem falhar, aborta.
            if (origem.EmitirTransferencia(valor))
            {
                return false;
            }

            // envia o valor para a conta de destino
            destino.ReceberTransferencia(valor);

            return true;
        }

        public double CalcularSaldoGlobal(Titular titular)
        {
            return this.CalcularSaldoGlobal(titular.Nif);
        }

        private double CalcularSaldoGlobal(int nif)
        {
            return this
                .ListarContasCliente(nif)
                .Sum(c => c.ConsultarSaldo());
        }
    } 
}