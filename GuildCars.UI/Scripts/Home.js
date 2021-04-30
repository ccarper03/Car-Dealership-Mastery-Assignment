$(document).ready(function () {
  
});

function getDetails(CarId) {
    window.location.replace("Details/" + CarId);
}

function purchaseVehicle(CarId) {
    window.location.replace("Sales/Purchase/" + CarId);
}
function editVehicle(CarId) {
    window.location.replace("Admin/Edit/" + CarId);;
}

function deleteCar(carId) {
    
    var url = "http://localhost:44351/Admin/Delete/" + carId;

    var encodedUrl = encodeURI(url);

    if (confirm("Are you sure you want to delete this Car?")) {
        $.ajax({
            type: "DELETE",
            url: encodedUrl,
           
            success: function (status) {
                window.location.replace("Admin/Index/");
                alert("Car has been deleted from database.");
                

            },
            error: function () {
                $("#errorMessages").append(
                    $("<li>")
                        .attr({
                            class: "list-group-item list-group-item-danger"
                        })
                        .text("There was a problem deleting the car from the database. Contact It.")
                );
            }
        });
    }
    return false;
}

function deleteSpecial(specialId) {

    var url = "http://localhost:44351/Admin/DeleteSpecial/" + specialId;

    var encodedUrl = encodeURI(url);

    if (confirm("Are you sure you want to delete this Special?")) {
        $.ajax({
            type: "DELETE",
            url: encodedUrl,

            success: function (status) {
                window.location.reload();
                alert("Special has been deleted from database.");
                

            },
            error: function () {
                $("#errorMessages").append(
                    $("<li>")
                        .attr({
                            class: "list-group-item list-group-item-danger"
                        })
                        .text("There was a problem deleting the special from the database. Contact It.")
                );
            }
        });
    }
    return false;
}

function setMakeId() {
    $("#MakeId").attr("value", $("#Makes").val());
}

function setCarIds() {

    $("#MakeId").attr("value", $("#Makes").val()); // Ok. this is the code which sets the #MakeId == <value ="XXX">. so if XXX = toyota, then
                                                   // toyota is sent over the wire. if you can change this to an INT then it will bind ok.
                                                   // that said, you need to make sure you correctly send the text to the server when
                                                   // you need to get the list of MODELS of the take. (trashcan, etc)
                                                   // right now, you might be creating the url => admin/models/toyota or whatever... 
                                                   // this might need to get canged to admin/models/1 (where 1 == tooytal)
    $("#ModelId").attr("value", $("#Models").val());
    $("#BodyStyleId").attr("value", $("#BodyStyleType").val())
    $("#TransmissionId").attr("value", $("#TransmissionType").val())
    $("#IntColorId").attr("value", $("#InteriorColor").val())
    $("#BodyColorId").attr("value", $("#BodyColor").val())
    $("#deleteId").attr("value", $("#CarId").val())
}

function getModels(makeId) {
    var url = "/Admin/GetModels/" + makeId;

    $.ajax({
        url: url,
        type: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            var markup = "";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#Models").html(markup).show();
        },
        error: function () {
            $("#errorMessages").append(
                $("<li>")
                    .attr({
                        class: "list-group-item list-group-item-danger"
                    })
                    .text("There was a problem getting models for this vehicle make.")
            );
        }
    });
}

function searchVehicles(vehicleSearch) {
    // we need to clear the previous content so we don't append to it
    clearVehicleSearch();

    $("#errorMessages").empty();

    $("#VehicleSearchResultsDiv").hide();


     var vehicleSearchRows = $("#vehicleSearchRows");

    var minPrice = $("#minPriceSelectBox").val();

    var maxPrice = $("#maxPriceSelectBox").val();

    var minYear = $("#minYearSelectBox").val();

    var maxYear = $("#maxYearSelectBox").val();

    var searchTerm = $("#carSearchTermInput").val();

    var searchType = vehicleSearch;

    var detailsOrPurchase = callSearchType(searchType);

    var buttonLabel = determineButtonLabel(searchType);

    if (searchTerm === "") {
        searchTerm = "null";
    }

    var urlString = "http://localhost:44351/api/inventory/" + searchType + "/"  + searchTerm + "/" + minYear + "/" + maxYear + "/" + minPrice + "/" + maxPrice;

    var encodedUrl = encodeURI(urlString);

    $.ajax({
        type: "GET",
        url: encodedUrl,
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data, status) {
            $.each(data, function (index, car) {
                var CarId = car.CarId;
                var Make = car.Make;
                var Model = car.Model;
                var Year = car.Year;
                var Img = car.IMGURL;
                var IntColor = car.InteriorColor;
                var BodyColor = car.BodyColor;
                var BodyStyle = car.BodyStyle;
                var Transmission = car.Transmission;
                var Mileage = car.Mileage;
                var VIN = car.VIN;
                var SalePrice = car.SalePrice;
                var MSRP = car.MSRP;

                var row = "<tr>";
                row +=
                    '<td><a onclick="getDetails(' + CarId +')"><ul class="vehicleResultsList"><li class="vehicleResultsListRow">' + ' ' + Year + ' ' + Make + ' ' + Model + '</li>' +
                    '<li class="vehicleResultsListRow"><img class="resize" src="' + Img + '" /></li>' +
                    '</a></ul ></td>';
                row += '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">BodyStyle: ' + BodyStyle + '</li>' +
                    '<li class="vehicleResultsListRow">Trans:' + Transmission + '</li>' +
                    '<li class="vehicleResultsListRow">Color:' + BodyColor + '</li>' +
                    '</ul></td >';
                row += '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">Interior: ' + IntColor  + '</li>' +
                    '<li class="vehicleResultsListRow">Mileage:' + Mileage + '</li>' +
                    '<li class="vehicleResultsListRow">VIN:' + VIN + '</li>' +
                    '</ul></td >';
                row += '<td><ul class="vehicleResultsList"><li class="vehicleResultsListRow">Sale Price: ' + "$" + formatPrice(SalePrice, 2, '.',',') + '</li>' +
                    '<li class="vehicleResultsListRow">MSRP: ' + '$' + formatPrice(MSRP, 2, '.', ',') + '</li>' +
                    '<li class="vehicleResultsListRow"><button type="button" id="vehicleDetailsButton" class="btn btn-default" onclick="' + detailsOrPurchase + CarId + ')"> ' + buttonLabel + '</button > ' + '</li > ' +
                    "</ul></td >";
                row += "</tr>";
                vehicleSearchRows.append(row);
            });
        },
        error: function () {
            $("#errorMessages").append(
                $("<li>")
                    .attr({
                        class: "list-group-item list-group-item-danger"
                    })
                    .text("Your search returned no results. Please try again.")
            );
        }
    });

    $("#VehicleSearchResultsDiv").show();
}

