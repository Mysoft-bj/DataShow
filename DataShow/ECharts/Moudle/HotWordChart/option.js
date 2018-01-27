
function CreateBarAndLineChar_1(divId, jsonData) {
    var defaultOption = {
        backgroundColor: ""//容器背景色，为空背景透明
      
    }
    var newData = $.extend({}, defaultOption, jsonData);
    var dataName = [];
    var dataA = [];
    var dataB = [];
    var dataAB = [];
    var data = newData.data;
    var option = {
        title: {
            text: '热点分析',
            link: 'https://www.baidu.com/s?wd=' + encodeURIComponent('ECharts'),
            x: 'center',
            textStyle: {
                fontSize: 23
            }

        },
        backgroundColor: '#F7F7F7',
        tooltip: {
            show: true
        },
        toolbox: {
            feature: {
                saveAsImage: {
                    iconStyle: {
                        normal: {
                            color: '#FFFFFF'
                        }
                    }
                }
            }
        },
        series: [{
            name: '热点分析',
            type: 'wordCloud',
            //size: ['9%', '99%'],
            sizeRange: [6, 66],
            //textRotation: [0, 45, 90, -45],
            rotationRange: [-45, 90],
            //shape: 'circle',
            textPadding: 0,
            autoSize: {
                enable: true,
                minSize: 6
            },
            textStyle: {
                normal: {
                    color: function () {
                        return 'rgb(' + [
                            Math.round(Math.random() * 160),
                            Math.round(Math.random() * 160),
                            Math.round(Math.random() * 160)
                        ].join(',') + ')';
                    }
                },
                emphasis: {
                    shadowBlur: 10,
                    shadowColor: '#333'
                }
            },
            data: [{
                name: "Jayfee",
                value: 666
            }, {
                name: "Nancy",
                value: 520
            }]
        }]
    };
    option.series[0].data = data;
    var myChart = echarts.init(document.getElementById(divId));
    myChart.setOption(option);

    myChart.on('click', function (params) {
        window.open('https://www.baidu.com/s?wd=' + encodeURIComponent(params.name));

    });
}








