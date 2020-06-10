using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SistemaBancario.Armazenamento;

namespace SistemaBancario.Model
{
    internal class Program
    {
        private static Banco banco;

        private static void Main(string[] args)
        {
            banco = new Banco();

            Inicializar();

            int opcao;
            do
            {
                LimparTela();
                ImprimirMenuPrincipal();

                var escolha = Console.ReadLine();
                if (int.TryParse(escolha, out opcao))
                {
                    switch (opcao)
                    {
                        case 0:
                            break;

                        case 1:
                            ListarTitulares();
                            break;

                        case 2:
                            CadastrarTitular();
                            break;

                        case 3:
                            ListarContas();
                            break;

                        case 4:
                            CadastrarConta();
                            break;
                        case 5:
                            EditarConta();
                            break;

                        default:
                            ExibirMensagemOpcaoInvalida();
                            break;
                    }
                }
                else
                {
                    ExibirMensagemOpcaoInvalida();
                }
            } while (opcao != 0);

            Salvar();
        }

        private static void EditarConta()
        {
            LimparTela();
            Console.WriteLine("Edição de contas");

            var conta = LerConta();
            if (conta == null)
            {
                return;
            }

            var opcao = 0;
            do
            {
                LimparTela();

                Console.WriteLine("------Dados da Conta----");
                Console.WriteLine("Numero: {0}", conta.Numero);
                Console.WriteLine("Titulares: {0}", string.Join(",", conta.Titulares.Select(t => t.Nome)));
                Console.WriteLine("Saldo: {0}", conta.ConsultarSaldo());
                Console.WriteLine("Saldo Futuro: {0}", conta.ConsultarSaldoFuturo());
                Console.WriteLine("-------Extrato----------");
                foreach (var item in conta.ListarMovimentos())
                {
                    Console.WriteLine("Data: {0} | Tipo: {1} | Valor: {2}", item.Data, item.Tipo.ToString(), item.Valor);
                }
                Console.WriteLine("------------------------");

                Console.WriteLine("------------------------");

                Console.WriteLine("Operações:");
                Console.WriteLine("1 - Adicionar Titular");
                Console.WriteLine("2 - Remover Titular");
                Console.WriteLine("3 - Deposito");
                Console.WriteLine("4 - Levantamento");

                if (conta is ContaPrazo contaPrazo || conta is ContaPoupanca contaPoupanca)
                {
                    Console.WriteLine("5 - Calcular rendimentos");
                }

                Console.WriteLine("0 - Sair");

                opcao = LerInteiro();

                switch (opcao)
                {
                    case 1: // Addicionar titular
                        var titularNovo = LerTitular();
                        if (titularNovo != null)
                        {
                            conta.AdicionarTitular(titularNovo);
                            Console.WriteLine("Titular Adicionado!");
                        }
                        break;
                    case 2: // Remover Titular
                        var titularAntigo = LerTitular();
                        if (titularAntigo != null)
                        {
                            if (conta.RemoverTitular(titularAntigo))
                            {
                                Console.WriteLine("Titular removido!");
                            }
                            else
                            {
                                Console.WriteLine("Não foi possível remover titular!");
                            }
                        }
                        break;
                    case 3: // Deposito
                        Console.WriteLine("Digite o valor:");
                        var valorDeposito = LerDouble();
                        if (valorDeposito != 0)
                        {
                            conta.EfetuarDeposito(valorDeposito);
                            Console.WriteLine("Deposito realizado!");
                        }
                        break;
                    case 4: // Levantamento
                        Console.WriteLine("Digite o valor:");
                        var valorLevantamento = LerDouble();
                        if (valorLevantamento != 0)
                        {
                            conta.EfetuarLevantamento(valorLevantamento);
                            Console.WriteLine("Levantamento realizado!");
                        }
                        break;
                    case 5: // Rendimentos
                        if (conta is ContaPrazo prazo)
                        {
                            prazo.ProcessarRendimento();
                            Console.WriteLine("Rendimentos calculados!");
                        }
                        else if( conta is ContaPoupanca poupanca)
                        {
                            poupanca.ProcessarRendimentos();
                            Console.WriteLine("Rendimentos calculados!");
                        }
                        break;
                }
                ExibirDigiteParaContinuar();
            } while (opcao != 0);

            Console.ReadKey();
        }

        private static double LerDouble()
        {
            var escolha = Console.ReadLine().Trim();

            if (double.TryParse(escolha, out var numero) && numero >= 0)
            {
                return numero;
            }
            else
            {
                Console.WriteLine("Número inválido, tente novamente, ou digite 0 para cancelar");
                return LerDouble();
            }
        }

        private static void RemoverTitular()
        {
            throw new NotImplementedException();
        }

