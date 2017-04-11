import os
import random
from flask import Flask
from flask import request
from models import *
from flask import request, jsonify, make_response, abort, render_template
import requests
import os
from playhouse.shortcuts import *
from models import *
from flask import Flask
from flask import request, jsonify, make_response, abort, render_template
import grequests

app = Flask(__name__)


def init():
    try:
        Clients.create_table()
    except OperationalError:
        return 'failed'
    except ProgrammingError:
        return 'failed'

    try:
        Firms.create_table()
    except OperationalError:
        return 'failed'
    except ProgrammingError:
        return 'failed'

    try:
        SMSProvider.create_table()
        SMSProvider.create(Name='smsc.ru', Login='Debian17', Password='qwerty12')
    except OperationalError:
        return 'failed'
    except ProgrammingError:
        return 'failed'

    try:
        TempCode.create_table()
    except OperationalError:
        return 'failed'
    except ProgrammingError:
        return 'failed'


def get_rand():
    return random.randint(10000, 99999)


def create_response(PhoneNumber, Code):
    baseURL = 'http://smsc.ru/sys/send.php?login=Debian17&psw=qwerty12&charset=utf-8'
    baseURL += '&phones='
    baseURL += PhoneNumber
    baseURL += '&mes=Ваш код активации:'
    baseURL += str(Code)
    return baseURL


@app.route('/api/reg_code', methods=['POST'])
@db_session
def generate_reg_code():
    new_code = get_rand()
    if TempCode.select().where(TempCode.PhoneNumber == request.args.get('PhoneNumber')).exists():
        query = TempCode.update(Code=new_code).where(TempCode.PhoneNumber == request.args.get('PhoneNumber'))
        query.execute()
        r = create_response(request.args.get('PhoneNumber'), new_code)
        urls = [r]
        rs = (grequests.post(u) for u in urls)
        grequests.map(rs)
        return make_response(jsonify(True), 200)
    else:
        code = TempCode.create(PhoneNumber=request.args.get('PhoneNumber'), Code=new_code)
        code.save()
        r = create_response(code.PhoneNumber, code.Code)
        urls = [r]
        rs = (grequests.post(u) for u in urls)
        grequests.map(rs)
        return make_response(jsonify(True), 200)


@app.route('/api/login_code', methods=['POST'])
def generate_login_code():
    new_code = get_rand()
    if Clients.select().where(Clients.PhoneNumber == request.args.get('PhoneNumber')).exists():
        if TempCode.select().where(TempCode.PhoneNumber == request.args.get('PhoneNumber')).exists():
            query = TempCode.update(Code=new_code).where(TempCode.PhoneNumber == request.args.get('PhoneNumber'))
            query.execute()
            r = create_response(request.args.get('PhoneNumber'), new_code)
            urls = [r]
            rs = (grequests.post(u) for u in urls)
            grequests.map(rs)
            return make_response(jsonify(True), 200)
        else:
            code = TempCode.create(PhoneNumber=request.args.get('PhoneNumber'), Code=new_code)
            code.save()
            r = create_response(code.PhoneNumber, code.Code)
            urls = [r]
            rs = (grequests.post(u) for u in urls)
            grequests.map(rs)
            return make_response(jsonify(True), 200)
    else:
        return make_response(jsonify(False), 400)


@app.route('/api/e', methods=['GET'])
def e():
    if Clients.select().where(Clients.PhoneNumber == '79604652121').exists():
        return 'true'
    else:
        return 'false'


@app.route('/api/client_reg', methods=['POST'])
def client_registration():
    if Clients.select().where(Clients.PhoneNumber == request.json['PhoneNumber']).exists():
        return make_response("Такой пользователь уже существует!", 400)
    else:
        if TempCode.select().where(TempCode.PhoneNumber == request.json['PhoneNumber']).exists():
            temp_acc = TempCode.get(TempCode.PhoneNumber == request.json['PhoneNumber'])
            if str(temp_acc.Code) == request.json['Code']:
                newAccount = Clients.create(Name=request.json['Name'], PhoneNumber=request.json['PhoneNumber'])
                newAccount.save()
                query = TempCode.delete().where(TempCode.PhoneNumber == request.json['PhoneNumber'])
                query.execute()
                return make_response(jsonify(model_to_dict(newAccount)), 200)
            else:
                return make_response("Неверный код", 404)


@app.route('/api/client_login', methods=['POST'])
def client_log_in():
    if Clients.select().where(Clients.PhoneNumber == request.json['PhoneNumber']).exists():
        if TempCode.select().where(TempCode.PhoneNumber == request.json['PhoneNumber']).exists():
            code = TempCode.get(TempCode.PhoneNumber == request.json['PhoneNumber'])
            if str(code.Code) == request.json['Code']:
                query = TempCode.delete().where(TempCode.PhoneNumber == request.json['PhoneNumber'])
                query.execute()
                return make_response(jsonify(True), 200)
            else:
                return make_response(jsonify(False), 401)
        else:
            return make_response(jsonify(False), 410)
    else:
        return make_response(jsonify(False), 404)


@app.route('/api/driver_login', methods=['POST'])
def driver_log_in():
    if Workers.select().where(Workers.PhoneNumber == request.json['Login']).exists():
        w = Workers.get(Workers.PhoneNumber == request.json['Login'])
        if w.Passwd == request.json['Passwd']:
            return make_response(jsonify(True), 200)
        else:
            return make_response(jsonify(False), 400)
    else:
        return make_response(jsonify(False), 400)

@app.route('/api/firm_reg', methods=['POST'])
def driver_reg():



@app.route("/api/clients")
def get_clients():
    clients = []
    for client in Clients.select():
        clients.append(model_to_dict(client))
    if not clients:
        return make_response("Клиенты не найдены", 404)
    return make_response(jsonify(clients), 200)


@app.route('/api/hello/<string:name>', methods=['GET'])
def hello_p(name):
    a = request.args.get('name')
    return "Hello bitch!" + name


@app.route('/api/code', methods=['GET'])
def get_all_code():
    codes = []
    for code in TempCode.select():
        codes.append(model_to_dict(code))
    if not codes:
        return make_response("Временные коды не найдены", 404)
    return make_response(jsonify(codes), 200)


@app.route('/')
def main():
    return init()


if __name__ == "__main__":
    app.config['JSON_AS_ASCII'] = False
    # app.run(debug=True)
    app.run(host="0.0.0.0", port=os.environ.get('PORT', 5000))
