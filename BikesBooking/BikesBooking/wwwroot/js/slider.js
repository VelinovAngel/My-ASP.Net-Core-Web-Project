// Intervallo tra le immagini (in millisecondi)
var speed = 6000;

/* Array dei percorsi delle immagini */
var images = new Array();
images[0] = "img/Yamaha-MT09.jpg"; //assegnamo la 1° immagine
images[1] = "img/Triumph.png"; //assegnamo la 2° immagine
images[2] = "img/Ducati-Diavel.jpg"; //assegnamo la 3° immagine

// Preassegnamo il percorso delle immagini ad un Array di oggetti Image()
var imagePath = new Array();
for (i = 0; i < images.length; i++) {
    imagePath[i] = new Image();
    imagePath[i].src = images[i];
}

var nrImg = 0; //assegno come immagine di partenza la prima dell'array images

function runBodySlide() {
    document.body.background = imagePath[nrImg].src; //assegno l'immagine di background
    nrImg++; //incremento il numeo dell'immagine (posizione nell'array)
    if (nrImg > (images.length - 1))
        nrImg = 0;
    setTimeout('runBodySlide()', speed);
}

if (document.all || document.getElementById)
    window.onload = runBodySlide();