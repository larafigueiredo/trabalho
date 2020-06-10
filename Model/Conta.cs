using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBancario.Model
{
    public abstract class Conta
    {
        protected DateTime _dataUltimoMovimento;
        protected List<Movimento> _movimentos;
        protected List<Movimento> _movimentosFuturos;
        protected double _saldo;
        protected double _saldoFuturo;

        public Conta(int numeroConta)
        {
            this.Numero = numeroConta;
            this.Titulares = new List<Titular>();
            this._movimentos = new List<Movimento>();
            this._movimentosFuturos = new List<Movimento>();
        }

        protected Conta(
            int numero,
            List<Titular> titulares,
            List<Movimento> movimentos, 
            List<Movimento> movimentosFuturos, 
            double saldo, 
            double saldoFuturo)
        {
            _movimentos = movimentos;
            _movimentosFuturos = movimentosFuturos;
            _saldo = saldo;
            _saldoFuturo = saldoFuturo;
            Numero = numero;
            Titulares = titulares;
        }

        public int Numero { get; protected set; }

        public List<Titular> Titulares { get; protected set; }

        public void AdicionarTitular(Titular titular)
        {
            this.Titulares.Add(titular);
        }

        public bool RemoverTitular(Titular titular)
        {
            // Uma conta não pode ficar sem titular
            if (this.Titulares.Count == 1)
            {
                return false;
            }

            this.Titulares.Remove(titular);
            return true;
        }

        public double ConsultarSaldo()
        {
            return this._saldo;
        }

        public double ConsultarSaldoFuturo()
        {
            return this._saldoFuturo;
        }

        public List<Movimento> ListarMovimentos()
        {
            return this._movimentos.ToList();
        }

        public List<Movimento> ListarMovimentosFuturos()
        {
            return this._movimentosFuturos.ToList();
        }

        protected virtual void AdicionarMovimento(Movimento movimento)
        {
            if (_movimentos.Count >= 50)
            {
                var maisAntigo = this._movimentos
                   .OrderBy(m => m.Data)
                   .First();

                this._movimentos.Remove(maisAntigo);
            }

            this._movimentos.Add(movimento);
            this._dataUltimoMovimento = DateTime.Now;
        }

        public virtual void AdicionarMovimentoFuturo(Movimento movimento)
        {
            // Movimentos futuros tem sempre que ter data superior a atual.
            if (movimento.Data <= DateTime.Now)
            {
                return;
            }

            this._saldoFuturo += movimento.Valor;
            this._movimentosFuturos.Add(movimento);
        }

        public virtual void EfetuarDeposito(double valor)
        {
            _saldo += valor;
            // Movimentos de depósito, tem valor positivo.
            var movimento = new Movimento(TipoMovimento.Deposito, valor, DateTime.Now);
            this.AdicionarMovimento(movimento);
        }

        public virtual bool EfetuarLevantamento(double valor)
        {
            // Só permite levantamento com saldo disponível
            if (this._saldo <= valor)
            {
                return false;
            }

            _saldo -= valor;
            // Movimentos de saída, tem valor negativo.
            var valorNegativo = valor * -1;
            var movimento = new Movimento(TipoMovimento.Levantamento, valorNegativo, DateTime.Now);
            this.AdicionarMovimento(movimento);
            return true;
        }

        public virtual void ReceberTransferencia(double valor)
        {
            _saldo += valor;
            var movimento = new Movimento(TipoMovimento.Transferencia, valor, DateTime.Now);
            this.AdicionarMovimento(movimento);
        }

        public virtual bool EmitirTransferencia(double valor)
        {
            // Só permite saída com saldo disponível
            if (this._saldo <= valor)
            {
                return false;
            }

            // Movimentos de saída, tem valor negativo.
            var valorNegativo = valor * -1;
            var movimento = new Movimento(TipoMovimento.Transferencia, valorNegativo, DateTime.Now);
            this.AdicionarMovimento(movimento);
            return true;
        }

        protected int CalcularDiferencaEmMeses(DateTime inicioPeriodo, DateTime fimPeriodo)
        {
            return ((inicioPeriodo.Year - fimPeriodo.Year) * 12) + inicioPeriodo.Month - fimPeriodo.Month;
        }
    }
}