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

    buttonSettings?.addEventListener('click', () => {
        formSettingProfile.classList.add('show');
        background.classList.add('show');
        background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
        background.style.height = `${document.documentElement.offsetHeight}px`;
    });

    background.addEventListener('click', (e) => closeSettings(e));
    document.addEventListener('keydown', (e) => {
        if (e.code === 'Escape') {
            closeSettings(e);
        }
    });

    function closeSettings(e) {
        const formSettings = document.querySelector('.block-settings'),
            blockPlaylist = document.querySelector('.current-playlist');

        background.classList.remove('show');

        if (formSettings.classList.contains('show')) formSettingProfile.classList.remove('show');

        if (blockPlaylist.classList.contains('show')) blockPlaylist.classList.remove('show');
    }
}

showSettingsProfile();

function playMusicAndShowLine() {
    const blockMusic = document.querySelectorAll('.block-one-music');

    blockMusic.forEach(mus => mus.addEventListener('click', (e) => {
        const threePoint = mus.querySelector('.music-contol'),
            threePointOther = mus.querySelector('.music-other-user-control');
            
        if (e.target != threePoint && e.target != threePointOther) {
            const currentAudio = mus.querySelector('.audio'),
                currentAvatar = mus.querySelector('.avatar-music'),
                blockCurrentMusic = document.querySelector('.block-current-music');

            blockCurrentMusic.innerHTML = `<img class="current-music-avatar" src="${currentAvatar.src}" />
        <audio class="current-audio" src="${currentAudio.src}" controls controlsList="nodownload noplaybackrate"></audio>
        <button><img class="icon-control icon-loop-music" src="/img/icon-povtor.png" /></button>
        <button><img class="icon-control icon-shuffle-music" src="/img/icon-shuffle.png" /></button>
        <button><img class="icon-control icon-last-music" src="/img/icon-last-music.png" /></button>
        <button><img class="icon-control icon-next-music" src="/img/icon-next-music.png" /></button>`;

            blockCurrentMusic.querySelector('.current-audio').play();
            blockCurrentMusic.classList.add('display-flex');
        }
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
        blockPlaylist.classList.add('show');
    });
}

showCurrentPlaylist();

const buttonOpenPopupinProfile = document.querySelectorAll('.block-control'),
    popupBlockChangeMusic = document.querySelector('.popup-change-mymusic'),
    blockAddInPLaylist = document.querySelector('.block-add-in-playlist'),
    buttonaddInPlaylist = popupBlockChangeMusic?.querySelector('.button-show-addinplaylist');

buttonOpenPopupinProfile.forEach(item => item.addEventListener('click', (e) => {
    e.preventDefault();

    popupBlockChangeMusic.classList.toggle('show');
    popupBlockChangeMusic.style.top = e.pageY - 50 + 'px';
    popupBlockChangeMusic.style.left = e.pageX - 280 + 'px';
}));

popupBlockChangeMusic?.addEventListener('click', (e) => {
    if (e.target == buttonaddInPlaylist) {
        blockAddInPLaylist.classList.toggle('show');
        blockAddInPLaylist.style.top = e.pageY - 100 + 'px';
        blockAddInPLaylist.style.left = e.pageX - 400 + 'px';
    }
});

const buttonOpenPopupOtherMusic = document.querySelectorAll('.block-music-other-user-control'),
    popupOtherMusicChange = document.querySelector('.popup-change-friendmusic'),
    buttonAddInPlayListOther = popupOtherMusicChange?.querySelector('.button-show-addinplaylist');
    
buttonOpenPopupOtherMusic.forEach(item => item.addEventListener('click', (e) => {
    e.preventDefault();

    popupOtherMusicChange.classList.toggle('show');
    popupOtherMusicChange.style.top = e.pageY - 50 + 'px';
    popupOtherMusicChange.style.left = e.pageX - 280 + 'px';
}));

popupOtherMusicChange?.addEventListener('click', (e) => {
    if (e.target == buttonAddInPlayListOther) {
        blockAddInPLaylist.classList.toggle('show');
        blockAddInPLaylist.style.top = e.pageY - 100 + 'px';
        blockAddInPLaylist.style.left = e.pageX - 400 + 'px';
    }
});



const blockFilterMusic = document.querySelector('.block-mymusic-favorite'),
    filterMyMusic = document.querySelector('.my-music-filter'),
    filetFavoriteMusic = document.querySelector('.favor-music-filter');

blockFilterMusic.addEventListener('click', (e) => {
    if (e.target == filterMyMusic) {
        filterMyMusic.classList.add('current-filter');
        filetFavoriteMusic.classList.remove('current-filter');
    }
    else if (e.target == filetFavoriteMusic) {
        filetFavoriteMusic.classList.add('current-filter');
        filterMyMusic.classList.remove('current-filter');
    }
});

const formAddPlaylist = document.querySelector('.block-form-add-playlist'),
    buttonOpenFornAddPlaylist = document.querySelector('.block-button-add-playlist'),
    background = document.querySelector('.background');

buttonOpenFornAddPlaylist?.addEventListener('click', (e) => {
    e.preventDefault();

    formAddPlaylist.classList.add('show');
    background.classList.add('show');
    background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
    background.style.height = `${document.documentElement.offsetHeight}px`;
});

const formAddMusic = document.querySelector('.block-form-add-music'),
    buttonOpenFormAddMusic = document.querySelector('.block-button-add-music');

buttonOpenFormAddMusic?.addEventListener('click', (e) => {
    e.preventDefault();

    formAddMusic.classList.add('show');
    background.classList.add('show');
    background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
    background.style.height = `${document.documentElement.offsetHeight}px`;
});

const playlists = document.querySelectorAll('.playlist'),
    playlistOpen = document.querySelector('.current-playlist');

playlists.forEach(item => item.addEventListener('click', () => {
    e.preventDefault();

    playlistOpen.classList.add('show');
    background.classList.add('show');
    background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
    background.style.height = `${document.documentElement.offsetHeight}px`;
}));

background.addEventListener('click', () => {
    formAddPlaylist.classList.remove('show');

    if (formAddMusic.classList.contains('show')) formAddMusic.classList.remove('show');

    if (playlistOpen.classList.contains('show')) playlistOpen.classList.remove('show');
});