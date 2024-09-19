var sum = 0
var x = document.querySelector("body > div.l-container.u-min-height-600 > div.l-grid.u-mb-120 > div.l-col-12 > div > div.b-user-info-list__content")
var y = x.children
for (let index = 0; index < y.length; index++) {
    const a = Number(y[index].children[3].textContent.replace(" zł", "").replace(",","."))
    sum = sum + a
    console.log(a)
}
console.log('======')
console.log(sum)