        private static void Inicializar()
        {
            var pasta = Directory.GetCurrentDirectory();
            var ficheiroTitulares = Path.Combine(pasta, "titulares.json");
            if (File.Exists(ficheiroTitulares))
            {
                var titularesJson = File.ReadAllText(ficheiroTitulares);
                var titulares = JsonConvert.DeserializeObject<List<TitularSalvo>>(titularesJson);

                foreach (var item in titulares)
                {
                    banco.AdicionarTitular(new Titular(
                        item.Nome,
                        item.Nif,
                        item.DataNascimento,
                        item.Genero,
                        item.Morada));
                }
            }

            var listaContas = new List<ContaSalva>();
            var ficheiroContasOrdem = Path.Combine(pasta, "contasOrdem.json");
            if (File.Exists(ficheiroContasOrdem))
            {
                var json = File.ReadAllText(ficheiroContasOrdem);
                var lista = JsonConvert.DeserializeObject<List<ContaOrdemSalva>>(json);
                listaContas.AddRange(lista);
            }

            var ficheiroContasPrazo = Path.Combine(pasta, "contasPrazo.json");
            if (File.Exists(ficheiroContasPrazo))
            {
                var json = File.ReadAllText(ficheiroContasPrazo);
                var lista = JsonConvert.DeserializeObject<List<ContaPrazoSalva>>(json);
                listaContas.AddRange(lista);
            }

            var ficheiroContasPoupanca = Path.Combine(pasta, "contasPoupanca.json");
            if (File.Exists(ficheiroContasPoupanca))
            {
                var json = File.ReadAllText(ficheiroContasPoupanca);
                var lista = JsonConvert.DeserializeObject<List<ContaPoupancaSalva>>(json);
                listaContas.AddRange(lista);
            }

            listaContas = listaContas.OrderBy(c => c.Numero).ToList();

            foreach (var item in listaContas)
            {
                var titulares = banco.Titulares
                    .Where(t => item.Titulares.Contains(t.Nif))
                    .ToList();

                var movimentos = TransformarMovimentos(item.Movimentos);
                var movimentosFuturos = TransformarMovimentos(item.MovimentosFuturos);

                if (item is ContaPrazoSalva contaPrazo)
                {
                    banco.AdicionarConta(new ContaPrazo(
                        item.Numero,
                        titulares,
                        movimentos,
                        movimentosFuturos,
                        item.Saldo,
                        item.SaldoFuturo,
                        contaPrazo.DataDeDisponibilizacaoDeSaque,
                        contaPrazo.DataUltimoAniversario,
                        contaPrazo.TaxaDeRendimento
                        ));
                }
                else if (item is ContaPoupancaSalva contaPoupanca)
                {
                    banco.AdicionarConta(new ContaPoupanca(
                        item.Numero,
                        titulares,
                        movimentos,
                        movimentosFuturos,
                        item.Saldo,
                        item.SaldoFuturo,
                        contaPoupanca.IntervaloAniversarioEmMeses,
                        contaPoupanca.DataUltimoAniversario,
                        contaPoupanca.TaxaDeRendimento
                        ));
                }
                else if (item is ContaOrdemSalva)
                {
                    banco.AdicionarConta(new ContaOrdem(
                        item.Numero,
                        titulares,
                        movimentos,
                        movimentosFuturos,
                        item.Saldo,
                        item.SaldoFuturo
                        ));
                }
            }
        }

        private static List<Movimento> TransformarMovimentos(List<MovimentoSalvo> movimentos)
        {
            return movimentos.Select(m => new Movimento(m.Tipo, m.Valor, m.Data)).ToList();
        }

        private static void Salvar()
        {
            var pasta = Directory.GetCurrentDirectory();
            SalvarTitulares(pasta);
            SalvarContas(pasta);
        }

        private static void SalvarTitulares(string pasta)
        {
            var titularesParaSalvar = banco.Titulares.Select(t => new TitularSalvo
            {
                Nif = t.Nif,
                Nome = t.Nome,
                DataNascimento = t.DataNascimento,
                Genero = t.Genero,
                Morada = t.Morada
            }).ToList();

            var jsonTitulares = JsonConvert.SerializeObject(
                titularesParaSalvar,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.Indented
                }
            );

            File.WriteAllText(Path.Combine(pasta, "titulares.json"), jsonTitulares);
        }

