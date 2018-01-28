﻿
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
    for (var i = 0; i < data.length; i++) {
        dataName.push(data[i].name);
        dataA.push(data[i].value1);
        dataB.push(data[i].value2)
        dataAB.push(parseFloat(data[i].value1) + parseFloat(data[i].value2));
    }
    var option = {
        backgroundColor: "#344b58",
        title: {
            show:false,
            text: "本年商场顾客男女人数统计",
            subtext: "BY Wang Dingding",
            x: "4%",

            textStyle: {
                color: '#fff',
                fontSize: '22'
            },
            subtextStyle: {
                color: '#90979c',
                fontSize: '16',

            },
        },
        tooltip: {
            trigger: "axis",
            axisPointer: {
                type: "shadow",
                textStyle: {
                    color: "#fff"
                }

            },
        },
        grid: {
            "borderWidth": 0,
            "top": 10,
            "bottom": 40,
            textStyle: {
                color: "#fff"
            }
        },
        legend: {
            show:false,
            x: '4%',
            top: '11%',
            textStyle: {
                color: '#90979c',
            },
            data: ['女', '男', '平均']
        },


        calculable: true,
        xAxis: [{
            show: true,
            type: "category",
            axisLine: {
                lineStyle: {
                    color: '#90979c'
                }
            },
            splitLine: {
                show: false
            },
            axisTick: {
                show: false
            },
            splitArea: {
                show: false
            },
            axisLabel: {
                interval: 0,

            },
            data: dataName,
        }],
        yAxis: [{
            show: false,
            type: "value",
            splitLine: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: '#90979c'
                }
            },
            axisTick: {
                show: false
            },
            axisLabe: {
                interval: 0,

            },
            splitArea: {
                show: false
            },

        }],
        dataZoom: [{
            show: false,
            height: 30,
            xAxisIndex: [
                0
            ],
            bottom: 30,
            start: 0,
            end: 50,
            handleIcon: 'path://M306.1,413c0,2.2-1.8,4-4,4h-59.8c-2.2,0-4-1.8-4-4V200.8c0-2.2,1.8-4,4-4h59.8c2.2,0,4,1.8,4,4V413z',
            handleSize: '110%',
            handleStyle: {
                color: "#d3dee5",

            },
            textStyle: {
                color: "#fff"
            },
            borderColor: "#90979c"


        }, {
            type: "inside",
            show: true,
            height: 15,
            start: 1,
            end: 35
        }],
        series: [{
            name: "大户型",
            type: "bar",
            stack: "总量",
            barMaxWidth: 15,
            barGap: "10%",
            itemStyle: {
                normal: {
                    color: "rgba(255,144,128,1)",
                    label: {
                        show: true,
                        textStyle: {
                            color: "#fff"
                        },
                        position: "insideTop",
                        formatter: function (p) {
                            return p.value > 0 ? (p.value) : '';
                        }
                    }
                }
            },
            data: dataA,
        },

            {
                name: "小户型",
                type: "bar",
                stack: "总量",
                itemStyle: {
                    normal: {
                        color: "rgba(0,191,183,1)",
                        barBorderRadius: 0,
                        label: {
                            show: true,
                            position: "top",
                            formatter: function (p) {
                                return p.value > 0 ? (p.value) : '';
                            }
                        }
                    }
                },
                data: dataB
            }, {
                name: "总户数",
                type: "line",
                stack: "总量",
                symbolSize: 10,
                symbol: 'circle',
                itemStyle: {
                    normal: {
                        color: "rgba(252,230,48,1)",
                        barBorderRadius: 0,
                        label: {
                            show: true,
                            position: "top",
                            formatter: function (p) {
                                return p.value > 0 ? (p.value) : '';
                            }
                        }
                    }
                },
                data: dataAB
            },
        ]
    };
    var myChart = echarts.init(document.getElementById(divId));
    myChart.setOption(option);
}








