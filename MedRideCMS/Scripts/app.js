 $(document).ready(function () {
        $(".task-selector").on("click", "button", function (event) {
            event.preventDefault();
            var elem = $(this);
            var elemSiblings = elem.siblings("button");

            if (elem.attr("id") === "customer-lookup") {
                elem.removeClass("btn-link").addClass("btn-primary");
            } else if(elem.attr("id") === "add-customer") {
                elem.removeClass("btn-link").addClass("btn-primary");
            } else if (elem.attr("id") === "all-customers") {
                elem.removeClass("btn-link").addClass("btn-primary");
            } else {
                return;
            }

            for (var i =  0; i < elemSiblings.length; i++) {
                $(elemSiblings[i]).removeClass("btn-primary").addClass("btn-link");
            }
        });
    });

/*
@section scripts
{
    @Scripts.Render("~/bundles/app")
}
*/