function searchSales() {
    // we need to clear the previous content so we don't append to it
    clearSalesSearch();

    $("#errorMessages").empty();

    $("#salesSearchResultsDiv").hide();


    // grab the the tbody element that will hold the rows of dvd information
    var salesSearchRows = $("#salesSearchRows");

    var minYear = $("#minYearSelectBox").val();

    var maxYear = $("#maxYearSelectBox").val();

    var searchTerm = $("#salesSearchTermInput").val();

    if (searchTerm === "") {
        searchTerm = "null";
    }

    var urlString = "http://localhost:44351/api/reports/sales/" + searchTerm + "/" + minYear + "/" + maxYear + "/";

    var encodedUrl = encodeURI(urlString);

    $.ajax({
        type: "GET",
        url: encodedUrl,
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data, status) {
            if (data.length == 0) {
                $("#errorMessages").append(
                    $("<li>")
                        .attr({
                            class: "list-group-item list-group-item-danger"
                        })
                        .text("Your search returned no results. Please try again.")
                );
            }
            $.each(data, function(index, sale) {
                var user = sale.UserName;
                var carsSold = sale.CarsSold;
                var sales = sale.Sales;
                
                var row = "<tr>";
                row += '<td>' + user + '</td>';
                row += '<td>' + '$' + sales + '</td>';
                row += '<td>' + carsSold +'</td>';
                row += "</tr>";
                salesSearchRows.append(row);
            });
        },
        error: function () {
            $("#errorMessages").append(
                $("<li>")
                    .attr({
                        class: "list-group-item list-group-item-danger"
                    })
                    .text("There was a problem with communicating with the server please try again.")
            );
        }
    });

    $("#salesSearchResultsDiv").show();
}

function clearSalesSearch() {
    $("#salesSearchRows").empty();
}

function callSearchType(searchType) {
    if (searchType === 'SearchNewCars' || searchType === 'SearchUsedCars'){
        return "getDetails(";
    }

    if (searchType === 'SalesSearchCars') {
        return "purchaseVehicle(";
    }

    if (searchType === 'AdminSearchCars') {
        return "editVehicle(";
    }
}

function determineButtonLabel(searchType) {
    if (searchType === 'SearchNewCars' || searchType === 'SearchUsedCars') {
        return "Details";
    }

    if (searchType === 'SalesSearchCars') {
        return "Purchase";
    }

    if (searchType === 'AdminSearchCars') {
        return "Edit";
    }
}

function toTitleCase(str) {
    return str.replace(/(?:^|\s)\w/g, function (match) {
        return match.toUpperCase();
    });
}

function formatPrice(value, decimals, decimalSeparator, thousandSeparator) {
    if (value == null || isNaN(value))
        return "";

    var decimals = isNaN(c = Math.abs(decimals)) ? 2 : decimals;
    var decimalSeparator = decimalSeparator == undefined ? "." : decimalSeparator;
    var thousandSeparator = thousandSeparator == undefined ? " " : thousandSeparator;

    var negativeSign = value < 0 ? "-" : "";

    var valueNoDecimals = String(parseInt(value = Math.abs(Number(value) || 0).toFixed(decimals)));

    var spacingStart = 0;
    if ((valueNoDecimals.length) > 3)
        spacingStart = valueNoDecimals.length % 3;

    var leadingNumber = (spacingStart ? valueNoDecimals.substr(0, spacingStart) + thousandSeparator : "");
    var separatedMiddle = valueNoDecimals.substr(spacingStart).replace(/(\d{3})(?=\d)/g, "$1" + thousandSeparator);
    var decimals = (decimals ? decimalSeparator + Math.abs(value - valueNoDecimals).toFixed(decimals).slice(2) : "");

    var result = negativeSign + leadingNumber + separatedMiddle + decimals;
    return result.trim();
};


function clearVehicleSearch() {
    $("#vehicleSearchRows").empty();
}