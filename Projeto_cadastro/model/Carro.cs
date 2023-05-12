using System;
using System.IO;
using System.Net;

class Carro
{
    public string Modelo { get; set; }
    public string Marca { get; set; }
    public int Ano { get; set; }
    public string Comprador { get; set; }
    public string Cpf { get; set; }
    public string Aniversario { get; set; }
    public string Compra { get; set; }
    public int Parcela { get; set; }
    public double Valor { get; set; }
    public string Statusdofinanciamento { get; set; }

    public Carro(string modelo, string marca, int ano, string comprador, string cpf, string aniversario, string compra, int parcela, double valor, string statusdofinanciamento)
    {
        Modelo = modelo;
        Marca = marca;
        Ano = ano;
        Comprador = comprador;
        Cpf = cpf;
        Aniversario = aniversario;
        Compra = compra;
        Parcela = parcela;
        Valor = valor;
        Statusdofinanciamento = statusdofinanciamento;
    }
}