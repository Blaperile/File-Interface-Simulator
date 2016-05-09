function removeWorkflow(id) {
    $.ajax('/Operational/RemoveWorkflowRPC/', {
        type: "GET",
        data: { id: id }
    })
.done(function (data) {
    var workflowRow = document.getElementById('workflow_' + id);
    workflowRow.parentNode.removeChild(workflowRow);
})
.fail(function (data) {
    var errorMessage = document.getElementById('delete_error_' + id);
    errorMessage.innerHTML = data.statusText;
})
}