        private static void SalvarContas(string pasta)
        {
            var contasOrdem = new List<ContaOrdemSalva>();
            var contasPoupanca = new List<ContaPoupancaSalva>();
            var contasPrazo = new List<ContaPrazoSalva>();

            foreach (var conta in banco.Contas)
            {
                var titulares = conta.Titulares
                    .Select(t => t.Nif)
                    .ToList();

                var movimentos = conta.ListarMovimentos()
                    .Select(m => new MovimentoSalvo
                    {
                        Data = m.Data,
                        Tipo = m.Tipo,
                        Valor = m.Valor
                    })
                    .ToList();

                var movimentosFuturos = conta.ListarMovimentosFuturos()
                    .Select(m => new MovimentoSalvo
                    {
                        Data = m.Data,
                        Tipo = m.Tipo,
                        Valor = m.Valor
                    })
                    .ToList();

                if (conta is ContaPrazo contaPrazo)
                {
                    contasPrazo.Add(new ContaPrazoSalva
                    {
                        Numero = conta.Numero,
                        Saldo = conta.ConsultarSaldo(),
                        SaldoFuturo = conta.ConsultarSaldoFuturo(),
                        Titulares = titulares,
                        Movimentos = movimentos,
                        MovimentosFuturos = movimentosFuturos,
                        DataDeDisponibilizacaoDeSaque = contaPrazo.DataDeDisponibilizacaoDeSaque,
                        DataUltimoAniversario = contaPrazo.DataUltimoAniversario,
                        TaxaDeRendimento = contaPrazo.TaxaDeRendimento
                    });
                }
                else if (conta is ContaPoupanca contaPoupanca)
                {
                    contasPoupanca.Add(new ContaPoupancaSalva
                    {
                        Numero = conta.Numero,
                        Saldo = conta.ConsultarSaldo(),
                        SaldoFuturo = conta.ConsultarSaldoFuturo(),
                        Titulares = titulares,
                        Movimentos = movimentos,
                        MovimentosFuturos = movimentosFuturos,
                        DataUltimoAniversario = contaPoupanca.DataUltimoAniversario,
                        IntervaloAniversarioEmMeses = contaPoupanca.IntervaloAniversarioEmMeses,
                        TaxaDeRendimento = contaPoupanca.TaxaDeRendimento
                    });
                }
                else
                {
                    contasOrdem.Add(new ContaOrdemSalva
                    {
                        Numero = conta.Numero,
                        Saldo = conta.ConsultarSaldo(),
                        SaldoFuturo = conta.ConsultarSaldoFuturo(),
                        Titulares = titulares,
                        Movimentos = movimentos,
                        MovimentosFuturos = movimentosFuturos
                    });
                }
            }

            var jsonContasOrdem = JsonConvert.SerializeObject(contasOrdem, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            });

