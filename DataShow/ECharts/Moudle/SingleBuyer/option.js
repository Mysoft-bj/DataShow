
function CreateSingleBuyer(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var jsonName = my.getJsonNameByJosn(data);
    var iMaleDealed = 0;
    var iFemaleDealed = 0;
    var iMultiDealed=0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].name == "male") {
            iMaleDealed += data[i].value * 1;
        }
        else if (data[i].name == "female") {
            iFemaleDealed += data[i].value * 1;
        }
        else {
            iMultiDealed += data[i].value * 1;
        }
    }
    var rateMale, rateFemale;
    rateMale = ((iMaleDealed * 10000 / (iMaleDealed * 10000+iFemaleDealed * 10000 + iMultiDealed * 10000))*100).toFixed(2);
    rateFemale = ((iFemaleDealed * 10000 / (iMaleDealed * 10000+iFemaleDealed * 10000 + iMultiDealed * 10000)) * 100).toFixed(2);
    option = {
        title: {
            text: '男女购房分析',
            subtext: '',
            x: 'center',
            textStyle: {
                color: '#fff'
            }
        },
        series: [
            {
                name: '男',
                type: 'pie',
                radius: ['25%', '30%'],
                center: ['30%', '30%'],
                startAngle: 225,
                color: [new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#00a2ff'
                }, {
                    offset: 1,
                    color: '#70ffac'
                }]), "transparent"],
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                label: {
                    normal: {
                        position: 'center'
                    }
                },
                data: [{
                    value: 75,
                    name: '',
                    label: {
                        normal: {
                            formatter: '',
                            textStyle: {
                                color: '#fff',
                                fontSize: 16

                            }
                        }
                    }
                }, {
                    value: 25,
                    name: '%',
                    label: {
                        normal: {
                            formatter: '\n' + rateMale+'%',
                            textStyle: {
                                color: '#007ac6',
                                fontSize: 18

                            }
                        }
                    }
                },
                {
                    value: 0,
                    name: '%',
                    label: {
                        normal: {
                            formatter: '男(单人)',
                            textStyle: {
                                color: '#63B8FF',
                                fontSize: 16

                            }
                        }
                    }
                }]
            },
            {
                name: '女',
                type: 'pie',
                radius: ['25%', '30%'],
                center: ['70%', '30%'],
                startAngle: 225,
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                label: {
                    normal: {
                        position: 'center'
                    }
                },
                data: [{
                    value: 75,
                    "itemStyle": {
                        "normal": {
                            "color": new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                "offset": 0,
                                "color": '#f125ff'
                            }, {
                                "offset": 1,
                                "color": '#2dcbff'
                            }]),
                        }
                    },
                    name: '现有门店',
                    label: {
                        normal: {
                            formatter: '',
                            textStyle: {
                                color: '#fff',
                                fontSize: 16

                            }
                        }
                    }
                }, {
                    value: 25,
                    name: '%',
                    label: {
                        normal: {
                            formatter: '\n' + rateFemale + '%',
                            textStyle: {
                                color: '#f125ff',
                                fontSize: 18

                            }
                        }
                    }
                },
                {
                    value: 0,
                    name: '%',
                    label: {
                        normal: {
                            formatter: '女(单人)',
                            textStyle: {
                                color: '#EE7AE9',
                                fontSize: 16

                            }
                        }
                    }
                }]
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


