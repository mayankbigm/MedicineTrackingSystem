const uri = "api/medicines";
let Medicines = null;

function getCount(data) {
    const el = $("#counter");
    let name = "Medicine";
    if (data) {
        if (data > 1) {
            name = "Medicines";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#Medicines");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append(
                        $("<td></td>").append(
                            $("<input/>", {
                                type: "checkbox",
                                disabled: true,
                                checked: item.isComplete
                            })
                        )
                    )
                    .append($("<td></td>").text(item.name))
                    .append($("<td></td>").text(item.brand))
                    .append($("<td></td>").text(item.quantity))
                    .append($("<td></td>").text(item.price))
                    .append($("<td></td>").text(item.expiryDate))
                    .append($("<td></td>").text(item.notes))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            Medicines = data;
        }
    });
}

function addItem() {
    const item = {
        name: $("#add-name").val(),
        brand: $("#add-brand").val(),
        quantity: $("#add-quantity").val(),
        price: $("#add-price").val(),
        expiryDate: $("#add-expiryDate").val(),
        notes: $("#add-notes").val(),
        isComplete: false
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong in Add!");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
            $("#add-brand").val("");
            $("#add-quantity").val("");
            $("#add-price").val("");
            $("#add-expiryDate").val("");
            $("#add-notes").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(Medicines, function (key, item) {
        if (item.id === id) {
            $("#edit-id").val(item.id);
            $("#edit-name").val(item.name);
            $("#edit-brand").val(item.brand);
            $("#edit-quantity").val(item.quantity);
            $("#edit-price").val(item.price);
            $("#edit-expiryDate").val(item.expiryDate.split("T")[0]);
            $("#edit-notes").val(item.notes);
            $("#edit-isComplete")[0].checked = item.isComplete;
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        brand: $("#edit-brand").val(),
        quantity: $("#edit-quantity").val(),
        price: $("#edit-price").val(),
        expiryDate: $("#edit-expiryDate").val(),
        notes: $("#edit-notes").val(),
        id: $("#edit-id").val()
        //isComplete: $("#edit-isComplete").is(":checked"),
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function(jqXHR, textStatus, errorThrown) {
            alert("Something went wrong in Edit!");
        },
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}