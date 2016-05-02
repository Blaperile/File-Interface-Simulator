var plusIconSource = '/images/plus_icon_small.png';
var minusIconSource = '/images/minus_icon.png';

function collapse(image, id) {
    if (image.src.indexOf('minus') > -1) {
        image.src = plusIconSource;
    } else {
        image.src = minusIconSource;
    }

    var element = document.getElementById(id);
    element.hidden = !element.hidden;
}