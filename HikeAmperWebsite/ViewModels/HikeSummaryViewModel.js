var summary;
var HikeUrl = ko.observable("");
var HikeExists = ko.observable(false);
var ZipCode = ko.observable("");
var InvalidZipcode = ko.observable(false);
var ZipCodeAvailable = ko.observable(false);
var DisplayZipCode = ko.observable(false);
var DisplayHikes = ko.observable(false);
var PreviousZipCode;

function GetName() {
    var user = document.getElementById("user").defaultValue;
    user = user.replace(eval("/[^a-zA-Z0-9]/g"), "");
    return user;
}

function IsInvalidZipCode() {
    return !(/(^\d{5}$)|(^\d{5}-\d{4}$)/.test(ZipCode()));
}

$(document).ready(function () {
    var userName = GetName();
    HikeExists(false);
    document.getElementById("zipCode").disabled = true;
    ZipCodeAvailable(false);

    $.ajax({
        url: "https://hikeservice.azurewebsites.net/user/" + userName,
        contentType: "application/json",
        type: "GET",
        async: false,
        success: function (data) {
            ZipCode(data);
            ZipCodeAvailable(true);
            DisplayZipCode(true);
        },
        error: function () {
            alert("error occurred");
        }
    });
    $.ajax({
        url: "https://hikeservice.azurewebsites.net/hikes/" + userName,
        contentType: "application/json",
        type: "GET",
        async: false,
        success: function (data) {
            summary = ko.mapping.fromJS(data);
            ko.applyBindings(summary);
            DisplayHikes(true);
        },
        error: function () {
            alert("error occurred");
        }
    });
});

function AddZipCode() {
    InvalidZipcode(IsInvalidZipCode());
    var userName = GetName();
    var data = "{\"Value\":\"" + ZipCode() + "\"}";

    $.ajax({
        url: "https://hikeservice.azurewebsites.net/user/" + userName,
        contentType: "application/json",
        type: "POST",
        data: data,
        async: false,
        success: function () {
            // TODO: show a message saying Zipcode was added.
            ZipCodeAvailable(true);
            document.getElementById("zipCode").disabled = true;
            UpdateHikeDetails();
        },
        error: function (jqxhr, textStatus, errorThrown) {
            // TODO: handle error output.
            console.log(textStatus);
            console.log(errorThrown);
        }
    });
}

function UpdateHikeDetails() {
    //    var userName = GetName();
    //    $.ajax({
    //        url: "https://hikeservice.azurewebsites.net/hikes/" + userName,
    //        contentType: "application/json",
    //        type: "GET",
    //        async: false,
    //        success: function (data) {
    //            data = ko.mapping.fromJS(data);
    //            summary(data);
    //        },
    //        error: function () {
    //            alert("error occurred");
    //        }
    //    });
    //TODO: HACK untill view model is fixed to update page
    location.reload();
}

function EditZipCode() {
    PreviousZipCode = ZipCode();
    if (IsInvalidZipCode(ZipCode())) {
        ZipCode("");
    }
    document.getElementById("zipCode").disabled = false;
    ZipCodeAvailable(false);
}

function CancelEdit() {
    document.getElementById("zipCode").disabled = true;
    ZipCode(PreviousZipCode);
    ZipCodeAvailable(true);
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