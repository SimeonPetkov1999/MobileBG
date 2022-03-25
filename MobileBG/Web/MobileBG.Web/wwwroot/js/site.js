
var makesList = document.getElementById("makes");
function populateDropdownForModels() {
    var index = makesList.selectedIndex;
    if (index == 0) {
        document.getElementById('models').innerHTML = "";
    }
    else {
        var id = makesList.options[index].value;
        $.ajax({
            type: "get",
            url: "/api/Models/ForMake",
            data: {
                makeId: id,
            },
            success: function (result) {
                var select = document.getElementById('models')
                select.innerHTML = "";
                $.each(result, function (i, val) {
                    $('#models').append($('<option></option>').val(val.id).html(val.name));
                })
            }
        });
    }
}
$('#makes').change(populateDropdownForModels)