
function CreateStarSign(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var jsonName = my.getJsonNameByJosn(data);
    option = {
        color: ['#1E90FF', '#87CEFA', '#00FFFF', '#FFA500', '#FF8247', '#E9967A', '#00FA9A', '#7D26CD', '#836FFF', '#7FFF00', '#6E8B3D', '#FFFF00'],
        backgroundColor: '',
        //tooltip: {
        //    trigger: 'item',
        //    formatter: "{a} <br/>{b}: {c} ({d}%)"
        //},
        legend: {
            orient: 'vertical',
            x: 'left',
            data: jsonName,
            show:false
        },
        series: [
            {
                name: '访问量',
                type: 'pie',
                roseType: 'area',
                radius: ['15%', '45%'],
                label: {
                    normal: {
                        formatter: "{b}:{c}\n({d}%)",
                        backgroundColor: 'rgb(5,13,2)',
                        borderColor: '#00FFFF',
                        borderWidth: 1,
                        borderRadius: 4,
                        shadowBlur: 3,
                        shadowOffsetX: 2,
                        shadowOffsetY: 2,
                        shadowColor: '#00FFFF',
                        padding: [0, 2],
                        rich: {
                            hr: {
                                borderColor: '#00FFFF',
                                width: '100%',
                                borderWidth: 0.5,
                                height: 0
                            },
                            b: {
                                fontSize: 8,
                                lineHeight: 26
                            },
                            per: {
                                color: '#eee',
                                backgroundColor: '#334455',
                                padding: [2, 4],
                                borderRadius: 2
                            }
                        }
                    }
                },
                itemStyle: {
                    normal: {
                        shadowBlur: 10
                    }
                },
                data: data
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