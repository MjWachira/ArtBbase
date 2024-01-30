window.ShowMessage = (type, message) => {
    if (type === 'success') {
         toggleSidebar()

        Swal.fire({
            title: "Added Successfully!",
            text: message,
            icon: "success"
        });
    }
    if (type === 'error') {
        Swal.fire({
            title: "Deleted!",
            text: message,
            icon: "success"
        });
    }
    if (type === 'edit') {
        toastr.success(message)
    }

    if (type === 'login') {
        toastr.success(message)
    }



}
function toggleNav() {
    var x = document.getElementById("myTopnav");
    var icon = document.querySelector('.topnav a.icon');

    if (x.className.indexOf("responsive") === -1) {
        x.className += " responsive";
        icon.innerHTML = '<div class="close">&#10006;</div>';
    } else {
        x.className = "topnav";
        icon.innerHTML = '<div class="hamburger">&#9776;</div>';
    }
}

