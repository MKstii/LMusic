const divCountRequests = document.querySelector('.ddddddddd');
const cooooount = fetch("http://127.0.0.1/GetFriendsRequestCount")
    .then(requests => requests.json())
    .then(count => {
        if (count > 0) {
            divCountRequests.innerHTML = `<div class="count-requestststs">
                                <span>${count}</span>
                                </div>`;
        }
    })

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
            //blockPlaylist = document.querySelector('.current-playlist'),
            blockChangePlaylist = document.querySelector('.block-form-change-playlist'),
            blockChangeMusic = document.querySelector('.block-form-change-music'),
            blockOpenPlaylist = document.querySelector('.block-open-playlist'),
            blockOpenMusicInPlaylist = document.querySelector('.block-change-music-playlist'),
            blockAddInPLaylist = document.querySelector('.block-add-in-playlist');

        background.classList.remove('show');

        if (formSettings?.classList.contains('show')) formSettingProfile.classList.remove('show');

        //if (blockPlaylist.classList.contains('show')) blockPlaylist.classList.remove('show');

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
    popupBlockChangeMysicMYYYYYYYYY = document.querySelector('.popup-change-mymusic-MYYYYYY'),
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
            titleeee = mus.querySelector('.HHHHHHHHHHHHH').innerHTML;
            blockCurrentMusic = document.querySelector('.block-current-music');


        const arrayMusicsAllMy = document.querySelectorAll('.block-one-music');
        let indexCurrentMusic = Array.from(arrayMusicsAllMy).findIndex(el => {
            return el.querySelector('.audio').src == currentAudio.src;
        });
        const startIndex = indexCurrentMusic;

        let arrayNeedMusic = [];
        for (var i in arrayMusicsAllMy) {
            if (i >= indexCurrentMusic) {
                const avatarSrc = arrayMusicsAllMy[i].querySelector('.avatar-music').src;
                const musicSrc = arrayMusicsAllMy[i].querySelector('.audio').src;
                arrayNeedMusic.push([avatarSrc, musicSrc]);
            }
        };

        let index = 0;
        blockCurrentMusic.innerHTML = `<img class="current-music-avatar" src="${arrayNeedMusic[index][0]}" />
        <span class="title-musssiiiiicccc">${titleeee}</span>
    <audio class="current-audio" src="${arrayNeedMusic[index][1]}" controls controlsList="nodownload noplaybackrate"></audio>
    <button><img class="icon-control icon-last-music" src="/img/icon-last-music.png" /></button>
    <button><img class="icon-control icon-next-music" src="/img/icon-next-music.png" /></button>`;
        const currentLineAudio = blockCurrentMusic.querySelector('.current-audio');
        currentLineAudio.play();

        const buttonPrevAudio = blockCurrentMusic.querySelector('.icon-last-music');
        const buttonNextAudio = blockCurrentMusic.querySelector('.icon-next-music');

        blockCurrentMusic.addEventListener('click', (e) => {
            if (e.target == buttonPrevAudio) {
                index = index - 1;
                if (index < 0) index = 0;
                currentLineAudio.src = arrayNeedMusic[index][1];
                blockCurrentMusic.querySelector('.current-music-avatar').src = arrayNeedMusic[index][0];
                currentLineAudio.play();
            } else if (e.target == buttonNextAudio) {
                index = index + 1;
                if (index > arrayNeedMusic.length - 1) index = 0;
                currentLineAudio.src = arrayNeedMusic[index][1];
                blockCurrentMusic.querySelector('.current-music-avatar').src = arrayNeedMusic[index][0];
                currentLineAudio.play();
            }
        })

        currentLineAudio.onended = function () {
            index++;
            if (index > arrayNeedMusic.length - 1) index = 0;
            currentLineAudio.src = arrayNeedMusic[index][1];
            blockCurrentMusic.querySelector('.current-music-avatar').src = arrayNeedMusic[index][0];
            currentLineAudio.play();
        }
        blockCurrentMusic.classList.add('display-flex');
    }
    else if (e.target == threePoint) {
        e.preventDefault();
        const parentBlockControl = e.target.closest('.block-control');
        const musicid = parentBlockControl.querySelector('.music-my-id').innerHTML;
        const musictitle = parentBlockControl.querySelector('.music-my-title').innerHTML;
        const musicmusician = parentBlockControl.querySelector('.music-my-musician').innerHTML;
        if (popupBlockChangeMusic) {
            popupBlockChangeMusic.querySelector('.musicidpopup').value = musicid;
            popupBlockChangeMusic.querySelector('.musiciddelete').value = musicid;
            popupBlockChangeMusic.querySelector('.musiciddeleteplalist').value = musicid;
            popupBlockChangeMusic.querySelector('.music-change-idd').innerHTML = musicid;
            popupBlockChangeMusic.querySelector('.titlemusiccc').value = musictitle;
            popupBlockChangeMusic.querySelector('.musicianmusiccc').value = musicmusician;

            popupBlockChangeMusic.classList.toggle('show');
            popupBlockChangeMusic.style.top = e.pageY - 50 + 'px';
            popupBlockChangeMusic.style.left = e.pageX - 280 + 'px';
        }
        

        if (popupBlockChangeMysicMYYYYYYYYY) {
            popupBlockChangeMysicMYYYYYYYYY.querySelector('.musicidpopup').value = musicid;
            popupBlockChangeMysicMYYYYYYYYY.querySelector('.musiciddelete').value = musicid;
            popupBlockChangeMysicMYYYYYYYYY.querySelector('.music-change-idd').innerHTML = musicid;
            popupBlockChangeMysicMYYYYYYYYY.querySelector('.titlemusiccc').value = musictitle;
            popupBlockChangeMysicMYYYYYYYYY.querySelector('.musicianmusiccc').value = musicmusician;

            const musicInFavorite = fetch("http://127.0.0.1/IsMusicInFavorite?musicId=" + musicid)
                .then(response => response.json())
                .then(isFavorite => {
                    const FORMAA = popupBlockChangeMysicMYYYYYYYYY.querySelector('.form-add-music');
                    if (!isFavorite) {
                        FORMAA.innerHTML = `<input type="hidden" id="musicidaddplalist" class="aaaaaaaaaaaaaaa musiciddeleteplalist" name="musicId" value="${musicid}" />
                <button type="submit" class="popup-button button-delete-music-favorite">
                    <img class="popup-image" src="~/img/icon-plus.png" />
                    Добавить в избранное
                </button>`;
                    } else {
                        FORMAA.innerHTML = '';
                    }
                });

            popupBlockChangeMysicMYYYYYYYYY.classList.toggle('show');
            popupBlockChangeMysicMYYYYYYYYY.style.top = e.pageY - 50 + 'px';
            popupBlockChangeMysicMYYYYYYYYY.style.left = e.pageX - 280 + 'px';
        }
        
        

        

        if (blockAddInPLaylist.classList.contains('show')) blockAddInPLaylist.classList.remove('show');

        const buttonChangeMusic = popupBlockChangeMusic?.querySelector('.button-change-music'),
            blockChangeMusic = document.querySelector('.block-form-change-music');

        buttonChangeMusic?.addEventListener('click', (e) => {
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

        if (popupBlockChangeMusic) {
            buttonaddInPlaylist = popupBlockChangeMusic.querySelector('.button-show-addinplaylist');
        } else if (popupBlockChangeMysicMYYYYYYYYY) {
            buttonaddInPlaylist = popupBlockChangeMysicMYYYYYYYYY.querySelector('.button-show-addinplaylist');
        }
        

        buttonaddInPlaylist.addEventListener('click', (e) => {
            blockAddInPLaylist.querySelector('.musicId').value = musicid;

            const divBlockCheckboxMyPlaylists = blockAddInPLaylist.querySelector('.block-checkbox-my-playlists');
            var htmlPlaylists = '';

            const playlists = fetch("http://127.0.0.1/GetMyPlaylists")
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
                        '<img class="popup-image" src="/img/icon-krestic.png"" />' +
                        'Убрать из избранного' +
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

                    const playlists = fetch("http://127.0.0.1/GetMyPlaylists")
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

    //if (playlistOpen.classList.contains('show')) playlistOpen.classList.remove('show');
});
    
const buttonImageOpenPlaylist = document.querySelectorAll('.playlist'),
    blockOpenPlaylist = document.querySelector('.block-open-playlist');

async function AAAAAAAA() {
    

    buttonImageOpenPlaylist.forEach(item => item.addEventListener('click', async (e) => {
        e.preventDefault();
        const threePoint = item.querySelector('.image-for-my-playlist'),
            threePointOther = item.querySelector('.image-for-other-playlist');

        if (e.target != threePoint && e.target != threePointOther) {
            const divMusicPlaylist = blockOpenPlaylist.querySelector('.playlist-music');
            divMusicPlaylist.innerHTML = '';
            const namePlaylist = item.querySelector('.info-my-playlist .tiiiitle-playlist').innerHTML;
            const imagePlaylistSrc = item.querySelector('.playlist-avatar').src;
            const idPlaylist = item.querySelector('.info-my-playlist .iiiid-playlist').innerHTML;

            blockOpenPlaylist.querySelector('.title-open-playlist').innerHTML = namePlaylist;
            blockOpenPlaylist.querySelector('.avatar-playlist').src = imagePlaylistSrc;

            var htmlPlaylists = '';

            let isMy = await isMyPlaylist(idPlaylist);

            const playlistmusic = fetch("http://127.0.0.1/playlist/" + idPlaylist)
                .then(response => response.json())
                .then(playlistmusic => {
                    

                    for (var i = 0; i < playlistmusic.length; i++) {
                        
                        if (!isMy) {
                            htmlPlaylists += '<div class="block-one-music"><div class="music">' +
                                '<div class="info-music">' +
                                '<audio class="audio" src="' + playlistmusic[i]['musicPath'] + '"></audio>' +
                                '<button class="play-music"><img class="avatar-music" src="' + playlistmusic[i]['photoPath'] + '" /></button>' +
                                '<div class="block-name-music">' +
                                '<span class="HHHHHHHHHHHHH">' + playlistmusic[i]['title'] + '</span>' +
                                '<span>' + playlistmusic[i]['musician'] + '</span>' +
                                '</div>' +
                                '</div>' +
                                '</div>' +
                                '</div>';
                        } else {
                            htmlPlaylists += '<div class="block-one-music"><div class="music">' +
                                '<div class="info-music">' +
                                '<audio class="audio" src="' + playlistmusic[i]['musicPath'] + '"></audio>' +
                                '<button class="play-music"><img class="avatar-music" src="' + playlistmusic[i]['photoPath'] + '" /></button>' +
                                '<div class="block-name-music">' +
                                '<span class="HHHHHHHHHHHHH">' + playlistmusic[i]['title'] + '</span>' +
                                '<span>' + playlistmusic[i]['musician'] + '</span>' +
                                '</div>' +
                                '</div>' +
                                '<button type="submit" class="block-control-music-playlist">' +
                                '<input type = "hidden" class="musicId" name="musicId" value = "' + playlistmusic[i]['id'] + '" />' +
                                '<input type="hidden" class="playlistId" name="playlistId" value="' + idPlaylist + '"/>' +
                                '<img class="music-contol-my-playlist" src="/img/icon-krestic.png" />' +
                                '</button>' +
                                '</div>' +
                                '</div>';
                        }

                        divMusicPlaylist.innerHTML = htmlPlaylists;

                        const blockMusic = blockOpenPlaylist.querySelectorAll('.block-one-music');
                        blockMusic.forEach(mus => mus.addEventListener('click', (e) => {
                            const threePoint = mus.querySelector('.music-contol-my-playlist');

                            if (e.target != threePoint) {
                                const currentAudio = mus.querySelector('.audio'),
                                    currentAvatar = mus.querySelector('.avatar-music'),
                                    blockCurrentMusic = document.querySelector('.block-current-music'),
                                    titleeee = mus.querySelector('.HHHHHHHHHHHHH').innerHTML;

                                const arrayMusicsAllMy = blockOpenPlaylist.querySelectorAll('.block-one-music');
                                let indexCurrentMusic = Array.from(arrayMusicsAllMy).findIndex(el => {
                                    return el.querySelector('.audio').src == currentAudio.src;
                                });

                                let arrayNeedMusic = [];
                                for (var i in arrayMusicsAllMy) {
                                    if (i >= indexCurrentMusic) {
                                        const avatarSrc = arrayMusicsAllMy[i].querySelector('.avatar-music').src;
                                        const musicSrc = arrayMusicsAllMy[i].querySelector('.audio').src;
                                        arrayNeedMusic.push([avatarSrc, musicSrc]);
                                    }
                                }

                                let index = indexCurrentMusic;
                                blockCurrentMusic.innerHTML = `<img class="current-music-avatar" src="${arrayNeedMusic[index][0]}" />
                                <span class="title-musssiiiiicccc">${titleeee}</span>
                            <audio class="current-audio" src="${arrayNeedMusic[index][1]}" controls controlsList="nodownload noplaybackrate"></audio>
                            <button><img class="icon-control icon-loop-music" src="/img/icon-povtor.png" /></button>
                            <button><img class="icon-control icon-last-music" src="/img/icon-last-music.png" /></button>
                            <button><img class="icon-control icon-next-music" src="/img/icon-next-music.png" /></button>`;

                                const buttonPrevAudio = blockCurrentMusic.querySelector('.icon-last-music');
                                const buttonNextAudio = blockCurrentMusic.querySelector('.icon-next-music');

                                blockCurrentMusic.addEventListener('click', (e) => {
                                    if (e.target == buttonPrevAudio) {
                                        index = index - 1;
                                        if (index < 0) index = 0;
                                        currentLineAudio.src = arrayNeedMusic[index][1];
                                        blockCurrentMusic.querySelector('.current-music-avatar').src = arrayNeedMusic[index][0];
                                        currentLineAudio.play();
                                    } else if (e.target == buttonNextAudio) {
                                        index = index + 1;
                                        if (index > arrayNeedMusic.length - 1) index = 0;
                                        currentLineAudio.src = arrayNeedMusic[index][1];
                                        blockCurrentMusic.querySelector('.current-music-avatar').src = arrayNeedMusic[index][0];
                                        currentLineAudio.play();
                                    }
                                })


                                const currentLineAudio = blockCurrentMusic.querySelector('.current-audio');
                                currentLineAudio.play();

                                currentLineAudio.onended = function () {
                                    index++;
                                    if (index > arrayNeedMusic.length) index = 0;
                                    currentLineAudio.src = arrayNeedMusic[index][1];
                                    blockCurrentMusic.querySelector('.current-music-avatar').src = arrayNeedMusic[index][0];
                                    currentLineAudio.play();
                                }
                                blockCurrentMusic.classList.add('display-flex');
                            } else {
                                const musicId = mus.querySelector('.musicId').value;
                                const playlistId = mus.querySelector('.playlistId').value;

                                fetch("http://127.0.0.1/DeleteMusicFromPlaylist?musicId=" + musicId + "&playlistId=" + playlistId, {
                                    method: "POST"
                                });

                                location.reload();
                            }
                        }))

                    }
                });

            blockOpenPlaylist.classList.add('show');
            background.classList.add('show');
            background.style.width = `${document.documentElement.clientWidth + navigation.style.width}px`;
            background.style.height = `${document.documentElement.offsetHeight}px`;
        } else if (e.target == threePoint) {
            const parentBlockA = threePoint.closest('.button-for-my-playlist');
            const formChangePlaylistAAA = document.querySelector('.block-change-my-playlist');

            const idPlaylist = parentBlockA.querySelector('.playlist-my-id').value;
            const titlePlaylist = parentBlockA.querySelector('.playlist-my-title').value;

            formChangePlaylistAAA.innerHTML = '<form asp-action="Delete" asp-controller="Playlist" method="post" enctype="multipart/form-data">' +
                '<input type = "hidden" name = "id" value = "' + idPlaylist + '" />' +
                '<button type="submit" class="delete-playlisttttt buttoooooooooooon"><img src="/img/icon-delete.png" width="20px" height="20px"/>Удалить</button></form>' +
                '<button class="button-playlist-change buttoooooooooooon">' +
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
            const parentBlock = threePointOther.closest('.button-for-other-playlist');

            const formChangePlaylistBBB = document.querySelector('.block-change-my-playlist');
            const idPlaylist = parentBlock.querySelector('.playlist-my-id').value;

            const otherPlaylist = fetch("http://127.0.0.1/UserHasPlaylist?playlistId=" + idPlaylist)
                .then(response => response.json())
                .then(isMy => {
                    let innertTextpopup = '';
                    if (isMy) {
                        innertTextpopup = '<button type="submit" class="button-remove-playlist-forme"><input type = "hidden" name = "playlistId" value = "' + idPlaylist + '" />Удалить со своей страницы</button>';
                    } else {
                        innertTextpopup = '<button type="submit" class="button-add-playlist-forme"><input type = "hidden" name = "playlistId" value = "' + idPlaylist + '" />Добавить на свою страницу</button>';
                    }
                    formChangePlaylistBBB.innerHTML = innertTextpopup;

                    const buttonRemoveOtherPlaylistForMe = formChangePlaylistBBB.querySelector('.button-remove-playlist-forme');
                    buttonRemoveOtherPlaylistForMe?.addEventListener('click', (e) => {
                        fetch("http://127.0.0.1/RemovePlaylistFromUser?playlistId=" + idPlaylist, {
                            method: "POST"
                        });

                        window.location.href = window.location.href;
                    });

                    const buttonAddOtherPlaylistForMe = formChangePlaylistBBB.querySelector('.button-add-playlist-forme');
                    buttonAddOtherPlaylistForMe?.addEventListener('click', (e) => {
                        fetch("http://127.0.0.1/AddPlaylistToUser?playlistId=" + idPlaylist, {
                            method: "POST"
                        });

                        location.reload();
                    });
                });

            formChangePlaylistBBB.classList.toggle('show');
            formChangePlaylistBBB.style.top = e.pageY - 40 + 'px';
            formChangePlaylistBBB.style.left = e.pageX + 25 + 'px';
        }
    }));
}

AAAAAAAA();

async function isMyPlaylist(idPlaylist) {
    const d = await fetch("http://127.0.0.1/IsMyPlaylist?playlistId=" + idPlaylist);
    return await d.json();
}

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

// у юзера отоюражать либо заявка отправлена либо отправить заявку
if (document.querySelector('.add-from-friends-my')) {
    const formAddRequests = document.querySelector('.add-from-friends-my');
    const tgIsUser = formAddRequests?.closest('.block-number-space').querySelector('.hiddenuseridddddddddddd').value;
    let htmlValues = '';

    const resultt = fetch("http://127.0.0.1/HasFriendRequest?addresseTgId=" + tgIsUser)
        .then(response => response.json())
        .then(isHas => {
            if (isHas) {
                htmlValues = '<span>Заявка отправлена</span>';
            } else {
                htmlValues = '<input type="hidden" name="addresseTgId" value="' + tgIsUser + '">' +
                    '<button class="button button-add-friend">' +
                    '<span>Отправить заявку в друзья</span></button>';
            }
            formAddRequests.innerHTML = htmlValues;
        })
}

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