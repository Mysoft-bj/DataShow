var jsonAll;
var data = '{"SaleRanking":[{"name":"广渠金茂府","value":1880},{"name":"金茂珑悦-一期","value":1047},{"name":"金茂梅溪湖-一期","value":838}],"DiffAgeBuy":[{"name":"30后","value":37},{"name":"40后","value":111},{"name":"50后","value":418},{"name":"60后","value":1200},{"name":"70后","value":1945},{"name":"80后","value":2069},{"name":"90后","value":391},{"name":"00后","value":77},{"name":"10后","value":17}],"TwoDiffAge":[{"name":"eldest","value":4},{"name":"oldest","value":106}],"MostCstGjjl":[{"name":"方兴北京","value":48},{"name":"方兴丽江","value":36},{"name":"方兴丽江","value":28},{"name":"方兴丽江","value":26},{"name":"方兴丽江","value":21}],"DiffProductCnt":[{"name":"住宅别墅","value":43},{"name":"别墅","value":60},{"name":"商业别墅","value":16},{"name":"住宅","value":4062},{"name":"酒店式公寓","value":1728}],"MaxProductCnt":[{"name":"住宅别墅","max":5000},{"name":"别墅","max":5000},{"name":"商业别墅","max":5000},{"name":"住宅","max":5000},{"name":"酒店式公寓","max":5000}],"MediaOppCnt":[{"name":"户外广告","value":19116},{"name":"报纸","value":5556},{"name":"电视","value":3502},{"name":"活动展会","value":2436},{"name":"网络","value":2095}],"DiffMonthDealCnt":[{"name":"1月","value":378},{"name":"2月","value":141},{"name":"3月","value":424},{"name":"4月","value":745},{"name":"5月","value":744},{"name":"6月","value":850},{"name":"7月","value":192},{"name":"8月","value":120},{"name":"9月","value":276},{"name":"10月","value":315},{"name":"11月","value":1089},{"name":"12月","value":635}],"RemovalRate":[{"name":"","value":54.18}],"AchievedProjectCount":[{"name":"北京市","value":3},{"name":"长沙市","value":3},{"name":"朝阳市","value":3},{"name":"海口市","value":1},{"name":"丽江市","value":4},{"name":"南京市","value":2},{"name":"青岛市","value":1},{"name":"上海市","value":6},{"name":"重庆市","value":2}],"Constellations":[{"name":"天秤座","value":605},{"name":"处女座","value":546},{"name":"狮子座","value":554},{"name":"双子座","value":472},{"name":"射手座","value":597},{"name":"白羊座","value":505},{"name":"水瓶座","value":534},{"name":"巨蟹座","value":507},{"name":"魔羯座","value":592},{"name":"天蝎座","value":643},{"name":"双鱼座","value":526},{"name":"金牛座","value":435}],"AvgWomenPercentage":[{"name":"other","value":56.85},{"name":"平均","value":43.15}],"MostWomenPercentage":[{"name":"other","value":28.33},{"name":"上海","value":71.67}],"LeastWomenPercentage":[{"name":"other","value":86.64},{"name":"贵阳","value":13.36}]}'
function GetJsonAll() {
    if (jsonAll == undefined) {
        // htmlobj = $.ajax({ url: "data/data.txt", async: false });
        jsonAll = JSON.parse(data);
    }
}

function div2_1Load() {
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
    //GetJsonAll();
    //var jsonName;
    //var jsonValue;
    //var json = jsonAll.AchievedProjectCount;
    //jsonName = my.getJsonNameByJosn(json);
    //jsonValue = my.getJsonValueByJosn(json);
    //drawDiv2_1Pic(json);
   
    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/ChinaMap/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    setDiv2_1Remark(json.ProjMap.data);
    CreateProjMap("div2-1", json.ProjMap);
}

