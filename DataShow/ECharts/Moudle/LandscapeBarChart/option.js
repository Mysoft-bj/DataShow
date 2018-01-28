
function CreateBarAndLineChar_1(divId, jsonData) {
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
            show:false,
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








