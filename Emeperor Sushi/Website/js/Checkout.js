jQuery(document).ready(function($)
{
    var cart = null;
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
                if(result.Status == "Success")
                {
                    cart = result.Cart;
                    console.log(cart);
                    if(cart.length == 0)
                        window.location = "Cart.html";

                    var items = 0;
                    var total = 0;
                    cart.forEach(function(item)
                    {
                        items += item.Quantity;
                        total += (item.MenuItem.ItemPrice*item.Quantity);
                    });
                    $("#Item-Count").html("Items: "+items);
                    $("#Order-Cost").html("Total before tax: $"+total.toFixed(2));
                    //4.5%
                    var totalWithTax = total + (total * 0.045);
                    $("#Order-Total").html("Order Subtotal: $"+totalWithTax.toFixed(2));
                }
                else
                {
                    window.location = "index.html";
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    var sessionKey = localStorage.getItem("SessionKey");

    if(sessionKey == null)
    {
        window.location = "SignIn.html";
        console.log("not logged in");
    }

    GetCart();

    $("#Checkout-Form").on('submit', function(e)
    {
        e.preventDefault();
        var FirstName = $("#FirstName").val();
        var LastName = $("#LastName").val();
        var Email = $("#Email").val();
        var Address = $("#Address").val();
        var State = $("#State").val();
        var City = $("#City").val();
        var Zip = $("#Zip").val();

        var CardName = $("#Name").val();
        var CardNumber = $("#Number").val();
        var ExpirationDate = $("#Exp").val();
        var CVV = $("#CVV").val();

        var cartID = cart[0].Cart;

        var JsonData = {
            FirstName: FirstName,
            LastName: LastName,
            Email: Email,
            Address: Address,
            State: State,
            City: City,
            Zip: Zip,
            CardName: CardName,
            CardNumber: CardNumber,
            ExpirationDate: ExpirationDate,
            CVV: CVV
        };


        $.ajax({
            url: "http://localhost:65067/api/Orders/"+cartID,
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                'SessionKey': sessionKey
            },
            data: JSON.stringify(JsonData),
            crossDomain: true,
            beforeSend: function(xhr){
                xhr.withCredentials = true;
          },
            success: function (result) {
                console.log(result);
                if(result.Status == "Success")
                {
                    console.log("order has been placed");
                }
                else
                {
                    console.log("Failed");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
    
});