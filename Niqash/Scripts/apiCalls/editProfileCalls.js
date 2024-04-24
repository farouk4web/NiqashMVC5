$(document).ready(function () {

    $("#editProfileForm").on("submit", function (e) {
        e.preventDefault();
        var fName = $("#FirstName").val();
        var lName = $("#LastName").val();
        var about = $("#AboutMe").val();

        $.ajax({
            url: "/Manage/EditProfile",
            type: "POST",
            dataType: "Json",
            data: {
                firstName: fName,
                lastName: lName,
                aboutMe: about
            },
            success: function (account) {
                $(".succesMsg").slideDown();

                setTimeout(function () {
                    $(".succesMsg").slideUp();
                }, 2500);

                console.log(account);
            },
            error: function (erorr) {
                $(".erorrMsg").slideDown();

                setTimeout(function () {
                    $(".erorrMsg").slideUp();
                }, 4000);

                console.log(erorr);
            }
        });
    });

});