angular.module('enozomApp').factory('RolesFactory', ['$http', 'settings', 'appConfigs', '$cookieStore'
    , 'RequestFactory', function ($http, settings, appConfigs, $cookieStore, RequestFactory) {
    return {
        getFeaturesRights: function () {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + 'Roles/getFeaturesRights/', null, null);
        },
        getRole: function (id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + 'Roles/' + id, null, null);
        },
        getRoleSideMenu: function () {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + 'Roles/getRoleSideMenu/', null, null);
        },
        checkName: function (name, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Roles/RoleNameExists?id=" + id + "&name=" + name, null, null)
        }
    }
}]);
