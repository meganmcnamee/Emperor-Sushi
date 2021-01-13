$(document).ready( function() { // Wait until document is fully parsed
    $("#SignUp-Form").on('submit', function(e){
  
        e.preventDefault();
        var Username = $("#Username").val();
        var Password = $("#Password").val();
        var CPassword = $("#ConfirmPassword").val();
        
        $.ajax({
            url: "http://localhost:65067/api/User/Register",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: '{"Username": "'+Username+'", "Password": "'+Password+'", "ConfirmPassword": "'+CPassword+'"}',
            success: function (result) {
                console.log(result);
                if(result.Status == "Success")
                {
                    window.location = "SignIn.html";
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