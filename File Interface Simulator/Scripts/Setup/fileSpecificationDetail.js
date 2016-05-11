function goToFieldCondition(id) {
    goTo("/SpecificationSetup/FileSpecificationFieldDetail/" + id);
}

function goToGroupCondition(id) {
    goTo("/SpecificationSetup/FileSpecificationGroupDetail/" + id);
}

function goTo(path) {
    window.location.href = document.location.origin + path;
}