using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, decimal> catalogo = new Dictionary<string, decimal>
        {
            { "Buque de rosas", 80.00m },
            { "Buque de petalas roxas e brancas", 40.00m },
            { "Buque copo de leite", 60.00m },
            { "Girassol", 15.00m },
            { "Cravo vermelho", 20.00m },
            { "Margarida", 30.00m },
            { "Orquidea", 25.00m },
            { "Margarida amarela", 35.00m },
            { "Conjunto amarelo de flores", 100.00m },
            { "Flor no Pote", 15.00m }
        };

        Console.WriteLine("=====================================================================================================");
        Console.WriteLine("\n                     Bem-vindo ao método de pagamento da loja Florescência!\n");

        // Variável para rastrear o total da compra
        decimal totalCompra = 0m;
        decimal totalFrete = 0m;

        bool continuarComprando = true;

        Dictionary<string, int> carrinho = new Dictionary<string, int>();

        while (continuarComprando)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
            Console.WriteLine("               Por favor, escolha os itens da lista abaixo e a quantidade desejada:\n");

            foreach (var item in catalogo.Keys)
            {
                Console.WriteLine($"{item} - R${catalogo[item]:F2}");
            }

            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
            Console.Write("Item: ");
            string escolha = Console.ReadLine();

            // Procurar o item insensível a maiúsculas/minúsculas
            string itemEncontrado = null;
            foreach (var item in catalogo.Keys)
            {
                if (string.Equals(item, escolha, StringComparison.OrdinalIgnoreCase))
                {
                    itemEncontrado = item;
                    break;
                }
            }

            if (itemEncontrado != null)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.Write("Quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());

                decimal valorItem = catalogo[itemEncontrado];
                decimal totalItens = valorItem * quantidade;

                // Adicione o valor dos itens ao total da compra
                totalCompra += totalItens;

                // Adicione a taxa de frete a cada 100 reais gastos
                if (totalCompra >= 100)
                {
                    decimal taxaFrete = Math.Floor(totalCompra / 100) * 15;
                    totalFrete += taxaFrete;
                }

                // Adicione ao carrinho
                if (carrinho.ContainsKey(itemEncontrado))
                {
                    carrinho[itemEncontrado] += quantidade;
                }
                else
                {
                    carrinho[itemEncontrado] = quantidade;
                }

                Console.Write("\nDeseja inserir mais itens? (S/N): ");
                string resposta = Console.ReadLine();

                switch (resposta.ToUpper())
                {
                    case "S":
                        continuarComprando = true;
                        break;
                    case "N":
                        continuarComprando = false;
                        break;
                    default:
                        Console.WriteLine("Resposta inválida. A compra será finalizada.");
                        continuarComprando = false;
                        break;
                }
            }
            else
            {
                Console.WriteLine("\n*** Item não encontrado no catálogo, voltando à tela de seleção de itens. ***");
            }
        }

        // Remover itens do carrinho
        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        Console.Write("\nDeseja retirar algum item do carrinho? (S/N): ");
        string retirarItemResposta = Console.ReadLine();

        if (retirarItemResposta.ToUpper() == "S")
        {
            while (true)
            {
                Console.WriteLine("\nItens no carrinho:");
                foreach (var item in carrinho)
                {
                    Console.WriteLine($"{item.Key} - Quantidade: {item.Value}");
                }

                Console.Write("\nInforme o nome do item que deseja retirar (ou digite 'S' para sair): ");
                string itemARetirar = Console.ReadLine();

                if (itemARetirar.ToUpper() == "S")
                {
                    break;
                }

                if (carrinho.ContainsKey(itemARetirar))
                {
                    Console.Write("Quantidade a ser retirada: ");
                    int quantidadeARetirar = int.Parse(Console.ReadLine());

                    if (quantidadeARetirar >= carrinho[itemARetirar])
                    {
                        totalCompra -= catalogo[itemARetirar] * carrinho[itemARetirar];
                        carrinho.Remove(itemARetirar);
                    }
                    else
                    {
                        carrinho[itemARetirar] -= quantidadeARetirar;
                        totalCompra -= catalogo[itemARetirar] * quantidadeARetirar;
                    }
                }
                else
                {
                    Console.WriteLine("Item não encontrado no carrinho.");
                }
            }
        }

        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        // Solicitar código promocional
        Console.Write("\nInforme o código promocional para 10% de desconto (ou deixe em branco): ");
        string codigoPromocional = Console.ReadLine();

        decimal desconto = 0m;

        // Aplicar desconto se o código promocional for "QUEROFLORES"
        if (codigoPromocional != null && codigoPromocional.Equals("queroflores", StringComparison.OrdinalIgnoreCase))
        {
            desconto = totalCompra * 0.1m; // Aplicar desconto de 10%
        }

        decimal totalComDesconto = totalCompra + totalFrete - desconto;

        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        Console.Write("\nInforme o método de pagamento,\n\n [1] débito\n [2] crédito\n [3] PIX\n\nInsira o número correspondente: ");
        string metodoPagamento = Console.ReadLine();

        string metodo = "";
        string nomes = "";

        switch (metodoPagamento)
        {
            case "1":
                metodo = "Débito";
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine("Informe seu nome completo: ");
                string nome = Console.ReadLine();

                Console.WriteLine("\nInforme a bandeira do cartão: ");
                string cartao3 = Console.ReadLine();

                Console.WriteLine("\nInforme o número do cartão: ");
                string numCartao = Console.ReadLine();

                Console.WriteLine("Insira o CVV: ");
                string cvv = Console.ReadLine();

                metodo = "Crédito - Bandeira: " + cartao3;

                nomes = nome;

                break;
            case "2":
                {

                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Informe seu nome completo: ");
                    string nome2 = Console.ReadLine();

                    Console.WriteLine("\nInforme a bandeira do cartão: ");
                    string cartao2 = Console.ReadLine();

                    Console.WriteLine("\nInforme o número do cartão: ");
                    string numCartao2 = Console.ReadLine();

                    Console.WriteLine("Insira o CVV: ");
                    string cvv2 = Console.ReadLine();

                    metodo = "Crédito - Bandeira: " + cartao2;

                    nomes = nome2;
                }

                break;

            case "3":
                {
                    string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    Random random = new Random();
                    string chavePix = new string(Enumerable.Repeat(caracteres, 32)
                                                         .Select(s => s[random.Next(s.Length)])
                                                         .ToArray());

                    metodo = "PIX - Chave: " + chavePix;
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Informe seu nome completo: ");
                    string nome3 = Console.ReadLine();

                    Console.WriteLine("\nInforme o número do seu CPF: ");
                    string numCartao3 = Console.ReadLine();

                    nomes = nome3;
                }

                break;
        }

        Console.WriteLine("\n============================================ * RECIBO * ============================================\n");
        Console.WriteLine($"Método de pagamento selecionado: {metodo}");
        Console.WriteLine($"\nValor dos itens: R${totalCompra:F2}");
        Console.WriteLine($"Total de frete (R$15 a cada R$100 em compras): R${totalFrete:F2}");
        Console.WriteLine($"Desconto aplicado: R${desconto:F2}");
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------");
        Console.WriteLine($"                                   Valor total a pagar: R${totalComDesconto:F2}");
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        Console.WriteLine("\nCompra finalizada. Obrigado(a) " + nomes + " por escolher nossas flores!\n");

        // Aguarde a entrada do usuário antes de fechar o aplicativo
        Console.WriteLine("*** Pressione qualquer tecla para sair... ***");
        Console.ReadKey();
    }
}
