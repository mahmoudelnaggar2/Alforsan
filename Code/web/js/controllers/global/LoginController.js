angular.module('enozomApp').controller('LoginController', login_Controller);

login_Controller.$inject = ['$scope', '$rootScope', '$location', 'LoginFactory', '$cookieStore', '$translate'];

function login_Controller($scope, $rootScope, $location, LoginFactory, $cookieStore, $translate) {
    $scope.forgotPass = false;
    $scope.forgotPassFun = function() {
        $scope.forgotPass = !$scope.forgotPass;
    };
    // reset login status
    $translate.use('en');
    $scope.login = function () {
        debugger;
        $scope.edit = true;
        if ($scope.Loginform.$valid)
        {
            LoginFactory.Login($scope.username, $scope.password).success(function (data, status, headers, config) {
                $cookieStore.put('key', data.access_token);
                document.getElementsByClassName('page-spinner-bar');
                var element = angular.element(document.getElementsByClassName('page-spinner-bar')[0]);
                element.removeClass('hide');
                window.location = "index.html";
            })
         .error(function (data, status, headers, config) {
             $scope.error = data.error_description;
             if (data.error == "InActiveUser") {
                 bootbox.alert({
                     message: $translate.instant('User is not Active'),
                     buttons: {
                         ok: {
                             label: $translate.instant('close')
                         }
                     },
                     callback: function () {
                         return;
                     }
                 });
             } else {
                 bootbox.alert({
                     message: $translate.instant('Invalid Credentials'),
                     buttons: {
                         ok: {
                             label: $translate.instant('close')
                         }
                     },
                     callback: function () {
                         return;
                     }
                 });
             }
         });
        }
     
    };

    $scope.hasError = function (field, validation) { 
        if (validation) {
            return ($scope.Loginform[field].$dirty && $scope.Loginform[field].$error[validation]) || ($scope.edit && $scope.Loginform[field].$error[validation]);
        }
        return ($scope.Loginform[field].$dirty && $scope.Loginform[field].$invalid) || ($scope.edit && $scope.Loginform[field].$invalid);
    };
}