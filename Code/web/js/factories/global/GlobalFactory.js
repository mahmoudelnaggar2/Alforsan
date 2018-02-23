angular.module('enozomApp').factory('CRUDFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function ($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        getList: function (ControllerName) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + ControllerName, null, null);
        },
        getPaginatedList: function (ControllerName, filterObject) {
            filterObject.PageSize = settings.pageSize;
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + ControllerName + "/FilteredList", filterObject, null);
        },
        get: function (ControllerName, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + ControllerName + '/' + id, null, null);
        },
        add: function (ControllerName, addedObject) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + ControllerName, addedObject, null);
        },
        edit: function (ControllerName, editedObject, id) {
            return RequestFactory.SendRequest('PUT', appConfigs.apiBaseURL + ControllerName + '/' + id, editedObject, null);
        },
        delete: function (ControllerName, id) {
            return RequestFactory.SendRequest('DELETE', appConfigs.apiBaseURL + ControllerName + '/' + id, null, null);
        },
    }
}]);

