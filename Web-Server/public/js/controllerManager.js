/*
Copyright (C) 2015 Electronic Arts Inc.  All rights reserved.
 
This software is solely licensed pursuant to the Hackathon License Agreement,
Available at:  www.eapathfinders.com/license
All other use is strictly prohibited. 
*/

var gyroTime = 0;
var tilt = function(events, connection)
{
	if(gyroTime % 5 == 0)
	{
		gyroTime = 0;
		connection.sendMessage(events);
	}
}

$(document).ready(function () 
{
	console.log("Document Loaded");

	// INIT..
	conn = new Connection();
	conn.sendMessage({"type": "connect"});
	conn.sendMessage({"type": "boost"});

	//send gyro data
	if (window.DeviceOrientationEvent) 
	{
    	window.addEventListener("deviceorientation", function () {
    		gyroTime++;
        	tilt({"type":"gyro", "alpha": event.alpha, "beta": event.beta, "gamma": event.gamma}, conn);
    	}, true);
	}

	$("#circle").on("tap", function(){
		console.log("Boost");
		conn.sendMessage({"type": "boost"});
	});

	var m_circle = new BoostButton($("#circle"));
	
	// Process incoming game messages
	$(document).on("game_message", function (e, message) 
	{
		console.log("Received Message: " + JSON.stringify(message));
		var payload = message.payload;
		switch (payload.type) 
		{
			//your code here
		}
	});
});

