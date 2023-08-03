(function () {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            var spinnerElement = document.getElementById('spinner');
            if (spinnerElement) {
                spinnerElement.classList.remove('show');
            }
        }, 100);
    };
    spinner();


        // Initiate the wowjs
        new WOW().init();



    // Fixed Navbar
    window.addEventListener('scroll', function () {
        if (window.innerWidth < 992) {
            if (window.scrollY > 45) {
                document.querySelector('.fixed-top').classList.add('blackbg', 'shadow');
            } else {
                document.querySelector('.fixed-top').classList.remove('shadow');
            }
        } else {
            if (window.scrollY > 45) {
                document.querySelector('.fixed-top').classList.add('blackbg', 'shadow');
                document.querySelector('.fixed-top').style.top = '0';
            } else {
                document.querySelector('.fixed-top').classList.remove( 'shadow');
                document.querySelector('.fixed-top').style.top = '0';
            }
        }
    });


    // Back to top button
    window.addEventListener('scroll', function () {
        if (window.scrollY > 300) {
            document.querySelector('.back-to-top').style.display = 'block';
        } else {
            document.querySelector('.back-to-top').style.display = 'none';
        }
    });
    // document.querySelector('.back-to-top').addEventListener('click', function () {
    //     window.scrollTo({ top: 0, behavior: 'smooth' });
    // });
})();