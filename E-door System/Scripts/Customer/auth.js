var Auth =
    {
        vars: {
            lowin: document.querySelector('.lowin'),
            lowin_brand: document.querySelector('.lowin-brand'),
            lowin_wrapper: document.querySelector('.lowin-wrapper'),
            lowin_login: document.querySelector('.lowin-login'),
            lowin_wrapper_height: 0,
            login_back_link: document.querySelector('.login-back-link'),
            forgot_link: document.querySelector('.forgot-link'),
            login_link: document.querySelector('.login-link'),
            passward_error: document.querySelector('.login-error'),
            login_btn: document.querySelector('.login-btn'),
            signup_btn: document.querySelector('.signup-btn'),
            register_link: document.querySelector('.register-link'),
            password_group: document.querySelector('.password-group'),
            password_group_height: 0,
            lowin_register: document.querySelector('.lowin-register'),
            lowin_footer: document.querySelector('.lowin-footer'),
            box: document.getElementsByClassName('lowin-box'),
            option: {}
        },
        register(e) {
            Auth.vars.lowin_login.className += ' lowin-animated';
            setTimeout(() => { Auth.vars.lowin_login.style.display = 'none'; }, 500);
            Auth.vars.lowin_register.style.display = 'block';
            Auth.vars.lowin_register.className += ' lowin-animated-flip';
            Auth.setHeight(Auth.vars.lowin_register.offsetHeight + Auth.vars.lowin_footer.offsetHeight);
            e.preventDefault();
        },
        login(e) {
            Auth.vars.lowin_register.classList.remove('lowin-animated-flip');
            Auth.vars.lowin_register.className += ' lowin-animated-flipback';
            Auth.vars.lowin_login.style.display = 'block';
            Auth.vars.lowin_login.classList.remove('lowin-animated');
            Auth.vars.lowin_login.className += ' lowin-animatedback';
            setTimeout(() => { Auth.vars.lowin_register.style.display = 'none'; }, 500);
            setTimeout(() => {
                Auth.vars.lowin_register.classList.remove('lowin-animated-flipback');
                Auth.vars.lowin_login.classList.remove('lowin-animatedback');
            }, 1000);
            Auth.setHeight(Auth.vars.lowin_login.offsetHeight + Auth.vars.lowin_footer.offsetHeight);
            e.preventDefault();
        },
        forgot(e) {
            Auth.vars.password_group.classList += ' lowin-animated';
            Auth.vars.login_back_link.style.display = 'block';
            setTimeout(() => {
                Auth.vars.login_back_link.style.opacity = 1; Auth.vars.password_group.style.height = 0;
                Auth.vars.password_group.style.margin = 0;
            }, 100);
            Auth.vars.login_btn.innerText = 'Forgot Password';
            Auth.setHeight(Auth.vars.lowin_wrapper_height - Auth.vars.password_group_height);
            Auth.vars.lowin_login.querySelector('form').setAttribute('action', Auth.vars.option.forgot_url); e.preventDefault();
        },
        loginback(e) {
            Auth.vars.password_group.classList.remove('lowin-animated');
            Auth.vars.password_group.classList += ' lowin-animated-back';
            Auth.vars.password_group.style.display = 'block';
            setTimeout(() => {
                Auth.vars.login_back_link.style.opacity = 0;
                Auth.vars.password_group.style.height = Auth.vars.password_group_height + 'px';
                Auth.vars.password_group.style.marginBottom = 30 + 'px';
            }, 100);
            setTimeout(() => {
                Auth.vars.login_back_link.style.display = 'none';
                Auth.vars.password_group.classList.remove('lowin-animated-back');
            }, 1000);
            Auth.vars.login_btn.innerText = 'Sign In';
            Auth.vars.lowin_login.querySelector('form').setAttribute('action', Auth.vars.option.login_url);
            Auth.setHeight(Auth.vars.lowin_wrapper_height); e.preventDefault();
        },
        setHeight(height) {
            Auth.vars.lowin_wrapper.style.minHeight = height + 'px';
        },
        brand() {
            Auth.vars.lowin_brand.classList += ' lowin-animated';
            setTimeout(() => {
                Auth.vars.lowin_brand.classList.remove('lowin-animated');
            }, 1000);
        },
        UserLogin(e) {
            var form = Auth.vars.lowin_login.querySelector('form')
            var ele = form.getElementsByTagName("input");
            var epnum = document.getElementById(ele[0].id).value;
            var password = document.getElementById(ele[1].id).value;
            $.ajax({
                url: '/Login/Auth',
                type: 'GET',
                data: {
                    employeeNum: epnum,
                    password: password,
                },
                dataType:'JSON',
                success: function (data, status, xhr) {
                    if (data) {
                        $.ajax({
                            url: '/User/QuerybyRange?ran=' + Math.random(),
                            type: 'GET',
                            data: {
                                employeeNum: epnum,
                            },
                            dataType:'JSON',
                            success: function (data, status, xhr) {
                                setCookie("edoor-id", data[0].id, 2)
                                setCookie("edoor-NTID", data[0].ntid, 2);
                                setCookie("edoor-displayname", data[0].displayname, 2);
                                setCookie("edoor-EmployeeID", data[0].employeeNum, 2);
                                setCookie("edoor-Project", data[0].project, 2);
                                setCookie("edoor-Department", data[0].department, 2);
                                setCookie("edoor-eMail", data[0].eMail, 2);
                                setCookie("edoor-PhoneNum", data[0].phoneNum, 2);
                                var power = 0;
                                switch (data[0].power) {
                                    case "User":
                                        power = 1;
                                        break;
                                    case "Administrator":
                                        power = 7;
                                        break;
                                    default:
                                        break;
                                }
                                setCookie("edoor-Power", power, 2);
                                setCookie("edoor-Tier", data[0].tier, 2);
                                window.location = "/Home/Index";
                            },
                            fail: function (err, status, xhr) {
                                console.log(xhr.status + ":" + xhr.statusText);
                            }
                        });
                    }
                    else {
                        Auth.vars.passward_error.removeAttribute('hidden');
                        console.log("User doesn't exist...");
                    }
                },
                fail: function (data, status, xhr) {
                    console.log(xhr.status + ":" + xhr.statusText);
                }
            })
        },
        UserSignup(e) {
            var form = Auth.vars.lowin_register.querySelector('form')
            var ele = form.getElementsByTagName("input");
            var epnum = document.getElementById(ele[0].id).value;
            var email = document.getElementById(ele[1].id).value;
            var password = document.getElementById(ele[2].id).value;
        },
        init(option) {
            Auth.setHeight(Auth.vars.box[0].offsetHeight + Auth.vars.lowin_footer.offsetHeight);
            Auth.vars.password_group.style.height = Auth.vars.password_group.offsetHeight + 'px';
            Auth.vars.password_group_height = Auth.vars.password_group.offsetHeight;
            Auth.vars.lowin_wrapper_height = Auth.vars.lowin_wrapper.offsetHeight;
            Auth.vars.option = option;
            Auth.vars.lowin_login.querySelector('form').setAttribute('action', option.login_url);
            var len = Auth.vars.box.length - 1;
            for (var i = 0; i <= len; i++) {
                if (i !== 0)
                { Auth.vars.box[i].className += ' lowin-flip'; }
            }
            Auth.vars.forgot_link.addEventListener("click", (e) => {
                Auth.forgot(e);
            });
            Auth.vars.register_link.addEventListener("click", (e) => {
                Auth.brand();
                Auth.register(e);
            });
            Auth.vars.login_link.addEventListener("click", (e) => {
                Auth.brand();
                Auth.login(e);
            });
            Auth.vars.login_back_link.addEventListener("click", (e) => {
                Auth.loginback(e);
            });
            Auth.vars.login_btn.addEventListener("click", (e) => {
                Auth.UserLogin(e);
            });
            Auth.vars.signup_btn.addEventListener("click", (e) => {
                Auth.UserSignup(e);
            });
        }
    }