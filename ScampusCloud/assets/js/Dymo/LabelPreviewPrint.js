var _label;

// stores loaded label info
function previewdata(xml, _labelData) {
    _label = dymo.label.framework.openLabelXml(xml);

    // applies data to the label
    var names = _label.getObjectNames();
    for (var name in _labelData)
        if (itemIndexOf1(names, name) >= 0)
            _label.setObjectText(name, _labelData[name]);


    // updates label preview image
    // Generates label preview and updates corresponend <img> element
    // Note: this does not work in IE 6 & 7 because they don't support data urls
    // if you want previews in IE 6 & 7 you have to do it on the server side
    if (!_label)
        return;

    var pngData = _label.render();
    var labelImage = $("#labelImage");
    labelImage.attr('src', "data:image/png;base64," + pngData);
    //$("#divmain").show();
}

//returns an index of an item in an array. Returns -1 if not found
function itemIndexOf1(array, item) {
    for (var i = 0; i < array.length; i++)
        if (array[i] == item) return i;

    return -1;
}

//// loads all supported printers into a combo box 
function loadPrinters() {
    var printersSelect = document.getElementById('printersSelect');
    var printers = dymo.label.framework.getPrinters();
   
    if (printers.length == 0) {
        toastr.error("No DYMO printers are installed. Install DYMO printers.");
        if ($("#btnPrint").length > 0 && $("#printersDiv").length > 0) {
            $("#btnPrint").hide();
            $("#printersDiv").hide();
        }        
        return;
    }

    for (var i = 0; i < printers.length; i++) {
        var printerName = printers[i].name;

        var option = document.createElement('option');
        option.value = printerName;
        option.appendChild(document.createTextNode(printerName));
        printersSelect.appendChild(option);
    }
    if ($("#btnPrint").length > 0 && $("#printersDiv").length > 0) {
        $("#printersDiv").show();
        $("#btnPrint").show();
    }
};

// prints the label
function printLabel() {
    if (!_label) {
        toastr.error("Load label before printing");
        return;
    }
    _label.print(printersSelect.value);
}

