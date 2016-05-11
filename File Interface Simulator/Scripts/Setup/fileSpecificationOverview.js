function goToFileSpecification(id) {
    window.location.href = document.location.origin + "/SpecificationSetup/FileSpecificationDetail/" + id;
}

function removeFileSpecification(id) {
    $.ajax('/SpecificationSetup/RemoveFileSpecificationRPC/', {
        type: "GET",
        data: { id: id }
    })
    .done(function (data) {
        var fileSpecRow = document.getElementById('file_spec_' + id);
        fileSpecRow.parentNode.removeChild(fileSpecRow);
    })
    .fail(function (data) {
        var errorMessage = document.getElementById('delete_error_' + id);
        errorMessage.innerHTML = data.statusText;
    })
}