var wb;
var uploadArray = [];

$("#showmodel").click(function () {
    $("#UploadModal").modal("show");
});

function handleFiles(files) {
    var fileData = files[0]
    $("#filepath").val(fileData.name);
    var reader = new FileReader();
    var strArr = [];
    var rABS = false;
    var that = this;
    reader.onload = function (res) {
        var data = res.content;
        if (rABS) {
            wb = XLSX.read(btoa(this.fixdata(data)), { //手动转化
                type: 'base64'
            });
        } else {
            wb = XLSX.read(data, {
                type: 'binary'
            });
        }
        var option = "";
        for (var i = 0; i < wb.SheetNames.length; i++) {
            option += "<option value=\"" + wb.SheetNames[i] + "\">" + wb.SheetNames[i] + "</option>";
        }
        $("#sheetsele").html("");
        $("#sheetsele").html(option);
        $("#sheetsele").selectpicker('refresh');
    };
    if (rABS) {
        reader.readAsArrayBuffer(fileData);
    } else {
        reader.readAsBinaryString(fileData);
    }
}
//extend FileReader
FileReader.prototype.readAsBinaryString = function (fileData) {
    var binary = "";
    var pt = this;
    var reader = new FileReader();
    reader.onload = function (e) {
        var bytes = new Uint8Array(reader.result);
        var length = bytes.byteLength;
        for (var i = 0; i < length; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        pt.content = binary;
        pt.onload(pt);
    }
    reader.readAsArrayBuffer(fileData);
}

function ShowUserSheet() {
    var sheetName = $("#sheetsele").val();
    var worksheet = wb.Sheets[sheetName];
    var tempArr = XLSX.utils.sheet_to_json(worksheet);  //dictionary类
    var option = "";
    for (var i in tempArr) {
        option += "<tr><td id=\" tr" + i + "index\" >" + tempArr[i].ID + "</td>";
        option += "<td id=\" tr" + i + "ntid\" >" + tempArr[i].ntid + "</td>";
        option += "<td id=\" tr" + i + "employeeID\" >" + tempArr[i].employeeNum + "</td>";
        option += "<td id=\" tr" + i + "username\" >" + tempArr[i].displayname + "</td>";
        option += "<td id=\" tr" + i + "tier\" >" + tempArr[i].tier + "</td>";
        option += "<td id=\" tr" + i + "department\" >" + tempArr[i].department + "</td>";
        option += "<td id=\" tr" + i + "project\" >" + tempArr[i].project + "</td>";
        option += "<td id=\" tr" + i + "phone\" >" + tempArr[i].phoneNum + "</td>";
        option += "<td id=\" tr" + i + "mail\" >" + tempArr[i].eMail + "</td>";
        option += "<td id=\" tr" + i + "role\" >" + tempArr[i].power + "</td></tr>";
        var user = {
            id: null,
            displayname: tempArr[i].displayname,
            ntid: tempArr[i].ntid,
            employeeNum: tempArr[i].employeeNum,
            tier: tempArr[i].tier,
            department: tempArr[i].department,
            project: tempArr[i].project,
            phoneNum: tempArr[i].phoneNum,
            eMail: tempArr[i].eMail,
            password: "123456",
            power: tempArr[i].power,
        };
        uploadArray.push(user);
    }
    $("#user-upload-table-tr").html("");
    $("#user-upload-table-tr").html(option);
}
function UploadUser() {
    $.ajax({
        url: "/User/CreateLists",
        type: "post",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(uploadArray), 
        success: function (data, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
            $("#UploadModal").modal("hide");
            user_table();
        },
        fail: function (err, status, xhr) {
            console.log(xhr.status + ":" + xhr.statusText);
        }
    })
}

