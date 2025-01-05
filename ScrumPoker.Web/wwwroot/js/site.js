"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();

connection.on("ReceiveVote", function (data) {
    console.log(data);
 
    if (data.isJoin) {
        console.log("step 1");
        //var participantRow = `<tr>
        //                        <td class="table-user">
        //                            <img src="/images/users/user-4.jpg" alt="table-user" class="me-2 rounded-circle">
        //                                <a href="javascript:void(0);" class="text-body fw-semibold">${data.userName}</a>
        //                        </td>
        //                     </tr>`;
        //$("#tblParticipants tbody").append(participantRow);
        console.log("okk");
    }
    else {
        console.log("step 2");   //li.textContent = `${data.userName} kullanıcısı > vote : ${data.votePoint} > connectionId : ${data.cid} > roomId : ${data.roomId}`;
    }
});

connection.on("ReceiveUserListInRoom", function (data) {
    console.log("ReceiveUserListInRoom");
    console.log(data);
    $("#tblParticipants tbody").html("");
    $.each(data, function (index, item) {
        console.log("each");
        console.log(index);
        console.log(item);
        var participantRow = `<tr>
                                <td class="table-user">
                                    <img src="/images/users/user-4.jpg" alt="table-user" class="me-2 rounded-circle">
                                        <a href="javascript:void(0);" class="text-body fw-semibold">${item.userName}</a>
                                </td>
                             </tr>`;
        $("#tblParticipants tbody").append(participantRow);
    });
});

connection.start().then(function () {

    console.log("Connected!");
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

function Vote(votePoint) {
    var user = document.getElementById("userInput").value;

    var data = {
        "UserName": user,
        "VotePoint": votePoint,
        "RoomId": window.roomId
    };

    console.log(data.UserName + " : " + data.VotePoint);
    connection.invoke("SendVote", data).catch(function (err) {
        return console.log(err.toString());
    });
}

function JoinToRoom(roomId) {
    console.log("JoinToRoom: " + roomId);
    var username = document.getElementById("userInput").value;
    connection.invoke("Join", roomId, username).catch(function (err) {
        return console.log(err.toString());
    });
}