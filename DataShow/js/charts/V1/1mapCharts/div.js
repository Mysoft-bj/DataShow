var jsonAll;
function GetJsonAll(url) {
    if (jsonAll == undefined) {
        htmlobj = $.ajax({ url: url, async: false });
        jsonAll = JSON.parse(htmlobj.responseText);
    }
}
// 基于准备好的dom，初始化echarts实例
function div2_1Load() {
    var jsonName;
    var jsonValue;
    var json;
    //$.post("/Charts/GetSaleRanking", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    drawDiv1Pic(json, jsonName, jsonValue);
    //    setDiv1Remark(json);
    //});

    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll("/js/charts/V1/1mapCharts/data.txt");
    var jsonName;
    var jsonValue;
    var json = jsonAll.ProjMap;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv2_1Pic(json, jsonName, jsonValue);
    setDiv2_1Remark(json);
}

function drawDiv2_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div2-1'));
    var data = json;
    //后台需封装算法，用于获取城市经纬度
    var geoCoordMap = {
        '北京': [116.46, 39.92],
        '武汉': [114.31, 30.52],
        '合肥': [117.27, 31.86]
    };
    // 指定图表的配置项和数据
    var convertData = function (data) {
        var res = [];
        for (var i = 0; i < data.length; i++) {
            var geoCoord = geoCoordMap[data[i].name];
            if (geoCoord) {
                res.push({
                    name: data[i].name,
                    value: geoCoord.concat(data[i].value)
                });
            }
        }
        return res;
    };

    option = {
        backgroundColor: '',
        title: {
            text: '全国落地城市',
            subtext: '',
            left: 'center',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {
            trigger: 'item'
        },
        legend: {
            orient: 'vertical',
            y: 'bottom',
            x: 'right',
            data: [],
            textStyle: {
                color: '#fff'
            }
        },
        geo: {
            map: 'china',
            label: {
                emphasis: {
                    show: false
                }
            },
            roam: true,
            itemStyle: {
                normal: {
                    areaColor: '#323c48',
                    borderColor: '#111'
                },
                emphasis: {
                    areaColor: '#2a333d'
                }
            }
        },
        series: [
            {
                name: 'pm2.5',
                type: 'scatter',
                coordinateSystem: 'geo',
                data: convertData(data),
                symbolSize: function (val) {
                    return val[2] / 10;
                },
                label: {
                    normal: {
                        formatter: '{b}',
                        position: 'right',
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#ddb926'
                    }
                }
            },
            {
                name: 'Top 5',
                type: 'effectScatter',
                coordinateSystem: 'geo',
                data: convertData(data.sort(function (a, b) {
                    return b.value - a.value;
                }).slice(0, 6)),
                symbolSize: function (val) {
                    return val[2] / 10;
                },
                showEffectOn: 'render',
                rippleEffect: {
                    brushType: 'stroke'
                },
                hoverAnimation: true,
                label: {
                    normal: {
                        formatter: '{b}',
                        position: 'right',
                        show: true
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#f4e925',
                        shadowBlur: 10,
                        shadowColor: '#333'
                    }
                },
                zlevel: 1
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