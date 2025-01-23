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
        $(".live-info").html("Choose a card.");
        this.hideMyChart();

        if (!window.isOwner) {
           $(".live-info").off('click');
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
            $(".live-info").html("Reveal Votes");
            $(".live-info").on('click');
        } else {
            $(".live-info").html("Waiting for the room owner..");
            $(".live-info").off('click');
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
        $("#divUserList").html("");
    }

    renderUser(user, index, isAdminOpenedCards) {
        const vote = this.getVoteMarkup(user.votePoint, isAdminOpenedCards);
        const participantRow = this.createParticipantRow(user, vote);
        $("#divUserList").append(participantRow);
        $('[data-bs-toggle="tooltip"]').tooltip();
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
                hoverOffset: 1
            }]
        };

        const config = {
            type: 'bar',
            data: data,
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Results'
                    }
                },
                scales: {
                    x: {
                        grid: {
                            display: false
                        },
                        barThickness: 5
                    },
                    y: {
                        grid: {
                            display: false
                        },
                        ticks: {
                            callback: function (value) {
                                // Sadece tam sayılar görünsün
                                if (Number.isInteger(value)) {
                                    return value;
                                }
                                return null;
                            }
                        }
                    }
                }
            },
        };

        const ctx = document.getElementById('myChart');
        this.chartInstance = new Chart(ctx, config);
    }

    getVoteMarkup(votePoint, isAdminOpenedCards) {
        var votePointSpan = `<span class="btn-white badge badge-soft-success font-13" style="padding: 12px; width: 40px;">${votePoint}</span>`;
        var votedSpan = `<span class="btn-white badge badge-soft-info font-13" style="padding:12px;width: 40px;"> <i class="fe-check"></i></span>`;
        var voteWaitingSpan = `<span class="btn-white badge badge-soft-dark font-13" style="padding:12px;width: 40px;"> <i class="ti-time"></i></span>`;
        if (votePoint > 0) {
            return isAdminOpenedCards ? votePointSpan : votedSpan;
        }
        return voteWaitingSpan;
    }

    createParticipantRow(user, vote) {
        return `<div class="inbox-item d-flex align-items-center justify-content-between">
                    <div class="d-flex align-items-center">
                        <div class="inbox-item-img">
                            <img src="/images/users/user-2.jpg" class="rounded-circle" alt="" style="width: 40px; height: 40px;">
                        </div>
                        <p class="inbox-item-author mb-0">
                            ${user.userName}
                            <br />
                            <span class="badge badge-soft-secondary">Backend</span>
                        </p>

                    </div>
                    <div class="d-flex align-items-center">
                        <div class="btn-group">
                            <button type="button" class="btn btn-white dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">...</button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Remove</a>
                                <a class="dropdown-item" href="#">Make Observer</a>
                                <a class="dropdown-item" href="#">Make Admin</a>
                            </div>
                        </div>
                        <div class="ms-1">
                            ${vote}
                        </div>
                    </div>
                </div>`;
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
