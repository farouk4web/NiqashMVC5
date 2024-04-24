$(document).ready(function () {
    var currentUserId = $("#userId").text(),
        navegationBarHeight = $(".navbar").outerHeight(true),
        footerHeight = $("footer").outerHeight(true),
        windowHeight = $(window).innerHeight(),
        contentHeight = windowHeight - footerHeight;


    // return page Direction if user chossen arabic language
    //var siteLanguage = document.cookie.replace(/(?:(?:^|.*;\s*)Language\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    //if (siteLanguage === "ar-EG") {
    //    $("html").attr("dir", "rtl");
    //}


    $(".content").css({
        "minHeight": contentHeight,
        "paddingTop": navegationBarHeight + 10
    });
    $(".add").css("paddingTop", navegationBarHeight + 10);

    //// make input change direct from rtl and ltr
    //$("textarea").on("keyup", function () {

    //    if ($(this).val().charCodeAt(0) > 200) {
    //        $(this).css("direction", "rtl");
    //    } else {
    //        $(this).css("direction", "ltr");
    //    }
    //});

    // .posts form textarea.active
    $("#postForm textarea").on("focus", function () {
        $(this).addClass("active");
    });

    $("#postForm textarea").on("blur", function () {
        $(this).removeClass("active");
    });

    // searsh button and search section
    $("#searchBtn").on("click", function (e) {
        e.preventDefault();
        $(".searchSection").fadeToggle();
    });

    $("#submitBtn").on("click", function () {

        var query = $(this).siblings("input");
        if (query.val().length === 0) {
            $(".alert").show();
        }
        else {
            $(this).parents("form").submit();
        }
    });

    $(".searchSection").on("click", ".closeIcon", function () {
        $(this).parents(".searchSection").hide();
    });

    $("#searchQuery").focus(function () {
        $("#searchAlertMsg").fadeOut();
    });




    // get current user Image and first, last Name
    var currentUserId1 = $("#currentUserId").attr("data-value");
    $.ajax({
        url: "/api/users/" + currentUserId1,
        method: "get",
        success: function (account) {

            $(".navUserimg").attr({
                "src": account.profilePicSrc,
                "alt": account.firstName,
                "title": account.firstName
            });

            $(".navUserName").text(account.firstName);
        }
    });


    // check email confirmation state
    var inManagePages = $(".manageNav").attr("data-manage");
    if (inManagePages === "true") {
        $.ajax({
            url: "/manage/mailConfirmState",
            method: "get",
            success: function (data) {
                if (data.EmailConfirmState === false) {

                    var MsgPlaceholder = $(".emailConfirmationState p"),
                        msg = MsgPlaceholder.attr("data-confirmEmailMsg");

                    MsgPlaceholder.text(msg);
                    MsgPlaceholder.parents(".emailConfirmationState").fadeIn();
                }
            }
        });
    }

});