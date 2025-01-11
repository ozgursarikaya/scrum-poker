"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();

connection.on("ReceiveVote", function (data) {
    console.log(data);
    GetUserListInRoom(window.roomId, false);
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
    var allVotes = [];
    $.each(data.userList, function (index, item) {
        allVotes.push(item.votePoint);

        console.log(index);
        console.log(item);
        var vote = "";
        if (item.votePoint > 0) {
            if (data.isAdminOpenedCards) {
                vote = "" + item.votePoint;
            }
            else {
                vote = `<i class="ti-thumb-up"></i>`;
            }
        }
        else {
            vote = `<i class="ti-time"></i>`;
        }

        var participantRow = `<tr  style="display: none;" class="newRow">
                                <td class="table-user">
                                    <img src="/images/users/user-4.jpg" alt="table-user" class="me-2 rounded-circle">
                                        <a href="javascript:void(0);" class="text-body fw-semibold">${item.userName}</a>
                                </td>
                                <td align="right">${vote}</td>
                             </tr>`;
        $("#tblParticipants tbody").append(participantRow);
        $("#tblParticipants tbody .newRow").show("slow");
        if (item.votePoint > 0) {
            $("#tblVotedUserList tbody").append(participantRow);
        }
        else {
            $("#tblVoteExpectedUserList tbody").append(participantRow);
        }
        $("#tblVotedUserList tbody .newRow").show("slow");
        $("#tblVoteExpectedUserList tbody .newRow").show("slow");

        if ($("#userId").val() == item.userId) {
            vote = "" + item.votePoint;
        }
        else {
            if (item.votePoint > 0) {
                vote = `<i class="ti-thumb-up"></i>`;
            }
            if (data.isAdminOpenedCards) {
                vote = "" + item.votePoint;
            }
        }

        var pokerTableUserRow = `<div class="text-center">
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

    if ($("#tblVotedUserList tbody").html().length == 0) {
        $("#divVotedUserList").hide();
    }
    else {
        $("#divVotedUserList").show();
    }

    if ($("#tblVoteExpectedUserList tbody").html().length == 0) {
        $("#divVoteExpectedUserList").hide();
    }
    else {
        $("#divVoteExpectedUserList").show();
    }

    var uniqueVoteData = allVotes.filter((value, index, array) => array.indexOf(value) === index);
    var chartData = [];
    $.each(uniqueVoteData, function (index, item) {
        chartData.push(allVotes.filter(x => x === item).length);
    });

    if (data.isAdminOpenedCards) {
        const data = {
            labels: uniqueVoteData,
            datasets: [{
                label: 'Vote',
                data: chartData,
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    'rgb(255, 205, 86)'
                ],
                hoverOffset: 4
            }]
        };

        const config = {
            type: 'doughnut',
            data: data,
        };

        const ctx = document.getElementById('myChart');

        new Chart(ctx, config);
    }
    

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
    GetUserListInRoom(window.roomId, false);

    $(".next-round-custom").html("Aşağıdan bir kart seçiniz");

}).catch(function (err) {
    return console.log(err.toString());
});

function GetUserListInRoom(roomId, isAdminOpenedCards) {
    console.log("GetUserListInRoom!");
    connection.invoke("GetUserListInRoom", roomId, isAdminOpenedCards).catch(function (err) {
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

    console.log(window.isOwner);
    if (window.isOwner == "True") {
        $(".next-round-custom").html("Oyları Göster");
    }
    else {
        $(".next-round-custom").html("Oda sahibi bekleniyor..");
    }
    
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

$('#btnOpenCards').click(function () {
    GetUserListInRoom(window.roomId, true);
    $(".next-round-custom").html("Sonraki Tur");
});