function CreateBuyerTrend(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var jsonName = my.getJsonNameByJosn(data);
    

    var arrYear = [];
    var arrMale = [];
    var arrFemale = [];
    var arrMulti = [];

    //首先算出有多少年份
    for (var i = 0; i < data.length; i++) {
        if ($.inArray(data[i].year, arrYear) == -1) {
            arrYear.push(data[i].year);
        }
    }
    for (var j = 0; j < arrYear.length; j++) {
        //依次计算每一年的男女占比情况
        var iMaleDealed = 0;
        var iFemaleDealed = 0;
        var iMultiDealed = 0;
        //1.首先算出每年的男女数量
        for (var i = 0; i < data.length; i++) {
            if (data[i].year == arrYear[j]) {
                if (data[i].name == "male") {
                    iMaleDealed += data[i].value * 1;
                }
                else if (data[i].name == "female") {
                    iFemaleDealed += data[i].value * 1;
                }
                else {
                    iMultiDealed += data[i].value * 1;
                }
            }
        }
        //2.其次算出占比
        var rateMale, rateFemale, rateMulti;
        rateMale = ((iMaleDealed * 10000 / (iMaleDealed * 10000 + iFemaleDealed * 10000 + iMultiDealed * 10000)) * 100).toFixed(2);
        rateFemale = ((iFemaleDealed * 10000 / (iMaleDealed * 10000 + iFemaleDealed * 10000 + iMultiDealed * 10000)) * 100).toFixed(2);
        rateMulti = (((100 * 10000).toFixed(0) - (rateMale * 10000).toFixed(0) - (rateFemale * 10000).toFixed(0)) / 10000).toFixed(2);
        arrMale.push(rateMale);
        arrFemale.push(rateFemale);
        arrMulti.push(rateMulti);
    }


    option = {
        backgroundColor: '#394056',
        title: {
            text: '单人购房占比走势',
            textStyle: {
                fontWeight: 'normal',
                fontSize: 16,
                color: '#F1F1F3'
            },
            left: '6%'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                lineStyle: {
                    color: '#57617B'
                }
            }
        },
        legend: {
            icon: 'rect',
            itemWidth: 14,
            itemHeight: 5,
            itemGap: 13,
            data: ['男单', '女单', '多人'],
            right: '4%',
            textStyle: {
                fontSize: 12,
                color: '#F1F1F3'
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [{
            type: 'category',
            boundaryGap: false,
            axisLine: {
                lineStyle: {
                    color: '#57617B'
                }
            },
            data: arrYear
        }],
        yAxis: [{
            type: 'value',
            axisTick: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: '#57617B'
                }
            },
            axisLabel: {
                margin: 10,
                textStyle: {
                    fontSize: 14
                }
            },
            splitLine: {
                lineStyle: {
                    color: '#57617B'
                }
            }
        }],
        series: [{
            name: '多人',
            type: 'line',
            smooth: true,
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(137, 189, 27, 0.3)'
                    }, {
                        offset: 0.8,
                        color: 'rgba(137, 189, 27, 0)'
                    }], false),
                    shadowColor: 'rgba(0, 0, 0, 0.1)',
                    shadowBlur: 10
                }
            },
            itemStyle: {
                normal: {
                    color: 'rgb(137,189,27)'
                }
            },
            data: arrMulti
        }, {
            name: '男单',
            type: 'line',
            smooth: true,
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(0, 136, 212, 0.3)'
                    }, {
                        offset: 0.8,
                        color: 'rgba(0, 136, 212, 0)'
                    }], false),
                    shadowColor: 'rgba(0, 0, 0, 0.1)',
                    shadowBlur: 10
                }
            },
            itemStyle: {
                normal: {
                    color: 'rgb(0,136,212)'
                }
            },
            data: arrMale
        }, {
            name: '女单',
            type: 'line',
            smooth: true,
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            areaStyle: {
                normal: {
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(219, 50, 51, 0.3)'
                    }, {
                        offset: 0.8,
                        color: 'rgba(219, 50, 51, 0)'
                    }], false),
                    shadowColor: 'rgba(0, 0, 0, 0.1)',
                    shadowBlur: 10
                }
            },
            itemStyle: {
                normal: {
                    color: 'rgb(219,50,51)'
                }
            },
            data: arrFemale
        }, ]
    };

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    setTimeout(function () {
        $('.counter').countUp()
        , 2000
    }
)
}