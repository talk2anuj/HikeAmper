var summary;
var HikeUrl = ko.observable("");
var HikeExists = ko.observable(false);

function GetName() {
    var user = document.getElementById("user").defaultValue;
    user = user.replace(eval("/[^a-zA-Z0-9]/g"), "");
    return user;
}

$(document).ready(function () {
    var user = GetName();
    HikeExists(false);
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
    var data = "{\"Value\":\"" + HikeUrl() + "\"";
    HikeUrl("");
    HikeExists(false);
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
            } else {
                HikeExists(true);
            }
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
    HikeExists(false);
    $.ajax({
        url: "https://hikeservice.azurewebsites.net/hikes/" + user,
        contentType: "application/json",
        type: "DELETE",
        data: data,
        async: false,
        success: function () {
            summary.remove(hike);
        },
        error: function (jqxhr, textStatus, errorThrown) {
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}