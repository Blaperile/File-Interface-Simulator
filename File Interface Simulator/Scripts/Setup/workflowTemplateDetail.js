var newRow = document.getElementById('newRow');
var saveButton = document.getElementById('save');

var sequenceNumber = document.getElementById('sequenceNumber');
var maxSequenceNumber = document.getElementById('maxSequenceNumber');
var name = document.getElementById('name');
var type = document.getElementById('type');
var errorMessage = document.getElementById('errorMessage');

function removeStep(workflowTemplateStepId, workflowTemplateId) {
      $.ajax('/WorkflowTemplateSetup/RemoveStepFromWorkflowTemplateRPC/', {
        type: "GET",
        data: { workflowTemplateStepId : workflowTemplateStepId, workflowTemplateId: workflowTemplateId}
    })
    .done(function (data) {
        var workflowTemplateStepRow = document.getElementById('workflow_templ_step_' + workflowTemplateStepId);
        workflowTemplateStepRow.parentNode.removeChild(workflowTemplateStepRow);
    })
    .fail(function (data) {
        var errorMessage = document.getElementById('delete_error_' + workflowTemplateStepId);
        errorMessage.innerHTML = data.statusText;
    })
}

function addNewRow(addButton) {
 
   newRow.hidden = false;
    addButton.parentNode.removeChild(addButton);
    saveButton.type = 'submit';
}

function addStep() {
    alert("addStep");
    if (Number(sequenceNumber.value) != sequenceNumber.value || Number(sequenceNumber.value) == 0) {
        errorMessage.innerHTML = 'Sequence number must be numeric.';
    } else if (sequenceNumber.value > maxSequenceNumber.innerHTML) {
        errorMessage.innerHTML = 'Sequence number can not be higher than ' + maxSequenceNumber.innerHTML;
    } else if (sequenceNumber.value < 0) {
        errorMessage.innerHTML = 'Sequence number must be at least 1';
    }
    else {
        errorMessage.innerHTML = '';
    }
}


