angular.module('enozomApp').factory('yearFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        yearNameUnique: function(yearName, yearId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Year/YearNameUnique?yearName=" + yearName + "&yearId=" + yearId)
        },
        shortNameUnique: function(shortName, yearId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Year/shortNameUnique?shortName=" + shortName + "&yearId=" + yearId)
        },
        CurrentYear: function() {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Year/CurrentYear")
        }
    }
}]);