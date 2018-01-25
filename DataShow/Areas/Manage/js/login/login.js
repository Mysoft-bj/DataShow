(function ($) {
    $.login = {

        formMessage: function (msg) {
            $("#errortip").html(msg);
        },

        loginClick: function () {
            var $username = $("#username");
            var $password = $("#password");
            if ($username.val() == "") {
                $username.focus();
                return false;
            }
            else if ($password.val() == "") {
                $password.focus();
                return false;
            }
            else {
                $("#loginButton").attr('disabled', 'disabled').find('span').html("登录中...");
                //$.ajax({
                //    url: "/Login/CheckLogin",
                //    data: { username: $.trim($username.val()), password: $.trim($password.val()), code: $.trim($code.val()) },
                //    type: "post",
                //    dataType: "json",
                //    success: function (data) {
                //        if (data.state == "success") {
                //            $("#loginButton").find('span').html("登录成功，正在跳转...");
                //            window.setTimeout(function () {
                //                window.location.href = "/Home/Index";
                //            }, 500);
                //        }
                //        else {
                //            $("#loginButton").removeAttr('disabled').find('span').html("登录");
                //            $("#switchCode").trigger("click");
                //            $code.val('');
                //            $.login.formMessage(data.message);
                //        }
                //    }
                //});
            }
        },

        init: function () {
            $("#loginButton").click(function () {
                $.login.loginClick();
            });
        }

    };
    $(function () {
        $.login.init();
    });
})(jQuery);