window.ShowMessage = (type, message) => {
    if (type === 'success') {
     
        Swal.fire({
            title: "Added Successfully!",
            text: message,
            icon: "success"
        });
    }
    if (type === 'error') {
        Swal.fire({
            title: "Error!",
            text: message,
            icon: "error"
        });
    }
    if (type === 'edit' || type === 'login') {
        toastr.success(message);
    }
};

// Define your alert implementation here
window.ShowAlert = function (type, message) {
    alert(message); // Example: Use the browser's native alert function
};

// Function to toggle navigation
/*
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
*/
// Function to show toastr notifications
window.ShowToastr = function (type, message) {
    if (type === 'success') {
        toastr.success(message);
    } else if (type === 'error') {
        toastr.error(message);
    }
};
