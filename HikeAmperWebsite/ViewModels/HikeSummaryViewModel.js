var summary;

function GetName() {
    var user = document.getElementById("user").defaultValue;
    user = user.replace(eval("/[^a-zA-Z0-9]/g"), "");
    return user;
}

$(document).ready(function () {
    var user = GetName();
    $.ajax({
        url: "https://hikeservice.azurewebsites.net/hikes/" + user,
        contentType: "application/json",
        type: "GET",
        async: false,
        success: function (data) {
            summary = ko.mapping.fromJS(data);
            ko.applyBindings(summary);
        },
        error: function () {
            alert("error occured");
        }
    });
});