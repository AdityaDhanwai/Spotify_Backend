let allSongs = [];
let userSongs = [];
let currentSong = new Audio();
let currentSongIndex = -1;

const secondsToMinutesSeconds = (seconds) => {
    if (isNaN(seconds) || seconds < 0) return "00:00";
    let minutes = Math.floor(seconds / 60);
    let remainingSeconds = Math.floor(seconds % 60);
    return `${String(minutes).padStart(2, '0')}:${String(remainingSeconds).padStart(2, '0')}`;
};

//const playMusic = (filePath, title = "Unknown") => {
//    currentSong.src = `${filePath}`;
//    console.log(currentSong.src);
//    currentSong.play();
//    document.getElementById("play").src = "assets/pause.svg";
//    document.querySelector(".songinfo").innerText = title;
//};
//const playMusic = (filePath, title = "Unknown", index = -1) => {
//    currentSong.src = `${filePath}`;
//    currentSong.play();
//    document.getElementById("play").src = "assets/pause.svg";
//    document.querySelector(".songinfo").innerText = title;

//    // Set the current song index!
//    currentSongIndex = index;
//};


const playMusic = (filePath, title = "Unknown", index = -1, autoPlay = true) => {
    currentSong.src = `${filePath}`;
    currentSongIndex = index;
    document.querySelector(".songinfo").innerText = title;
    document.getElementById("play").src = autoPlay ? "assets/pause.svg" : "assets/play.svg";

    if (autoPlay) {
        currentSong.play();
    }
};


//const fetchSongs = async () => {
//    const all = await fetch("/api/allsongs").then(res => res.json());
//    const user = await fetch("/api/usersongs").then(res => res.json());
//    allSongs = all;
//    userSongs = user;
//};

const fetchSongs = async () => {
    try {
        const all = await fetch("/api/allsongs").then(res => res.json());
        allSongs = all;
    } catch (error) {
        console.error("Error fetching all songs:", error);
        allSongs = [];
    }

    try {
        const userRes = await fetch("/api/usersongs");
        if (userRes.ok) {
            userSongs = await userRes.json();
        } else {
            userSongs = [];
        }
    } catch (error) {
        console.warn("User not logged in or error fetching favorites:", error);
        userSongs = [];
    }
};



const renderAllSongs = () => {
    const container = document.querySelector(".cardContainer");
    container.innerHTML = "";

    //allSongs.forEach(song => {
    //    const card = document.createElement("div");
    //    card.className = "card";
    //    card.innerHTML = `
    //        <div class="play">
    //            <svg width="16" height="16" viewBox="0 0 24 24" fill="none">
    //                <path d="M5 20V4L19 12L5 20Z" stroke="#141B34" fill="#000" stroke-width="1.5" stroke-linejoin="round"/>
    //            </svg>
    //        </div>
    //        <img src="${song.coverImage}" alt="${song.title}">
    //        <h2>${song.title}</h2>
    //        <p>${song.artist}</p>
    //    `;

    //    card.addEventListener("click", () => {
    //        playMusic(song.filePath, song.title);
    //    });

    //    container.appendChild(card);
    //});
    allSongs.forEach((song, index) => {
        const card = document.createElement("div");
        card.className = "card";
        card.innerHTML = `
                <div class="play">
                <svg width="16" height="16" viewBox="0 0 24 24" fill="none">
                    <path d="M5 20V4L19 12L5 20Z" stroke="#141B34" fill="#000" stroke-width="1.5" stroke-linejoin="round"/>
                </svg>
            </div>
        <img src="${song.coverImage}" alt="${song.title}">
        <h2>${song.title}</h2>
        <p>${song.artist}</p>
    `;

        card.addEventListener("click", () => {
            playMusic(song.filePath, song.title, index); // Pass index
        });

        container.appendChild(card);
    });

};


