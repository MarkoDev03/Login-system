var section = document.getElementById('section');
section.style.height = window.innerHeight  + 'px';

document.body.style.overflowY = 'hidden';

var wrapperImage = document.getElementById('wrapper-img');
wrapperImage.style.height = window.innerHeight - ((window.innerHeight / 100) * 15) + 'px';

var wrapperImage2 = document.getElementById('wrapper-img-2');
wrapperImage2.style.height = window.innerHeight - ((window.innerHeight / 100) * 15) + 'px';