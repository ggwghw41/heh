function togglePassword() {
    const inputpass = document.getElementById("pass");
    const showpass = document.getElementById("toggleIcon");

    // Toggle password visibility
    if (inputpass.type === "password") {
        inputpass.type = "text";
        showpass.classList.remove("fa-lock");
        showpass.classList.add("fa-unlock");

    } else {
        inputpass.type = "password";
        showpass.classList.remove("fa-unlock");
        showpass.classList.add("fa-lock");

    }
}
function confirmPassword() {
    const inputpass = document.getElementById("confirmpass");

    const confirm = document.getElementById("confirmtoggleIcon");
    // Toggle password visibility
    if (inputpass.type === "password") {
        inputpass.type = "text";

        confirm.classList.remove("fa-lock");
        confirm.classList.add("fa-unlock");
    } else {
        inputpass.type = "password";

        confirm.classList.remove("fa-unlock");
        confirm.classList.add("fa-lock");
    }
}
function createLeaf() {
    const leaf = document.createElement('div');
    leaf.classList.add('leaf');
    leaf.style.left = Math.random() * 100 + 'vw';  // Random vị trí ngang
    leaf.style.animationDuration = Math.random() * 3 + 2 + 's';  // Thời gian rơi random
    leaf.style.opacity = Math.random() * 0.5 + 0.5;  // Độ trong suốt ngẫu nhiên

    document.querySelector('.falling-leaves').appendChild(leaf);

    // Xóa lá sau khi rơi hết
    setTimeout(() => {
        leaf.remove();
    }, 5000); // Xóa sau 5 giây
}

// Tạo lá rơi liên tục
setInterval(createLeaf, 4000);
