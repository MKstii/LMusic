const iconMenu = document.querySelector('.icon-menu'),
    navigation = document.querySelector('.page-sidebar'),
    sideItemSpan = navigation.querySelectorAll('.menu-link span'),
    bodyContent = document.querySelector('.content-wrapper');

function toggleLeftMenu() {
    navigation.classList.toggle('active');
    sideItemSpan.forEach(item => {
        item.classList.toggle('hidden');
    });
    bodyContent.style.width = `${document.documentElement.clientWidth - 230}px`;
}

iconMenu.addEventListener('click', toggleLeftMenu);

bodyContent.style.width = `${document.documentElement.clientWidth - 70}px`;
console.log('AAA');


//const audio = document.querySelectorAll('.audio');
//console.log(audio);
//for (let i = 0; i < audio.length; i++) {
//    audio[i].addEventListener('ended', function () {
//        if (audio[i].duration === audio[i].currentTime) {
//            audio[i + 1].play();
//        }
//    });
//}