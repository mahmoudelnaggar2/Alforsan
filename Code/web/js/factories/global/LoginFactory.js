enozomApp.factory('LoginFactory', ['$http', '$rootScope', 'appConfigs', function ($http, $rootScope, appConfigs) {
    return {
        Login: function (username, password) {
            debugger;
            var result = 
            $http({
                url: appConfigs.apiBaseURL + 'token',
                method: 'POST',
                data: "userName=" + username + "&password=" + password + "&grant_type=password"
                });
            debugger;
            return result;
        }
    }
}]);

