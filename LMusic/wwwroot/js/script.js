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

        if (formSettings?.classList.contains('show')) formSettingProfile.classList.remove('show');

        if (blockPlaylist.classList.contains('show')) blockPlaylist.classList.remove('show');

        if (blockChangePlaylist?.classList.contains('show')) blockChangePlaylist.classList.remove('show');

        if (blockChangeMusic?.classList.contains('show')) blockChangeMusic.classList.remove('show');

        if (blockOpenPlaylist?.classList.contains('show')) blockOpenPlaylist.classList.remove('show');

        if (blockOpenMusicInPlaylist?.classList.contains('show')) blockOpenMusicInPlaylist.classList.remove('show');

        if (blockAddInPLaylist?.classList.contains('show')) blockAddInPLaylist.classList.remove('show');
    }
}

showSettingsProfile();

///////////////////////////////////////////////////////////////////////////////////
// тут чисто все события на появление радактирования формы, удаления и т.д.
const blockMusic = document.querySelectorAll('.block-one-music'),
    popupBlockChangeMusic = document.querySelector('.popup-change-mymusic'),
    blockAddInPLaylist = document.querySelector('.block-add-in-playlist'),
    popupOtherMusicChange = document.querySelector('.popup-change-friendmusic');

blockMusic.forEach(mus => mus.addEventListener('click', (e) => {
    const threePoint = mus.querySelector('.music-contol'),
        buttonViewChangeMusicMy = mus.querySelector('.block-control'),
        threePointOther = mus.querySelector('.music-other-user-control'),
        threePointPlaylist = mus.querySelector('.music-contol-my-playlist');

    if (e.target != threePoint && e.target != threePointOther && e.target != threePointPlaylist && e.target != buttonViewChangeMusicMy) {
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
    else if (e.target == threePoint) {
        e.preventDefault();
        const parentBlockControl = e.target.closest('.block-control');
        const musicid = parentBlockControl.querySelector('.music-my-id').innerHTML;
        const musictitle = parentBlockControl.querySelector('.music-my-title').innerHTML;
        const musicmusician = parentBlockControl.querySelector('.music-my-musician').innerHTML;

        popupBlockChangeMusic.querySelector('.musicidpopup').value = musicid;
        popupBlockChangeMusic.querySelector('.musiciddelete').value = musicid;
        popupBlockChangeMusic.querySelector('.musiciddeleteplalist').value = musicid;
        popupBlockChangeMusic.querySelector('.music-change-idd').innerHTML = musicid;
        popupBlockChangeMusic.querySelector('.titlemusiccc').value = musictitle;
        popupBlockChangeMusic.querySelector('.musicianmusiccc').value = musicmusician;

        popupBlockChangeMusic.classList.toggle('show');
        popupBlockChangeMusic.style.top = e.pageY - 50 + 'px';
        popupBlockChangeMusic.style.left = e.pageX - 280 + 'px';

        if (blockAddInPLaylist.classList.contains('show')) blockAddInPLaylist.classList.remove('show');

        const buttonChangeMusic = popupBlockChangeMusic.querySelector('.button-change-music'),
            blockChangeMusic = document.querySelector('.block-form-change-music');

        buttonChangeMusic.addEventListener('click', (e) => {
            e.preventDefault();
            const idMusic = buttonChangeMusic.querySelector('.music-change-idd').innerHTML;
            const titlemusic = popupBlockChangeMusic.querySelector('.titlemusiccc').value;
            const musicianmusic = popupBlockChangeMusic.querySelector('.musicianmusiccc').value;

            blockChangeMusic.querySelector('.aaaaaaaid').value = idMusic;
            blockChangeMusic.querySelector('.aaaaatitle').value = titlemusic;
            blockChangeMusic.querySelector('.aaaamusic').value = musicianmusic;

            blockChangeMusic.classList.add('show');
            background.classList.add('show');
            background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
            background.style.height = `${document.documentElement.offsetHeight}px`;
        });

        buttonaddInPlaylist = popupBlockChangeMusic.querySelector('.button-show-addinplaylist');

        buttonaddInPlaylist.addEventListener('click', (e) => {
            blockAddInPLaylist.querySelector('.musicId').value = musicid;

            const divBlockCheckboxMyPlaylists = blockAddInPLaylist.querySelector('.block-checkbox-my-playlists');
            var htmlPlaylists = '';

            const playlists = fetch("http://127.0.0.1/GetUserPlaylists")
                .then(response => response.json())
                .then(playlists => {

                    for (var i = 0; i < playlists.length; i++) {
                        htmlPlaylists += '<div class="block-checkbox">' +
                            '<input id="' + playlists[i]['id'] + '" type="radio" name="playlistId" value="' + playlists[i]['id'] + '" />' +
                            '<label for="' + playlists[i]['id'] + '">' + playlists[i]['name'] + '</label></div>';
                    }
                    divBlockCheckboxMyPlaylists.innerHTML = htmlPlaylists;
                });

            blockAddInPLaylist.classList.toggle('show');
            blockAddInPLaylist.style.top = e.pageY - 100 + 'px';
            blockAddInPLaylist.style.left = e.pageX - 400 + 'px';
        });
    }
    else if (e.target = threePointOther) {
        const parentMusicUserOther = threePointOther.closest('.block-music-other-user-control');
        e.preventDefault();

        const musicid = parentMusicUserOther.querySelector('.music-id').innerHTML;
        const musicInFavorite = fetch("http://127.0.0.1/IsMusicInFavorite?musicId=" + musicid)
            .then(response => response.json())
            .then(isFavorite => {
                let innertTextpopup = '';
                if (isFavorite) {
                    innertTextpopup = '<button type="submit" class="popup-button button-remove-mymusic">' +
                        '<img class="popup-image" src="/img/icon-plus.png" />' +
                        'Удалить из избранного' +
                        '<input id = "musicidpopup" class="input-hidden-musicid" type="hidden" name="musicId" value="' + musicid + '" />' +
                        '</button>';
                } else {
                    innertTextpopup = '<button type="submit" class="popup-button button-addin-mymusic">' +
                        '<input id="musicidpopup" class="input-hidden-musicid" type="hidden" name="musicId" value="" />' +
                        '<img class="popup-image" src="/img/icon-plus.png" />' +
                        'Добавить себе' +
                        '</button>';
                }
                innertTextpopup += '<button class="popup-button button-show-addinplaylist">' +
                    '<img class="popup-image" src = "/img/icon-plus.png" />' +
                    'Добавить в плейлист' +
                    '</button>';
                popupOtherMusicChange.innerHTML = innertTextpopup;
                popupOtherMusicChange.querySelector('.input-hidden-musicid').value = musicid;

                buttonaddInPlaylistOther = popupOtherMusicChange.querySelector('.button-show-addinplaylist');

                buttonaddInPlaylistOther?.addEventListener('click', (e) => {
                    blockAddInPLaylist.querySelector('.musicId').value = musicid;

                    const divBlockCheckboxMyPlaylists = blockAddInPLaylist.querySelector('.block-checkbox-my-playlists');
                    var htmlPlaylists = '';

                    const playlists = fetch("http://127.0.0.1/GetUserPlaylists")
                        .then(response => response.json())
                        .then(playlist => {

                            for (var i = 0; i < playlist.length; i++) {
                                htmlPlaylists += '<div class="block-checkbox">' +
                                    '<input id="' + playlist[i]['id'] + '" type="radio" name="playlistId" value="' + playlist[i]['id'] + '" />' +
                                    '<label for="' + playlist[i]['id'] + '">' + playlist[i]['name'] + '</label></div>';
                            }
                            divBlockCheckboxMyPlaylists.innerHTML = htmlPlaylists;
                        });

                    blockAddInPLaylist.classList.toggle('show');
                    blockAddInPLaylist.style.top = e.pageY - 100 + 'px';
                    blockAddInPLaylist.style.left = e.pageX - 400 + 'px';
                });

                const buttonDelete = popupOtherMusicChange.querySelector('.button-remove-mymusic');
                buttonDelete?.addEventListener('click', (e) => {
                    fetch("http://127.0.0.1/DeleteMusicFromUser?musicId=" + musicid, {
                        method: "POST"
                    });

                    location.reload();
                });

                const buttonAdd = popupOtherMusicChange.querySelector('.button-addin-mymusic');
                buttonAdd?.addEventListener('click', (e) => {
                    fetch("http://127.0.0.1/AddMusicToFav?musicId=" + musicid, {
                        method: "POST"
                    });

                    location.reload();
                });
            });

        popupOtherMusicChange.classList.toggle('show');
        popupOtherMusicChange.style.top = e.pageY - 50 + 'px';
        popupOtherMusicChange.style.left = e.pageX - 280 + 'px';
    }
}));

///////////////////////////////////////////////////////////////////////////////////////

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

const buttonOpenPopupinProfile = document.querySelectorAll('.block-control');
    //popupBlockChangeMusic = document.querySelector('.popup-change-mymusic'),

const blockFilterMusic = document.querySelector('.block-mymusic-favorite'),
    filterMyMusic = document.querySelector('.my-music-filter'),
    filetFavoriteMusic = document.querySelector('.favor-music-filter');

blockFilterMusic?.addEventListener('click', (e) => {
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

//const playlists = document.querySelectorAll('.playlist'),
//    playlistOpen = document.querySelector('.current-playlist');

//playlists.forEach(item => item.addEventListener('click', (e) => {
//    e.preventDefault();

//    playlistOpen.classList.add('show');
//    background.classList.add('show');
//    background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
//    background.style.height = `${document.documentElement.offsetHeight}px`;
//}));

background.addEventListener('click', () => {
    formAddPlaylist.classList.remove('show');

    if (formAddMusic.classList.contains('show')) formAddMusic.classList.remove('show');

    if (playlistOpen.classList.contains('show')) playlistOpen.classList.remove('show');
});
    
const buttonImageOpenPlaylist = document.querySelectorAll('.playlist'),
    blockOpenPlaylist = document.querySelector('.block-open-playlist');

buttonImageOpenPlaylist.forEach(item => item.addEventListener('click', (e) => {
    e.preventDefault();
    const threePoint = item.querySelector('.button-for-my-playlist'),
        threePointOther = item.querySelector('.button-for-other-playlist');

    if (e.target != threePoint && e.target != threePointOther) {
        const divMusicPlaylist = blockOpenPlaylist.querySelector('.playlist-music');
        const namePlaylist = item.querySelector('.info-my-playlist .tiiiitle-playlist').innerHTML;
        const idPlaylist = item.querySelector('.info-my-playlist .iiiid-playlist').innerHTML;

        blockOpenPlaylist.querySelector('.title-open-playlist').innerHTML = namePlaylist;
        var htmlPlaylists = '';

        const playlistmusic = fetch("http://127.0.0.1/playlist/" + idPlaylist)
            .then(response => response.json())
            .then(playlistmusic => {
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
                        '<img class="music-contol-my-playlist" src="/img/icon-three-tochki.png" />' +
                        '</button>' +
                        '</form>' +
                        '</div>' +
                        '</div>';
                }
                divMusicPlaylist.innerHTML = htmlPlaylists;
            });

        blockOpenPlaylist.classList.add('show');
        background.classList.add('show');
        background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
        background.style.height = `${document.documentElement.offsetHeight}px`;
    } else if (e.target == threePoint) {
        const formChangePlaylistAAA = document.querySelector('.block-change-my-playlist');

        const idPlaylist = threePoint.querySelector('.playlist-my-id').innerHTML;
        const titlePlaylist = threePoint.querySelector('.playlist-my-title').innerHTML;

        formChangePlaylistAAA.innerHTML = '<form asp-action="Delete" asp-controller="Playlist" method="post" enctype="multipart/form-data">' +
            '<input type = "hidden" name = "id" value = "' + idPlaylist + '" />' +
            '<button type="submit" class="delete-playlisttttt">Удалить</button></form>' +
            '<button class="button-playlist-change">' +
            '<img class="icon-change-playlist" src="/img/icon-reduct.png" />' +
            '<span>Редактировать</span>' +
            '<span class="idplaylistchange" style="visibility:hidden;">' + idPlaylist + '</span>' +
            '<span class="titleplaylist" style="visibility:hidden;">' + titlePlaylist + '</span>' +
            '</button>';

        const buttonDelete = formChangePlaylistAAA.querySelector('.delete-playlisttttt');
        buttonDelete.addEventListener('click', (e) => {
            fetch("http://127.0.0.1/Delete?id=" + idPlaylist, {
                method: "POST"
            });
        });

        buttonChangePlaylist = formChangePlaylistAAA.querySelector('.button-playlist-change');
        const blockChangePLaylist = document.querySelector('.block-form-change-playlist');

        buttonChangePlaylist.addEventListener('click', (e) => {
            e.preventDefault();

            const idplaylist = buttonChangePlaylist.querySelector('.idplaylistchange').innerHTML;
            const titleplaylist = buttonChangePlaylist.querySelector('.titleplaylist').innerHTML;

            blockChangePLaylist.querySelector('.id-playlist').value = idplaylist;
            blockChangePLaylist.querySelector('.title-playlist').value = titleplaylist;

            blockChangePLaylist.classList.add('show');
            background.classList.add('show');
            background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
            background.style.height = `${document.documentElement.offsetHeight}px`;
        });

        formChangePlaylistAAA.classList.toggle('show');
        formChangePlaylistAAA.style.top = e.pageY - 40 + 'px';
        formChangePlaylistAAA.style.left = e.pageX + 25 + 'px';
    } else if (e.target == threePointOther) {
        const idPlaylist = threePointOther.querySelector('.playlist-my-id').innerHTML;





        formChangePlaylistAAA.innerHTML = '<form asp-action="AddPlaylistToUser" asp-controller="Playlist" method="post" enctype="multipart/form-data">' +
            '<input type = "hidden" name = "playlistId" value = "' + idPlaylist + '" />' +
            '<button type="submit">Добавить на свою страницу</button></form>'

        formChangePlaylistAAA.innerHTML = '<form asp-action="RemovePlaylistFromUser" asp-controller="Playlist" method="post" enctype="multipart/form-data">' +
            '<input type = "hidden" name = "playlistId" value = "' + idPlaylist + '" />' +
            '<button type="submit">Удалить со своей страницы</button></form>';
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