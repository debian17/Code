var area;
var 


function fillIncr(arr,length, startValue){
    for (var i=0 ; i<length ; i+=1){
        arr.push(i+startValue);
    }
}

function allMembers(arr, value){
    for(var i=0;i<arr.length;i+=1){
        if(arr[i]!==value){
            return false;
        }
    }
    return true;
}

function findAndReplace(arr, find, replace){
    var index = arr.indexOf(find);
    if(index>-1){
        arr[index]=replace;
    }
}

function popRandom(arr){
    return arr.splice(Math.floor(Math.random()*this.length), 1)[0];
}

function shuffle(arr){
    for(var i=0;i<arr.length;i+=1){
        var index = Math.floor(Math.random() * (i+1));
        var saved = arr[index];
        arr[index] = arr[i];
        arr[i] = saved;
    }
}

function addClass(Elem, className){
    var classes = Elem.className.split(" ");
    if(classes.indexOf(className)<0){
        classes.push(className);
        Elem.className=classes.join(" ").trim();
    }
}

function removeClass(Elem, className){
    var classes = Elem.className.split(" ");
    var index = classes.indexOf(className);
    if (index >= 0){
        classes.splice(classes.indexOf(className),1);
        this.className = classes.join(" ").trim();
    }
}

function Sudoku(area){
    area = area || 3;
    var table = document.createElement('table');
    addClass(table,"sudoku");
    var expo = area * area;
    for (var i=0 ; i<expo ; i+=1){
        var row = table.insertRow(-1);
        for (var j=0 ; j<expo ; j+=1){
            var cell = row.insertCell(-1);
            switch (i%area){
                case 0:
                    addClass(cell,"top");
                    break;
                case area-1:
                    addClass(cell,"bottom");
                    break;
            }
            switch (j%area){
                case 0:
                    addClass(cell,"left");
                    break;
                case area-1:
                    addClass(cell,"right");
                    break;
            }
        }
    }
}

function SudokuFill(table, values){
    Array.prototype.forEach.call(table.rows,)
}

































