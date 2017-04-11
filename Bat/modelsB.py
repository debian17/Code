from peewee import *

db = SqliteDatabase('KG_DB.db')


class Films(Model):
    name = CharField()
    descript = TextField()
    rates = DoubleField()
    poster = CharField()

    class Meta:
        database = db  # модель будет использовать базу данных 'KG_DB.db'


class HallCapacity(Model):
    hall_id = IntegerField(unique=True)
    rows = IntegerField()
    seats_in_row = IntegerField()

    class Meta:
        database = db


class Sessions(Model):
    time = CharField()
    date = CharField()
    film = ForeignKeyField(Films, related_name='sessions')
    price = DoubleField()
    hall_id = ForeignKeyField(HallCapacity, related_name='sessions')
    seats_status = TextField()

    class Meta:
        database = db
