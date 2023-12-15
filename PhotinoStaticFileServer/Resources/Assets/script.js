
alert("Hellow From script!");


function callDotNet()
{
    window.external.sendMessage("Hi from browser.");
}

window.external.receiveMessage(message => receiveHandler(message));

function receiveHandler(message)
{
    alert(message);
}