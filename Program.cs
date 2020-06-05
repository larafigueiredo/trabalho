using System;

namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var banco = new Banco();
            var titular1 = new Titular();
            
            titular1.DefinirNome("Banana");
            titular1.DefinirNif(123456789);
            titular1.DefinirDataNascimento(new DateTime(2000, 10, 10));
            titular1.DefinirGenero(Titular.EnumSexo.Feminino);
            
            Console.WriteLine("Titular1.Nome: {0}", titular1.Nome);
            Console.WriteLine("Titular1.Nif: {0}", titular1.Nif);
            Console.WriteLine("Titular1.DataNascimento: {0}", titular1.DataNascimento);
            Console.WriteLine("Titular1.Genero: {0}", titular1.Genero);

            titular1 = banco.CriarTitular(titular1);
            if (titular1 == null)
            {
                Console.WriteLine("número máximo de títulares atingido.");
            }

            var conta1 = new Conta();

            conta1.DefinirNumero(1);
            conta1.AdicionarTitular(titular1);

            Console.WriteLine("Conta.número: {0}", conta1.Numero);
            Console.WriteLine("titulares:");
            foreach (var item in conta1.Titulares)
            {
                Console.WriteLine(item.Nome);
            }

            conta1 = banco.CriarConta(conta1);
            if (conta1 == null)
            {
                Console.WriteLine("número máximo de contas atingido.");
            }

            for (int i = 0; i < 52; i++)
            {
                conta1.AdicionarMovimento(1);
                Console.WriteLine("Saldo: {0}", conta1.ConsultarSaldo());
                Console.WriteLine("Tamanho movimentos: {0}", conta1.ListarMovimentos().Count);
            }
            */

            // ----------------------------------------------------------------------

            
            var banco = new Banco();
            var titular1 = new Titular();

            titular1.DefinirNome("Banana");
            titular1.DefinirNif(123456789);
            titular1.DefinirDataNascimento(new DateTime(2000, 10, 10));
            titular1.DefinirGenero(Titular.EnumSexo.Feminino);

            Console.WriteLine("Titular1.Nome: {0}", titular1.Nome);
            Console.WriteLine("Titular1.Nif: {0}", titular1.Nif);
            Console.WriteLine("Titular1.DataNascimento: {0}", titular1.DataNascimento);
            Console.WriteLine("Titular1.Genero: {0}", titular1.Genero);

            titular1 = banco.CriarTitular(titular1);
            if (titular1 == null)
            {
                Console.WriteLine("número máximo de títulares atingido.");
            }

            var conta1 = new ContaPrazo();

            conta1.DefinirNumero(1123);
            Console.WriteLine("O NUMERO EEEEEEEEEEEEEEE " + conta1._numero + " DASDASD");

            conta1.AdicionarTitular(titular1);

            Console.WriteLine("Conta.número: {0}", conta1._numero);
            Console.WriteLine("titulares:");
            foreach (var item in conta1.Titulares)
            {
                Console.WriteLine(item.Nome);
            }

            conta1 = banco.CriarContaPrazo(conta1);
            if (conta1 == null)
            {
                Console.WriteLine("número máximo de contas atingido.");
            }

            for (int i = 0; i < 52; i++)
            {
                conta1.AdicionarMovimento(1);
                Console.WriteLine("Saldo: {0}", conta1.ConsultarSaldo());
                Console.WriteLine("Tamanho movimentos: {0}", conta1.ListarMovimentos().Count);
            }

            conta1.Deposito(1000);
            conta1.Levantar(15);
            conta1.AdicionarRendimento();
            conta1.ConsultarSaldo();
            Console.WriteLine(conta1.ConsultarSaldo());
            

            //------------------------------------------------------------------
            /*
            var banco = new Banco();
            var titular1 = new Titular();

            titular1.DefinirNome("POUPANÇA");
            titular1.DefinirNif(123456789);
            titular1.DefinirDataNascimento(new DateTime(2000, 10, 10));
            titular1.DefinirGenero(Titular.EnumSexo.Feminino);

            Console.WriteLine("Titular1.Nome: {0}", titular1.Nome);
            Console.WriteLine("Titular1.Nif: {0}", titular1.Nif);
            Console.WriteLine("Titular1.DataNascimento: {0}", titular1.DataNascimento);
            Console.WriteLine("Titular1.Genero: {0}", titular1.Genero);

            titular1 = banco.CriarTitular(titular1);
            if (titular1 == null)
            {
                Console.WriteLine("número máximo de títulares atingido.");
            }

            var conta1 = new ContaPoupanca();

            conta1.DefinirNumero(1123);
            Console.WriteLine("O NUMERO EEEEEEEEEEEEEEE " + conta1._numero + " DASDASD");

            conta1.AdicionarTitular(titular1);

            Console.WriteLine("Conta.número: {0}", conta1._numero);
            Console.WriteLine("titulares:");
            foreach (var item in conta1.Titulares)
            {
                Console.WriteLine(item.Nome);
            }

            conta1 = banco.CriarContaPoupanca(conta1);
            if (conta1 == null)
            {
                Console.WriteLine("número máximo de contas atingido.");
            }

            for (int i = 0; i < 52; i++)
            {
                conta1.AdicionarMovimento(1);
                Console.WriteLine("Saldo: {0}", conta1.ConsultarSaldo());
                Console.WriteLine("Tamanho movimentos: {0}", conta1.ListarMovimentos().Count);
            }

            conta1.Deposito(1000);
            conta1.Levantar(15);
            conta1.AdicionarRendimento();
            conta1.ConsultarSaldo();
            Console.WriteLine(conta1.ConsultarSaldo());
            */

        }
    }
}
