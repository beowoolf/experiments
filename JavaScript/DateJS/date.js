function getNumberOfDaysInMonth(month, year) {
	var d = 0;
	
	switch(month) {
		case "2":
			d = (year % 4 == 0 ? 29 : 28);
			break;
		case "4":
		case "6":
		case "9":
		case "11":
			d = 30;
			break;
		case "1":
		case "3":
		case "5":
		case "7":
		case "8":
		case "10":
		case "12":
			d = 31;
			break;
	}
	
	return d;
}

function saveDay() {
	prevDay = document.getElementById("d").value;
}

function setDays() {
	var currYear = document.getElementById("y").value;
	var currMonth = document.getElementById("m").value;
	
	var dd = "<option value='0'>DD</option>";
	if ((currYear > 0) && (currMonth > 0)) {
		var d = getNumberOfDaysInMonth(currMonth, currYear);
		
		var daysInMonth = dd;
		for (var i = 1; i <= d; i++)
			daysInMonth += "<option value='" + i + "'>" + (i < 10 ? "0" + i : i) + "</option>";
		document.getElementById("d").innerHTML = daysInMonth;
		
		s = 0;
		
		if (prevDay > d)
			prevDay = d;
			
		document.getElementById("d").value = prevDay;
	} else
		document.getElementById("d").innerHTML = dd;
}

var y = new Date().getFullYear();

var years = "<option value='0'>YYYY</option>";
for (var i = 0; i <= 100; i++)
	years += "<option value='" + (y-i) + "'>" + (y-i) + "</option>";
document.getElementById("y").innerHTML = years;

var months = "<option value='0'>MM</option>";
for (var i = 1; i <= 12; i++)
	months += "<option value='" + i + "'>" + (i < 10 ? "0" + i : i) + "</option>";
document.getElementById("m").innerHTML = months;