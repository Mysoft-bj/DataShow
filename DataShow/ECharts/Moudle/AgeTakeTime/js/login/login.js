var icon = "<i class='fa fa-times-circle'></i>"
var validateObject = {
    loginForm: {
        rules:{
            username:{
                required: true,
                maxlength: 100
            },
            password:{
                required: true,
                maxlength: 100
            }
        },
        messages:{
            username:{
               required: icon + "请输入用户名！",
               maxlength: icon + "长度必须100个字符以内"
           },
            password:{
                required: icon + "请输入密码！",
                maxlength: icon + "长度必须100个字符以内"
            }
        },
        errorPlacement: function (error, element) {
            error.appendTo(element.parent().parent());
        }
    },
    registerForm: {
        rules:{
            username:{
                required: true,
                maxlength: 100,
                remote: "/login/CheckUserName"
            },
            mobile:{
                required: true,
                maxlength: 20,
                number: true,
                remote: "/login/CheckMobile"
            },
            email:{
                required: true,
                maxlength: 100,
                email: true
            },
            password:{
                required: true,
                maxlength: 100
            },
            repassword: {
                required: true,
                maxlength: 100,
                equalTo: "#register_password"
            }
        },
        messages:
        {
            username:{
               required: icon + "请输入用户名！",
               maxlength: icon + "长度必须100个字符以内！",
               remote: icon + "用户名不能重复！"
           },
            mobile: {
                required: icon +"请输入手机号！",
                maxlength: icon + "长度必须20个字符以内！",
                number: icon + "手机号只能是数字",
                remote: icon + "手机号已被使用！"
            },
            email: {
                required: icon + "请输入邮箱！",
                maxlength: icon + "长度必须100个字符以内！",
                email: icon + "请输入正确的邮箱格式"
            },
            password:
            {
                required: icon + "请输入密码！",
                maxlength: icon + "长度必须100个字符以内"
            },
            repassword: {
                required: icon + "请输入确认密码！",
                maxlength: icon + "长度必须100个字符以内",
                equalTo: icon + "两次密码输出不一致"
            }
        },
        errorPlacement: function (error, element) {
            error.appendTo(element.parent().parent());
        }
    }

}



$().ready(function () {
    
    $("#loginForm").validate(validateObject.loginForm);
    $("#registerForm").validate(validateObject.registerForm);
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '10%' 
    });
    if ($("#GoToTab").val() == "reg") {
        $('#login-tab a[href="#register"]').tab('show');
    }
    
})
