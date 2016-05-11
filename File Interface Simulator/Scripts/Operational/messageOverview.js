function goToMessage(id) {
    window.location.href = document.location.origin + "/Operational/MessageDetail/" + id;
}

function removeMessage(id) {
    $.ajax('/Operational/RemoveMessageRPC/', {
        type: "GET",
        data: { id: id }
    })
.done(function (data) {
    var messageRow = document.getElementById('message_' + id);
    messageRow.parentNode.removeChild(messageRow);
})
.fail(function (data) {
    var errorMessage = document.getElementById('delete_error_' + id);
    errorMessage.innerHTML = data.statusText;
})
}