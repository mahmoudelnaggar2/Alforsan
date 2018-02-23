angular.module('enozomApp').factory('DicountsTypesFactory', ['$http', 'appConfigs', 'RequestFactory', function ($http, appConfigs, RequestFactory) {
    return {
        checkName: function (name, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "DiscountsTypes/DiscountsNameExists?id=" + id + "&name=" + name, null, null)
        },
    }
}])