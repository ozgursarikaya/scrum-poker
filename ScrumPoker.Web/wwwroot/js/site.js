"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();

connection.on("ReceiveVote", function (data) {
    console.log(data);
    GetUserListInRoom(window.roomId);
});

connection.on("ReceiveUserListInRoom", function (data) {
    console.log("ReceiveUserListInRoom");
    $("#tblParticipants tbody").html("");
    $("#tblVotedUserList tbody").html("");
    $("#tblVoteExpectedUserList tbody").html("");
    $("#topUserList").html("");
    $("#bottomUserList").html("");
    $("#tblParticipants tbody").append("<tr><td>Katılımcı</td><td align=right>Oy Durumu</td></tr>");
    console.log(data);
    $.each(data, function (index, item) {
        console.log(index);
        console.log(item);
        var participantRow = `<tr>
                                <td class="table-user">
                                    <img src="/images/users/user-4.jpg" alt="table-user" class="me-2 rounded-circle">
                                        <a href="javascript:void(0);" class="text-body fw-semibold">${item.userName}</a>
                                </td>
                                <td align="right">${item.votePoint > 0 ? `<i class="ti-thumb-up"></i>` : `<i class="ti-time"></i>`}</td>
                             </tr>`;
        $("#tblParticipants tbody").append(participantRow);

        if (item.votePoint > 0) {
            $("#tblVotedUserList tbody").append(participantRow);
        }
        else {
            $("#tblVoteExpectedUserList tbody").append(participantRow);
        }

        var vote = "";
        if ($("#userId").val() == item.userId) {
            vote = "" + item.votePoint;
        }
        else {
            if (item.votePoint > 0) {
                vote = `<i class="ti-thumb-up"></i>`;
            }
        }

       var pokerTableUserRow =  `<div class="text-center">
                                                        <div class="user-circle-custom">Avatar</div>
                                                        <div class="card-custom">${vote}</div>
                                                        <div class="user-name-custom">${item.userName}</div>
                                                    </div>`;
        if (index % 2 == 0) {
            $("#topUserList").append(pokerTableUserRow);
        }
        else {
            $("#bottomUserList").append(pokerTableUserRow);
        }
    });
});

connection.start().then(function () {

    console.log("Connected!");

    if (document.cookie != '') {
        var cookieData = JSON.parse(document.cookie.split(";")[0].split("=")[1]);
        console.log("cookieData");
        console.log(cookieData);
        $("#userId").val(cookieData.UserId);
        $("#userName").val(cookieData.UserName);
        $("#headerDisplayName").html(cookieData.UserName);

        SaveProfile();
    }
    else {
        $('#profileSettings').modal("show");
    }

    JoinToRoom(window.roomId);
    GetUserListInRoom(window.roomId);

}).catch(function (err) {
    return console.log(err.toString());
});

function GetUserListInRoom(roomId) {
    console.log("GetUserListInRoom!");
    connection.invoke("GetUserListInRoom", roomId).catch(function (err) {
        return console.log(err.toString());
    });
}

function SaveProfile() {
    if ($("#userId").val() == "" || $("#userName").val() == "")
        return;

    console.log("SaveProfile");

    var data = {
        "UserId": $("#userId").val(),
        "UserName": $("#userName").val(),
        "RoomId": window.roomId
    };
    console.log(data);
    connection.invoke("SaveProfile", data).catch(function (err) {
        return console.log(err.toString());
    });

    var cookieData = {
        "UserId": $("#userId").val(),
        "UserName": $("#userName").val()
    };

    $("#headerDisplayName").html(cookieData.UserName);

    SetCookie("kollabisi", JSON.stringify(cookieData), 30);

    $('#profileSettings').modal("hide");
}

function SetCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function Vote(votePoint) {

    var userId = $("#userId").val();

    var data = {
        "UserId": userId,
        "VotePoint": votePoint,
        "RoomId": window.roomId
    };

    console.log(data.UserId + " : " + data.VotePoint);
    connection.invoke("SendVote", data).catch(function (err) {
        return console.log(err.toString());
    });
}

function JoinToRoom(roomId) {
    console.log("JoinToRoom: " + roomId);
    
    var userId = $("#userId").val();
    connection.invoke("Join", roomId, userId).catch(function (err) {
        return console.log(err.toString());
    });
}

$('#poker-cards ul li').click(function () {
    $('#poker-cards ul li').removeClass('selected-poker-card');

    $(this).attr("class", "selected-poker-card");
});