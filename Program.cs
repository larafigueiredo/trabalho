using System;

namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {
          //testando titulares
            var banco = new Banco();
            var titular1 = new Titular();
            var conta = new Conta();
                        
            titular1.DefinirNome("Banana");
            titular1.DefinirNif(123456789);
            titular1.DefinirDataNascimento(new DateTime(2000, 10, 10));
            titular1.DefinirGenero(Titular.Sexo.Feminino);
            titular1.DefinirResidencia("rua dos bobos, numero 0");

            Console.WriteLine("Titular1.Nome: {0}", titular1.Nome);
            Console.WriteLine("Titular1.Nif: {0}", titular1.Nif);
            Console.WriteLine("Titular1.DataNascimento: {0}", titular1.DataNascimento);
            Console.WriteLine("Titular1.Genero: {0}", titular1.Genero);
            Console.WriteLine("Titular1.Morada: {0}", titular1.Morada);

            //testando banco

            titular1 = banco.CriarTitular(titular1);
            if (titular1 == null)
            {
                Console.WriteLine("número máximo de títulares atingido.");
            }
            else
            {
                Console.WriteLine("Possivel adicionar titular.");
            }
           
            Console.WriteLine("Conta.número: {0}", conta.Numero);
            Console.WriteLine("titulares:");
            foreach (var item in conta.Titulares)
            {
                Console.WriteLine(item.Nome);
            }
            
            conta = banco.CriarConta(conta);
            if (conta == null)
            {
                Console.WriteLine("número máximo de contas atingido.");
            }
            else
            {
                Console.WriteLine("Possivel avançar com criação de conta");
            }

            /*for (int i = 0; i < 52; i++)
            {
                conta1.AdicionarMovimento(1);
                Console.WriteLine("Saldo: {0}", conta1.ConsultarSaldo());
                Console.WriteLine("Tamanho movimentos: {0}", conta1.ListarMovimentos().Count);
            }*/
          
            
            
        }

        void TestarConta(Conta conta)// consultar saldo,  registrar movimento,  listar movimentos, listas lançamentos futuros, listar titulares, listar contas
        {
                       
            //consultar saldo
            var saldo = conta.ConsultarSaldo();
            
                Console.WriteLine("O saldo é de {0}", saldo);                    

            //registrar movimento
            //listar movimento
            //listar lançamentos futuros
            //listar titulares
            //listar contas
        }
    }
}
