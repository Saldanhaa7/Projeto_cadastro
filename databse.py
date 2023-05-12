from flask import Flask, request
from pymongo import MongoClient
import api

client = MongoClient("localhost", 27017)
#print(client.list_database_names())

db = client.Cadastro

db.carros.insert_many(
    [
        {"modelo" : api.Modelo},
        {"marca" : api.Marca},
        {"ano" : api.Ano},
        {"Comprador" : api.Comprador},
        {"cpf" : api.Cpf},
        {"nascimento" : api.Nascimento},
        {"compra" : api.Compra},
        {"parcela" : api.Parcela},
        {"valor" : api.Valor},
        {"status_financiamento" : api.Statusdofinanciamento}
    ]
        
    
)