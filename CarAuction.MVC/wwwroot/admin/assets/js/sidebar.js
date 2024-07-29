document.querySelectorAll(".nav-link").forEach(item => {
    item.addEventListener("click", () => {
        document.querySelectorAll(".nav-link").forEach(item1 => {
            item1.classList.add("collapsed")
            item1.parentElement.classList.remove("active")
        });
        item.classList.remove("collapsed")
        item.parentElement.classList.add("active")

    })
})
const links = document.querySelectorAll(".nav-item .menu-title ")
const collapses = document.querySelectorAll(".collapse")
const currentUrl = window.location.pathname;
let controller = currentUrl.split("/")[2]
if (controller == "associate") {
    controller = "sponsor";
}
else if (controller == "abouttext") {
    controller = "text";
}
else if (controller == "aboutskill") {
    controller = "skill";
}
else if (controller == "textwhy") {
    controller = "why";
}
links.forEach(link => {
    if (link.textContent.toLowerCase().includes(controller.toLowerCase())) {
        link.style.color = "#4d83ff"
        link.parentElement.querySelector("i").style.color = "#4d83ff"
    }
    else {
        link.style.color = "#000"
        link.parentElement.querySelector("i").style.color = "#000"
    }
})
collapses.forEach(collapse => {
    collapse.classList.remove("show")
})
const width = document.querySelector(".navbar .navbar-menu-wrapper .navbar-nav .nav-item.nav-search");
const searchInputs = document.querySelector(".navbar .navbar-menu-wrapper .navbar-nav .nav-item.nav-search .input-group .form-control");
const containers = document.querySelector(".searchValue")
containers.style.width = width.offsetWidth + "px";
function setSearch() {
    const width = document.querySelector(".navbar .navbar-menu-wrapper .navbar-nav .nav-item.nav-search");
    containers.style.width = width.offsetWidth + "px";
}
window.addEventListener('resize', setSearch);
searchInputs.addEventListener("input", (e) => {
    e.preventDefault();
    if (searchInputs.value == "") {
        containers.style.display = "none";
        return;
    }
    let href = `/home/search?search=${searchInputs.value}`;
    fetch(href)
        .then(x => x.json())
        .then(x => {
            containers.innerHTML = ""
            if (x.length != 0) {
                containers.style.display = "block";
            }
            x.forEach(item => {
                let price = item.initialPrice.toLocaleString('en-US');
                let image = null;
                for (let i = 0; i < item.carImages.length; i++) {
                    if (item.carImages[i].isMain) {
                        image = item.carImages[i].imageUrl;
                    }
                }
                let view = `<div class="searchItem">
                                                    <img src="${image}"/>
                                                    <a href="/car/update/${item.id}">
                                                        <h4>Brand Model</h4>
                                                        <p>${item.model.name} ${item.fabricationYear}</p>
                                                    </a>
                                                      <a href="/shop/detail/${item.id}">
                                                        <h4>Price</h4>
                                                         <p>$${price} </p>
                                                    </a>

                                 </div>`;
                containers.innerHTML += view;
            })
        })
})