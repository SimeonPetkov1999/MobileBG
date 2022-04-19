"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, type) {
    var li = document.createElement("li");
    //li.textContent = message;
    var divImg = document.createElement("div")
    li.className = type;
    var img = document.createElement("img")
    img.className = "chat-img"
    img.src = "/user.png";
    divImg.appendChild(img);

    var divContent = document.createElement("div");
    divContent.className = "chat-body";

    var divMessage = document.createElement("div");
    divMessage.className = "chat-message";

    var userName = document.createElement("h5");
    userName.textContent = user;

    var messageP = document.createElement("p");
    messageP.textContent = message;


    divContent.appendChild(divMessage);
    divMessage.appendChild(userName);
    divMessage.appendChild(messageP);

    li.appendChild(divImg);
    li.appendChild(divContent);

    //console.log("test")
    var ul = document.getElementById("messagesList")
    ul.appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    //li.textContent = message;
    //console.log("test")
    //var li = document.createElement("li");
    //document.getElementById("messagesList").appendChild(li);
    //// We can assign user-supplied strings to an element's textContent because it
    //// is not interpreted as markup. If you're assigning in any other way, you 
    //// should be aware of possible script injection concerns.
    //li.textContent = `${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    if (!message || message.trim().length === 0) {
        return;
    }
    connection.invoke("SendMessage", message).catch(function (err) {
        console.log(err);
        return console.error(err.toString());
    });
    event.preventDefault();
});