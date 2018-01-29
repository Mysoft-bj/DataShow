
function CreateAgeTakeTime(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var maxdata = jsonData.maxdata;
    jsonValue = my.getJsonValueByJosn(data);
    //tooltip 只显示选中点数据
    //引用的基于echarts-3.6.2.js修改后的js，具体看“脚本”。此js对地图缩放也有修改(缩放被禁止)。
    //雷达部分修改都是基于github上大佬们的代码。
    //只需要雷达修改版的，留邮箱。

    option = {
        title: {
            text: '各年龄段平均成交周期(月)',
            left: 'center',
            textStyle: {
                color: '#fff',
                fontSize:16
            }
        },
        tooltip: {
            position: ['50%', '30%'],
            show:true
        },
        radar: {
            shape: 'circle',
            indicator: maxdata,
            radius: 70,
            name: {
                textStyle: {
                    color: 'rgb(238, 197, 102)'
                }
            },
            splitLine: {
                lineStyle: {
                    color: [
                        'rgba(238, 197, 102, 0.1)', 'rgba(238, 197, 102, 0.2)',
                        'rgba(238, 197, 102, 0.4)', 'rgba(238, 197, 102, 0.6)',
                        'rgba(238, 197, 102, 0.8)', 'rgba(238, 197, 102, 1)'
                    ].reverse()
                }
            },
            splitArea: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: 'rgba(238, 197, 102, 0.5)'
                }
            }
        },
        series: [{
            name: '成交需要时间(月)',
            type: 'radar',
            radius: '30%',
            // areaStyle: {normal: {}},
            data: [
                {
                    value: jsonValue,
                    name: '成交需要时间(月)'
                }
            ],
            itemStyle: {
                normal: {
                    color: '#F9713C'
                }
            },
            areaStyle: {
                normal: {
                    opacity: 0.1
                }
            }
        }]
    };
    

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    setTimeout(function () {
        $('.counter').countUp()
        , 2000
    }
)
}