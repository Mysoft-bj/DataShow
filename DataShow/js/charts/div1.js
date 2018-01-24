var jsonAll;
function GetJsonAll() {
    if (jsonAll == undefined) {
        htmlobj = $.ajax({ url: "/data/data.txt", async: false });
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
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.SaleRanking;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv2_1Pic(json, jsonName, jsonValue);
    setDiv2_1Remark(json);
}

function drawDiv2_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div2-1'));
    // 指定图表的配置项和数据
    var option = {
        title: {
            text: '项目成交量TOP3',
            x: 'center',
            textStyle:{
                color: "#eacb20"
            }
        },
        tooltip: {},
        legend: {
            data: []
        },
        xAxis: {
            data: jsonName,
            axisLabel: {
                interval: 0,
                textStyle: {
                    color: "#eacb20"
                }
            }
        },
        yAxis: {
            axisLabel: {
                interval: 0,
                textStyle: {
                    color: "white"
                }
            }
        },
        series: [{
            name: '成交量',
            type: 'bar',
            data: jsonValue,
            itemStyle: {
                normal: {
                    color: function (params) {
                        // build a color map as your need.
                        var colorList = ['#FE8463', '#9BCA63', '#FAD860'];
                        return colorList[params.dataIndex]
                    }
                }
            }
        }],
        lable: {
            normal: {
                textStyle: {
                    fontsize: '40px'
                }
            }
        }
    };
    

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
    setTimeout(function () {
        $('.counter').countUp()
        , 2000
    }
)
}

function setDiv2_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
    }
    var info =tempName;
    $("#div2-1Span1").html(info);
}


// 基于准备好的dom，初始化echarts实例
function div3_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //$.post("/Charts/GetDiffAgeBuy", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    drawDiv2Pic(json, jsonName, jsonValue);
    //    setDiv2Remark(json);
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.DiffAgeBuy;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv3_1Pic(json, jsonName, jsonValue);
    setDiv3_1Remark(json);
}

function drawDiv3_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div3-1'));
    // 指定图表的配置项和数据
    option = {
        title: {
            text: '各年龄段购买比例',
            x: 'center',
            textStyle: {
                color: "#eacb20"
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
                color:"white"
            }
        },
        series: [
            {
                name: '年龄段成交量',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: json,
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
//    setTimeout(function () {
//        $('.counter').countUp()
//        , 2000
//    }
//)
}

function setDiv3_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
    }
    //$.post("/Charts/GetTwoDiffAge", "", function (result) {
    //    if (result == "") {
    //        var info = "2017年" + tempName + "购买力最强";
    //        $("#div2Span1").html(info);
    //    }
    //    else {
    //        json = eval('(' + result + ')');
    //        var info = "2017年" + tempName + "购买力最强，其中最大购房年龄是" + json[1].value + "岁，最小购房年龄是" + json[0].value + "岁";
    //        $("#div2Span1").html(info);
    //    }
    //});
    htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    var jsonAll = eval('(' + htmlobj.responseText + ')');
    json = jsonAll.TwoDiffAge;

    $("#div3-1Span1").html(tempName);
    $("#div3-1Span2").html(json[1].value);
    $("#div3-1Span3").html(json[0].value);

    //if (json.length == 0) {
    //    var info = "2017年" + tempName + "购买力最强";
    //    $("#div3-1Span1").html(info);
    //}
    //else {
    //    var info = "2017年" + tempName + "购买力最强，其中最大购房年龄是<span class = 'counter'>" + json[1].value + "</span>岁，最小购房年龄是<span class = 'counter'>" + json[0].value + "</span>岁";
    //    $("#div3-1Span1").html(info);
    //}
}



// 基于准备好的dom，初始化echarts实例
function div4_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //$.post("/Charts/GetMostCstGjjl", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    drawDiv3Pic(json, jsonName, jsonValue);
    //    setDiv3Remark(json);
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.Constellations;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv4_1Pic(json, jsonName, jsonValue);
    setDiv4_1Remark(json);
}

