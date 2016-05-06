function selectWorkflowTemplate(id) {
    $.ajax('/WorkflowTemplateSetup/SelectWorkflowTemplateRPC/', {
        type: "GET",
        data: { id: id}
    })
    .done(function (data) {
        document.location.reload(true);
    })
}

function removeWorkflowTemplate(id) {
    $.ajax('/WorkflowTemplateSetup/RemoveWorkflowTemplateRPC/', {
        type: "GET",
        data: { id: id }
    })
    .done(function (data) {
        var workflowTemplateRow = document.getElementById('workflow_templ_' + id);
        workflowTemplateRow.parentNode.removeChild(workflowTemplateRow);
    })
    .fail(function (data) {
        var errorMessage = document.getElementById('delete_error_' + id);
        errorMessage.innerHTML = data.statusText;
    })
}