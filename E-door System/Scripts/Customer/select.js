//Select
function select_department() {
    $.ajax({
        url: "/Department/Query?ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = "";
            res.forEach(i => {
                option += '<option value="' + i.department1 + '">' + i.department1 + '</option>';
            })
            $("#departsele").html("");
            $("#departsele").html(option);
            $("#departsele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_project() {
    $.ajax({
        url: "/Project/Query?ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = "";
            res.forEach(i => {
                option += "<option value=\"" + i.project1 + "\">" + i.project1 + "</option>";
            })
            $("#projsele").html("");
            $("#projsele").html(option);
            $("#projsele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_location() {
    $.ajax({
        url: "/Location/Query?ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = "";
            res.forEach(i => {
                option += "<option value=\"" + i.location1 + "\">" + i.location1 + "</option>";
            })
            $("#locationsele").html("");
            $("#locationsele").html(option);
            $("#locationsele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_power() {
    $.ajax({
        url: "/Power/Query?ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = "";
            res.forEach(i => {
                option += "<option value=\"" + i.type + "\">" + i.type + "</option>";
            })
            $("#powersele").html("");
            $("#powersele").html(option);
            $("#powersele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_tier() {
    $.ajax({
        url: "/Tier/Query?ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        success: function (res) {
            var option = "";
            res.forEach(i => {
                option += "<option value=\"" + i.tier_level + "\">" + i.tier_level + "</option>";
            })
            $("#tiersele").html("");
            $("#tiersele").html(option);
            $("#tiersele").selectpicker('refresh');
        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_first_tab() {
    $.ajax({
        url: "/Tab/First_Tab_Query?ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        async: false,
        success: function (res) {
            var option = "";
            res.forEach(i => {
                option += '<option value="' + i.tab_type + '" data-desc="' + i.description + '">' + i.tab_type + '(' + i.description +')' + '</option>';
            })
            $("#tab1sele").html("");
            $("#tab1sele").html(option);
            $('#tab1sele').editableSelect({
                effects: 'slide',
                items_then_scroll: 6,
                isfilter: true,
                onSelect: function () {
                    select_second_tab();
                }
            }); 

        },
        fail: function (err) {
            console.log(err);
        }
    })
}
function select_second_tab() {
    var item = {
        first_type: $("#tab1sele").val(),
        second_type: null,
        description: null,
    }
    $.ajax({
        url: "/Tab/Second_Tab_Query_byRange",
        type: "GET",
        async: false,
        data:item,
        dataType: "json",
        success: function (res) {
            var option = '<label for="tab2sele" class="form-label text-right">Second Type:</label><select id = "tab2sele" class="form-control" > ';
            res.forEach(i => {
                option += '<option value="' + i.second_type + '" data-desc="' + i.description + '">' + i.second_type + '(' + i.description + ')'+ '</option>';
            })
            option += '</select >';
            $("#select2").html("");
            $("#select2").html(option);
            $('#tab2sele').editableSelect({
                bg_iframe: false,
                items_then_scroll: 6,
                effects: 'slide',
                isFilter: true,
            }); 
        },
        fail: function (err) {
            console.log(err);
        }
    })
}


//Get Table
function project_table() {
    var url = "/Project/Query?ran=" + Math.random();
    $('#table').bootstrapTable('refresh', { 'url': url });
}
function department_table() {
    var url = "/Department/Query?ran=" + Math.random();
    $('#table').bootstrapTable('refresh', { 'url': url });
}
function location_table() {
    var url = "/Location/Query?ran=" + Math.random();
    $('#table').bootstrapTable('refresh', { 'url': url });
}
function user_table() {
    var url = "/User/QuerybyRange?ran=" + Math.random();
    $('#table').bootstrapTable('refresh', { 'url': url });
}
function user_QuerybyRange() {
    var opt = {
        url: "/User/QuerybyRange",
        silent: true,
        query: {
            displayname: $("#displayname").val(),
            ntid: $("#ntid").val(),
            employeeNum: $("#employeeid").val(),
            tier: $("#tiersele").val()[0],
            department: $("#departsele").val()[0],
            project: $("#projsele").val()[0],
            power: $("#powersele").val()[0],              
        } 
    };
    $('#table').bootstrapTable('refresh', opt);
}
function tab_table() {
    var url = '/Tab/First_Tab_Query?ran = ' + Math.random()
    $('#table').bootstrapTable('refresh', { 'url': url });
}

//Get User Entity
function user_entity() {
    var ntid = $("#ntid").val();
    $.ajax({
        url: "/User/GetUserEntity?ntid="+ ntid +"&ran=" + Math.random(),
        type: "GET",
        dataType: "json",
        success: function (data) {
            $("#displayname").val(data.DisplayName);
            $("#email").val(data.Email);
        },
        fail: function (err) {
            console.log(err)
        }
    })
}