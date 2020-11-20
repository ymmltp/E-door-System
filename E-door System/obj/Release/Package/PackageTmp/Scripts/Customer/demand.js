var demand = {};
var isSemdMail = true;
var eMail = getCookie("edoor-eMail")


//Demand select
function select_mailandUserName() {
    $.ajax({
        url: "/User/Query",
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = '';
            res.forEach(i => {
                option += '<option value="' + i.employeeNum + '">' + i.displayname + '</option>';
            })
            $("#from").html("");
            $("#from").html(option);
            $("#from").selectpicker('refresh');
            $("#to").html("");
            $("#to").html(option);
            $("#to").selectpicker('refresh');
            $("#cc").html("");
            $("#cc").html(option);
            $("#cc").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_UserNamebyPower() {
    var power = getCookie("edoor-Power");
    var epNum = null;
    if (power < 7 ) {
        epNum = getCookie("edoor-EmployeeID");
    }
    $.ajax({
        url: "/User/QuerybyRange",
        type: "GET",
        data: {
            employeeNum: epNum,
        },
        dataType: "json",
        success: function (res) {
            var option = '';
            res.forEach(i => {
                option += '<option value="' + i.employeeNum + '">' + i.displayname + '</option>';
            })
            $("#from").html("");
            $("#from").html(option);
            $("#from").selectpicker('refresh');
            $("#to").html("");
            $("#to").html(option);
            $("#to").selectpicker('refresh');
            $("#cc").html("");
            $("#cc").html(option);
            $("#cc").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_tb1() {
    $.ajax({
        url: "/Tab/First_Tab_Query",
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = '';
            res.forEach(i => {
                option += '<option value="' + i.tab_type + '" data-desc="' + i.description + '">' + i.tab_type + '(' + i.description + ')' + '</option>';
            })
            $("#tab1sele").html("");
            $("#tab1sele").html(option);
            $("#tab1sele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_tb2() {
    var item = {
        first_type: $("#tab1sele").val()[0],
        second_type: null,
        description: null,
    }
    $.ajax({
        url: "/Tab/Second_Tab_Query_byRange",
        type: "GET",
        data: item,
        dataType: "json",
        success: function (res) {
            var option = '';
            res.forEach(i => {
                option += '<option value="' + i.second_type + '" data-desc="' + i.description + '">' + i.second_type + '(' + i.description + ')' + '</option>';
            })
            $("#tab2sele").html("");
            $("#tab2sele").html(option);
            $("#tab2sele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function demand_list(items) {
    var option = '<ul>';
    items.forEach(i => {
        switch (i.status) {
            case 'open':
                option += '<li class="sheet-card card-open" data-id="' + i.id + '">';
                break;
            case 'ongoing':
                option += '<li class="sheet-card card-ongoing" data-id="' + i.id + '">';
                break;
            default:
                break;
        }
        var d = new Date(parseInt(i.session_time.replace("/Date(", "").replace(")/", ""), 10));
        option += '<label class="item-title">Topic: <span class="item-text-blue" > ' + i.senior_tab + '</span ></label >'
            + '<label class="item-title">Session Time:<br /> <span class="item-text-blue">' + formatDate(d) + '</span> </label>'
            + ' <label class="item-title">Location:<br /> <span class="item-text-blue">' + i.session_location + '</span></label>';
        option += '</li>';
    })
    option += '</ul>';
    return option;
}


//Apply Demand
function showModel() {
    $("#DemandModel").modal("show");
}
function ini_Apply_Demand() {

}
function init_calendar() {
   
}
function init_demand_list() {
    $.ajax({
        url: '/Demand/QueryDemandbyRange',
        data: {
            from: eMail,
        },
        type:'GET',
        dataType: 'JSON',
        success: function (data, status, xhr) {
            var obj = $("#demand-list");
            var datahtml = demand_list(data);
            obj.html(datahtml);
        },
        fail: function () {
        }
    })
}
function submitDemand() {
    var demanditem = {
        from: $("#from").val(),
        senior_tab: $("#tab1sele").val()[0],
        open_time: formatDate(new Date()),
        tab: $("#tab2sele").val()[0],
        content: $("#content").val(),
        status: "open",
        to: $("#to").val(),
        //cc: $("#cc").val() === null ? null : $("#cc").val().join(';')
    };
    $.ajax({
        url: "/Demand/ApplyDemand",
        type: "POST",
        data: demanditem,
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            alert("Add Success");
            $("#from").val("");
            $("#from").selectpicker('refresh');
            $("#tab1sele").val("");
            $("#tab1sele").selectpicker('refresh');
            $("#tab2sele").val("");
            $("#tab2sele").selectpicker('refresh');
            $("#content").val("");
            $("#to").val("");
            $("#to").selectpicker('refresh');
            //$("#cc").val("");
            //$("#cc").selectpicker('refresh');
            $("#DemandModel").modal("hide");
            $("#table").bootstrapTable('refresh');

            if ($("#isSendMail").is(":checked")) {
                $.ajax({
                    url: '/SendMail/SendApplyEmail',
                    type: "POST",
                    data: demanditem,
                    success: function (data, status, xhr) {
                        console.log(xhr.status + ":" + xhr.statusText);
                    },
                    fail: function (err, status, xhr) {
                        console.log(xhr.status + ":" + xhr.statusText);
                    }
                });
            }
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })

}


//Arrange Demand
//加载demand
function demand_byID(row) {
    $.ajax({
        url: '/Demand/QuerybyID',
        type: 'GET',
        data: { id: row.id },
        dataType: 'JSON',
        success: function (data, status, xhr) {
            demand = data;
            //var tolist = data.mailTrom.split(';');
            //$('#to').selectpicker('val', tolist);
            //$('#to').selectpicker('refresh');
            //var cclist = data.cc == null ? null : data.cc.split(';');
            //$('#cc').selectpicker('val', cclist);
            //$('#cc').selectpicker('refresh');
            console.log(xhr.status + ":" + xhr.statusText);
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}
//检查时间是否被占用
function checkTimeUsed() {
    var checklist = {
        to: demand.to,
        session_time: $("#charttime").val()
    };
    $.ajax({
        url: '/Demand/QueryIsTimeOK',
        type: 'GET',
        data: checklist,
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            if (data.length > 0) {
                alert("Time is used, be sure you want to arrange this session in this time.(时间已被占用)");
            }
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
} 
//上传数据
function submitArrange() {
    //demand['to'] = $("#to").val().join(';');
    //demand['cc'] = $("#cc").val().join(';');
    demand['status'] = 'ongoing';
    demand['session_time'] = $("#charttime").val();
    demand['session_duration'] = $("#chartduration").val();
    demand['session_location'] = $("#locationsele").val();
    var d = new Date(parseInt(demand['open_time'].replace("/Date(", "").replace(")/", ""), 10));
    demand['open_time'] = formatDate(d);
    $.ajax({
        url: '/Demand/Edit',
        type: 'POST',
        data: demand,
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            $("#DemandModel").modal('hide');
            $("#table").bootstrapTable('refresh');
            if ($("#isSendMail").is(":checked")) {
                $.ajax({
                    url: '/SendMail/SendArrangeEmail',
                    type: "POST",
                    data: demand,
                    success: function (data, status, xhr) {
                        console.log(xhr.status + ":" + xhr.statusText);
                    },
                    fail: function (err, status, xhr) {
                        console.log(xhr.status + ":" + xhr.statusText);
                    }
                });
            }
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })

}
//Close Demand
function closeDemand(row) {
    demand['remark'] = $("#remark").val();
    demand['status'] = 'close';
    demand['close_time'] = formatDate(new Date());
    var d = new Date(parseInt(demand['open_time'].replace("/Date(", "").replace(")/", ""), 10));
    demand['open_time'] = formatDate(d);
    var dt = new Date(parseInt(demand['session_time'].replace("/Date(", "").replace(")/", ""), 10));
    demand['session_time'] = formatDate(dt);
    $.ajax({
        url: '/Demand/Edit',
        type: 'POST',
        data: demand,
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            $("#CloseDemandModel").modal('hide');
            $("#table").bootstrapTable('refresh');
            $.ajax({
                url: '/SendMail/SendCloseEmail',
                type: "POST",
                data: demand,
                success: function (data, status, xhr) {
                    console.log(xhr.status + ":" + xhr.statusText);
                },
                fail: function (err, status, xhr) {
                    console.log(xhr.status + ":" + xhr.statusText);
                }
            });
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}



//Search Demand
function init_searchDemand() {
    $("#table").bootstrapTable('destroy').bootstrapTable({
        cache: false,
        url:'/Demand/QueryDemandbyRange',
        type: 'GET',
        queryParams: {
            from: $("#from").val()[0],
            to: $("#to").val()[0],
            senior_tab: $("#tab1sele").val()[0],
            tab: $("#tab2sele").val()[0],
            status: $("#status").val()[0]
        },
        dataType: 'json',
        pagination: true,  //设置为 true 会在表格底部显示分页条。
        paginationLoop: false, //设置为 true 启用分页条无限循环的功能。
        pageSize: 10,//每页初始显示的条数
        pageList: [10, 15, 20],
        //showLoading: false,
        //formatLoadingMessage: function () { return ""; }, //自定义加载语句
        //showColumns: true,           
        columns: [{
            field: 'mailFrom',
            title: 'From'
        }, {
            field: 'open_time',
            title: 'Open Time',
            formatter: function (value, row, index) {
                return changeDateFormat(value);
            }
        }, {
            field: 'senior_tab',
            title: 'Topic'
        }, {
            field: 'tab',
            title: 'Tab'
        }, {
            field: 'content',
            title: 'Content'
        }, {
            field: 'mailTo',
            title: 'TO'
        }, {
            field: 'session_time',
            title: 'Session Time',
            formatter: function (value, row, index) {
                return changeDateFormat(value);
            }
        }, {
            field: 'session_location',
            title: 'Session Location'
        }, {
            field: 'remark',
            title: 'Remark'
        }, {
            field: 'status',
            title: 'Status'
        }]
    });

    function changeDateFormat(cellval) {
        if (cellval != null) {
            var d = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
            return formatDate(d);
        } else {
            return cellval;
        }
    }
}


