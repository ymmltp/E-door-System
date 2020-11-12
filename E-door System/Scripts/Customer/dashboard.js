function Weekly_Conversation_Summarization() {
    getData("/Dashboard/Weekly_Conversation_Summarization", { d1: '2020-10-10', d2: '2020-11-11' })
        .then(res => {
            var category = [];
            var data = [];
            res.forEach(i => {
                category.push(i.senior_tab);
                data.push(i.QTY);
            })
            return ({ category: category, data: data });
        })
        .then(res => {
            var myChart1 = echarts.init(document.getElementById('Weekly_count'));
            var option = {
                title: {
                    text: 'Weekly Conversation Summarization',
                    left: 'center',
                    subtext: '每周总结'
                },
                color: ["#d87c7c", "#919e8b", "#d7ab82", "#6e7074", "#61a0a8", "#efa18d", "#787464", "#cc7e63", "#724e58", "#4b565b"],
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                    }
                },
                xAxis: {
                    type: 'category',
                    data: res.category,
                    axisLabel: {
                        formatter: function (value)   //竖排显示
                        {
                            return value.split(" ").join("\n");
                        }
                    }
                },
                yAxis: {
                    type: 'value'
                },
                series: [{
                    data: res.data,
                    type: 'bar',
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大值' },
                            { type: 'min', name: '最小值' }
                        ]
                    },
                }]
            };
            myChart1.setOption(option);
        })
        .catch(err => {
            alert(err);
        })
}

function Monthly_conversation_status_tracking() {
    getData("/Dashboard/Monthly_conversation_status_tracking", { d1: '2020-10-10', d2: '2020-11-11' })
        .then(res => {
            var category = [];
            res.forEach(i => {
                category.push(i.name);
            })
            return ({ category: category, data: res });
        })
        .then(res => {
            var myChart1 = echarts.init(document.getElementById('Monthly_count'));
            var option = {
                title: {
                    text: 'Monthly conversation status tracking',
                    left: 'center',
                    subtext: '每月会话状态追踪'
                },
                color: ["#d87c7c", "#919e8b", "#d7ab82", "#6e7074", "#61a0a8", "#efa18d", "#787464", "#cc7e63", "#724e58", "#4b565b"],
                tooltip: {
                    trigger: 'item',
                    formatter: '{a} <br/>{b} : {c} ({d}%)'
                },
                legend: {
                    orient: 'vertical',  //,horizontal 
                    left: 'right',
                    top: '50px',
                    data: res.category,
                },
                series: [{
                    type: 'pie',
                    name: "数量",
                    radius: '55%',
                    center: ['50%', '50%'],
                    data: res.data,
                    label: {
                        position: 'inner',
                        formatter: '{b}\n{d}%'
                    },
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }]
            };
            myChart1.setOption(option);
        })
        .catch(err => {
            alert(err);
        })
}

