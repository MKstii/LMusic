const iconMenu = document.querySelector('.icon-menu'),
    navigation = document.querySelector('.page-sidebar'),
    sideItemSpan = navigation.querySelectorAll('.menu-link span'),
    bodyContent = document.querySelector('.content-wrapper'),
    currentMusic = document.querySelector('.block-current-music');

function toggleLeftMenu() {
    navigation.classList.toggle('active');
    sideItemSpan.forEach(item => {
        item.classList.toggle('hidden');
    });

    const widthMenu = navigation.classList.contains('active') ? 200 : 70;
    bodyContent.style.width = `${document.documentElement.clientWidth - widthMenu}px`;
    currentMusic.style.width = `${document.documentElement.clientWidth - widthMenu}px`;
}

iconMenu.addEventListener('click', toggleLeftMenu);
bodyContent.style.width = `${document.documentElement.clientWidth - navigation.clientWidth}px`;
currentMusic.style.width = `${document.documentElement.clientWidth - navigation.clientWidth}px`;

function showSettingsProfile() {
    const buttonSettings = document.querySelector('.settings-profile'),
        formSettingProfile = document.querySelector('.block-settings'),
        background = document.querySelector('.background');

    buttonSettings.addEventListener('click', () => {
        formSettingProfile.classList.add('show');
        background.classList.add('show');
        background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
        background.style.height = `${document.documentElement.offsetHeight}px`;
    });

    background.addEventListener('click', closeSettings);
    document.addEventListener('keydown', (e) => {
        if (e.code === 'Escape') {
            closeSettings();
        }
    });

    function closeSettings() {
        const formSettings = document.querySelector('.block-settings'),
            blockPlaylist = document.querySelector('.current-playlist');

        background.classList.remove('show');

        if (formSettings.classList.contains('show')) formSettingProfile.classList.remove('show');

        if (blockPlaylist.classList.contains('display-flex')) blockPlaylist.classList.remove('display-flex');
    }
}

showSettingsProfile();

function playMusicAndShowLine() {
    const blockMusic = document.querySelectorAll('.block-one-music');

    blockMusic.forEach(mus => mus.addEventListener('click', (e) => {
        const currentAudio = mus.querySelector('.audio'),
            currentAvatar = mus.querySelector('.avatar-music'),
            blockCurrentMusic = document.querySelector('.block-current-music');

        blockCurrentMusic.innerHTML = `<img class="current-music-avatar" src="${currentAvatar.src}" />
        <audio class="current-audio" src="${currentAudio.src}" controls controlsList="nodownload noplaybackrate"></audio>
        <button><img class="icon-control icon-loop-music" src="/img/icon-povtor.png" /></button>
        <button><img class="icon-control icon-shuffle-music" src="/img/icon-shuffle.png" /></button>
        <button><img class="icon-control icon-last-music" src="/img/icon-last-music.png" /></button>
        <button><img class="icon-control icon-next-music" src="/img/icon-next-music.png" /></button>`;
        console.log('AAA');
        blockCurrentMusic.querySelector('.current-audio').play();
        blockCurrentMusic.classList.add('display-flex');
    }));
}

playMusicAndShowLine();

function showCurrentPlaylist() {
    const currentSong = document.querySelector('.block-current-music'),
        background = document.querySelector('.background'),
        blockPlaylist = document.querySelector('.current-playlist');

    currentSong.addEventListener('click', () => {
        background.classList.add('show');
        background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
        background.style.height = `${document.documentElement.offsetHeight}px`;
        blockPlaylist.classList.add('display-flex');
    });
}

showCurrentPlaylist();

