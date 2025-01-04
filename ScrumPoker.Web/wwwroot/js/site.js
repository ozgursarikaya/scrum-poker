"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();

connection.on("ReceiveVote", function (data) {
    console.log(data);
    var li = document.createElement("li");
    
    document.getElementById("messagesList").appendChild(li);
    if (data.isJoin) {
        li.textContent = `${data.userName} kullanıcısı giriş yaptı.`;
    }
    else {
        li.textContent = `${data.userName} kullanıcısı > vote : ${data.votePoint} > connectionId : ${data.cid} > roomId : ${data.roomId}`;
    }
});

connection.start().then(function () {

    console.log("Connected!");
    JoinToRoom(window.roomId);

}).catch(function (err) {
    return console.log(err.toString());
});


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