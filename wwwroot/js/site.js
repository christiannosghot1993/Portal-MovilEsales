// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Basic example
function toogleSideBar() {
    console.log('toogleSideBar');
    event.preventDefault();
    document.body.classList.toggle('sb-sidenav-toggled');
    localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
}


/**
 * 
 * Sirve para tener submenus en el menu principal
 */
function mostrarOcultarSubmenu(id) {
    const element = document.getElementById(id).getElementsByTagName("ul");
    const ancla = document.getElementById(id).getElementsByTagName("a");
    if (element[0].className == 'aparecer-submenu') {
        element[0].className = 'desaparecer-submenu';
        ancla[0].className = 'ancla-principal-previous';
    } else {
        element[0].className = 'aparecer-submenu';
        ancla[0].className = 'ancla-principal-next';
    }

}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})
