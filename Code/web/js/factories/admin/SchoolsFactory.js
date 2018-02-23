angular.module('enozomApp').factory('SchoolsFactory', ['$http', 'appConfigs', 'RequestFactory', function($http, appConfigs, RequestFactory) {
    return {
        checkName: function(name, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Schools/SchoolNameExists?id=" + id + "&name=" + name, null, null)
        },
        checkPrefix: function(prefix, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Schools/PrefixExists?id=" + id + "&prefix=" + prefix, null, null)
        }
    }
}])