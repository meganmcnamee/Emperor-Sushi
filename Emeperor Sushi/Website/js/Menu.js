jQuery(document).ready(function($){

	function CreateSlideItem(item, isFirst)
	{
		var menuitem = $("<div>").addClass("col-md-3")
						.addClass("col-xl-2")
						.css("float", "left");
		if(isFirst)
			menuitem.addClass("offset-xl-2");
		
		var div1 = $("<div>").addClass("card").addClass("mb-2").addClass("border-danger");
		var img = $("<img>").addClass("card-img-top").addClass("rounded").attr("src", item.ProductImage).attr("alt", "img");
		var div2 = $("<div>").addClass("card-body").addClass("text-center").addClass("bg-dark");

		var h4 = $("<h4>").addClass("card-title").addClass("text-center").addClass("text-danger").html(item.ItemName);
		var h6 = $("<h6>").addClass("card-text").addClass("text-center").addClass("text-white").html(item.ItemDescription);
		var h5 = $("<h5>").addClass("card-text").addClass("text-center").addClass("text-danger").html("$"+item.ItemPrice.toFixed(2));
		var a = $("<a>").addClass("btn").addClass("btn-danger").html("Add to Cart").attr("id", item.ID);

		a.click(function()
		{
			var itemID = this.id;
			var sessionKey = localStorage.getItem("SessionKey");
			var nav = null;
			if(sessionKey == null)
			{
				$("#ItemText").html("You must be logged in to complete this action");
			}
			else
			{
				$.ajax({
					url: "http://localhost:65067/api/Cart/"+itemID,
					type: 'PUT',
					headers: {
						'SessionKey': sessionKey
					},
					crossDomain: true,
            		beforeSend: function(xhr){
                		xhr.withCredentials = true;
         			},
					success: function (result) {
					   // CallBack(result);
					   console.log(result);
					},
					error: function (error) {
						console.log(error);
					}
				});
				$("#ItemText").html(item.ItemName + " has been added to your cart");
			}
			$('#myModal').modal();
		
		});


		div2.append(h4);
		div2.append(h6);
		div2.append(h5);
		div2.append(a);
		div1.append(img);
		div1.append(div2);
		menuitem.html(div1);
		return menuitem;
	}

	function CreateSlides(MenuItems)
	{
		var itemCount = MenuItems.length;
		var SlidesCount = (MenuItems.length / 4) + (MenuItems.length % 4);
		var curItem = 0;

		for(var i = 0;i<SlidesCount;i++)
		{
			var carouselItem = null;
			if(i == 0)
			{
				carouselItem = $("<div>").addClass("carousel-item").addClass("active");
			}
			else
			{
				carouselItem = $("<div>").addClass("carousel-item");
			}

			for(var j = 0;j<4;j++)
			{
				if(curItem >= itemCount)
					break;
				var item = CreateSlideItem(MenuItems[curItem], ((j == 0) ? true : false));
				carouselItem.append(item);
				curItem++;
			}

			$("#slides").append(carouselItem);
			if(curItem >= itemCount)
				break;
		}
	}
	$(".dot").click(function()
	{
		$("#slides").empty();
		$("#Ga, #Gs, #Gso, #Gsa, #Gb").removeClass("dota");
		var button = this.id;
		$("#"+button).addClass("dota");
		$.get("http://localhost:65067/api/Menu", function(data, status)
		{
			var MenuItems = [];
			data.forEach(function(item) {
				if(button == "Gs" && item.Category == "Special Rolls")
				{
					MenuItems.push(item);
				}
				else if(button == "Ga" && item.Category == "Appetizers")
				{
					MenuItems.push(item);
				}
				else if(button == "Gso" && item.Category == "Soups")
				{
					MenuItems.push(item);
				}
				else if(button == "Gsa" && item.Category == "Salads")
				{
					MenuItems.push(item);
				}
				else if(button == "Gb" && item.Category == "Beverages")
				{
					MenuItems.push(item);
				}
			});
			CreateSlides(MenuItems);
		});
	});

	$("#Ga").trigger("click");
});