var bird = document.getElementById("bird");
var ceiling = document.getElementById("Ceiling");
bird.style.top=700;
ceiling.style.top=300;

function GameLoop() {
    bird.style.top=parseInt(bird.style.top)+2+'px';
    if(parseInt(bird.style.top)>=825){
        bird.style.top=825;
        bird.style.animation='none';
    }
}

function Up(e) {
    bird.style.animation= 'animBird 300ms steps(4) infinite';
    if(e.keyCode==32){
        if(parseInt(bird.style.top)-50<=parseInt(ceiling.style.top)+20){
            bird.style.top=parseInt(ceiling.style.top)+20+'px';
        }
        else{
            bird.style.top=parseInt(bird.style.top)-50+'px';
        }
    }
}

window.addEventListener('keydown',Up);

GameLoop();
setInterval(GameLoop,30);