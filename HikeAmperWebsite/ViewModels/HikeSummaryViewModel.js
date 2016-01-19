var summary;
var HikeUrl = ko.observable("");
var HikeExists = ko.observable(false);
var ZipCode = ko.observable("");
var InvalidZipcode = ko.observable(false);
var ZipCodeAvailable = ko.observable(false);

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

function AddZipCode() {
    InvalidZipcode(IsInvalidZipCode());
    var user = GetName();
    var data = "{\"Value\":\"" + ZipCode() + "\"}"
    ZipCodeAvailable(false);

    $.ajax({
        url: "https://hikeservice.azurewebsites.net/user/" + user,
        contentType: "application/json",
        type: "POST",
        data: data,
        async: false,
        success: function (data) {
            // TODO: show a message saying Zipcode was added.
            ZipCodeAvailable(true);
            document.getElementById("zipCode").disabled = true;
        },
        error: function (jqxhr, textStatus, errorThrown) {
            ZipCodeAvailable(false);
            // TODO: handle error output.
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}

function IsInvalidZipCode() {
    return !(/(^\d{5}$)|(^\d{5}-\d{4}$)/.test(ZipCode()));
}

function EditZipCode() {
    var user = GetName();
    document.getElementById("zipCode").disabled = false;
    var data = "{\"Value\":\"" + ZipCode() + "\"}"
    ZipCodeAvailable(false);
}

function AddHike() {
    var user = GetName();
    var data = "{\"Value\":\"" + HikeUrl() + "\"}";
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
            // TODO: handle error output.
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