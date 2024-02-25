const arrowOpen = document.querySelector('.open-icon'),
    arrowClose = document.querySelector('.close-icon'),
    navigation = document.querySelector('.page-sidebar'),
    sideItemSpan = navigation.querySelectorAll('.menu-link span'),
    bodyContent = document.querySelector('.content-wrapper');

function openLeftPanel() {
    navigation.classList.add('active');
    arrowOpen.classList.add('hidden');
    arrowClose.classList.remove('hidden');
    sideItemSpan.forEach(item => {
        item.classList.remove('hidden');
    });
    bodyContent.style.width = `${document.documentElement.clientWidth - 230}px`;
}

function closeLeftPanel() {
    navigation.classList.remove('active');
    arrowOpen.classList.remove('hidden');
    arrowClose.classList.add('hidden');
    sideItemSpan.forEach(item => {
        item.classList.add('hidden');
    });
    bodyContent.style.width = `${document.documentElement.clientWidth - 70}px`;
}

arrowOpen.addEventListener('click', openLeftPanel);
arrowClose.addEventListener('click', closeLeftPanel);
bodyContent.style.width = `${document.documentElement.clientWidth - 70}px`;

const audio = document.querySelectorAll('.audio');
console.log(audio);
for (let i = 0; i < audio.length; i++) {
    audio[i].addEventListener('ended', function () {
        if (audio[i].duration === audio[i].currentTime) {
            audio[i + 1].play();
        }
    });
}