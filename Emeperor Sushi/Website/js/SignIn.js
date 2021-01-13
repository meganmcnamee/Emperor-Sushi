$(document).ready( function() { // Wait until document is fully parsed
    $("#SignIn-Form").on('submit', function(e){
  
        e.preventDefault();
        var Username = $("#Username").val();
        var Password = $("#Password").val();
        
        $.ajax({
            url: "http://localhost:65067/api/User/Login",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: '{"Username": "'+Username+'", "Password": "'+Password+'"}',
            crossDomain: true,
            beforeSend: function(xhr){
                xhr.withCredentials = true;
          },
            success: function (result) {
                console.log(result);
                if(result.Status == "Success")
                {
                    localStorage.setItem("SessionKey", result.SessionKey);
                    window.location = "index.html";
                }
                else
                {
                    $("#error").show();
                }
            },
            error: function (error) {
                console.log(error);
                $("#error").show();
            }
        });
    });
});