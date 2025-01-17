﻿"use strict";

class RoomManager {
    constructor(roomId, isOwner) {
        this.roomId = roomId;
        this.isOwner = isOwner;
        this.connection = new signalR.HubConnectionBuilder().withUrl("/roomHub").build();
        this.selectedCard = null; // Seçilen kart bilgisini saklamak için
        this.cardValues = [1, 2, 3, 5, 8, 13, 21]; // Kart puanları
        this.setupConnection();
        this.createCards(); // Kartları oluştur
        this.chartInstance; 
    }

    hideMyChart() {
        $("#divMyChart").css("visibility", "hidden");
        $("#divMyChart").css("height", "0px");
    }

    showMyChart() {
        $("#divMyChart").css("visibility", "visible");
        $("#divMyChart").css("height", "300px");
    }

    setupConnection() {
        this.connection.on("ReceiveVote", (data) => this.handleReceiveVote(data));
        this.connection.on("ReceiveUserListInRoom", (data) => this.handleReceiveUserListInRoom(data));

        this.connection.start()
            .then(() => this.onConnectionStart())
            .catch((err) => console.error(err.toString()));
    }

    onConnectionStart() {
        const cookieData = this.getCookieData();
        if (cookieData) {
            $("#userId").val(cookieData.UserId);
            $("#userName").val(cookieData.UserName);
            $("#headerDisplayName").html(cookieData.UserName);
            this.saveProfile();
        } else {
            $('#profileSettings').modal("show");
        }

        this.joinRoom(this.roomId);
        $(".next-round-custom").html("Aşağıdan bir kart seçiniz");
        this.hideMyChart();

        if (!window.isOwner) {
           $(".next-round-custom").off('click');
        }
    }

    nextRound() {
        this.hideMyChart();
        this.getUserListInRoom(this.roomId, false, true);
    }

    showRoundResults() {
        this.hideMyChart();
        this.getUserListInRoom(this.roomId, true, false);
    }

    handleReceiveVote(data) {
        this.getUserListInRoom(this.roomId, false, false);
    }

    handleReceiveUserListInRoom(data) {
        this.clearUserLists();

        const allVotes = data.userList.map(user => user.votePoint);
        data.userList.forEach((item, index) => {
            this.renderUser(item, index, data.isAdminOpenedCards);
        });

        this.updateVoteSectionsVisibility();
        if (data.isAdminOpenedCards) {
            this.renderChart(allVotes);
        }

        if (data.nextRound) {
            this.hideMyChart();
        }
    }

    getUserListInRoom(roomId, isAdminOpenedCards, resetAllVotes) {
        this.connection.invoke("GetUserListInRoom", roomId, isAdminOpenedCards, resetAllVotes).catch((err) => console.error(err.toString()));
    }

    saveProfile() {
        const userId = $("#userId").val();
        const userName = $("#userName").val();

        if (!userId || !userName) return;

        const data = { UserId: userId, UserName: userName, RoomId: this.roomId };
        this.connection.invoke("SaveProfile", data).catch((err) => console.error(err.toString()));

        this.setCookie("kollabisi", JSON.stringify({ UserId: userId, UserName: userName }), 30);
        $('#profileSettings').modal("hide");
    }

    joinRoom(roomId) {
        const userId = $("#userId").val();
        this.connection.invoke("Join", roomId, userId).catch((err) => console.error(err.toString()));
    }

    // Poker kartına tıklandığında çalışır.
    sendCardToServer(votePoint) {
        const userId = $("#userId").val();
        const data = { UserId: userId, VotePoint: votePoint, RoomId: this.roomId };

        this.connection.invoke("SendVote", data).catch((err) => console.error(err.toString()));
        if (this.isOwner === "True") {
            $(".next-round-custom").html("Oyları Göster");
            $(".next-round-custom").on('click');
        } else {
            $(".next-round-custom").html("Oda sahibi bekleniyor..");
            $(".next-round-custom").off('click');
        }
    }

    // Kartları oluşturan fonksiyon
    createCards() {
        const container = document.getElementById('poker-cards');

        this.cardValues.forEach((value) => {
            const card = document.createElement('button');
            card.className = 'card-custom';
            card.textContent = value;

            card.addEventListener('click', () => {
                this.handleCardClick(value, card);
            });

            container.appendChild(card);
        });
    }

