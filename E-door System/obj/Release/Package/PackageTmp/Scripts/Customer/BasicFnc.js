//Promise Function
function getData(url,para=null) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: url,
            data: para,
            type: "GET",
            dataType: "json",
            success: function (data) {
                resolve(data);
            },
            fail:function(err) {
                reject(err);
            },
        });
    });
}

function postData(url, para) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: url,
            type: "POST",
            data: para,
            success(data, status, xhr) {
                resolve({ data: data, status: status, xhr: xhr });
            },
            fail(err) {
                reject({ err: err, status: status, xhr: xhr });
            },
        });
    });
}

function deleteData(url, para = null) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: url,
            type: "GET",
            data: para,
            success(data, status, xhr) {
                resolve({ data: data, status: status, xhr: xhr });
            },
            fail(err, status, xhr) {
                reject({ err: err, status: status, xhr: xhr });
            },
        });
    });
}


//format date
function formatDate(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    m = m < 10 ? ('0' + m) : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;
    var h = date.getHours();
    var minute = date.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;
    var second = date.getSeconds();
    second = minute < 10 ? ('0' + second) : second;
    return y + '-' + m + '-' + d + ' ' + h + ':' + minute + ':' + second;
}

