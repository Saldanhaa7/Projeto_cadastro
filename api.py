from flask import Flask, request
from pymongo import MongoClient


app = Flask(__name__)
@app.route('/cadastro', methods=['POST'])
def cadastro(*args):
    Modelo = request.form['Modelo']
    Marca = request.form['Marca']
    Ano = request.form['Ano']
    Comprador = request.form['Comprador']
    Cpf = request.form['Cpf']
    Nascimento = request.form['Nascimento']
    Compra = request.form['Compra']
    Parcela = request.form['Parcela']
    Valor = request.form['Valor']
    status_financiamento = request.form['Statusdofinanciamento']
    situacao = True
    if all(map(lambda x: x is not None, args)):
        client = MongoClient("localhost", 27017)
        #print(client.list_database_names())

        db = client.Cadastro

        db.carros.insert_many(
        [
        {"modelo" : Modelo},
        {"marca" : Marca},
        {"ano" : Ano},
        {"Comprador" : Comprador},
        {"cpf" : Cpf},
        {"nascimento" : Nascimento},
        {"compra" : Compra},
        {"parcela" : Parcela},
        {"valor" : Valor},
        {"status_financiamento" : status_financiamento}
        ]
        )

        return {'situacao': situacao}
    else:
        situacao = False
        return {'situacao': situacao}




@app.route('/checando_status', methods=['POST'])
def checando_status():
    Comprador = request.form['Comprador']
    status_financiamento = request.form['status_financiamento']
    financiamento_regular = True
    
    if(status_financiamento == "atrasado"):
        status = False
        financiamento_regular = status
        return {'financiamento_regular': financiamento_regular}
    else:
        return {'financiamento_regular': financiamento_regular}
    


if __name__ == '__main__':
    app.run(debug=True)