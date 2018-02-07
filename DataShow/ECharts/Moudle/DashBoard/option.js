
function CreateDashBoard(divId, jsonData) {
    var defaultOption = {
        backgroundColor: "", //容器背景色，为空背景透明
        formatvalue: "%", //格式化显示配置,会加在数据显示后面
        color: "#b2975a", //仪表盘及标题显示颜色
        width: "5", //表盘弧形宽度
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
            title : {
                textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                    fontWeight: 'bolder',
                    fontSize: 20,
                    fontStyle: 'italic'
                }
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
            axisTick: {            // 坐标轴小标记
                length: 8,        // 属性length控制线长
                lineStyle: {       // 属性lineStyle控制线条样式
                    color: 'auto'
                }
            },
            splitLine: {           // 分隔线
                length: 14,         // 属性length控制线长
                lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                    color: 'auto'
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

