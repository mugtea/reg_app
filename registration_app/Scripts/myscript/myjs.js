$(window).on('load', function () {
    validation.init("form");

    document.getElementById("phoneValidation").addEventListener("keyup", phoneValidation, false);
    document.getElementById("phoneValidation").addEventListener("blur", phoneValidation, false);

    validation.addClassValidation("#phoneValidation", ".my-class-invalid", "asadasdsadasdas");

    document.getElementById("emailValidation").addEventListener("keyup", emailValidation, false);
    document.getElementById("emailValidation").addEventListener("blur", emailValidation, false);

    document.getElementById("loginBtn").addEventListener("click", redirectLoginpage, false);
    //generate month
    var listMonth = [
           { id: 1, name: 'JANUARI' },
           { id: 2, name: 'FEBRUARI' },
           { id: 3, name: 'MARET' },
           { id: 4, name: 'APRIL' },
           { id: 5, name: 'MEI' },
           { id: 6, name: 'JUNI' },
           { id: 7, name: 'JULI' },
           { id: 8, name: 'AGUSTUS' },
           { id: 9, name: 'SEPTEMBER' },
           { id: 10, name: 'OKTOBER' },
           { id: 11, name: 'NOVEMBER' },
           { id: 12, name: 'DESEMBER' }
    ];
    $.each(listMonth, function (key, value) {
        $('#month').append($("<option></option>")
                       .attr("value", value.id)
                       .text(value.name));
    });

    //generate years
    var generateYears = function (startYear) {
        var currentYear = new Date().getFullYear(), years = [];
        startYear = startYear || 1980;
        while (startYear <= currentYear) {
            years.push(startYear++);
        }
        return years;
    }
    var listYears = generateYears(new Date().getFullYear() - 50);
    $.each(listYears, function (key, value) {
        $('#year').append($("<option></option>")
                       .attr("value", value)
                       .text(value));
    });

    //generate dates
    var monthV = $('#month')[0].value;
    var yearV = $('#year')[0].value;
    generateListDateOfMonth(monthV, yearV);
    function generateListDateOfMonth(month, year) {
        var listDate = [];
        var d = new Date(year, month, 0);
        for (var i = 1; i < d.getDate() ; i++) {
            listDate.push(i);
        }


        $.each(listDate, function (key, value) {
            $('#date').append($("<option></option>")
                           .attr("value", value)
                           .text(value));
        });
    }

    //submit
    function submit() {
        if (validation.validate()) {
            if (phoneValidation() == true) {
                if (emailValidation() == true) {
                    var param = {};
                    var month = $('#month')[0].value;
                    var year = $('#year')[0].value;
                    var date = $('#date')[0].value;
                    param.date_of_birth = (year + "-" + month + "-" + date);
                    param.first_name = $('#firstname')[0].value;
                    param.last_name = $('#lastname')[0].value;
                    param.gender = $('#genderFemale')[0].checked ? "F" : "M";
                    param.email = $('#emailValidation')[0].value;
                    param.mobile_number = $('#phoneValidation')[0].value;

                    submitForm(param).done(function (result) {
                        //alert(result);
                        $('#inputform *').attr("disabled", "disabled").off('click');
                        $('#btnLoginDiv').show();
                        $('#footerDiv').hide();
                    }).fail(function (error) {
                        alert(error);
                    })
                }
            }
        }
    }
    document.getElementById("registerBtn").addEventListener("click", submit, false);

    function submitForm(param) {
        return $.ajax({
            type: 'POST',
            url: baseurl + '/api/registration',
            dataType: 'json',
            data: jQuery.param(param)
        });
    }

});
var baseurl = "http://localhost:62843";

function redirectLoginpage() {
    location.href = baseurl + "/Login/Index";
}
function validateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
        return (true)
    }
    return (false)
}
function emailValidation() {
    if (!validateEmail($('#emailValidation')[0].value)) {
        validation.show("#emailValidation", "Please enter an valid email");
        $('#emailValidation').addClass('validate-error');
        return false;
    }
    return true;
};
function validatePhone(phone) {
    if (/^((?:\+62|62)|08|02)[1-9]{1}[0-9]+$/.test(phone)) {
        return (true)
    }
    return (false)
}
function phoneValidation() {
    if (!validatePhone($('#phoneValidation')[0].value)) {
        validation.show("#phoneValidation", "Please enter valid Indonesia phone number");
        $('#phoneValidation').addClass('validate-error');
        return false;
    }
    return true;
};