function CreateProjMap(divId, jsonData) {
    var myChart = echarts.init(document.getElementById(divId));
    var data = jsonData.data;
    var center = jsonData.center;
    //后台需封装算法，用于获取城市经纬度
    var geoCoordMap = {
        '北京': [116.46, 39.92],
        '武汉': [114.31, 30.52],
        '合肥': [117.27, 31.86],
        '郑州': [113.65, 34.76]
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
        var arrColor = ['#ff3333', 'orange', 'yellow', 'lime', 'aqua'];
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
            text: '全国落地城市',
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
                    areaColor: '#323c48',
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
function setDiv2_1Remark(json) {
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
    $("#div2-1Span1").html(info);
    setTimeout(function () {
        $('.counter').countUp()
        , 2500
    }
)
}
//预设的城市坐标

// 基于准备好的dom，初始化echarts实例
function div13_1Load() {
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
    drawDiv13_1Pic(json, jsonName, jsonValue);
    setDiv13_1Remark(json);
}

function drawDiv13_1Pic(json, jsonName, jsonValue) {
    var myChart = echarts.init(document.getElementById('div13-1'));
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

function setDiv13_1Remark(json) {
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
    $("#div13-1Span1").html(info);
}


// 基于准备好的dom，初始化echarts实例
function div3_1Load() {
  
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.DiffAgeBuy;

   
    //jsonName = my.getJsonNameByJosn(json);
    //jsonValue = my.getJsonValueByJosn(json);
    //drawDiv3_1Pic(json, jsonName, jsonValue);
    setDiv3_1Remark(json);
    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/AgeSpread/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    CreateAgeSpread("div3-1", json.AgeSpread);
}
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
    //drawDiv4_1Pic(json, jsonName, jsonValue);
    setDiv4_1Remark(json);

    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/StarSign/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    CreateStarSign("div4-1", json.StarSign);
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
  
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    var jsonMost = jsonAll.MostWomenPercentage;
    var jsonLeast = jsonAll.LeastWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    //drawDiv5_1Pic(json, jsonName, jsonValue, jsonMost, jsonLeast);
    //setDiv5_1Remark(json, jsonMost, jsonLeast);

    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/SingleBuyer/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
     
    CreateSingleBuyer("div5-1-1", json.SingleBuyer);
    CreateBuyerTrend("div5-1-2", json.SingleBuyer);
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
                            formatter: '\n' + rateMale + '%',
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
            text: '单人购房占比走势(%)',
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
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    var jsonMost = jsonAll.MostWomenPercentage;
    var jsonLeast = jsonAll.LeastWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    //drawDiv6_1Pic(json, jsonName, jsonValue, jsonMost, jsonLeast);
    setDiv6_1Remark(json, jsonMost, jsonLeast);

    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/AgeTakeTime/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    CreateAgeTakeTime("div6-1", json.AgeTakeTime);
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
    var info = "<span class = 'key_numb counter'>0.1</span><span class = 'key_unit'>月</span>";
    $("#div6-1Span1").html(info);
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
                color: '#fff',
                fontSize: 16
            }
        },
        tooltip: {
            position: ['50%', '30%'],
            show: true
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


// 基于准备好的dom，初始化echarts实例
function div7_1Load() {
    var htmlobj1 = $.ajax({ url: "../../../../ECharts/Moudle/DashBoard/data1.txt", async: false });
    var json1 = JSON.parse(htmlobj1.responseText);
    var htmlobj2 = $.ajax({ url: "../../../../ECharts/Moudle/DashBoard/data2.txt", async: false });
    var json2 = JSON.parse(htmlobj2.responseText);
    var htmlobj3 = $.ajax({ url: "../../../../ECharts/Moudle/DashBoard/data3.txt", async: false });
    var json3 = JSON.parse(htmlobj3.responseText);
    CreateDashBoard("div7-1-1", json1.dashboard);
    CreateDashBoard("div7-1-2", json2.dashboard);
  
   
}

function CreateDashBoard(divId, jsonData) {
    var defaultOption = {
        backgroundColor: "", //容器背景色，为空背景透明
        formatvalue: "%", //格式化显示配置,会加在数据显示后面
        color: "#b2975a", //仪表盘及标题显示颜色
        width: "10", //表盘弧形宽度
        datavale: "50", //数据值
        dataname: "XX"//显示名称
    }
    var newData = $.extend({}, defaultOption, jsonData)
    var option = {
        backgroundColor: newData.backgroundColor,
        series: [{
            name: '',
            type: 'gauge',
            detail: {
                formatter: '{value}' + newData.formatvalue
            },
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
            data: [{
                value: newData.datavale,
                name: newData.dataname,
            }]

        }]
    };
    var myChart = echarts.init(document.getElementById(divId));
    myChart.setOption(option);
}



// 基于准备好的dom，初始化echarts实例
function div8_1Load() {
   
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.AvgWomenPercentage;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
  

    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/BarAndLineChart_2/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    CreateBarAndLineChar_1("div8-1", json.barandlinechart_2);
    setDiv8_1Remark(json);
}


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
            show: false,
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
            show: false,
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
    
    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.DiffProductCnt;
    var jsonMax = jsonAll.MaxProductCnt;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    //drawDiv9_1Pic(json, jsonName, jsonValue, jsonMax);
    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/LandscapeBarChart/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    CreateBarAndLineChar_2("div9-1", json.landscapebarchart);
    setDiv9_1Remark(json);
    //提前加载下一个引用js
    $.getScript("../../../../ECharts/Moudle/HotWordChart/echarts-wordcloud.min.js", function () {  //加载test.js,成功后，并执行回调函数
        console.log("加载js文件");
    });

}

function CreateBarAndLineChar_2(divId, jsonData) {
    var defaultOption = {
        backgroundColor: ""//容器背景色，为空背景透明

    }
    var newData = $.extend({}, defaultOption, jsonData);
    var dataName = [];
    var dataA = [];
    var dataB = [];
    var data = newData.data;
    for (var i = 0; i < data.length; i++) {
        dataName.push(data[i].name);
        dataA.push(data[i].value1);
        dataB.push(data[i].value2)
    }
    var option = {
        backgroundColor: '#0E2A43',
        legend: {
            show: false,
            bottom: 20,
            textStyle: {
                color: '#fff',
            },
            data: ['异地购房数量', '总购房数量']
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '10%',
            containLabel: true
        },

        tooltip: {
            show: "true",
            trigger: 'axis',
            axisPointer: { // 坐标轴指示器，坐标轴触发有效
                type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
            }
        },
        xAxis: {
            type: 'value',
            axisTick: { show: false },
            axisLine: {
                show: false,
                lineStyle: {
                    color: '#fff',
                }
            },
            splitLine: {
                show: false
            },
        },
        yAxis: [
                {
                    type: 'category',
                    axisTick: { show: true },
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: '#fff',
                        }
                    },
                    data: dataName
                },
                {
                    type: 'category',
                    axisLine: { show: false },
                    axisTick: { show: false },
                    axisLabel: { show: false },
                    splitArea: { show: false },
                    splitLine: { show: false },
                    data: dataName
                },

        ],
        series: [
            {
                name: '总购房数量',
                type: 'bar',
                yAxisIndex: 1,
                itemStyle: {
                    normal: {
                        show: true,
                        color: '#277ace',
                        barBorderRadius: 0,
                        borderWidth: 0,
                        borderColor: '#333',
                    }
                },
                barGap: '0%',
                barCategoryGap: '80%',
                data: dataB
            },
            {
                name: '异地购房数量',
                type: 'bar',
                itemStyle: {
                    normal: {
                        show: true,
                        color: '#5de3e1',
                        barBorderRadius: 0,
                        borderWidth: 0,
                        borderColor: '#333',
                    }
                },
                barGap: '0%',
                barCategoryGap: '80%',
                data: dataA
            }

        ]
    };;
    var myChart = echarts.init(document.getElementById(divId));
    myChart.setOption(option);
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

    GetJsonAll();
    var jsonName;
    var jsonValue;
    var json = jsonAll.MediaOppCnt;
    jsonName = my.getJsonNameByJosn(json);
    jsonValue = my.getJsonValueByJosn(json);
    //drawDiv10_1Pic(json, jsonName, jsonValue);
    setDiv10_1Remark(json);
    var htmlobj = $.ajax({ url: "../../../../ECharts/Moudle/HotWordChart/data.txt", async: false });
    var json = JSON.parse(htmlobj.responseText);
    CreateHotWordChart("div10-1", json.hotwordchart);
}


function CreateHotWordChart(divId, jsonData) {
    var defaultOption = {
        backgroundColor: ""//容器背景色，为空背景透明

    }
    var newData = $.extend({}, defaultOption, jsonData);
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
        opacity:'0.7',
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


