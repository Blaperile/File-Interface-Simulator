function changeType() {
    if (document.getElementById('rbOutput').checked) {
        document.getElementById('outputDirectories').hidden = false;
        document.getElementById('inputDirectories').hidden = true;
    } else {
        document.getElementById('outputDirectories').hidden = true;
        document.getElementById('inputDirectories').hidden = false;
    }
}