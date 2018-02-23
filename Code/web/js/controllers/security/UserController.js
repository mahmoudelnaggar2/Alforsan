angular.module('enozomApp').controller('UserController', ['$rootScope', '$scope', 'CRUDFactory', '$location', '$stateParams', '$q', '$translate', '$state', 'UsersFactory',
    function($rootScope, $scope, CRUDFactory, $location, $stateParams, $q, $translate, $state, UsersFactory) {
        $scope.user = {}

        $scope.rolesList = [];
        $rootScope.submitted = false;
        $scope.edit = false;

        var p1 = CRUDFactory.getList("Roles").success(function(data, status, headers, config) {
            $scope.rolesList = data;
        });
        var p2 = CRUDFactory.getList("Schools").success(function(data, status, headers, config) {
            $scope.schools = data;
        })
        var currentUser = UsersFactory.getCurrentUser().then((result) => {
            $scope.currentUser = result.data
            if ($scope.currentUser.SchoolId != null) {
                $scope.disableSchool = true
                $scope.user.SchoolId = $scope.currentUser.SchoolId
            }
        })
        if ($stateParams.id) {
            $q.all([p1, p2]).then(function() {
                CRUDFactory.get("Users", $stateParams.id).success(function(data, status, headers, config) {
                    $scope.user = data;
                });
            });
        }




        $scope.redirect = function() {
            $location.path('users.html');
        }
        $scope.hasError = function(field, validation) {

            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };
        $scope.save = function() {
            $rootScope.submitted = true;
            $scope.edit = true;
            if ($scope.addForm.$valid && !$scope.validName) { //submit form if valid
                if ($stateParams.id) { //if edit
                    CRUDFactory.edit("Users", $scope.user, $scope.user.UserID).success(function(data, status, headers, config) {
                        //  $location.path('users.html');
                        $state.go('users');
                    });
                } else { //if add
                    CRUDFactory.add("Users", $scope.user).success(function(data, status, headers, config) {
                        $state.go('users');
                    });
                }
            } else {
                $rootScope.submitted = false;
            }
        }

        $scope.reset = function () {
            $scope.edit = false;
            if ($stateParams.id) {
                CRUDFactory.get("Users", $stateParams.id).then((result) => {
                    $scope.user = result.data
                })
            } else {
                $scope.user = {}
                $scope.validName = false
            }
           // $scope.addForm.$setValidity();
            $scope.addForm.$setPristine();
            $scope.addForm.$setUntouched();
        }
        $scope.redirect = function () {
            $state.go('users');
        }

        $scope.isNamevalid = function(name) {
            var id = $stateParams.id > 0 ? $stateParams.id : 0;
            UsersFactory.checkName(name, id).then((result) => {
                $scope.validName = result.data
            })
        }
    }
]);