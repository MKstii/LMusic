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
            blockPlaylist = document.querySelector('.current-playlist'),
            blockChangePlaylist = document.querySelector('.block-form-change-playlist'),
            blockChangeMusic = document.querySelector('.block-form-change-music'),
            blockOpenPlaylist = document.querySelector('.block-open-playlist'),
            blockOpenMusicInPlaylist = document.querySelector('.block-change-music-playlist'),
            blockAddInPLaylist = document.querySelector('.block-add-in-playlist');

        background.classList.remove('show');

        if (formSettings.classList.contains('show')) formSettingProfile.classList.remove('show');

        if (blockPlaylist.classList.contains('show')) blockPlaylist.classList.remove('show');

        if (blockChangePlaylist.classList.contains('show')) blockChangePlaylist.classList.remove('show');

        if (blockChangeMusic.classList.contains('show')) blockChangeMusic.classList.remove('show');

        if (blockOpenPlaylist.classList.contains('show')) blockOpenPlaylist.classList.remove('show');

        if (blockOpenMusicInPlaylist.classList.contains('show')) blockOpenMusicInPlaylist.classList.remove('show');

        if (blockAddInPLaylist.classList.contains('show')) blockAddInPLaylist.classList.remove('show');
    }
}

showSettingsProfile();

function playMusicAndShowLine() {
    const blockMusic = document.querySelectorAll('.block-one-music');

    blockMusic.forEach(mus => mus.addEventListener('click', (e) => {
        const threePoint = mus.querySelector('.music-contol'),
            threePointOther = mus.querySelector('.music-other-user-control'),
            threePointPlaylist = mus.querySelector('.music-contol-my-playlist');

        if (e.target != threePoint && e.target != threePointOther && e.target != threePointPlaylist) {
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
    const musicid = item.querySelector('.music-my-id').innerHTML;
    const musictitle = item.querySelector('.music-my-title').innerHTML;
    const musicmusician = item.querySelector('.music-my-musician').innerHTML;

    popupBlockChangeMusic.getElementById('musicidpopup').value = musicid;
    popupBlockChangeMusic.getElementById('musiciddelete').value = musicid;
    popupBlockChangeMusic.getElementById('musiciddeleteplalist').value = musicid;
    popupBlockChangeMusic.querySelector('.music-change-idd').innerHTML = musicid;
    popupBlockChangeMusic.querySelector('.titlemusiccc').innerHTML = musictitle;
    popupBlockChangeMusic.querySelector('.musicianmusiccc').innerHTML = musicmusician;

    popupBlockChangeMusic.classList.toggle('show');
    popupBlockChangeMusic.style.top = e.pageY - 50 + 'px';
    popupBlockChangeMusic.style.left = e.pageX - 280 + 'px';

    if (blockAddInPLaylist.classList.contains('show')) blockAddInPLaylist.classList.remove('show');
}));

popupBlockChangeMusic?.addEventListener('click', (e) => {
    if (e.target == buttonaddInPlaylist) {
        const musicId = popupOtherMusicChange.getElementById('musicidpopup').value;

        blockAddInPLaylist.getElementById('musicId').value = musicId;

        const playlists = fetch("http://127.0.0.1/GetUserPlaylists")
            .then(response => response.json());
        var htmlPlaylists = '';
        for (var i = 0; i < playlists.length; i++) {
            htmlPlaylists += '<div class="block-checkbox">' +
                '<input id="' + playlists[i]['id'] + '" type="radio" name="playlistId" value="' + playlists[i]['id'] + '" />' +
                '<label for="' + playlists[i]['id'] + '">' + playlists[i]['name'] + '</label></div>';
        }
        const divBlockCheckboxMyPlaylists = blockAddInPLaylist.querySelector('.block-checkbox-my-playlists');
        divBlockCheckboxMyPlaylists.innerHTML = htmlPlaylists;

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
    const musicid = item.querySelector('.music-id').innerHTML;

    popupOtherMusicChange.getElementById('musicidpopup').value = musicid;

    popupOtherMusicChange.classList.toggle('show');
    popupOtherMusicChange.style.top = e.pageY - 50 + 'px';
    popupOtherMusicChange.style.left = e.pageX - 280 + 'px';
}));

popupOtherMusicChange?.addEventListener('click', (e) => {
    if (e.target == buttonAddInPlayListOther) {
        const musicid = popupOtherMusicChange.querySelector('.input-hidden-musicid');
        blockAddInPLaylist.getElementById('musicId').value = musicid;

        const playlists = fetch("http://127.0.0.1/GetUserPlaylists")
            .then(response => response.json());
        var htmlPlaylists = '';
        for (var i = 0; i < playlists.length; i++) {
            htmlPlaylists += '<div class="block-checkbox">' +
                '<input id="' + playlists[i]['id'] + '" type="radio" name="playlistId" value="' + playlists[i]['id'] + '" />' +
                '<label for="' + playlists[i]['id'] + '">' + playlists[i]['name'] + '</label></div>';
        }
        const divBlockCheckboxMyPlaylists = blockAddInPLaylist.querySelector('.block-checkbox-my-playlists');
        divBlockCheckboxMyPlaylists.innerHTML = htmlPlaylists;

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

const buttonAAAA = document.querySelectorAll('.button-for-my-playlist'),
    formChangePlaylistAAA = document.querySelector('.block-change-my-playlist');

buttonAAAA.forEach(item => item.addEventListener('click', (e) => {
    const idPlaylist = e.target.id;
    const titlePlaylist = e.target.dataset.title;
    formChangePlaylistAAA.innerHTML = '';
    if (e.target.getAttribute('data-change-my')) {
        formChangePlaylistAAA.innerHTML = '<form asp-action="/playlist/delete" asp-controller="Playlist" method="post" enctype="multipart/form-data">' +
            '<input type = "hidden" name = "id" value = "' + idPlaylist + '" />' +
            '<button type="submit">Удалить</button></form>' +
            '<button class="button-playlist-change">' +
            '<img class="icon-change-playlist" src="~/img/icon-reduct.png" />' +
            '<span class="idplaylistchange" style="visibility:hidden;">' + idPlaylist + '</span>' +
            '<span>Редактировать</span>' +
            '<span class="titleplaylist" style="visibility:hidden;">' + titlePlaylist + '</span>' +
            '</button>';
    }
    else {
        formChangePlaylistAAA.innerHTML = '<form asp-action="/playlist/removePlaylistFromUser" asp-controller="Playlist" method="post" enctype="multipart/form-data">' +
            '<input type = "hidden" name = "playlistId" value = "' + idPlaylist + '" />' +
            '<button type="submit">Удалить со своей страницы</button></form>';
    }

    formChangePlaylistAAA.classList.toggle('show');
    formChangePlaylistAAA.style.top = e.pageY - 40 + 'px';
    formChangePlaylistAAA.style.left = e.pageX + 25 + 'px';
}));

//const buttonOpenChangePlaylist = document.querySelectorAll('.button-for-my-playlist'),
//    divChangePlaylist = document.querySelector('.block-change-my-playlist'),
const blockChangePLaylist = document.querySelector('.block-form-change-playlist'),
    buttonChangePlaylist = formChangePlaylistAAA?.querySelector('.button-playlist-change');

//buttonOpenChangePlaylist.forEach(item => item.addEventListener('click', (e) => {
//    e.preventDefault();

//    divChangePlaylist.classList.toggle('show');
//    divChangePlaylist.style.top = e.pageY - 40 + 'px';
//    divChangePlaylist.style.left = e.pageX + 25 + 'px';
//}));

buttonChangePlaylist?.addEventListener('click', (e) => {
    e.preventDefault();

    const idplaylist = buttonChangePlaylist.querySelector('.idplaylistchange').innerHTML;
    const titleplaylist = buttonChangePlaylist.querySelector('.titleplaylist').innerHTML;

    blockChangePLaylist.getElementById('id-playlist').value = idplaylist;
    blockChangePLaylist.getElementById('title-playlist').value = titleplaylist;

    blockChangePLaylist.classList.add('show');
    background.classList.add('show');
    background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
    background.style.height = `${document.documentElement.offsetHeight}px`;
});

const buttonChangeMusic = popupBlockChangeMusic?.querySelector('.button-change-music'),
    blockChangeMusic = document.querySelector('.block-form-change-music');

buttonChangeMusic?.addEventListener('click', (e) => {
    e.preventDefault();
    const idMusic = buttonChangeMusic.querySelector('.music-change-idd').innerHTML;
    const parentpopup = buttonChangeMusic.closest('.popup-change-mymusic');
    const titlemusic = parentpopup.querySelector('.titlemusiccc').innerHTML;
    const musicianmusic = parentpopup.querySelector('.musicianmusiccc').innerHTML;
    

    blockChangeMusic.getElementById('aaaaaaaid').value = idMusic;
    blockChangeMusic.getElementById('aaaaatitle').value = titlemusic;
    blockChangeMusic.getElementById('aaaamusic').value = musicianmusic;

    blockChangeMusic.classList.add('show');
    background.classList.add('show');
    background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
    background.style.height = `${document.documentElement.offsetHeight}px`;
});

const buttonImageOpenPlaylist = document.querySelectorAll('.playlist'),
    blockOpenPlaylist = document.querySelector('.block-open-playlist');

buttonImageOpenPlaylist.forEach(item => item.addEventListener('click', (e) => {
    e.preventDefault();
    const threePoint = item.querySelector('.button-for-my-playlist');

    if (e.target != threePoint) {
        const divMusicPlaylist = blockOpenPlaylist.querySelector('.playlist-music');
        const namePlaylist = item.querySelector('.info-my-playlist .tiiiitle-playlist').innerHTML;
        const idPlaylist = item.querySelector('.info-my-playlist .iiiid-playlist').innerHTML;

        blockOpenPlaylist.querySelector('.title-open-playlist').innerHTML = namePlaylist;

        const playlistmusic = fetch("http://127.0.0.1/playlist/" + idPlaylist)
            .then(response => response.json());
        var htmlPlaylists = '';
        for (var i = 0; i < playlistmusic.length; i++) {
            htmlPlaylists += '<div class="block-one-music"><div class="music">' +
                '<div class="info-music">' +
                '<audio class="audio" src="' + playlistmusic[i]['musicPath'] + '"></audio>' +
                '<button class="play-music"><img class="avatar-music" src="' + playlistmusic[i]['photoPath'] + '" /></button>' +
                '<div class="block-name-music">' +
                '<span>' + playlistmusic[i]['title'] + '</span>' +
                '<span>' + playlistmusic[i]['musician'] + '</span>' +
                            '</div>' +
                        '</div>' +
                '<form class="form-deleteMusicForPlaylist" asp-action="DeleteMusicFromPlaylist" asp-controller="Music" method="post" enctype="multipart/form-data">' +
                '<input type = "hidden" name = "musicId" value = "' + playlistmusic[i]['id'] + '" />' +
                '<input type="hidden" name="playlistId" value="' + idPlaylist + '"/>' +
                    '<button type="submit" class="block-control-music-playlist">' +
                        '<img class="music-contol-my-playlist" src="~/img/icon-three-tochki.png" />' +
                    '</button>' +
                '</form>' +
                    '</div>' +
                '</div>';
        }
        const divBlockMusicPlaylist = blockOpenPlaylist.querySelector('.playlist-music');
        divBlockMusicPlaylist.innerHTML = htmlPlaylists;

        blockOpenPlaylist.classList.add('show');
        background.classList.add('show');
        background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
        background.style.height = `${document.documentElement.offsetHeight}px`;
    }
}));

const buttonChangeMusicPlaylist = blockOpenPlaylist?.querySelectorAll('.block-control-music-playlist'),
    blockChangeMusicPlaylist = document.querySelector('.block-change-music-playlist');

buttonChangeMusicPlaylist.forEach(item => item.addEventListener('click', (e) => {
    e.preventDefault();

    blockChangeMusicPlaylist.classList.toggle('show');
    blockChangeMusicPlaylist.style.top = e.pageY - 40 + 'px';
    blockChangeMusicPlaylist.style.left = e.pageX - 270 + 'px';

    if (blockAddInPLaylist.classList.contains('show')) blockAddInPLaylist.classList.remove('show');
}));

const blockOpenMusicInPlaylist = document.querySelector('.block-change-music-playlist'),
    buttonAddInPlaylistAA = blockOpenMusicInPlaylist?.querySelector('.button-music-add-in-playlist');

buttonAddInPlaylistAA?.addEventListener('click', (e) => {

    blockAddInPLaylist.classList.toggle('show');
    blockAddInPLaylist.style.top = e.pageY - 100 + 'px';
    blockAddInPLaylist.style.left = e.pageX - 400 + 'px';
});

const formAddMusicForm = document.querySelector('.form-add-music');
let oldSubmit = formAddMusicForm.onsubmit || function () { };
formAddMusicForm.onsubmit = function () {
    const title = formAddMusicForm.querySelector('.input-name-music'),
        musician = formAddMusicForm.querySelector('.input-songs-music'),
        music = formAddMusicForm.querySelector('.fileMusicAdd');

    if (title == "" || musician == "" || music.files.length == 0) {
        const divError = document.querySelector('.error-valid-music');
        divError.classList.add('show');
        return false;
    }
    oldSubmit();
}

const buttonSearchUsersss = document.querySelector('.button-search-friend');

buttonSearchUsersss?.addEventListener('click', (e) => {
    e.preventDefault();

    const textSearch = document.querySelector('.text-search-users');
    window.location.href = "/friend/search?filter=" + textSearch;
})