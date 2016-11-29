var animal = {
  eats: true
};

function Rabbit(name) {
  alert("Выполнилось тело");
  this.name = name;
}

Rabbit.prototype = animal;

var rabbit = new Rabbit("Кролик");

alert( rabbit.eats );