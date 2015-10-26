var summary;
var hikeExists = ko.observable(false);
var hikeUrl = ko.observable("");

function GetName() {
    var user = document.getElementById("user").defaultValue;
    user = user.replace(eval("/[^a-zA-Z0-9]/g"), "");
    return user;
}

$(document).ready(function () {
    hikeExists(false);
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
            alert("error occurred");
        }
    });
});

function AddHike() {
    var user = GetName();
    var data = "{\"Value\":\"" + hikeUrl() + "\"";
    hikeUrl("");
    $.ajax({
        url: "https://hikeservice.azurewebsites.net/hikes/" + user,
        contentType: "application/json",
        type: "POST",
        data: data,
        async: false,
        success: function (data) {
            var hikeDetails = ko.mapping.fromJS(data);
            if (hikeDetails().length > 0) {
                summary.push(hikeDetails()[0]);
            }
            hikeExists(hikeDetails().length === 0);
        },
        error: function (jqxhr, textStatus, errorThrown) {
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}

function DeleteHike(hike) {
    var user = GetName();
    var data = "{\"Value\":\"" + hike.Url() + "\"";
    $.ajax({
        url: "https://hikeservice.azurewebsites.net/hikes/" + user,
        contentType: "application/json",
        type: "DELETE",
        data: data,
        async: false,
        success: function () {
            summary.pop(hike);
        },
        error: function (jqxhr, textStatus, errorThrown) {
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}