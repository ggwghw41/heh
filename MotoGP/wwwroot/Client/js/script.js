
// JavaScript để ẩn/hiện menu khi nhấn vào hamburger icon
function toggleMenu() {
    const menu = document.querySelector('.nav-menu');
    menu.classList.toggle('active'); // Thêm hoặc gỡ bỏ class 'active'
}


// ------------carausel-------------
const carousel = document.querySelector(".carousel");
const next = document.querySelector(".next");
const prev = document.querySelector(".prev");

let currentPosition = 0;
const totalImages = 3; // Số ảnh trong carousel
const imageWidth = 100 / totalImages; // Chiều rộng mỗi ảnh (theo phần trăm)

next.addEventListener("click", function () {
    if (currentPosition > -(totalImages - 1) * imageWidth) {
        currentPosition -= imageWidth;
    } else {
        currentPosition = 0; // Quay lại ảnh đầu tiên nếu đã đến cuối
    }
    carousel.style.transform = `translateX(${currentPosition}%)`;
});

prev.addEventListener("click", function () {
    if (currentPosition < 0) {
        currentPosition += imageWidth;
    } else {
        currentPosition = -(totalImages - 1) * imageWidth; // Quay lại ảnh cuối cùng nếu đang ở đầu tiên
    }
    carousel.style.transform = `translateX(${currentPosition}%)`;
});
// ----------chi tiet sna pham----------
function changeImage(src) {
    document.getElementById('main-image').src = src;
}

function toggleDetail(detailId) {
    var detail = document.getElementById(detailId);
    if (detail.classList.contains("show")) {
        detail.classList.remove("show"); // Ẩn nội dung chi tiết
    } else {
        detail.classList.add("show"); // Hiển thị nội dung chi tiết
    }
}
// Tự động lướt carousel sau mỗi 3 giây
setInterval(function () {
    if (currentPosition > -(totalImages - 1) * imageWidth) {
        currentPosition -= imageWidth;
    } else {
        currentPosition = 0; // Quay lại ảnh đầu tiên nếu đã đến cuối
    }
    carousel.style.transform = `translateX(${currentPosition}%)`;
}, 3000); // 3000ms = 3 giây
