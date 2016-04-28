function changeType(newType) {
    var divOutputDirectories = document.getElementById('outputDirectories');
    var divInputDirectories = document.getElementById('inputDirectories');

    if (newType == 'output') {
        divOutputDirectories.hidden = false;
        divInputDirectories.hidden = true;
    } else {
        divOutputDirectories.hidden = true;
        divInputDirectories.hidden = false;
    }
}