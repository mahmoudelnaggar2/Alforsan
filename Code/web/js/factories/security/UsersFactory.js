angular.module('enozomApp').factory('UsersFactory', ['$http', 'settings', 'appConfigs', '$cookieStore', 'RequestFactory', function($http, settings, appConfigs, $cookieStore, RequestFactory) {
    return {
        getCurrentUser: function() {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + 'Users/CurrentUser', null, null);
        },

        checkName: function(name, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Users/UserNameExists?id=" + id + "&name=" + name, null, null)
        },

    }
}]);