const renderUserSongs = () => {
    const list = document.querySelector(".songList ul");
    list.innerHTML = "";

    userSongs.forEach((song, index) => {
        const li = document.createElement("li");
        li.innerHTML = `
            <img class="invert" src="assets/music.svg" alt="music_logo">
            <div class="info">
                <div>${song.title}</div>
                <div>${song.artist}</div>
            </div>
            <div class="playnow">
                <span>Play Now</span>
                <img class="invert" src="assets/play.svg" alt="">
            </div>
        `;
        li.addEventListener("click", () => {
            playMusic(song.filePath, song.title, index); // track index
        });
        list.appendChild(li);
    });
};



async function showSongList() {
    const songList = document.querySelector('.songList');
    //const createPlaylist = document.querySelector('.create-playlist');
    const podcast = document.querySelector('.podcast');
    const language = document.querySelector('.language');

    // Fetch favorite songs fresh when opened
    const user = await fetch("/api/usersongs").then(res => res.json());
    userSongs = user;
    renderUserSongs();

    songList.classList.remove('hidden');
    //  createPlaylist.classList.add('hidden');
    podcast.classList.add('hidden');
    language.classList.add('hidden');
    plusIcon.style.display = 'none';
    minusIcon.style.display = 'inline-block';
}

async function main() {
    await fetchSongs();
    renderAllSongs();
    renderUserSongs();
    if (allSongs.length > 0) {
        playMusic(allSongs[0].filePath, allSongs[0].title, 0, false);
    }
    // Playback buttons
    document.getElementById("play").addEventListener("click", () => {
        if (currentSong.paused) {
            currentSong.play();
            document.getElementById("play").src = "assets/pause.svg";
        } else {
            currentSong.pause();
            document.getElementById("play").src = "assets/play.svg";
        }
    });

    //document.getElementById("previous").addEventListener("click", () => {
    //    if (currentSongIndex > 0) {
    //        currentSongIndex--;
    //        playMusic(allSongs[currentSongIndex].filePath, allSongs[currentSongIndex].title);
    //    }
    //});

    //document.getElementById("next").addEventListener("click", () => {
    //    if (currentSongIndex < allSongs.length - 1) {
    //        currentSongIndex++;
    //        playMusic(allSongs[currentSongIndex].filePath, allSongs[currentSongIndex].title);
    //    }
    //});
    document.getElementById("previous").addEventListener("click", () => {
        if (currentSongIndex > 0) {
            currentSongIndex--;
            playMusic(
                allSongs[currentSongIndex].filePath,
                allSongs[currentSongIndex].title,
                currentSongIndex,
                true
            );
        }
    });

    document.getElementById("next").addEventListener("click", () => {
        if (currentSongIndex < allSongs.length - 1) {
            currentSongIndex++;
            playMusic(
                allSongs[currentSongIndex].filePath,
                allSongs[currentSongIndex].title,
                currentSongIndex,
                true
            );
        }
    });

    // Seekbar functionality
    const seekbar = document.querySelector(".seekbar");
    const circle = document.querySelector(".circle");
    const progress = document.querySelector(".progress");
    let isDragging = false;

    currentSong.addEventListener("timeupdate", () => {
        if (!isDragging) {
            let percent = (currentSong.currentTime / currentSong.duration) * 100 || 0;
            circle.style.left = percent + "%";
            progress.style.width = percent + "%";
            document.querySelector(".songtime").innerText =
                `${secondsToMinutesSeconds(currentSong.currentTime)} / ${secondsToMinutesSeconds(currentSong.duration)}`;
        }
    });

    seekbar.addEventListener("click", (e) => {
        let percent = (e.clientX - seekbar.getBoundingClientRect().left) / seekbar.offsetWidth * 100;
        currentSong.currentTime = (currentSong.duration * percent) / 100;
    });

    circle.addEventListener("mousedown", () => { isDragging = true; });
    document.addEventListener("mouseup", () => { isDragging = false; });

    // Hamburger
    document.querySelector(".hamburger").addEventListener("click", () => {
        document.querySelector(".left").style.left = "0";
    });
    document.querySelector(".close").addEventListener("click", () => {
        document.querySelector(".left").style.left = "-130%";
    });

    // Toggle songList
    //const plusIcon = document.querySelector('.add');
    //const minusIcon = document.querySelector('.minus');
    const songList = document.querySelector('.songList');
    //const createPlaylist = document.querySelector('.create-playlist');
    const podcast = document.querySelector('.podcast');
    const language = document.querySelector('.language');

    //function showSongList() {
    //    songList.classList.remove('hidden');
    //    createPlaylist.classList.add('hidden');
    //    podcast.classList.add('hidden');
    //    language.classList.add('hidden');
    //    plusIcon.style.display = 'none';
    //    minusIcon.style.display = 'inline-block';
    //}
    

    function hideSongList() {
        songList.classList.add('hidden');
       // createPlaylist.classList.remove('hidden');
        podcast.classList.remove('hidden');
        language.classList.remove('hidden');
        plusIcon.style.display = 'inline-block';
        minusIcon.style.display = 'none';
    }

    plusIcon.addEventListener('click', showSongList);
    podcastbtn.addEventListener('click', showSongList);
    minusIcon.addEventListener('click', hideSongList);
    minusIcon.style.display = 'none';


    

}