            var jsonContasPrazo = JsonConvert.SerializeObject(contasPrazo, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            });

            var jsonContasPoupanca = JsonConvert.SerializeObject(contasPoupanca, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            });

            File.WriteAllText(Path.Combine(pasta, "contasOrdem.json"), jsonContasOrdem);
            File.WriteAllText(Path.Combine(pasta, "contasPrazo.json"), jsonContasPrazo);
            File.WriteAllText(Path.Combine(pasta, "contasPoupanca.json"), jsonContasPoupanca);
        }

        private static void LimparTela()
        {
            Console.Clear();
            ImprimirTitulo();
        }

        private static void ExibirMensagemOpcaoInvalida()
        {
            Console.WriteLine("Opção inválida, tente novamente...");
            ExibirDigiteParaContinuar();
        }

        private static void ImprimirMenuPrincipal()
        {
            Console.WriteLine("Escolha a opção desejada:");
            Console.WriteLine("1 - Listar titulares");
            Console.WriteLine("2 - Cadastrar titular");
            Console.WriteLine("3 - Listar contas");
            Console.WriteLine("4 - Cadastrar conta");
            Console.WriteLine("5 - Editar conta");

            Console.WriteLine("0 - Sair");
        }

        private static void ImprimirTitulo()
        {
            Console.WriteLine("############# BANCO DOS RICOS ###########");
        }

        private static void ExibirDigiteParaContinuar()
        {
            Console.WriteLine("(Aperte uma tecla para continuar )");
            Console.ReadKey();
        }

        private static void ListarTitulares()
        {
            LimparTela();
            Console.WriteLine("Lista de titulares");

            if (banco.Titulares.Count > 0)
            {
                foreach (var titular in banco.Titulares)
                {
                    Console.WriteLine("Nif: {0} | Nome: {1}", titular.Nif, titular.Nome);
                }
            }
            else
            {
                Console.WriteLine("Nenhum titular está cadastrado");
            }

            ExibirDigiteParaContinuar();
        }

        private static void CadastrarTitular()
        {
            LimparTela();
            Console.WriteLine("Cadastro de titulares");

            Console.Write("Nif: ");
            var nif = LerInteiro();
            if (nif == 0)
            {
                return;
            }

            Console.Write("Gênero( M/F ) : ");
            var textoGenero = LerOpcao("M", "F");
            if (textoGenero == null)
            {
                return;
            }
            var genero = textoGenero.ToUpper() == "M" ? Sexo.Masculino : Sexo.Feminino;

            Console.Write("Nome: ");
            var nome = LerTexto();
            if (nome == null)
            {
                return;
            }

            Console.Write("Data Nascimento (DD/MM/AAAA): ");
            var dataNascimento = LerData();
            if (dataNascimento == DateTime.MinValue)
            {
                return;
            }

            Console.Write("Morada: ");
            var morada = LerTexto();
            if (morada == null)
            {
                return;
            }

            var titular = new Titular(nome, nif, dataNascimento, genero, morada);

            if (banco.CriarTitular(titular))
            {
                Console.WriteLine("Titular criado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível criar usuário!");
            }

            ExibirDigiteParaContinuar();
        }

        private static void ListarContas()
        {
            LimparTela();
            Console.WriteLine("Lista de contas");

            if (banco.Contas.Count > 0)
            {
                foreach (var conta in banco.Contas)
                {
                    Console.WriteLine(
                        "Numero: {0} | Tipo: {1} | Saldo: {2}",
                        conta.Numero,
                        conta.GetType().Name,
                        conta.ConsultarSaldo());
                }
            }
            else
            {
                Console.WriteLine("Nenhuma conta está cadastrada");
            }

            ExibirDigiteParaContinuar();
        }

        private static void CadastrarConta()
        {
            LimparTela();
            Console.WriteLine("Cadastro de contas");

            var titular = LerTitular();
            if (titular == null)
            {
                return;
            }

            Console.WriteLine("Qual o tipo de conta (O = Order, P = Poupanca, Z = Prazo) :");
            var opcao = LerOpcao("O", "P", "Z");
            if (opcao == null)
            {
                return;
            }

            Conta conta = null;

            switch (opcao)
            {
                case "O":
                    conta = banco.CriarContaAOrdem(titular);
                    break;
                case "P":
                    conta = banco.CriarContaPoupanca(titular);
                    break;
                case "Z":
                    conta = banco.CriarContaPrazo(titular);
                    break;
            }

            if (conta != null)
            {
                Console.WriteLine("Conta {0} criada com sucesso!", conta.Numero);
            }
            else
            {
                Console.WriteLine("Não foi possível criar Conta!");
            }

            ExibirDigiteParaContinuar();
        }

        private static DateTime LerData()
        {
            var texto = Console.ReadLine().Trim();

            if (texto == "0")
            {
                return DateTime.MinValue;
            }

            if (DateTime.TryParse(texto, out var data))
            {
                return data;
            }
            else
            {
                Console.WriteLine("Número inválido, tente novamente, ou digite 0 para cancelar");
                return LerData();
            }
        }

        private static string LerTexto()
        {
            var escolha = Console.ReadLine().Trim();

            if (escolha == "0")
            {
                return null;
            }

            if (escolha != "")
            {
                return escolha;
            }

            Console.WriteLine("Opção inválida, tente novamente, ou digite 0 para cancelar");
            return LerTexto();
        }

        private static string LerOpcao(params string[] valoresPossiveis)
        {
            var escolha = Console.ReadLine().Trim().ToUpper();

            if (escolha == "0")
            {
                return null;
            }

            if (valoresPossiveis.Any(v => v.ToUpper() == escolha))
            {
                return escolha;
            }

            Console.WriteLine("Opção inválida, tente novamente, ou digite 0 para cancelar");
            return LerOpcao(valoresPossiveis);
        }

        private static int LerInteiro()
        {
            var escolha = Console.ReadLine().Trim();

            if (int.TryParse(escolha, out var numero) && numero >= 0)
            {
                return numero;
            }
            else
            {
                Console.WriteLine("Número inválido, tente novamente, ou digite 0 para cancelar");
                return LerInteiro();
            }
        }

        private static Titular LerTitular()
        {
            Console.WriteLine("Digite a Nif do titular: ");
            var escolha = Console.ReadLine().Trim();

            if (int.TryParse(escolha, out var numero))
            {
                if (numero == 0)
                {
                    return null;
                }
                var titular = banco.Titulares.FirstOrDefault(t=> t.Nif == numero);
                if (titular != null)
                {
                    Console.WriteLine("Nome titular: {0}", titular.Nome);
                    return titular;
                }
            }

            Console.WriteLine("NIF inválido, tente novamente, ou digite 0 para cancelar");
            return LerTitular();
        }

        private static Conta LerConta()
        {
            Console.WriteLine("Digite a número da conta: ");
            var escolha = Console.ReadLine().Trim();

            if (int.TryParse(escolha, out var numero))
            {
                if (numero == 0)
                {
                    return null;
                }
                var conta = banco.Contas.FirstOrDefault(t => t.Numero == numero);
                if (conta != null)
                {
                    return conta;
                }
            }

            Console.WriteLine("Número inválido, tente novamente, ou digite 0 para cancelar");
            return LerConta();
        }
    }
}