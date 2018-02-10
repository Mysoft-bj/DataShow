function CreateProjMap(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var center = jsonData.center;
    //后台需封装算法，用于获取城市经纬度
    var geoCoordMap = {
        '北京': [116.46, 39.92],
        '武汉': [114.31, 30.52],
        '合肥': [117.27, 31.86],
        '郑州': [113.65, 34.76],
        '长春': [125.35, 43.88],
        '成都': [104.06, 30.67],
        '西安': [108.56, 34.15],
        '广州': [113.30, 23.20],
        '乌鲁木齐':[87.68,43.77]
    };
    var CenterData = function (data, center) {
        var res = [];
        var resChild1 = [];
        resChild1.push({
            name: center
        });
        for (var i = 0; i < data.length; i++) {
            if (data[i].name == center) { continue; }
            var geoCoord = geoCoordMap[data[i].name];
            var resChild2 = [];
            if (geoCoord) {
                resChild2.push({
                    name: data[i].name,
                    value: data[i].value
                });
                res.push(new Array(resChild1, resChild2));
            }
        }
        return res;
    }
    var lineData = CenterData(data, center);
    // 指定图表的配置项和数据

    var convertData = function (data) {
        var res = [];
        for (var i = 0; i < data.length; i++) {
            var geoCoord = geoCoordMap[data[i].name];
            if (geoCoord) {
                res.push({
                    name: data[i].name,
                    value: geoCoord.concat(data[i].value)
                    //, color: rangeColor(data, data[i].name)
                });
            }
        }
        return res;
    };
    var rangeColor = function (data, dataName) {
        var arrColor = ['#ff3333', 'orange', 'yellow', 'lime', 'aqua', '#C0FF3E', '#90EE90'];
        for (var i = 0; i < data.length; i++) {
            if (data[i].name == dataName) {
                return arrColor[i];
            }
        }
        return arrColor[0];
    };
    var convertLineData = function (data) {
        var res = [];
        for (var i = 0; i < data.length; i++) {
            var dataItem = data[i];
            var fromCoord = geoCoordMap[dataItem[0][0].name];
            var toCoord = geoCoordMap[dataItem[1][0].name];
            if (fromCoord && toCoord) {
                res.push({
                    fromName: dataItem[0][0].name,
                    toName: dataItem[1][0].name,
                    coords: [fromCoord, toCoord]
                    //,color: rangeColor(data, data[i].name)
                });
            }
        }
        return res;
    };

    option = {
        backgroundColor: '',
        title: {
            text: '',
            subtext: '',
            left: 'center',
            textStyle: {
                color: '#eacb20'
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
        color: ['#ff3333', 'orange', 'yellow', 'lime', 'aqua'],
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
                    areaColor: '#CC6666',
                    //color:'自适应',
                    borderColor: '#111'
                    //,opacity:0.2
                },
                emphasis: {
                    areaColor: '#2a333d'
                }
            },
            zoom: 1.2
        },
        series: [
        {
            name: '',
            type: 'effectScatter',
            coordinateSystem: 'geo',
            data: convertData(data),
            symbol: 'circle',
            symbolSize: function (val) {
                return 0.5;
            },
            showEffectOn: 'render',
            rippleEffect: {
                brushType: 'stroke',
                scale: 35,
                period: 5
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
                    //color: val.color,
                    //color: '#f4e925',
                    color: function (val) {
                        return rangeColor(data, val.name);
                    },
                    shadowBlur: 0,
                    shadowColor: '#333'
                }
            },
            zlevel: 1
        },
    {
        name: '',
        type: 'lines',
        zlevel: 2,
        symbol: ['none', 'arrow'],
        symbolSize: 7,
        effect: {
            show: true,
            period: 2,
            trailLength: 0,
            symbol: ['arrow'],
            symbolSize: 5
        },
        lineStyle: {
            normal: {
                //color: "#fff",
                color: function (val) {
                    return rangeColor(data, val.data.toName);
                },
                width: 1,
                opacity: 0.6,
                curveness: 0.2
            }
        },
        data: convertLineData(lineData)
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
                color: '#F9D9AE',
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
            radius: 60,
            name: {
                textStyle: {
                    color: 'rgb(255, 230, 134)'
                }
            },
            splitLine: {
                lineStyle: {
                    color: [
                        'rgba(255, 230, 134, 0.5)', 'rgba(255, 230, 134, 0.6)',
                        'rgba(255, 230, 134, 0.7)', 'rgba(255, 230, 134, 0.8)',
                        'rgba(255, 230, 134, 0.9)', 'rgba(255, 230, 134, 1)'
                    ].reverse()
                }
            },
            splitArea: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: 'rgba(255, 230, 134, 0.8)'
                }
            }
        },
        series: [{
            name: '成交需要时间(月)',
            type: 'radar',
            radius: '25%',
            // areaStyle: {normal: {}},
            data: [
                {
                    value: jsonValue,
                    name: '成交需要时间(月)'
                }
            ],
            itemStyle: {
                normal: {
                    color: '#FFE686'
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


function CreateDashBoard(divId, jsonData) {
    var defaultOption = {
        backgroundColor: "", //容器背景色，为空背景透明
        formatvalue: "%", //格式化显示配置,会加在数据显示后面
        color: "#b2975a", //仪表盘及标题显示颜色
        width: "3", //表盘弧形宽度
        axisTickLength: "8",//小刻度长度
        splitLine: "12",//大刻度长度
        pointerWidth: "3",//指针宽度
        pointerLength:"50",//指针长度
        datavale: "50", //数据值
        dataname: "XX"//显示名称
    }
    var newData = $.extend({}, defaultOption, jsonData)
    var option = {
        backgroundColor: newData.backgroundColor,
        series: [{
            name: '',
            type: 'gauge',
            //表盘样式
            axisLine: {
                show: true,
                lineStyle: {
                    width: newData.width == "" ? defaultOption.width : newData.width,
                    shadowBlur: 0,
                    color: [
                        [1, newData.color == "" ? defaultOption.color : newData.color]
                    ]
                }
            },
            //小标记刻度
            axisTick: {            // 坐标轴小标记
                length: newData.axisTickLength,        // 属性length控制线长
                lineStyle: {       // 属性lineStyle控制线条样式
                    color: 'auto'
                }
            },
            //大分割刻度
            splitLine: {           // 分隔线
                length: newData.splitLine,         // 属性length控制线长
                lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                    color: 'auto'
                }
            },
            //指针控制
            pointer: {
                width: newData.pointerWidth,
                length: newData.pointerLength
            },
            //上标题配置
            title: {
                fontSize: 16,
                color: newData.color
            },
            //下面描述配置
            detail: {
                fontWeight: 'bolder',
                fontSize: 16,
                formatter: '{value}' + newData.formatvalue
            },
            //数据
            data: [{
                value: newData.datavale,
                name: newData.dataname
            }]

        }]
    };
    var myChart = echarts.init(document.getElementById(divId));
    myChart.setOption(option);
}

function CreateAgeSpread(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var jsonName = my.getJsonNameByJosn(data);
    option = {
        //color: ['#5FD9CD', '#EAF786', '#FFB5A1', '#B8FFB8', '#B8F4FF', '#C0C0C0', '#F5F5DC'],
        //color: ['#279B00', '#DEB259', '#912928', '#6695AF', '#C58D50', '#B7968D', '#2E3D5C'],
        title: {
            text: '',
            subtext: '',
            x: 'center',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b}({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: jsonName,
            textStyle: {
                color: '#fff'
            },
            show: false
        },
        series: [
            {
                name: '成交量',
                type: 'pie',
                radius: '50%',
                center: ['50%', '60%'],
                data: data,
                label: {
                    normal: {
                        formatter: "{b}\n({d}%)"
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
            show: false
        },
        series: [
            {
                name: '访问量',
                type: 'pie',
                roseType: 'area',
                radius: ['15%', '45%'],
                label: {
                    normal: {
                        formatter: "{b}\n({d}%)",
                        backgroundColor: '#D86246',
                        borderColor: '#00FFFF',
                        borderWidth: 1,
                        borderRadius: 4,
                        shadowBlur: 3,
                        shadowOffsetX: 1,
                        shadowOffsetY: 1,
                        shadowColor: '#00FFFF',
                        padding: [1, 4],
                        rich: {
                            hr: {
                                borderColor: '#00FFFF',
                                width: '100%',
                                borderWidth: 1,
                                height: 2
                            },
                            b: {
                                fontSize: 8,
                                lineHeight: 28
                            },
                            per: {
                                color: '#eee',
                                backgroundColor: '#334455',
                                padding: [2, 8],
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

function CreateSingleBuyer(divId, jsonData) {

    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var jsonName = my.getJsonNameByJosn(data);
    var iMaleDealed = 0;
    var iFemaleDealed = 0;
    var iMultiDealed = 0;
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
    rateMale = ((iMaleDealed * 10000 / (iMaleDealed * 10000 + iFemaleDealed * 10000 + iMultiDealed * 10000)) * 100).toFixed(2);
    rateFemale = ((iFemaleDealed * 10000 / (iMaleDealed * 10000 + iFemaleDealed * 10000 + iMultiDealed * 10000)) * 100).toFixed(2);
    option = {
        title: {
            text: '单人购房占比',
            subtext: '',
            x: 'center',
            textStyle: {
                color: '#F9D9AE'
            }
        },
        series: [
            {
                name: '男',
                type: 'pie',
                radius: ['55%', '60%'],
                center: ['30%', '50%'],
                startAngle: 225,
                color: [new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: 'rgba(255, 230, 134,0.9)'
                }, {
                    offset: 1,
                    color: 'rgba(255, 230, 134, 0.3)'
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
                            formatter: '\n' + rateMale + '%',
                            textStyle: {
                                color: '#F9D9AE',
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
                                color: '#F9D9AE',
                                fontSize: 14

                            }
                        }
                    }
                }]
            },
            {
                name: '女',
                type: 'pie',
                radius: ['55%', '60%'],
                center: ['70%', '50%'],
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
                                "color": 'rgba(255, 230, 134,0.9)'
                            }, {
                                "offset": 1,
                                "color": 'rgba(255, 230, 134,0.3)'
                            }]),
                        }
                    },
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
                            formatter: '\n' + rateFemale + '%',
                            textStyle: {
                                color: '#F9D9AE',
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
                                color: '#F9D9AE',
                                fontSize: 14

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
        backgroundColor: '',
        title: {
            text: '单人购房占比走势',
            textStyle: {
                fontWeight: 'normal',
                fontSize: 16,
                color: '#F9D9AE'
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
                color: '#F9D9AE'
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
                    color: '#F9D9AE'
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
                    color: '#F9D9AE'
                }
            },
            axisLabel: {
                margin: 10,
                textStyle: {
                    fontSize: 14
                }
            },
            splitLine: {
                show: false,
                lineStyle: {
                    color: '#F9D9AE'
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