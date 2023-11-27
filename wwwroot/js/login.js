var totalData;

$(function () {
    $.ajax({
        type: 'GET',
        url: '../Home/GetProducts',
        success: function (response) {
            console.log(response);
            totalData = response;
            DisplayCards();
        },
        error: function (error) {
            console.error(error);
        }
    });
});

function DisplayCards() {

    var productsContainer = $('#productsContainer');
    productsContainer.empty();

    totalData.forEach(item => {
        var productDiv = $('<div>').addClass('mb-3 col-sm-6 col-md-4 col-lg-3');
        productDiv.html(`
                <div id="${item.productId}" class="card m-3 h-100">
                    <img class="card-img-top w-100 img-fluid" src="data:image/gif;base64, ${item.productImage}" alt="Card image">
                    <div class="card-body">
                        <h5 class="card-title">${item.productName}</h5>
                    </div>
                    <div class="card-footer">
                        <div class="row d-flex align-items-center">
                            <div class="col-6 text-center">
                                <p class="card-text">${item.productPrice} ₹</p>
                            </div>
                            <a href="#" class="btn btn-primary col-6">Add to Cart</a>
                        </div>
                    </div>
                </div>
            `);
        productsContainer.append(productDiv);
    });

}

function login() {
    var email = $('#login-email').val();
    var password = $('#login-password').val();

    if (isValidLogin()) {

        CheckLogin(email,password);
    }
}

function isValidLogin() {
    $("#login-form").validate({
        errorClass: 'label-error',
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 8
            }
        },
        messages: {
            email: {
                required: "Please enter your email",
                email: "Please enter a valid email address"
            },
            password: {
                required: "Please enter your password",
                minlength: "Password must be at least 8 characters long"
            }
        }
    });

    return $("#login-form").valid();
}

function CheckLogin(email,password) {

    // Hash the password (e.g., using CryptoJS)
    var hashedPassword = CryptoJS.SHA256(password).toString();

    var data = {
        email: email,
        password: hashedPassword
    }

    $.ajax({
        type: 'GET',
        url: '../Login/Validate',
        data: data,
        contentType: 'application/json',
        success: function (response) {
            if (response.valid)
            {
                windows.location.href = response.redirectUrl
            }
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function register() {
    var username = $('#username').val();
    var email = $('#register-email').val();
    var password = $('#register-password').val();

    var hashedPassword = CryptoJS.SHA256(password).toString();

    var data = {
        name: username,
        email: email,
        password: hashedPassword
    }

    if (isValidRegisterData()) {
        RegisterUser(data);
    }

   // RegisterUser(data);
}

function isValidRegisterData() {
    $("#register-form").validate({
        errorClass: 'label-error',
        rules: {
            username: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 8
            },
            confirmPassword:{
                required: true,
                equalTo: '#register-password'
            }
        },
        messages: {
            username: {
                required:"Please enter your name"
            },
            email: {
                required: "Please enter your email",
                email: "Please enter a valid email address"
            },
            password: {
                required: "Please enter your password",
                minlength: "Password must be at least 8 characters long"
            },
            confirmPassword: {
                required: "Please enter confirm password",
                equalTo: "Confirm Password not matching with Password"
            }
        }
    });

    return $("#register-form").valid();
}

function RegisterUser(user) {
    $.ajax({
        type: 'POST',
        url: '../Register/RegisterUser',
        data: JSON.stringify(user),
        contentType: 'application/json',
        success: function (response) {
            if (response.status)
            {
                window.location.href = response.redirectUrl
               // console.log(response.redirectUrl);
            }
        },
        error: function (error) {
            if (error.status == 409)
            {
                $('#Duplicate-email').text(error.responseText);
                console.error(error.responseText, error.status);
            }
        }
    });
}