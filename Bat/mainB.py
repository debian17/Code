import os
from playhouse.shortcuts import *
from models import *
from flask import Flask
from flask import request, jsonify, make_response, abort, render_template


app = Flask(__name__)


@app.route('/readme')
def readme():
    return render_template('readme.html',
                           baseURL=request.url_root)


@app.route('/admin')
def index():
    return render_template("index.html")


@app.route('/admin/table')
def render_table():
    table_content = []
    if request.args.get('table_name') == "films":
        table_content = filmsFromDB()
    elif request.args.get('table_name') == 'sessions':
        table_content = sessFromDB()
    elif request.args.get('table_name') == 'halls':
        table_content = hallsFromDB()
    return render_template("table.html",
                           contentFromDB = table_content,
                           len=len(table_content[0]),
                           header=table_content[0])


def filmsFromDB():
    films = []
    for film in Films.select():
        films.append(model_to_dict(film))
    return films


def sessFromDB():
    sessions = []
    for session in Sessions.select():
        sessions.append(model_to_dict(session))
    return sessions



def hallsFromDB():
    halls = []
    for hall in HallCapacity.select():
        halls.append(model_to_dict(hall))
    return halls

@app.route('/api/init')
def init():
    try:
        Films.create_table()
    except OperationalError:
        print("Films table already exists!")

    try:
        HallCapacity.create_table()
    except OperationalError:
        print("HallCapacity table already exists!")

    try:
        Sessions.create_table()
    except OperationalError:
        print("Sessions table already exists!")

    return 'success'


@app.route('/api/addhall')
def add_hall():
    try:
        HallCapacity.create(hall_id=1, rows=10, seats_in_row=10)
        return 'added'
    except IntegrityError:
        return 'failed. already exists'


@app.route('/api/addfilms')
def add_film():
    try:
        Films.create(name='Fargo', descript='Норм фильм',
                     rates=7.7, poster='img/1.jpg')
        Films.create(name='StarWars', descript='Пиу-пиу',
                     rates=8.1, poster='img/2.jpg')
        Films.create(name='Forsag', descript='Би-би',
                     rates=5.9, poster='img/2.jpg')
        return 'added'
    except IntegrityError:
        return 'failed. already exists'


@app.route('/api/addsess')
def add_sess():
    try:
        Sessions.create(time='17:40', date='19.02.17', film=Films.get(Films.name == 'Fargo'),
                        price=149.9, hall_id=HallCapacity.get(HallCapacity.hall_id == 1), seats_status='00000000000')
        Sessions.create(time='21:30', date='19.02.17', film=Films.get(Films.name == 'StarWars'),
                        price=249.9, hall_id=HallCapacity.get(HallCapacity.hall_id == 1), seats_status='00000000000')
        Sessions.create(time='23:50', date='19.02.17', film=Films.get(Films.name == 'Forsag'),
                        price=349.9, hall_id=HallCapacity.get(HallCapacity.hall_id == 1), seats_status='00000000000')
        return 'added'
    except IntegrityError:
        return 'failed. already exists'


@app.route('/api/films')
def get_films():

    return make_response(jsonify(filmsFromDB()), 200)


@app.route('/api/sessions')
def get_sessions():
    sessions = []
    for session in Sessions.select():
        sessions.append(model_to_dict(session))
    if not sessions:
        return make_response("Сеансы не найдены", 404)
    return make_response(jsonify(sessions), 200)


@app.route('/api/sessions/<string:film>')
def get_session(film):
    sessions = []
    for session in Sessions.select().join(Films).where(Films.name == film):
        sessions.append(model_to_dict(session))
    if not sessions:
        return make_response("Сеансы указанного фильма не найдены", 404)
    return make_response(jsonify(sessions), 200)


@app.route('/api/sessions/<int:id>/seat', methods=['PUT'])
def update_seats(id):
    if not request.args.get('seats_status'):
         abort(400)
    try:
        query = Sessions.update(seats_status=request.args.get('seats_status')).where(Sessions.id == id)
        query.execute()
        res = Sessions.select().where(Sessions.id == id).get()
    except Sessions.DoesNotExist:
        return make_response("Указанный сеанс не найден", 404)
    return make_response(jsonify(model_to_dict(res)), 200)


@app.route('/api/halls')
def get_halls():
    halls = []
    for hall in HallCapacity.select():
        halls.append(model_to_dict(hall))
    if not halls:
        return make_response("Схемы залов не найдены", 404)
    return make_response(jsonify(halls), 200)


@app.route('/api/halls/<int:id>')
def get_hall(id):
    try:
        hall = HallCapacity.select().where(HallCapacity.hall_id == id).get()
    except HallCapacity.DoesNotExist:
        return make_response("Указанная схема зала не найдена", 404)
    return make_response(jsonify(model_to_dict(hall)), 200)


if __name__ == '__main__':
     app.run(host="0.0.0.0", port=os.environ.get('PORT', 5000))
    # app.run(debug=True)
