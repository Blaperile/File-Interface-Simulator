function removeFieldSpecification(id) {
    $.ajax('/SpecificationSetup/RemoveFieldSpecificationRPC/', {
        type: "GET",
        data: { id: id }
    })
    .done(function (data) {
        var fieldSpecRow = document.getElementById('field_spec_' + id);
        fieldSpecRow.parentNode.removeChild(fieldSpecRow);
    })
    .fail(function (data) {
        var errorMessage = document.getElementById('delete_error_' + id);
        errorMessage.innerHTML = data.statusText;
    })
}