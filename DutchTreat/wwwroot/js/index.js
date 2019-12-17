var theForm = $("#theForm");
theForm.hide();

var button = $("#buyButton");
button.on("click", () => console.log("Buying Item"));

var productInfo = $(".product-props li");
productInfo.on("click", function () {
    console.log("You clicked on " + $(this).text());
});