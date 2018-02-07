var requireConfig = {
    baseUrl: "/js",
    waitSeconds: 0,
    paths: {

        //公用包
        "jquery": "/lib/jquery/jquery.min",
        "underscore": "underscore/underscore-min",
        "bootstrap": "/lib/bootstrap/js/bootstrap.min",
        "jquery.cookie": "/lib/jquery.cookie/jquery.cokie.min",
        "bootstrap-hover-dropdown": "/lib/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min",
        "jquery-slimscroll": "/lib/jquery-slimscroll/jquery.slimscroll.min",
        "jquery.blockui": "/lib/jquery.blockui/jquery.blockui.min",
        "uniform": "/lib/uniform/jquery.uniform.min",
        "bootstrap-switch": "/lib/bootstrap-switch/js/bootstrap-switch.min",
        "jquery.validate": "/lib/jquery-validation/js/jquery.validate.min",
        "additional-methods": "/lib/jquery-validation/js/additional-methods.min",
        "select2": "/lib/select2/js/select2.min",
        "select2.full": "/lib/select2/js/select2.full.min",
        "backstretch": "/lib/backstretch/jquery.backstretch.min",
        "app": "/lib/app/app.min",


        //自定义
        "login-4": "login/login-4"
    },
    map: {
        '*': {
            'css': '/lib/require-css/css.min.js'
        }
    },
    shim: {
        "underscore": {
            exports: "_"
        },
        "jquery": {
            exports: "$"
        },
        "bootstrap": {
            deps: ['jquery', 'css!/lib/bootstrap/css/bootstrap.min.css']
        },

        "uniform": {
            deps: ['css!/lib/uniform/css/uniform.default.css']
        },
        "bootstrap-switch": {
            deps: ['css!/lib/bootstrap-switch/css/bootstrap-switch.min.css']
        },
        "select2": {
            deps: ['css!/lib/select2/css/select2.min.css',
                   'css!/lib/select2/css/select2-bootstrap.min.css'
            ]
        },
        "plugins": {
            deps: ['css!/css/plugins/plugins.min.css']
        },

        "login-4": {
            deps: ['bootstrap',
                 'css!/css/login/login-4.css',
                'css!/lib/font-awesome/css/font-awesome.min.css',
                'css!/lib/simple-line-icons/simple-line-icons.min.css',
                'css!/css/components/components.css',
                'css!/css/plugins/plugins.min.css',
                'uniform',
                'bootstrap-switch',
                'select2',
                'select2.full',
                'backstretch',
                'jquery.cookie',
                'bootstrap-hover-dropdown',
                'jquery-slimscroll',
                'jquery.blockui',
                'jquery.validate',
                //'additional-methods',
                //'app'
            ]
        }




    }
};
requirejs.config(requireConfig)