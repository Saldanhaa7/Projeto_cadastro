// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        List<Carro> carros = new List<Carro>();

        while (true)
        {
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Cadastrar um carro");
            Console.WriteLine("2 - Consultar informações e ficha de um comprador");
            Console.WriteLine("3 - Sair");

            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                Console.WriteLine("Digite o modelo do carro:");
                string modelo = Console.ReadLine();

                Console.WriteLine("Digite a marca do carro:");
                string marca = Console.ReadLine();

                Console.WriteLine("Digite o ano do carro:");
                int ano = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o nome do comprador:");
                string comprador = Console.ReadLine();

                Console.WriteLine("Digite o CPF do comprador:");
                string cpf = Console.ReadLine();

                Console.WriteLine("Digite a data de nascimento (DD/MM/AAAA) do comprador:");
                string aniversario = Console.ReadLine();
                DateTime dt;
                while (!DateTime.TryParseExact(aniversario, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    Console.WriteLine("Data invalida, tente novamente:");
                    aniversario = Console.ReadLine();
                }

                Console.WriteLine("Digite o valor da parcela:");
                double valor = double.Parse(Console.ReadLine());

                Console.WriteLine("Digite em quantas vezes foi parcelado:");
                int parcela = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite a data de compra (DD/MM/AAAA) do carro:");
                string compra = Console.ReadLine();
                DateTime dt2;
                while (!DateTime.TryParseExact(compra, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt2))
                {
                    Console.WriteLine("Data invalida, tente novamente:");
                    compra = Console.ReadLine();
                }

                Console.WriteLine("Digite Status (em dia ou atrasado) do financiamento:");
                string statusdofinanciamento = Console.ReadLine();

                Carro carro = new Carro(modelo, ano, comprador, cpf, aniversario, compra, parcela, valor, statusdofinanciamento);
                carros.Add(carro);

                Console.WriteLine("Carro cadastrado com sucesso!");
            }
            else if (opcao == 2)
            {
                Console.WriteLine("Digite o nome do comprador:");
                string comprador = Console.ReadLine();

                Carro carro = carros.Find(c => c.Comprador == comprador);

                if (carro == null)
                {
                    Console.WriteLine("Comprador não encontrado!");
                }
                else
                {
                    Console.WriteLine($"Modelo: {carro.Modelo}");
                    Console.WriteLine($"Ano: {carro.Ano}");
                    Console.WriteLine($"Comprador: {carro.Comprador}");
                    Console.WriteLine($"CPF: {carro.Cpf}");
                    Console.WriteLine($"Nascimento: {carro.Aniversario}");
                    Console.WriteLine($"Compra: {carro.Compra}");
                    Console.WriteLine($"Valor: {carro.Valor}");
                    Console.WriteLine($"Parcelas: {carro.Parcela}");
                    Console.WriteLine($"Status: {carro.Statusdofinanciamento}");

                    // Verifica a regularidade do financiamento
                    PythonAPI api = new PythonAPI();
                    bool financiamento = api.status(carro.Comprador, carro.Statusdofinanciamento);

                    if (financiamento)
                    {
                        Console.WriteLine("Financiamento regular");
                    }
                    else
                    {
                        Console.WriteLine("Financiamento irregular");
                    }
                }
            }
            else if (opcao == 3)
            {
                break;
            }
        }
    }
}

class PythonAPI
{
    public bool status(string comprador, string statusdofinanciamento)
    {
        string url = "http://localhost:5000/checando_status";
        string dados = $"Comprador={comprador}&status_financiamento={statusdofinanciamento}";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = dados.Length;

        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(dados);
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string Body = "";

        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            Body = reader.ReadToEnd();
        }

        // Converte o resultado da API de JSON para um objeto dinâmico
        dynamic resultado = JsonConvert.DeserializeObject(Body);

        return (bool)resultado.financiamento_regular;
    }
}