function drawDiv4_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div4-1'));
    // 指定图表的配置项和数据
    //option = {
    //    title: {
    //        text: '跟进次数最多的客户及所属公司',
    //        subtext: ''
    //    },
    //    tooltip: {
    //        trigger: 'axis',
    //        axisPointer: {
    //            type: 'shadow'
    //        }
    //    },
    //    legend: {
    //        data: []
    //    },
    //    grid: {
    //        left: '3%',
    //        right: '4%',
    //        bottom: '3%',
    //        containLabel: true
    //    },
    //    xAxis: {
    //        type: 'value',
    //        boundaryGap: [0, 0.01]
    //    },
    //    yAxis: {
    //        type: 'category',
    //        data: jsonName
    //    },
    //    series: [
    //        {
    //            name: '',
    //            type: 'bar',
    //            data: jsonValue,
    //            label: {
    //                normal: {
    //                    show: true
    //                }
    //            }
    //        }
    //    ]
    //};
    var data = [];
    var labelData = [{ value: 1, name: "天秤座" }, { value: 1, name: "处女座" }, { value: 1, name: "狮子座" }, { value: 1, name: "双子座" },
    { value: 1, name: "射手座" }, { value: 1, name: "白羊座" }, { value: 1, name: "水瓶座" }, { value: 1, name: "巨蟹座" },
    { value: 1, name: "魔羯座" }, { value: 1, name: "天蝎座" }, { value: 1, name: "双鱼座" }, { value: 1, name: "金牛座" }];
    //for (var i = 0; i < 24; ++i) {
    //    data.push({
    //        value: Math.random() * 10 + 10 - Math.abs(i - 12),
    //        name: i + ':00'
    //    });
    //}

    option = {
        title: {
            text: '',
            left: '50%',
            textAlign: 'center',
            textStyle: {
                color: "#eacb20"
            }
        },
        color: ['#22C3AA'],
        series: [{
            type: 'pie',
            data: json,
            roseType: 'area',
            itemStyle: {
                normal: {
                    color: 'white',
                    borderColor: '#22C3AA'
                }
            },
            labelLine: {
                normal: {
                    show: false
                }
            },
            label: {
                normal: {
                    show: false
                }
            }
        }, {
            type: 'pie',
            data: labelData,
            radius: ['75%', '100%'],
            zlevel: -2,
            itemStyle: {
                normal: {
                    color: '#22C3AA',
                    borderColor: 'white'
                }
            },
            label: {
                normal: {
                    position: 'inside'
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

function setDiv4_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
    }
    var info =  tempName ;
    $("#div4-1Span1").html(info);
}



// 基于准备好的dom，初始化echarts实例
function div5_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetDiffProductCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    $.post("/Charts/GetMaxProductCnt", "", function (result1) {
    //        jsonMax = eval('(' + result1 + ')');
    //        drawDiv4Pic(json, jsonName, jsonValue, jsonMax);
    //        setDiv4Remark(json);
    //    });        
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    var jsonMost = jsonAll.MostWomenPercentage;
    var jsonLeast = jsonAll.LeastWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv5_1Pic(json, jsonName, jsonValue, jsonMost, jsonLeast);
    setDiv5_1Remark(json, jsonMost, jsonLeast);
}

function drawDiv5_1Pic(json, jsonName, jsonValue, jsonMost, jsonLeast) {
    var myChart = echarts.init(document.getElementById('div5-1'));
    // 指定图表的配置项和数据

    labelTop = {
        normal: {
            color: '#DC143C',
            label: {
                show: true,
                position: 'center',
                formatter: '{b}',
                textStyle: {
                    baseline: 'bottom',
                    color: "#DC143C"
                }
            },
            labelLine: {
                show: false
            }
        }
    };
    var labelFromatter = {
        normal: {
            label: {
                formatter: function (params) {
                    return 100 - params.value + '%'
                },
                textStyle: {
                    baseline: 'top',
                    color: "#DC143C"
                }
            }
        },
    }
    var labelBottom = {
        normal: {
            color: '#eacb20',
            label: {
                show: true,
                position: 'center'
            },
            labelLine: {
                show: false
            }
        },
        emphasis: {
            color: 'rgba(0,0,0,0)'
        }
    };
    var radius = [40, 55];
    option = {
        title: {
            text: '女同胞购房比例',
            subtext: '',
            x: 'center',
            textStyle: {
                color: "#DC143C"
            }
        },
        
        series: [
             {
                 type: 'pie',
                 center: ['50%', '30%'],
                 radius: radius,
                 x: '40%', // for funnel
                 itemStyle: labelFromatter,
                 data: [
                { name: 'other', value: json[0].value, itemStyle: labelBottom },
                { name: json[1].name, value: json[1].value, itemStyle: labelTop }
                 ]
             },
             {
                 type: 'pie',
                 center: ['25%', '70%'],
                 radius: radius,
                 y: '55%',   // for funnel
                 x: '0%',    // for funnel
                 itemStyle: labelFromatter,
                 data: [
                { name: 'other', value: jsonLeast[0].value, itemStyle: labelBottom },
                { name: '最低-'+jsonLeast[1].name, value: jsonLeast[1].value, itemStyle: labelTop }
                 ]
             },
             {
                 type: 'pie',
                 center: ['75%', '70%'],
                 radius: radius,
                 y: '55%',   // for funnel
                 x: '40%', // for funnel
                 itemStyle: labelFromatter,
                 data: [
                { name: 'other', value: jsonMost[0].value, itemStyle: labelBottom },
                { name: '最高-' + jsonMost[1].name, value: jsonMost[1].value, itemStyle: labelTop }
                 ]
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

function setDiv5_1Remark(json, jsonMost, jsonLeast) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class='divText pt-page-index-bounceInLeft1'>老婆最大<span class='EMFont5-1'>" + jsonMost[1].name + "</span></div><div class='divText pt-page-index-bounceInLeft2'>老公最大<span class='EMFont5-1'>" + jsonLeast[1].name + "</span></div>";
    $("#div5-1Span1").html(info);
}

// 基于准备好的dom，初始化echarts实例
function div6_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetDiffProductCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    $.post("/Charts/GetMaxProductCnt", "", function (result1) {
    //        jsonMax = eval('(' + result1 + ')');
    //        drawDiv4Pic(json, jsonName, jsonValue, jsonMax);
    //        setDiv4Remark(json);
    //    });        
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    var jsonMost = jsonAll.MostWomenPercentage;
    var jsonLeast = jsonAll.LeastWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv6_1Pic(json, jsonName, jsonValue, jsonMost, jsonLeast);
    setDiv6_1Remark(json, jsonMost, jsonLeast);
}

function drawDiv6_1Pic(json, jsonName, jsonValue, jsonMost, jsonLeast) {
    var myChart = echarts.init(document.getElementById('div6-1'));
    // 指定图表的配置项和数据

    option = {
        color: ['#3398DB'],
        title: {
            //text: '2000-2016年中国汽车销量及增长率'
        },
        tooltip: {
            trigger: 'axis'
        },
        grid: {
            containLabel: true
        },
        legend: {
            data: ['增速', '销量']
        },
        xAxis: [{
            type: 'category',
            axisTick: {
                alignWithLabel: true
            },
            axisLabel: {
                textStyle: {
                    color: "#eacb20"
                }
            },
            data: ['肖丽', '陈冲', '王爱梅', '李达', '黄友程']
        }],
        yAxis: [{
            type: 'value',
            name: '客户数',
            min: 0,
            max: 50,
            position: 'right',
            axisLabel: {
                formatter: '{value}',
                textStyle: {
                    color: "white"
                }
            },
            textStyle: {
                color: "#eacb20"
            },
            nameTextStyle:{
                color: "white"
            }
        }, {
            type: 'value',
            name: '佣金',
            min: 0,
            max: 400000,
            position: 'left',
            axisLabel: {
                formatter: '{value}',
                textStyle: {
                    color: "white"
                }
            },
            nameTextStyle: {
                color: "white"
            }
        }],
        series: [{
            name: '客户数',
            type: 'line',
            stack: '客户数',
            label: {
                normal: {
                    show: true,
                    position: 'top',
                }
            },
            lineStyle: {
                normal: {
                    width: 3,
                    shadowColor: 'rgba(0,0,0,0.4)',
                    shadowBlur: 10,
                    shadowOffsetY: 10
                }
            },
            data: [42, 39, 37, 34, 29]
        }, {
            name: '佣金',
            type: 'bar',
            yAxisIndex: 1,
            stack: '佣金',
            label: {
                normal: {
                    show: true,
                    position: 'top'
                }
            },
            data: [212548,198745,176545,142352,130125]
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

function setDiv6_1Remark(json, jsonMost, jsonLeast) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<span class = 'key_numb counter'>888888</span><span class = 'key_unit'>元</span>";
    $("#div6-1Span1").html(info);
}


// 基于准备好的dom，初始化echarts实例
function div7_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetDiffProductCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    $.post("/Charts/GetMaxProductCnt", "", function (result1) {
    //        jsonMax = eval('(' + result1 + ')');
    //        drawDiv4Pic(json, jsonName, jsonValue, jsonMax);
    //        setDiv4Remark(json);
    //    });        
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv7_1Pic(json, jsonName, jsonValue);
    setDiv7_1Remark(json);
}

function drawDiv7_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div7-1'));
    // 指定图表的配置项和数据

    option = {
        title: [{
            left: 'center',
            text: '单身/婚后首套房购买情况',
            textStyle: {
                color: "#eacb20"
            }
        }],
        tooltip: {
            trigger: 'axis'
        },
        xAxis: [{
            data: [2012, 2013, 2014, 2015, 2016],
            axisLabel: {
                textStyle: {
                    color: "#eacb20"
                }
            }
        }],
        yAxis: [{
            splitLine: { show: false },
            axisLabel: {
                textStyle: {
                    color: "white"
                },
                formatter: '{value}'
            }
        }],
        grid: [{
        }],
        series: [{
            type: 'line',
            name:"单身",
            data: [312, 523, 684, 1000, 1200],
            label: {
                normal: {
                    show: true,
                    position: 'top',
                    textStyle: {
                        color: '#CD0000'
                    }
                }
            },
            lineStyle: {
                normal: {
                    width: 3,
                    color: '#CD0000'
                }
            }
        }, {
            type: 'line',
            name: "婚后",
            data: [1507, 1468, 1258, 1035, 654],
            label: {
                normal: {
                    show: true,
                    position: 'top',
                    textStyle: {
                        color: '#4B0082'
                    }
                }
            },
            lineStyle: {
                normal: {
                    width: 3,
                    color: '#4B0082'
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

function setDiv7_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class='margin_top'>婚前买房?</div><div class = 'pt-page-index-bounceIn7 EMFont'>越来越普遍</div>";
    $("#div7-1Span1").html(info);
}

// 基于准备好的dom，初始化echarts实例
function div8_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetDiffProductCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    $.post("/Charts/GetMaxProductCnt", "", function (result1) {
    //        jsonMax = eval('(' + result1 + ')');
    //        drawDiv4Pic(json, jsonName, jsonValue, jsonMax);
    //        setDiv4Remark(json);
    //    });        
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv8_1Pic(json, jsonName, jsonValue);
    setDiv8_1Remark(json);
}

function drawDiv8_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div8-1'));
    // 指定图表的配置项和数据

    var dataStyle = {
        normal: {
            label: { show: false },
            labelLine: { show: false },
            shadowBlur: 40,
            shadowColor: 'rgba(40, 40, 40, 0.5)',
        }
    };
    var placeHolderStyle = {
        normal: {
            color: 'rgba(0,0,0,0)',
            label: { show: false },
            labelLine: { show: false }
        },
        emphasis: {
            color: 'rgba(0,0,0,0)'
        }
    };
    option = {
        color: ['#85b6b2', '#6d4f8d', '#cd5e7e', '#e38980', '#f7db88'],
        tooltip: {
            show: true,
            formatter: "{a} <br/>{b} : {c}"
        },
        legend: {
            x: 'center',
            y: 'bottom',
            data: ['3套以上平均年龄', '3套平均年龄', '2套平均年龄', '首套平均年龄'],
            textStyle: {
                color:['#85b6b2', '#6d4f8d', '#cd5e7e', '#e38980', '#f7db88']
            }
        },
        series: [
            {
                name: '',
                type: 'pie',
                clockWise: false,
                radius: [105, 120],
                itemStyle: dataStyle,
                hoverAnimation: false,
                data: [
                    {
                        value: 51,
                        name: '3套以上平均年龄'
                    },
                    {
                        value: 29,
                        name: '',
                        itemStyle: placeHolderStyle
                    }

                ]
            },
             {
                 name: '',
                 type: 'pie',
                 clockWise: false,
                 radius: [90, 105],
                 itemStyle: dataStyle,
                 hoverAnimation: false,

                 data: [
                     {
                         value: 42,
                         name: '3套平均年龄'
                     },
                     {
                         value: 38,
                         name: '',
                         itemStyle: placeHolderStyle
                     }
                 ]
             },
            {
                name: '',
                type: 'pie',
                clockWise: false,
                hoverAnimation: false,
                radius: [75, 90],
                itemStyle: dataStyle,

                data: [
                    {
                        value: 33,
                        name: '2套平均年龄'
                    },
                    {
                        value: 47,
                        name: '',
                        itemStyle: placeHolderStyle
                    }
                ]
            },
            {
                name: '',
                type: 'pie',
                clockWise: false,
                hoverAnimation: false,
                radius: [60, 75],
                itemStyle: dataStyle,

                data: [
                    {
                        value: 26,
                        name: '首套平均年龄'
                    },
                    {
                        value: 54,
                        name: 'invisible',
                        itemStyle: placeHolderStyle
                    }
                ]
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

function setDiv8_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class='pt-page-index-moveFromTop8 margin_top'><span class='EMFont'>51<span><span>岁<span></div><div>拥有多套房产的平均年龄</div>";
    $("#div8-1Span1").html(info);
}

// 基于准备好的dom，初始化echarts实例
function div9_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetDiffProductCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    $.post("/Charts/GetMaxProductCnt", "", function (result1) {
    //        jsonMax = eval('(' + result1 + ')');
    //        drawDiv4Pic(json, jsonName, jsonValue, jsonMax);
    //        setDiv4Remark(json);
    //    });        
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.DiffProductCnt;
    var jsonMax = jsonAll.MaxProductCnt;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv9_1Pic(json, jsonName, jsonValue, jsonMax);
    setDiv9_1Remark(json);
}

function drawDiv9_1Pic(json, jsonName, jsonValue, jsonMax) {
    var myChart = echarts.init(document.getElementById('div9-1'));
    // 指定图表的配置项和数据
    option = {
        title: {
            text: '各产品类型成交量',
            textStyle: {
                color: "#eacb20"
            }
        },
        tooltip: {},
        legend: {
            data: ['']
        },
        radar: {
            // shape: 'circle',
            indicator: jsonMax
        },
        series: [{
            name: '',
            type: 'radar',
            // areaStyle: {normal: {}},
            data: [
            {
                value: jsonValue,
                name: '成交情况'
            }
            ]
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

function setDiv9_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class='pt-page-moveFromTop font-size20'>卖得最多的产品<span class='EMFont9-1'>" + tempName + "</span></div>" + "<div class='pt-page-moveFromTop font-size20'>卖得最少的产品<span class='EMFont9-1'>" + tempNameLeast + "</span></div>";
    $("#div9-1Span1").html(info);
}


// 基于准备好的dom，初始化echarts实例
function div10_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetMediaOppCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    drawDiv5Pic(json, jsonName, jsonValue);
    //    setDiv5Remark(json);
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.MediaOppCnt;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv10_1Pic(json, jsonName, jsonValue);
    setDiv10_1Remark(json);
}

function drawDiv10_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div10-1'));
    // 指定图表的配置项和数据
    option = {
        title: {
            text: '各媒体类型机会情况',
            subtext: '',
            textStyle: {
                color: "#eacb20"
            },
            x: 'center',
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            x: 'center',
            y: 'bottom',
            data: jsonName,
            textStyle: {
                color:"white"
            }
        },
        toolbox: {
            show: true,
            feature: {

            }
        },
        calculable: true,
        series: [
            {
                name: '半径模式',
                type: 'pie',
                radius: [20, 110],
                roseType: 'radius',
                label: {
                    normal: {
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                lableLine: {
                    normal: {
                        show: true
                    },
                    emphasis: {
                        show: true
                    }
                }
            },
            {
                name: '机会量',
                type: 'pie',
                radius: [30, 110],
                roseType: 'area',
                data: json
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

function setDiv10_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class='EMFont margin_top'>" + tempName + "</div>" + "<div class='pt-page-moveFromRight'>最吸引顾客</div>";
    $("#div10-1Span1").html(info);
}

// 基于准备好的dom，初始化echarts实例
function div11_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetDiffMonthDealCnt", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    drawDiv6Pic(json, jsonName, jsonValue);
    //    setDiv6Remark(json);
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.DiffMonthDealCnt;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv11_1Pic(json, jsonName, jsonValue);
    setDiv11_1Remark(json);
}

function drawDiv11_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div11-1'));
    // 指定图表的配置项和数据
    option = {
        title: {
            text: '各月份成交量',
            subtext: '',
            textStyle: {
                color: "#eacb20"
            }
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['成交量'],
            textStyle: {
                color:"#D53A35"
            }
        },
        toolbox: {
            show: true
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: jsonName,
            axisLabel: {
                interval: 0,
                textStyle: {
                    color: "#eacb20"
                }
            }
        },
        yAxis: {
            type: 'value',
            axisLabel: {
                formatter: '{value}',
                textStyle: {
                    color: "white"
                }
            }
        },
        series: [
            {
                name: '成交量',
                type: 'line',
                data: jsonValue,
                label: {
                    normal: {
                        show:true
                    }
                },
                markPoint: {
                    data: [
                        { type: 'max', name: '最大值' },
                        { type: 'min', name: '最小值' }
                    ]
                },
                markLine: {
                    data: [
                        { type: 'average', name: '平均值' }
                    ]
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

function setDiv11_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class='EMFont pt-page-zoomIn'>" + tempName + "</div><div class='pt-page-zoomIn12'>成交量最高的月份</div>";
    //if (tempName != "3月") {
    //    info = "<div class='EMFont'>" + tempName + "</div><div class='pt-page-zoomIn12'>成交量最高的月份</div>"
    //    //+ "，楼市金三银四该改改啦";
    //}
    //else {
    //    info = "<div class='EMFont'>" + tempName + "</div><div class='pt-page-zoomIn12'>成交量最高的月份</div>"
    //    //+ "，金三银四魔咒仍然未打破";
    //}

    
    $("#div11-1Span1").html(info);
}

// 基于准备好的dom，初始化echarts实例
function div12_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetRemovalRate", "", function (result) {
    //    json = eval('(' + result + ')');
    //    jsonName = my.getJsonName(result);
    //    jsonValue = my.getJsonValue(result);
    //    drawDiv7Pic(json, jsonName, jsonValue);
    //    setDiv7Remark(json);
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.RemovalRate;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv12_1Pic(json, jsonName, jsonValue);
    setDiv12_1Remark(json);
}

function drawDiv12_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div12-1'));
    // 指定图表的配置项和数据
    option = {
        tooltip: {
            formatter: "{a} <br/>{b} : {c}%"
        },
        series: [
            {
                name: '去化率',
                type: 'gauge',
                detail: { show:false,formatter: '{value}%' },
                data: json
            }
        ]
    };

    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
}

function setDiv12_1Remark(json) {
    var obj = json;
    var tempName;
    var tempValue;
    var tempNameLeast;
    var tempValueLeast;
    for (var i = 0; i < obj.length; i++) {
        if (i == 0) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
        else if (obj[i].value > tempValue) {
            tempName = obj[i].name;
            tempValue = obj[i].value;
        }
        else if (obj[i].value < tempValueLeast) {
            tempNameLeast = obj[i].name;
            tempValueLeast = obj[i].value;
        }
    }
    var info = "<div class = 'counter'>" + tempValue + "</div><div class='divText pt-page-index-bounceInLeft2'>全公司平均去化率</div>";
    $("#div12-1Span1").html(info);
    setTimeout(function () {
        $('.counter').countUp()
        , 2500
    }
   )
}

function div13_1Load() {
    //var jsonName;
    //var jsonValue;
    //var json;
    //var jsonMax;
    //$.post("/Charts/GetAchievedProjectCount", "", function (result1) {
    //    json = result1;
    //    drawDiv8Pic(json);
    //    setDiv8Remark(json);
    //});
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AchievedProjectCount;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    drawDiv13_1Pic(json);
    setDiv13_1Remark(json);
}
function setDiv13_1Remark(json) {
    var data = json;
    var JSON_count = data.length;
    var info = "<span class ='first-words'>已在<span class = 'counter'>" + JSON_count + "</span>城市落地项目</span>";

    //if (JSON_count < 3) {
    //    info += "芝麻开花节节高，未来的不久全国各地将遍地开花";
    //}
    //else if (JSON_count > 3 && JSON_count < 10) {
    //    info += "八方来财，蒸蒸日上";
    //}
    //else if (JSON_count > 10) {
    //    info += "生意兴隆通四海,贸易通达跨三江"
    //}
    $("#div13-1Span1").html(info);
    setTimeout(function () {
        $('.counter').countUp()
        , 2500
    }
)
}
//预设的城市坐标
function drawDiv13_1Pic(json) {
    var geoCoordMap = {
        "海门市": [121.15, 31.89],
        "鄂尔多斯市": [109.781327, 39.608266],
        "招远市": [120.38, 37.35],
        "舟山市": [122.207216, 29.985295],
        "齐齐哈尔市": [123.97, 47.33],
        "盐城市": [120.13, 33.38],
        "赤峰市": [118.87, 42.28],
        "青岛市": [120.33, 36.07],
        "乳山市": [121.52, 36.89],
        "金昌市": [102.188043, 38.520089],
        "泉州市": [118.58, 24.93],
        "莱西市": [120.53, 36.86],
        "日照市": [119.46, 35.42],
        "胶南市": [119.97, 35.88],
        "南通市": [121.05, 32.08],
        "拉萨市": [91.11, 29.97],
        "云浮市": [112.02, 22.93],
        "梅州市": [116.1, 24.55],
        "文登市": [122.05, 37.2],
        "上海市": [121.48, 31.22],
        "攀枝花市": [101.718637, 26.582347],
        "威海市": [122.1, 37.5],
        "承德市": [117.93, 40.97],
        "厦门市": [118.1, 24.46],
        "汕尾市": [115.375279, 22.786211],
        "潮州市": [116.63, 23.68],
        "丹东市": [124.37, 40.13],
        "太仓市": [121.1, 31.45],
        "曲靖市": [103.79, 25.51],
        "烟台市": [121.39, 37.52],
        "福州市": [119.3, 26.08],
        "瓦房店市": [121.979603, 39.627114],
        "即墨市": [120.45, 36.38],
        "抚顺市": [123.97, 41.97],
        "玉溪市": [102.52, 24.35],
        "张家口市": [114.87, 40.82],
        "阳泉市": [113.57, 37.85],
        "莱州市": [119.942327, 37.177017],
        "湖州市": [120.1, 30.86],
        "汕头市": [116.69, 23.39],
        "昆山市": [120.95, 31.39],
        "宁波市": [121.56, 29.86],
        "湛江市": [110.359377, 21.270708],
        "揭阳市": [116.35, 23.55],
        "荣成市": [122.41, 37.16],
        "连云港市": [119.16, 34.59],
        "葫芦岛市": [120.836932, 40.711052],
        "常熟市": [120.74, 31.64],
        "东莞市": [113.75, 23.04],
        "河源市": [114.68, 23.73],
        "淮安市": [119.15, 33.5],
        "泰州市": [119.9, 32.49],
        "南宁市": [108.33, 22.84],
        "营口市": [122.18, 40.65],
        "惠州市": [114.4, 23.09],
        "江阴市": [120.26, 31.91],
        "蓬莱市": [120.75, 37.8],
        "韶关市": [113.62, 24.84],
        "嘉峪关市": [98.289152, 39.77313],
        "广州市": [113.23, 23.16],
        "延安市": [109.47, 36.6],
        "太原市": [112.53, 37.87],
        "清远市": [113.01, 23.7],
        "中山市": [113.38, 22.52],
        "昆明市": [102.73, 25.04],
        "寿光市": [118.73, 36.86],
        "盘锦市": [122.070714, 41.119997],
        "长治市": [113.08, 36.18],
        "深圳市": [114.07, 22.62],
        "珠海市": [113.52, 22.3],
        "宿迁市": [118.3, 33.96],
        "咸阳市": [108.72, 34.36],
        "铜川市": [109.11, 35.09],
        "平度市": [119.97, 36.77],
        "佛山市": [113.11, 23.05],
        "海口市": [110.35, 20.02],
        "江门市": [113.06, 22.61],
        "章丘市": [117.53, 36.72],
        "肇庆市": [112.44, 23.05],
        "大连市": [121.62, 38.92],
        "临汾市": [111.5, 36.08],
        "吴江市": [120.63, 31.16],
        "石嘴山市": [106.39, 39.04],
        "沈阳市": [123.38, 41.8],
        "苏州市": [120.62, 31.32],
        "茂名市": [110.88, 21.68],
        "嘉兴市": [120.76, 30.77],
        "长春市": [125.35, 43.88],
        "胶州市": [120.03336, 36.264622],
        "银川市": [106.27, 38.47],
        "张家港市": [120.555821, 31.875428],
        "三门峡市": [111.19, 34.76],
        "锦州市": [121.15, 41.13],
        "南昌市": [115.89, 28.68],
        "柳州市": [109.4, 24.33],
        "三亚市": [109.511909, 18.252847],
        "自贡市": [104.778442, 29.33903],
        "吉林市": [126.57, 43.87],
        "阳江市": [111.95, 21.85],
        "泸州市": [105.39, 28.91],
        "西宁市": [101.74, 36.56],
        "宜宾市": [104.56, 29.77],
        "呼和浩特市": [111.65, 40.82],
        "成都市": [104.06, 30.67],
        "大同市": [113.3, 40.12],
        "镇江市": [119.44, 32.2],
        "桂林市": [110.28, 25.29],
        "张家界市": [110.479191, 29.117096],
        "宜兴市": [119.82, 31.36],
        "北海市": [109.12, 21.49],
        "西安市": [108.95, 34.27],
        "金坛市": [119.56, 31.74],
        "东营市": [118.49, 37.46],
        "牡丹江市": [129.58, 44.6],
        "遵义市": [106.9, 27.7],
        "绍兴市": [120.58, 30.01],
        "扬州市": [119.42, 32.39],
        "常州市": [119.95, 31.79],
        "潍坊市": [119.1, 36.62],
        "重庆市": [106.54, 29.59],
        "台州市": [121.420757, 28.656386],
        "南京市": [118.78, 32.04],
        "滨州市": [118.03, 37.36],
        "贵阳市": [106.71, 26.57],
        "无锡市": [120.29, 31.59],
        "本溪市": [123.73, 41.3],
        "克拉玛依市": [84.77, 45.59],
        "渭南市": [109.5, 34.52],
        "马鞍山市": [118.48, 31.56],
        "宝鸡市": [107.15, 34.38],
        "焦作市": [113.21, 35.24],
        "句容市": [119.16, 31.95],
        "北京市": [116.46, 39.92],
        "徐州市": [117.2, 34.26],
        "衡水市": [115.72, 37.72],
        "包头市": [110, 40.58],
        "绵阳市": [104.73, 31.48],
        "乌鲁木齐市": [87.68, 43.77],
        "枣庄市": [117.57, 34.86],
        "杭州市": [120.19, 30.26],
        "淄博市": [118.05, 36.78],
        "鞍山市": [122.85, 41.12],
        "溧阳市": [119.48, 31.43],
        "库尔勒市": [86.06, 41.68],
        "安阳市": [114.35, 36.1],
        "开封市": [114.35, 34.79],
        "济南市": [117, 36.65],
        "德阳市": [104.37, 31.13],
        "温州市": [120.65, 28.01],
        "九江市": [115.97, 29.71],
        "邯郸市": [114.47, 36.6],
        "临安市": [119.72, 30.23],
        "兰州市": [103.73, 36.03],
        "沧州市": [116.83, 38.33],
        "临沂市": [118.35, 35.05],
        "南充市": [106.110698, 30.837793],
        "天津市": [117.2, 39.13],
        "富阳市": [119.95, 30.07],
        "泰安市": [117.13, 36.18],
        "诸暨市": [120.23, 29.71],
        "郑州市": [113.65, 34.76],
        "哈尔滨市": [126.63, 45.75],
        "聊城市": [115.97, 36.45],
        "芜湖市": [118.38, 31.33],
        "唐山市": [118.02, 39.63],
        "平顶山市": [113.29, 33.75],
        "邢台市": [114.48, 37.05],
        "德州市": [116.29, 37.45],
        "济宁市": [116.59, 35.38],
        "荆州市": [112.239741, 30.335165],
        "宜昌市": [111.3, 30.7],
        "义乌市": [120.06, 29.32],
        "丽水市": [119.92, 28.45],
        "洛阳市": [112.44, 34.7],
        "秦皇岛市": [119.57, 39.95],
        "株洲市": [113.16, 27.83],
        "石家庄市": [114.48, 38.03],
        "莱芜市": [117.67, 36.19],
        "常德市": [111.69, 29.05],
        "保定市": [115.48, 38.85],
        "湘潭市": [112.91, 27.87],
        "金华市": [119.64, 29.12],
        "岳阳市": [113.09, 29.37],
        "长沙市": [113, 28.21],
        "衢州市": [118.88, 28.97],
        "廊坊市": [116.7, 39.53],
        "菏泽市": [115.480656, 35.23375],
        "合肥市": [117.27, 31.86],
        "武汉市": [114.31, 30.52],
        "大庆市": [125.03, 46.58]
    };

    var data = json;

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

    convertedData = [
      convertData(data),
      convertData(data.sort(function (a, b) {
          return b.value - a.value;
      }).slice(0, 6))
    ];

    option = {

        animation: true,
        animationDuration: 1000,
        animationEasing: 'cubicInOut',
        animationDurationUpdate: 1000,
        animationEasingUpdate: 'cubicInOut',
        title: [
            {
                text: '全国各城市项目落地情况',
                subtext: '',

                left: 'center',
                textStyle: {
                    color: "#eacb20"
                }
            },
            {
                id: 'statistic',
                right: 120,
                top: 40,
                width: 100,
                textStyle: {
                    color: '#fff',
                    fontSize: 16
                }
            }
        ],
        toolbox: {
            show: false
        },
        brush: {
            outOfBrush: {
                color: '#abc'
            },
            brushStyle: {
                borderWidth: 2,
                color: 'rgba(0,0,0,0.2)',
                borderColor: 'rgba(0,0,0,0.5)',
            },
            seriesIndex: [0, 1],
            throttleType: 'debounce',
            throttleDelay: 300,
            geoIndex: 0
        },
        geo: {
            map: 'china',
            left: '10',
            right: '45%',
            center: [117.98561551896913, 31.205000490896193],
            zoom: 2.5,
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
        tooltip: {
            trigger: 'item'
        },
        grid: {
            right: 40,
            top: 100,
            bottom: 40,
            width: '30%'
        },
        xAxis: {
            type: 'value',
            scale: true,
            position: 'top',
            boundaryGap: false,
            splitLine: { show: false },
            axisLine: { show: false },
            axisTick: { show: false },
            axisLabel: { margin: 2, textStyle: { color: '#aaa' } },
        },
        yAxis: {
            type: 'category',
            name: 'TOP 20',
            nameGap: 16,
            axisLine: { show: false, lineStyle: { color: '#ddd' } },
            axisTick: { show: false, lineStyle: { color: '#ddd' } },
            axisLabel: { interval: 0, textStyle: { color: '#ddd' } },
            data: []
        },
        series: [
            {
                name: '项目落地数',
                type: 'scatter',
                coordinateSystem: 'geo',
                data: convertedData[0],
                symbolSize: function (val) {
                    return Math.max(val[2] / 10, 8);
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
                data: convertedData[1],
                symbolSize: function (val) {
                    return Math.max(val[2] / 10, 8);
                },
                showEffectOn: 'emphasis',
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
            },
            {
                id: 'bar',
                zlevel: 2,
                type: 'bar',
                symbol: 'none',
                itemStyle: {
                    normal: {
                        color: '#ddb926'
                    }
                },
                data: []
            }
        ]
    };

    var myChart = echarts.init(document.getElementById('div13-1'));
    myChart.on('brushselected', renderBrushed);

    myChart.setOption(option);

    setTimeout(function () {
        myChart.dispatchAction({
            type: 'brush',
            areas: [
                {
                    geoIndex: 0,
                    brushType: 'rect',
                    coordRange: [[134.00, 34.00], [134.00, 10.00], [140.00, 10.00], [140.00, 34.00]]
                }
            ]
        });
    }, 0);

}
function renderBrushed(params) {
    var mainSeries = params.batch[0].selected[0];

    var selectedItems = [];
    var categoryData = [];
    var barData = [];
    var maxBar = 30;
    var sum = 0;
    var count = 0;

    for (var i = 0; i < mainSeries.dataIndex.length; i++) {
        var rawIndex = mainSeries.dataIndex[i];
        var dataItem = convertedData[0][rawIndex];
        var pmValue = dataItem.value[2];

        sum += pmValue;
        count++;

        selectedItems.push(dataItem);
    }

    selectedItems.sort(function (a, b) {
        return a.value[2] - b.value[2];
    });

    for (var i = 0; i < Math.min(selectedItems.length, maxBar) ; i++) {
        categoryData.push(selectedItems[i].name);
        barData.push(selectedItems[i].value[2]);
    }

    this.setOption({
        yAxis: {
            data: categoryData
        },
        xAxis: {
            axisLabel: { show: !!count }
        },
        title: {
            id: 'statistic'
        },
        series: {
            id: 'bar',
            data: barData
        }
    });
}


//yangmy 添加
function animatExtend2_1() {
    setTimeout(function () {
        $(".divText-keyword2-1").removeClass("pt-page-zoomIn");
        $(".divText-keyword2-1").addClass("pt-page-moveUpDown");
        $(".divText2-1").removeClass("pt-page-moveFromLeft");
        $("#div2-1").removeClass("pt-page-moveFromTop");
        removeAnimatRemark(".page-2-1")
    }, 2800)
}

//移除底部文字通用样式
function removeAnimatRemark(ele) {
   $(ele).find(".page-remark-left").removeClass("pt-page-moveFromBottom");
   $(ele).find(".page-remark-text").removeClass("pt-page-moveFromBottom");
   $(ele).find(".page-remark-rightIcon").removeClass("pt-page-moveFromRight");
}
var isNew3 = true;
function animatExtend3_1() {
    setTimeout(function () {
        if (isNew3) {
            $('#div3-1Span2').show();
            $('#div3-1Span2').countUp()
        }
     }, 1200)
    setTimeout(function () {
        if (isNew3) {
            $('#div3-1Span3').show();
            $('#div3-1Span3').countUp()
        }
    }, 1200)

    setTimeout(function () {
        $(".divText-text3").removeClass("pt-page-moveFromRight");
        $(".divText3").addClass("pt-page-moveUpDown");
        $(".divText-title3").removeClass("pt-page-moveFromLeft");
        $("#div3-1").removeClass("pt-page-moveFromTop");
        removeAnimatRemark(".page-3-1")
        isNew3 = false;
    }, 2800)
}

function animatExtend4_1() {
    setTimeout(function () {
        $(".divText-text4-1").removeClass("pt-page-rotateInDownLeft");
        $(".divText-text4-2").removeClass("pt-page-rotateInUpRight");
        $(".divText-text4-1").addClass("pt-page-moveUpDown");
        $(".divText-text4-2").addClass("pt-page-moveUpDown");
        $("#div4-1").removeClass("pt-page-moveFromTop");
        removeAnimatRemark(".page-2-1")
    }, 1900)
}


