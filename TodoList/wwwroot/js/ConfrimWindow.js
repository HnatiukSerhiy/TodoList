let elems = document.querySelectorAll('.del-link');

let confirmIt = function (event) {
    if (!confirm('Are you sure?')) event.preventDefault();
};
for (var i = 0, l = elems.length; i < l; i++) {
    elems[i].addEventListener('click', confirmIt, false);
}