main();
const plusIcon = document.querySelector('.add');
const podcastbtn = document.querySelector('.podcast-btn');
const minusIcon = document.querySelector('.minus');
function ButtonClick() {
    alert("Coming Soon..");
}

//function addToFavorites() {
//    const song = allSongs[currentSongIndex];
//    if (!song) {
//        console.warn("No song is currently selected.");
//        return;
//    }

//    fetch('/api/favorite', {
//        method: "POST",
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(song.id)
//    })
//    .then(res => res.json())
//    .then(result => {
//        if (result.success) {
//            Swal.fire({
//                icon: 'success',
//                title: 'Added to Favorites!',
//                timer: 2000
//            });
//        } else {
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops!',
//                text: 'Something went wrong!'
//            });
//        }
//    })
//    .catch(err => {
//        console.error("Error adding to favorites:", err);
//        Swal.fire({
//            icon: 'error',
//            title: 'Failed',
//            text: 'Request failed. See console.'
//        });
//    });
//}

function addToFavorites() {
    if (!isUserAuthenticated) {
        Swal.fire({
            icon: 'info',
            title: 'Login Required!',
            text: 'Please login to add songs to your favorites.',
            confirmButtonText: 'OK'
        });
        return;
    }

    const song = allSongs[currentSongIndex];
    if (!song) {
        Swal.fire({
            icon: 'warning',
            title: 'No song playing!',
            text: 'Please play a song first.',
            confirmButtonText: 'OK'
        });
        return;
    }

    fetch('/api/favorite', {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song.id)
    })
        .then(res => res.json())
        .then(result => {
            if (result.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Added to Favorites!',
                    timer: 2000
                });
                refreshFavoritesList(); // Refresh List
            } else {
                Swal.fire({
                    icon: 'info',
                    title: 'Already Added',
                    text: result.message
                });
            }
        })
        .catch(err => {
            console.error("Error adding to favorites:", err);
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: 'Request failed. See console.'
            });
        });
}


function RemoveFromFavorites() {
    if (!isUserAuthenticated) {
        Swal.fire({
            icon: 'info',
            title: 'Login Required!',
            text: 'Please login to remove song from your favourites.',
            confirmButtonText: 'OK'
        });
        return;
    }
    const song = allSongs[currentSongIndex];
    if (!song) {
        console.warn("No song is currently selected.");
        return;
    }

    fetch('/api/unfavorite', {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(song.id)
    })
        .then(res => res.json())
        .then(result => {
            if (result.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Removed from Favorites!',
                    timer: 2000
                });
                refreshFavoritesList();// Refreshing the list dynamically
            } else {
                Swal.fire({
                    icon: 'info',
                    title: 'Not a Favorite',
                    text: result.message
                });
            }
        })
        .catch(err => {
            console.error("Error removing from favorites:", err);
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: 'Request failed. See console.'
            });
        });
}

function refreshFavoritesList() {
    minusIcon.click(); // Hide it
    setTimeout(() => {
        plusIcon.click(); // Show it again after short delay
    }, 200);
}




