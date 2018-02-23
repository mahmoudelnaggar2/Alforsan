angular.module('enozomApp').controller('ProfileController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', '$translate', function($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, $translate) {
    $rootScope.submitted = false;
    $translate.use('en');
    UsersFactory.getCurrentUser().success(function(data, status, headers, config) {
        $rootScope.user = data;
    });
    $scope.redirect = function() {
        if ($rootScope.previousState && $rootScope.previousParams) {
            $state.go($rootScope.previousState, $rootScope.previousParams);
        }
    }
    $scope.save = function() {
        $rootScope.submitted = true;
        if ($scope.addForm.$valid) { //submit form if valid                    
            CRUDFactory.edit("Users", $rootScope.user, $rootScope.user.UserID).success(function(data, status, headers, config) {
                $rootScope.submitted = false;
                if ($rootScope.previousState && $rootScope.previousParams) {
                    $state.go($rootScope.previousState, $rootScope.previousParams);
                }
            });
        } else {
            $rootScope.submitted = false;
            bootbox.alert({
                message: $translate.instant('form_invalid'),
                buttons: {
                    ok: {
                        label: $translate.instant('close')
                    }
                },
                callback: function() {
                    return;
                }
            });
        }
    }
    $scope.hasError = function(field, validation) {
        if (validation) {
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($rootScope.submitted && $scope.addForm[field].$error[validation]);
        }
        return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($rootScope.submitted && $scope.addForm[field].$invalid);
    };
}]);