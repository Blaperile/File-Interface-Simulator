function selectWorkflowTemplate(id) {
    $.ajax('/WorkflowTemplateSetup/SelectWorkflowTemplateRPC/', {
        type: "GET",
        data: { id: id}
    })
    .done(function (data) {
        document.location.reload(true);
    })
}