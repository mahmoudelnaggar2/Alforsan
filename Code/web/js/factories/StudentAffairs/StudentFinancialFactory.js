angular.module('enozomApp').factory('studentFinancialFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        getBalance: function(studentId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "StudentFinancial/GetBalance?studentId=" + studentId);
        }
    }
}]);