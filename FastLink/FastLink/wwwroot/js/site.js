
function login() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var userName = $('#userName').val();
    var password = $('#password').val();
    if (userName != "" && password != "") {
        $.ajax({
            type: 'Post',
            url: '/Home/Login',
            dataType: 'json',
            data:
            {
                userName: userName,
                password: password
            },
            success: function (result) {
                if (!result.isError) {
                    var n = 1;
                    localStorage.removeItem("on_load_counter");
                    localStorage.setItem("on_load_counter", n);
                    location.replace(result.dashboard);
                    sessionStorage.removeItem('user');
                    return;
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
                    $('#submit_btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert("An error occured, please try again.");
            }
        });
    } else {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }
   
}

function GenerateTrackId() {
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {};
    data.ClientName = $('#name').val();
    data.Phonenumber = $('#phone').val();
    data.Email = $('#email').val();
    data.Address = $('#address').val();
    data.CurrentLocation = $('#location').val();
    data.CurrentCity = $('#city').val();
    data.ItemName = $('#itemName').val();
    data.ItemWeight = $('#itemWeight').val();

    if (data.ClientName != "" && data.Phonenumber != "" && data.Email != "" && data.Address != "" && data.CurrentLocation != ""
        && data.CurrentCity != "" && data.ItemName != "" && data.ItemWeight != "") {
        let trackDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/GenerateId',
            dataType: 'json',
            data:
            {
                trackDetails: trackDetails,
            },
            success: function (data) {
                if (!data.isError) {
                    var text = data;
                    navigator.clipboard.writeText(text)
                    var url = '/Home/GenerateTrackId';
                    successAlertWithRedirect("Successful, your Tracking ID is " + text + ". Click Ok to Copy", url);
                    //successAlert("Successful, your Tracking ID is " + text + ".");
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
                    $('#submit_btn').attr("disabled", false);
                    errorAlert(data.msg);
                }
            }
        });
    } else {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }
}

function GetCurrentLocation() {
    var TrackingID = $("#track_Id").val();
    if (TrackingID != "") {
        $.ajax({
            type: 'GET',
            url: '/Admin/GetCurrentLocation',
            dataType: 'json',
            data:
            {
                trackingID: TrackingID,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Home/TrackOrder'
                    successAlertWithRedirect(result.msg, url)

                    //var location = result;
                    //var locationHtml = `<p class="lead" style="font-weight:700;"> ${location} </p>`;
                    //$('#location_para').append(locationHtml);
                    //$("#showLocation").modal('show');
                } else {
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                errorAlert(result.msg);
            }
        });
    } else {
        errorAlert("Please input your Tracking ID");
    }
}

function GetLocationToUpdate(id,trackID) {
    $.ajax({
        type: 'get',
        url: '/Admin/GetLocation',
        dataType: 'json',
        data:
        {
            id: id,
            trackingID: trackID,
        },
        success: function (result) {
            if (!result.isError) {
                $("#tr_Id").val(result.id);
                $("#track_ID").val(result.trackingID);
                $("#getLocationmodal").modal('show');
            }
        },
        error: function (ex) {
            errorAlert(result.msg);
        }
    });
}

function ChangeLocation() {
    var NewLocation = $("#newLocation").val();
    var id = $("#tr_Id").val();
    var Track_ID = $("#track_ID").val();
    if (NewLocation != "") {
        $.ajax({
            type: 'POST',
            url: '/Admin/ChangeCurrentLocation',
            dataType: 'json',
            data: {
                id: id,
                trackID: Track_ID,
                newLocation: NewLocation,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Admin/ChangeLocation';
                    successAlertWithRedirect(result.msg, url);
                }
                else {
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                "Something went wrong, contact the support - " + errorAlert(ex);
            }
        });
    }
    else {
        errorAlert("Please add the new location");
    }
}

function DeleteTrack(id) {
    debugger;
    $("#tr_Id").val(id);
    $("#delete_Track").modal('show');
}

function DeleteGeneratedId() {
    debugger
    var id = $('#tr_Id').val();
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DeleteTrack',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/ChangeLocation'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please check and try again. Please contact admin if issue persists..");
        }
    })
}

function ContactMsg() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {};
    data.Name = $('#name').val();
    data.Subject = $('#subject').val();
    data.ClientEmail = $('#email').val();
    data.Message = $('#message').val();

    if (data.Subject != "" && data.ClientEmail != "" && data.Message != "") {
        let contactDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/SaveContactMsg',
            dataType: 'json',
            data:
            {
                contactDetails: contactDetails,
            },
            success: function (result) {
                if (!result.isError) {
                    var url = '/Home/Contact'
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
                    $('#submit_btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert(result.msg);
            }
        });
    } else {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }
}

function GetMsg(id) {
    $.ajax({
        type: 'get',
        url: '/Admin/GetContactMsg',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                //$("#t_Id").val(result.id);
                $("#msg").val(result);
                $("#getMsgModal").modal('show');
            }
        },
        error: function (ex) {
            errorAlert(result.msg);
        }
    });


}

function DeleteMsg(id) {
    debugger;
    $("#msg_Id").val(id);
    $("#delete_Msg").modal('show');
}

function DeleteMessage() {
    debugger
    var id = $('#msg_Id').val();
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DeleteMsg',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/ClientMessages'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please check and try again. Please contact admin if issue persists..");
        }
    })
}

//add the msg response later
