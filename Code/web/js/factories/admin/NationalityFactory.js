angular.module('enozomApp').factory('NationalityFactory', ['$http', 'appConfigs', 'RequestFactory', function ($http, appConfigs, RequestFactory) {
    return {
        checkName: function(name, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Nationalities/NationalityNameExists?id=" + id + "&name=" + name, null, null)
        } 
    }
}])