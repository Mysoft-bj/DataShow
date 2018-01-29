//var jsonAll;
//function GetJsonAll(url) {
//    if (jsonAll == undefined) {
//        htmlobj = $.ajax({ url: url, async: false });
//        jsonAll = JSON.parse(htmlobj.responseText);
//    }
//}
//// 基于准备好的dom，初始化echarts实例
//function div2_1Load(divId) {
//    var jsonName;
//    var jsonValue;
//    var json;
//    //$.post("/Charts/GetSaleRanking", "", function (result) {
//    //    json = eval('(' + result + ')');
//    //    jsonName = my.getJsonName(result);
//    //    jsonValue = my.getJsonValue(result);
//    //    drawDiv1Pic(json, jsonName, jsonValue);
//    //    setDiv1Remark(json);
//    //});

//<<<<<<< HEAD:DataShow/EChars/Moudle/ChinaMap/option.js
//    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
//    //var jsonAll = eval('(' + htmlobj.responseText + ')');
//    GetJsonAll("/EChars/Moudle/ChinaMap/data.txt");
//    var jsonName;
//    var jsonValue;
//    var json = jsonAll.ProjMap.data;
//    jsonName = my.getJsonNameByJosn(json);
//    jsonValue = my.getJsonValueByJosn(json);
//    drawDiv2_1Pic(divId,json, jsonName, jsonValue,"郑州");
//    //setDiv2_1Remark(json);
//}
//=======
    //htmlobj = $.ajax({ url: "/data/data.txt", async: false });
    //var jsonAll = eval('(' + htmlobj.responseText + ')');
//    GetJsonAll("/EChars/Moudle/ChinaMap/data.json");
//    var jsonName;
//    var jsonValue;
//    var json = jsonAll.ProjMap;
//    jsonName = my.getJsonNameByJosn(json);
//    jsonValue = my.getJsonValueByJosn(json);
//    drawDiv2_1Pic(divId,json, jsonName, jsonValue);
//    //setDiv2_1Remark(json);
//}
//>>>>>>> 1609a569bc18999394b8b7408def67114b1da91e:DataShow/ECharts/Moudle/ChinaMap/option.js

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
    var CenterData = function (data,center) {
        var res = [];
        var resChild1 = [];
        resChild1.push({
            name: center
        });
        for (var i = 0; i < data.length; i++) {
            if (data[i].name == center) { continue;}
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
            zoom:1.2
        },
        series: [
        {
            name: '',
            type: 'effectScatter',
            coordinateSystem: 'geo',
            data: convertData(data),
            symbol:'circle',
            symbolSize: function (val) {
                return 0.5;
            },
            showEffectOn: 'render',
            rippleEffect: {
                brushType: 'stroke',
                scale: 35,
                period:5
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
                    color:function(val){
                        return rangeColor(data,val.name);
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
                color:function(val){
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