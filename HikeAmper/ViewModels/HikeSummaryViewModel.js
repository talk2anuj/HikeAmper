var hikeSummary;

$(document).ready(function () {
    var userName = document.getElementById("userName").defaultValue;
    $.ajax({
        url: "https://hikeservice.azurewebsites.net/hikes/" + userName + "/0",
        contentType: "application/json",
        type: "GET",
        crossDomain: true,
        async: false,
        success: function (data) {
            hikeSummary = ko.mapping.fromJS(data);
            ko.applyBindings(hikeSummary);
        },
        error: function () {
            alert("error occured");
        }
    });
});
