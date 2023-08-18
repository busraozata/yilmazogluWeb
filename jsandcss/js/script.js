var swiper = new Swiper(".mySwiperBanner", {
  effect: "fade",
  autoplay: {
    delay: 7500,
    disableOnInteraction: false,
  },
});
$(".card-deck a").fancybox({
  caption: function (instance, item) {
    return $(this).parent().find(".card-text").html();
  },
});

$(".show-btn").click(function () {
  $("#target").show(200);
 /*  $(".show-btn").hide(0); */
});
$(".hide-btn").click(function () {
  $("#target").hide(500);
  /* $(".show-btn").show(0); */
});
$(".toggle").click(function () {
  $("#target").toggle("slow");
});

var swiper = new Swiper(".mySwiperCompany", {
  spaceBetween: 60,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  breakpoints: {
    640: {
      slidesPerView: 2,
      spaceBetween: 20,
    },
    768: {
      slidesPerView: 1,
      spaceBetween: 40,
    },
    1024: {
      slidesPerView: 2,
      spaceBetween: 50,
    },
  },
});

var swiper = new Swiper(".mySwiperSSSArea", {
  autoplay: {
    delay: 7500,
    disableOnInteraction: false,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});

var swiperServiceSlide = new Swiper(".swiperServiceSlide", {
  autoplay: {
    delay: 2500,
    disableOnInteraction: false,
  },
  navigation: {
    nextEl: "#swiper-button-nextService",
    prevEl: "#swiper-button-prevService",
  },
});

var swiperGallery = new Swiper(".ImgGalleryMySwiper", {
  slidesPerView: 2,
  spaceBetween: 20,
  autoplay: {
    delay: 2500,
    disableOnInteraction: false,
  },
  breakpoints: {
    640: {
      slidesPerView: 2,
      spaceBetween: 20,
    },
    768: {
      slidesPerView: 2,
      spaceBetween: 40,
    },
    1024: {
      slidesPerView: 3,
      spaceBetween: 50,
    },
    1366: {
      slidesPerView: 4,
      spaceBetween: 50,
    },
  },
});

const header = document.querySelector(".header");
const headerHeight = parseInt(window.getComputedStyle(header).height);

var swiper = new Swiper(".mySwiperDirection", {
  direction: "vertical",
  slidesPerView: 3,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});
document.addEventListener("scroll", function () {
  if (window.scrollY > headerHeight) {
    header.classList.add("header_scroll");
  } else {
    header.classList.remove("header_scroll");
  }
});

