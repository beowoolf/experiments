var map = new Map();

var x = document.querySelector("body > div.l-container.u-min-height-600 > div.l-grid.u-mb-120 > div.l-col-12 > div > div.b-user-info-list__content")
var y = x.children
for (let index = 0; index < y.length; index++) {
    const a = Number(y[index].children[3].textContent.replace(" zł", "").replace(",","."))
    const b = y[index].children[1].textContent.split(".")
    const c = b[2]
    if (map.get(c))
        map.set(c, map.get(c) + a);
    else
        map.set(c, a);
    console.log(c + ' ' + a)
}
console.log("=================================")
for (let [key, value] of map)
    console.log(key + " = " + value);
