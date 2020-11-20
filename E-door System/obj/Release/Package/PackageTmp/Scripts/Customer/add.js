function add_department() {
    var para = $("#input-para").val();
    var url = "/Department/Create?ran=" + Math.random();
    $.ajax({
        url: url,
        type: "POST",
        data: {
            department1: para
        },
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            department_table();
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}

function add_project() {
    var para = $("#input-para").val();
    var url = "/Project/Create?ran=" + Math.random();
    $.ajax({
        url: url,
        type: "POST",
        data: {
            project1: para
        },
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            project_table();
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}

function add_location() {
    var para = $("#input-para").val();
    var url = "/Location/Create?ran=" + Math.random();
    $.ajax({
        url: url,
        type: "POST",
        data: {
            location1: para
        },
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            location_table();
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}

function add_user() {
    var useritem = {
        id: null,
        displayname: $("#displayname").val(),
        ntid: $("#ntid").val(),
        employeeNum: $("#employeeid").val(),
        tier: $("#tiersele").val(),
        department: $("#departsele").val(),
        project: $("#projsele").val(),
        phoneNum: $("#phone").val(),
        eMail: $("#email").val(),
        password: "123456",
        power: $("#powersele").val(),
    };
    $.ajax({
        url: "/User/Create?ran=" + Math.random(),
        type: "POST",
        data: useritem,
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            user_table();
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}

function add_first_tab() {
    var tab1 = $("#tab1sele_sele").val();
    if (tab1 != "") {
        var item = {
            tab_type: tab1.substring(0, tab1.indexOf("(")),
            description: tab1.substring(tab1.indexOf("(")+1, tab1.indexOf(")"))
        }
        $.ajax({
            url: "/Tab/First_Tab_Create",
            type: "POST",
            data: item,
            success: function (data, status, xhr) {
                console.log(xhr.status + ":" + xhr.statusText);
                add_second_tab();
            },
            fail: function (err, status, xhr) {
                console.log(xhr.status + ":" + xhr.statusText);
            }
        })
    }
}
function add_second_tab() {
    var tab1 = $("#tab1sele_sele").val();
    var tab2 = $("#tab2sele_sele").val();
    if (tab2 !== "") {
        var item = {
            first_type: tab1.substring(0, tab1.indexOf("(")),
            second_type: tab2.substring(0, tab2.indexOf("(")),
            description: tab2.substring(tab2.indexOf("(")+1, tab2.indexOf(")"))
        }
        $.ajax({
            url: "/Tab/Second_Tab_Create",
            type: "POST",
            data: item,
            success: function (data, status, xhr) {
                console.log(xhr.status + ":" + xhr.statusText);
                tab_table();
            },
            fail: function (err, status, xhr) {
                console.log(xhr.status + ":" + xhr.statusText);
            }
        })
    }
}