function Tabular_Matrix() {
    getData("/Dashboard/Tabular_Matrix", { d1: '2020-10-10', d2: '2020-11-11' })
        .then(res => {
            var tier = [];
            var tab = [];
            var dataMaxi = [];
            var item = [];
            res.forEach(i => {
                if (tier.indexOf(i.tier) == -1) tier.push(i.tier);
                if (tab.indexOf(i.senior_tab)==-1) tab.push(i.senior_tab);
            })
            res.forEach(i => {
                item.push(i.QTY == null ? 0 : i.QTY);
                if (item.length == tab.length) {
                    var series = {
                        name: i.tier,
                        type: "bar",
                        data: item,
                    };
                    dataMaxi.push(series);
                    item = [];
                } 
            })
            return ({ legend: tier, xAxis: tab, data: dataMaxi});
        })
        .then(res => {
            var myChart1 = echarts.init(document.getElementById('Tabular_Matrix'));
            var option = {
                title: {
                    text: 'Tabular Matrix',
                    left: 'left',
                    subtext: '各类数据汇总'
                },
                color: ["#d87c7c", "#919e8b", "#d7ab82", "#6e7074", "#61a0a8", "#efa18d", "#787464", "#cc7e63", "#724e58", "#4b565b"],
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                    }
                },
                toolbox: {
                    show: true,
                    feature: {
                        dataView:
                            {
                                show: true,
                                readOnly: false,
                                lang: ['Tabular Matrix', 'Close', 'Exit'],
                                optionToContent: function (opt) {
                                    var axisData = opt.xAxis[0].data;
                                    var series = opt.series;
                                    var tdHeads = '<td  style="padding:0 10px">Topic</td>';
                                    series.forEach(function (item) {
                                        tdHeads += '<td style="padding: 0 10px">' + item.name + '</td>';
                                    });
                                    var table = '<table id="dataview-table" border="1" style="margin-left:20px;border-collapse:collapse;font-size:14px;"><tbody><tr>' + tdHeads + '</tr>';
                                    var tdBodys = '';
                                    for (var i = 0, l = axisData.length; i < l; i++) {
                                        for (var j = 0; j < series.length; j++) {
                                            tdBodys += '<td style="text-align:center">' + series[j].data[i] + '</td>';
                                        }
                                        table += '<tr><td style="padding: 0 10px">' + axisData[i] + '</td>' + tdBodys + '</tr>';
                                        tdBodys = '';
                                    }
                                    table += '</tbody></table>';
                                    return table;
                                },
                                contentToOption: function (opt) {
                                    //toExcel('dataview-table', 'Technical Skill Matrix Summary')
                                }
                            },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                legend: {
                    orient: 'horizontal',  //,vertical
                    left: 'center',
                    top:'20px',
                    data: res.legend,
                },
                xAxis: {
                    type: 'category',
                    data: res.xAxis,
                    axisLabel: {
                        formatter: function (value)   //竖排显示
                        {
                            return value.split(" ").join("\n");
                        }
                    }
                },
                yAxis: {
                    type: 'value'
                },
                series: res.data,
                               
            };
            myChart1.setOption(option);
        })
        .catch(err => {
            alert(err);
        })
}

function Topic_Percent_of_Tier() {
    getData("/Dashboard/Tabular_Matrix", { d1: '2020-10-10', d2: '2020-11-11' })
        .then(res => {
            var tier = [];
            var tab = [];
            var dataMaxi = [];
            var series = [];
            res.forEach(i => {
                if (tier.indexOf(i.tier) == -1) tier.push(i.tier);
                if (tab.indexOf(i.senior_tab) == -1) tab.push(i.senior_tab);
            })
            var newArr = ["topic"].concat(tier);
            dataMaxi.push(newArr);
            tab.forEach(i => {
                var newArr = [i];
                dataMaxi.push(newArr);
            })
            res.forEach(i => {
                var m = tab.indexOf(i.senior_tab);
                var n = tier.indexOf(i.tier);
                dataMaxi[m + 1][n + 1] = i.QTY;
            })
            for (var i = 0; i < tier.length; i++) {
                var newArr = {
                    type: 'pie',
                    radius: [40, 80],
                    label: {
                        show: true,
                        position: 'center',
                        formatter: tier[i],
                        color:'black'
                    },
                    center: [(20 * i + 10).toString()+'%', '50%'],
                    encode: {
                        itemName: 'topic',
                        value: tier[i]
                    }
                }
                series.push(newArr);
            }
            return ({ dataset: dataMaxi, series: series });
        })
        .then(res => {
            var myChart1 = echarts.init(document.getElementById('Tab_Percent'));
            var option = {
                title: {
                    text: 'Topic Percent for Each Tier',
                    left: 'center',
                    subtext: '各话题占比分布'
                },
                color: ["#d87c7c", "#919e8b",  "#d7ab82", "#6e7074", "#61a0a8","#efa18d", "#787464", "#cc7e63",  "#724e58", "#4b565b"],
                //color: ['#919e8b', '#d7ab82', '#6e7074', '#61a0a8', '#efa18d', '#787464', '#cc7e63', '#724e58', '#4b565b', '#d87c7c'],
                //color: ['#9ad3bc','#f5b461', '#67e0e3', '#ee6f57', '#3797a4', '#799351', '#686d76'],
                legend: {
                    orient: 'horizontal',  //,horizontal 
                    left: 'center',
                    top: '50px',},
                tooltip: {  },
                dataset: {
                    source: res.dataset,
                },
                series: res.series,
            };
            myChart1.setOption(option);
        })
        .catch(err => {
            alert(err);
        })
}