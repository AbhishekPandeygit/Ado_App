$(document).ready(function () {

    //jQuery.ajaxSetup({ async: false });

    getCountries()

    getCustomers()

    $("#btnSubmit").click(function () {
        AddCustomer()
    })
    $("#ddlCountry").change(function () {

        emptyDropdown("ddlState")
        emptyDropdown("ddlCity")
        var cid = $("#ddlCountry").val();
        getState(cid)

    })
    $("#ddlState").change(function () {
        var cid = $("#ddlState").val();
        getCity(cid)

    })

    $("#btnUpload").click(function () {

        var id = $("#hdnId").val();
        var file = $("#txtFile").get(0).files[0]

        if (file != undefined) {

            var ext = file.name.substr(file.name.indexOf(".") + 1)
            if (ext == "jpg" || ext == "jpeg" || ext == "png") {
                //valid case

                var frmdata = new FormData()
                frmdata.append("id", id)
                frmdata.append("file", file)

                $.ajax({
                    url: "/Customer/UpdateProfileImage",
                    type: "POST",
                    data: frmdata,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        // console.log(response)
                        if (response.ok) {
                            $("#msgUpload").html(response.message).css("color", "green")
                            setTimeout(function () {
                                window.location.reload()
                            }, 4000)
                        }
                        else {
                            $("#msgUpload").html("Error in image upload").css("color", "red")

                        }
                    }
                })



            }
            else {
                alert("Please choose image file only!")
            }
        }
        else {
            alert("Please choose file!")
        }




    })

    getCustomers()

    HideModalPopUp()
})

function emptyDropdown(id) {
    $("#" + id).html("<option value=''>Select</option>")
}
function getCountries() {

    $.ajax({
        url: "/Customer/GetCountry",
        type: "GET",
        async: false,
        success: function (response) { 
            var ddl = "<option value=''>Select</option>"
            response.forEach((index, item) => {
                ddl += "<option value=" + index.id + ">" + index.name + "</option>"
                debugger
            })
            $("#ddlCountry").html(ddl)
        }
    })

    //$.get("/Customer/GetCountry", function (response) {

    //    var ddl = "<option value=''>Select</option>"
    //    response.forEach((item, index) => {
    //        ddl+="<option value="+item.id+">"+item.name+"</option>"
    //    })
    //    $("#ddlCountry").html(ddl)
    //})
}



function HideModalPopUp() {
    $('#exampleModal').modal('hide')
}

function getState(id) {
    //$.get("/Customer/GetState", {"id":id}, function (response) {

    //    var ddl = "<option value=''>Select</option>"
    //    response.forEach((item, index) => {
    //        ddl += "<option value=" + item.id + ">" + item.name + "</option>"
    //    })
    //    $("#ddlState").html(ddl)
    //})

    $.ajax({
        url: "/Customer/GetState",
        type: "GET",
        async: false,
        data: { "id": id },
        success: function (response) {
            var ddl = "<option value=''>Select</option>"
            response.forEach((item, index) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlState").html(ddl)
        }
    })

}

function getCity(id) {
    //$.get("/Customer/GetCity", { "id": id }, function (response) {

    //    var ddl = "<option value=''>Select</option>"
    //    response.forEach((item, index) => {
    //        ddl += "<option value=" + item.id + ">" + item.name + "</option>"
    //    })
    //    $("#ddlCity").html(ddl)
    //})

    $.ajax({
        url: "/Customer/GetCity",
        type: "GET",
        async: false,
        data: { "id": id },
        success: function (response) {
            var ddl = "<option value=''>Select</option>"
            response.forEach((item, index) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlCity").html(ddl)
        }
    })
}

function getCustomers() {
    $.get("/Customer/GetCustomers", function (response) {

        console.log(response)
        $("#tbl_Customer").DataTable(
            {
                data: response,
                columns: [
                    { "data": "id" },
                    { "data": "name" },
                    { "data": "email" },
                    { "data": "mobile" },
                    { "data": "gender" },
                    { "data": "country_id" },
                    { "data": "state_id" },
                    { "data": "city_id" },
                    {
                        "data": "id",
                        class: "text-center",
                        render: function (id) {
                            var link = "<a onclick='DeleteRecord(" + id + ")'><i class='fa fa-trash'></i></a>"
                            link += "<a onclick='EditRecord(" + id + ")'><i class='fa fa-edit'></i></a>"
                            return link
                        }
                    },
                    {
                        "data": "id",
                        class: "text-center",
                        render: function (id) {
                            var link = "<a onclick='UploadProfile(" + id + ")'><i class='fa fa-cloud'></i></a>"

                            return link
                        }
                    }

                ]
            }

        )
    })
}

function DeleteRecord(id) {
    if (confirm("Are you sure to delete this record ?") == false) {
        return false
    }
    else {
        $.get("/Customer/DeleteCustomer", { "id": id }, function (response) {
            if (response.ok) {
                alert(response.message)
                setTimeout(function () {
                    window.location.reload()
                }, 6000)
            }
            else {
                alert(response.message)
            }
        })
    }

}

function EditRecord(id) {
    //(id)
    $("#exampleModal").modal("show")
    $("#btnSubmit").val("Update")
    $.get("/Customer/GetCustomer", { "id": id }, function (response) {

        $("#txtId").val(response.id)
        $("#txtName").val(response.name)
        $("#txtEmail").val(response.email)
        $("#txtMobile").val(response.mobile)
        $("#ddlGender").val(response.gender)
        $("#ddlCountry").val(response.country)
        getState(response.country)
        $("#ddlState").val(response.state)
        getCity($("#ddlState").val())
        $("#ddlCity").val(response.city)

    })

}
function AddCustomer() {
    var customer = {
        "name": $("#txtName").val(),
        "email": $("#txtEmail").val(),
        "mobile": $("#txtMobile").val(),
        "gender": $("#ddlGender").val(),
        "country_id": $("#ddlCountry").val(),
        "state_id": $("#ddlState").val(),
        "city_id": $("#ddlCity").val()
    }
    if ($("#btnSubmit").val() == "Submit" && $("#txtId").val() == "") {
        $.post("/Customer/CreateCustomer", customer, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                setTimeout(function () {
                    window.location.reload()
                }, 6000)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

            }
        })
    }
    else {
        customer.id = $("#txtId").val()
        $.post("/Customer/UpdateCustomer", customer, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                $("#btnSubmit").val("Submit")
                setTimeout(function () {
                    window.location.reload()
                }, 6000)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

            }
        })
    }
}

function UploadProfile(id) {
    $("#hdnId").val(id)
    $("#modalUploadProfile").modal("show")
}

