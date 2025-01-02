"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();

connection.on("ReceiveVote", function (data) {
    console.log(data);
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${data.userName} vote : ${data.votePoint}`;
});

connection.start().then(function () {
    console.log("Connected to room!")

}).catch(function (err) {
    return console.log(err.toString());
});

function Vote(votePoint) {
    var user = document.getElementById("userInput").value;

    var data = {
        "UserName": user,
        "VotePoint": votePoint
    };

    console.log(data.UserName + " : " + data.VotePoint);
    connection.invoke("SendVote", data).catch(function (err) {
        return console.log(err.toString());
    });
}