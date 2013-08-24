//RandGur JavaScript File
//Developed By Mike Rosenberg
//© 2013 Michael Rosenberg
//http://mikerosenberger.com/blog
var numattempts = 0;

function generateimageURL(len, charSet) {
    charSet = charSet || 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var randomimg = 'http://i.imgur.com/';
    for (var i = 0; i < len; i++) {
    	var randomPoz = Math.floor(Math.random() * charSet.length);
    	randomimg += charSet.substring(randomPoz,randomPoz+1);
    }
	randomimg += '.jpg';
    return randomimg;
}


function loadimage() {
	var img2load = generateimageURL(5);
	var img = document.getElementById('imageid');
	var button = document.getElementById('button');
	var label = document.getElementById('attempts');
	button.disabled = true;
	img.style= 'visibility: hidden;';
	img.src = img2load;
	img.onload = function() 
	{
		if (img.width != '161' && img.height != '81')
		{
			img.style= 'visibility: visible;';
			button.disabled = false;
			label.innerHTML = numattempts + " Attempts For Random Image";
		}
		else
		{
			loadimage();
			numattempts = numattempts + 1;
			label.innerHTML = numattempts + " Attempts For Random Image";
		}
	};
	
}

function resetattemptometer() {
	numattempts = 0;

}

