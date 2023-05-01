from flask import Flask, request
app = Flask(__name__)

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