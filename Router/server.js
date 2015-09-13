//Cole
var express = require('express');
var app = express();
var numberOfPlayers = 0;

app.get('/', function (req, res) {
	numberOfPlayers++;
	console.log("Attempt to connect. " + numberOfPlayers + " players");

	if(numberOfPlayers <= 8)
	{
		console.log("Redirecting to page");
		res.redirect("http://35.3.54.157:8088/" + numberOfPlayers);
	}
	else
	{
		console.log("Match is full");
		res.send("<html><h1>Sorry, there are no available spots :(</h1></html>");
	}
 	
});

var server = app.listen(3000, function () 
{
	var host = server.address().address;
	var port = server.address().port;

	console.log('Example app listening at http://%s:%s', host, port);
});