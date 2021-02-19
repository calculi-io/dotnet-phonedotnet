function Reset() {
    $.ajax({
        type: 'POST',
        url: '/PhoneOrder/reset',
        async: true,
        success: function(model) {
            console.log('reset data.');
            createDefaultUser();
        }
    });
}

function createDefaultUser() {
    $.ajax({
        type: 'POST',
        url: '/PhoneOrder/createDefault',
        async: true,
        success: function(model) {
            console.log('created default user.');
            listAllToConsole();
        }
    });
}

function listAllToConsole() {
    clist.save({}, {
        success: function(model) {
            console.log(JSON.stringify(model));
        }
    });
}


var CustomerListModel = Backbone.Model.extend({
    urlRoot: '/PhoneOrder/listAll'
});

var clist = new CustomerListModel();

//Reset();