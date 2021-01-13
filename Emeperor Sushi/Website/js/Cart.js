$(document).ready( function() { // Wait until document is fully parsed


    function GetCart()
    {
        $.ajax({
            url: "http://localhost:65067/api/Cart",
            type: 'get',
            dataType: 'json',
            headers: {
                'SessionKey': sessionKey
            },
            crossDomain: true,
            beforeSend: function(xhr){
                xhr.withCredentials = true;
            },
            success: function (result) {
                $("#Table-Body").empty();
                if(result.Status == "Success")
                {
                    var total = 0.00;
                    result.Cart.forEach(function(item)
                    {
                        total += (item.MenuItem.ItemPrice*item.Quantity);
                        var obj = CreateCartItem(item);
                        $("#Table-Body").append(obj);
                    });
                    $("#total-value").html("Total: $"+total.toFixed(2));
                }
                else
                {
                    if(result.Message == "Not authenticated")
                    {
                        window.location = "SignIn.html";
                    }
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function CreateCartItem(Item)
    {
        var tr = $("<tr>");

        var th = $("<th>").attr("scope", "row").html(Item.Quantity);
        var td1 = $("<td>").html(Item.MenuItem.ItemName);
        var td2 = $("<td>").html("$"+Item.MenuItem.ItemPrice.toFixed(2));
        var td3 = $("<td>").html("$"+(Item.MenuItem.ItemPrice*Item.Quantity).toFixed(2));
        var i = $("<i>").addClass("fas").addClass("fa-trash");
        var btn = $("<div>").addClass("btn").addClass("btn-danger").html(i);

        btn.click(function()
        {
            
            $.ajax({
                url: "http://localhost:65067/api/Cart/"+Item.MenuItem.ID,
                type: 'delete',
                dataType: 'json',
                headers: {
                    'SessionKey': sessionKey
                },
                crossDomain: true,
                beforeSend: function(xhr){
                    xhr.withCredentials = true;
                },
                success: function (result) {
                    console.log(result);
                    if(result.Status == "Success")
                    {
                        GetCart();
                    }
                    else
                    {
                        if(result.Message == "Not authenticated")
                        {
                            window.location = "SignIn.html";
                        }
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });

        var td4 = $("<td>").html(btn);

        tr.append(th);
        tr.append(td1);
        tr.append(td2);
        tr.append(td3);
        tr.append(td4);
        return tr;
    }

    var sessionKey = localStorage.getItem("SessionKey");

    if(sessionKey == null)
    {
        window.location = "SignIn.html";
        console.log("not logged in");
    }
    else
    {
        GetCart();
    }
});