// Intervallo tra le immagini (in millisecondi)
var speed = 6000;

/* Array dei percorsi delle immagini */
var images = new Array();
    
images[0] = "https://www.wallpaperup.com/uploads/wallpapers/2013/06/28/110604/0285f6659537e1219141f7a57a0cd17b.jpg"; //assegnamo la 1° immagine
images[1] = "https://i.pinimg.com/originals/bf/6b/29/bf6b294dc8c45f328345bce8ea9c6d44.jpg"; //assegnamo la 2° immagine
images[2] = "https://cdn.statically.io/img/i.pinimg.com/originals/15/bb/9d/15bb9d3decb130a84648b1eb0af2baf2.jpg"; //assegnamo la 3° immagine
images[3] = "https://images8.alphacoders.com/813/813188.jpg"; //assegnamo la 3° immagine

// Preassegnamo il percorso delle immagini ad un Array di oggetti Image()
var imagePath = new Array();
for (i = 0; i < images.length; i++) {
    imagePath[i] = new Image();
    imagePath[i].src = images[i];
}

var nrImg = 0; //assegno come immagine di partenza la prima dell'array images

function runBodySlide() {
    document.body.background = imagePath[nrImg].src; //assegno l'immagine di background
    nrImg++; //incremento il numero dell'immagine (posizione nell'array)
    if (nrImg > (images.length - 1))
        nrImg = 0;
    setTimeout('runBodySlide()', speed);
}

if (document.all || document.getElementById)
    window.onload = runBodySlide();