
function CreateAgeSpread(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var jsonName = my.getJsonNameByJosn(data);
    option = {
        title: {
            text: '各年龄段成交量',
            subtext: '',
            x: 'center',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: jsonName,
            textStyle: {
                color: '#fff'
            },
            show:false
        },
        series: [
            {
                name: '成交量',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: data,
                label: {
                    normal: {
                        formatter: "{b}({d}%)"
                    }
                },
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    };
    

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    setTimeout(function () {
        $('.counter').countUp()
        , 2000
    }
)
}