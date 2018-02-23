angular.module('enozomApp').factory('FeesTypesFactory', ['$http', 'appConfigs', 'RequestFactory', function($http, appConfigs, RequestFactory) {
    return {
        checkName: function(name, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "FeesTypes/FeesNameExists?id=" + id + "&name=" + name, null, null)
        },
    }
}])