    // Kart tıklama olayı
    handleCardClick(value, cardElement) {
        // Önceki seçimi kaldır
        if (this.selectedCard !== null) {
            const previousSelected = document.querySelector('.card-custom.selected');
            if (previousSelected) {
                previousSelected.classList.remove('selected');
            }
        }

        // Yeni seçimi işaretle
        cardElement.classList.add('selected');
        this.selectedCard = value;

        this.sendCardToServer(value);
    }

    clearUserLists() {
        $("#tblVotedUserList tbody, #tblVoteExpectedUserList tbody, #topUserList, #bottomUserList, #users-in-the-room").html("");
    }

    renderUser(user, index, isAdminOpenedCards) {
        const vote = this.getVoteMarkup(user.votePoint, isAdminOpenedCards);
        const participantRow = this.createParticipantRow(user, vote);
        const pokerTableUserRow = this.createPokerTableRow(user, vote, user.votePoint);
         $("#users-in-the-room").append(this.createAvatarMarkup(user));
        if (user.votePoint > 0) {
            $("#tblVotedUserList tbody").append(participantRow);
        } else {
            $("#tblVoteExpectedUserList tbody").append(participantRow);
        }

        if (index % 2 === 0) {
            $("#topUserList").append(pokerTableUserRow);
        } else {
            $("#bottomUserList").append(pokerTableUserRow);
        }

        $('[data-bs-toggle="tooltip"]').tooltip();
    }

    updateVoteSectionsVisibility() {
        $("#divVotedUserList").toggle($("#tblVotedUserList tbody").html().length > 0);
        $("#divVoteExpectedUserList").toggle($("#tblVoteExpectedUserList tbody").html().length > 0);
    }

    renderChart(allVotes) {
        if (this.chartInstance) {
            this.chartInstance.destroy();
        }
        this.showMyChart();
        const uniqueVotes = [...new Set(allVotes)];
        const chartData = uniqueVotes.map(vote => allVotes.filter(x => x === vote).length);
        const data = {
            labels: uniqueVotes,
            datasets: [{
                label: 'Vote',
                data: chartData,
                backgroundColor: ['rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)'],
                hoverOffset: 4
            }]
        };

        const ctx = document.getElementById('myChart');
        this.chartInstance = new Chart(ctx, { type: 'doughnut', data });
    }

    getVoteMarkup(votePoint, isAdminOpenedCards) {
        if (votePoint > 0) {
            return isAdminOpenedCards ? `${votePoint}` : `<i class="ti-thumb-up"></i>`;
        }
        return `<i class="ti-time"></i>`;
    }

    createParticipantRow(user, vote) {
        return `<tr class="newRow">
                    <td class="table-user">
                        <img src="/images/users/user-4.jpg" alt="table-user" class="me-2 rounded-circle">
                        <a href="javascript:void(0);" class="text-body fw-semibold">${user.userName}</a>
                    </td>
                    <td align="right">${vote}</td>
                </tr>`;
    }

    createPokerTableRow(user, vote, votePoint) {
        var bgColor = "#ffffff";

        if (votePoint > 0) {
            bgColor = "#ecff4c";
        }

        return `<div class="col-md-1" style="margin: 10px;">
                    <div class="text-center card">
                        <div class="card-body" style="padding:0 !important; background-color:${bgColor}">
                            <div class="pt-2 pb-2">
                                <img src="/images/users/user-4.jpg" class="rounded-circle img-thumbnail avatar-xl" alt="profile-image">
                                <h4 class="mt-3"><a href="contacts-profile.html" class="text-dark">${vote}</a></h4>
                                <p class="text-muted">${user.userName}</p>
                            </div>
                        </div>
                    </div>
                </div>`;
    }

    createAvatarMarkup(user) {
        return `<a href="javascript:void(0);" class="avatar-group-item">
                    <img src="/images/users/user-4.jpg" class="rounded-circle avatar-md" alt="friend" 
                         data-bs-container="#users-in-the-room" 
                         data-bs-toggle="tooltip" 
                         data-bs-placement="bottom" 
                         title="${user.userName}">
                </a>`;
    }

    getCookieData() {
        if (document.cookie) {
            return JSON.parse(document.cookie.split(";")[0].split("=")[1]);
        }
        return null;
    }

    setCookie(name, value, days) {
        const d = new Date();
        d.setTime(d.getTime() + (days * 24 * 60 * 60 * 1000));
        document.cookie = `${name}=${value};expires=${d.toUTCString()};path=/`;
    }
}
