jQuery(document).ready(function($){
    //sign

    var sessionKey = localStorage.getItem("SessionKey");
	if(sessionKey == null)
	{
        $("#sign").html("Sign In");
    }
    else
    {
        $("#sign").html("Sign Out");
    }

    $("#sign").click(function()
    {
        var sessionKey = localStorage.getItem("SessionKey");
        if(sessionKey == null)
        {
            window.location = "SignIn.html";
        }
        else
        {
            $.ajax({
                url: "http://localhost:65067/api/User/Logout",
                type: 'get',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    'SessionKey': sessionKey
                },
                crossDomain: true,
                beforeSend: function(xhr){
                    xhr.withCredentials = true;
              },
                success: function (result) {
                    console.log(result);
                        $("#sign").html("Sign In");
                        localStorage.removeItem("SessionKey");
                        window.location = window.location;
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });

});