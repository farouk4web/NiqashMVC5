$(document).ready(function () {

    $("#changePasswordForm").on("submit", function (e) {
        e.preventDefault();
        var oldPasswordVal = $("#OldPassword").val();
        var newPasswordVal = $("#NewPassword").val();
        var confirmPasswordVal = $("#ConfirmPassword").val();

        $.ajax({
            url: "/Manage/changePassword",
            type: "POST",
            dataType: "Json",
            data: {
                oldPassword: oldPasswordVal,
                newPassword: newPasswordVal,
                confirmPassword: confirmPasswordVal
            },
            success: function (account) {
                $(".succesMsg").slideDown();

                setTimeout(function () {
                    $(".succesMsg").slideUp();
                }, 2500);

                console.log(account)
            },
            error: function (erorrd) {
                $(".erorrMsg").slideDown();

                setTimeout(function () {
                    $(".erorrMsg").slideUp();
                }, 4000);
                console.log(erorrd)
            }
        });
    });

});