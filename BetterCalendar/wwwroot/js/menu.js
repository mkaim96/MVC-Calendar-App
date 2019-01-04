var closeButton = document.querySelector("#close-menu-button")
var menuItems = document.querySelector("#menu-items")
var openButton = document.querySelector(".menu")
var menuOverlay = document.querySelector(".menu-overlay")




openButton.addEventListener("click", function (e) {
    menuOverlay.classList.remove("hide")
    menuOverlay.classList.add("show")
    menuItems.classList.remove("hide")
    menuItems.classList.add("show")
})

closeButton.addEventListener("click", function (e) {
    menuOverlay.classList.remove("show")
    menuOverlay.classList.add("hide")
    menuItems.classList.remove("show")
    menuItems.classList.add("hide")

})

menuOverlay.addEventListener("click", function (e) {
    menuOverlay.classList.remove("show")
    menuOverlay.classList.add("hide")
    menuItems.classList.remove("show")
    menuItems.classList.add("hide")
})

console.log(closeButton)
console.log(menuItems)
console.log(openButton)