angular.module('enozomApp').controller('ManageNationalityController', ['$scope', '$stateParams', '$state', 'CRUDFactory', 'NationalityFactory', '$timeout',
    function($scope, $stateParams, $state, CRUDFactory, NationalityFactory, $timeout) {
        $scope.pageTitle = $state.current.data.pageTitle
        $scope.submitted = false
        if ($stateParams.id) {
            CRUDFactory.get("Nationalities", $stateParams.id).then((result) => {
                $scope.nationality = result.data;
            }, (error) => {})
        }

        $scope.continue = function() {
            $scope.edit = true
            if ($scope.addForm.$valid && !$scope.validName && !$scope.validPrefix) {
                if ($stateParams.id) {
                    CRUDFactory.edit("Nationalities", $scope.nationality, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $timeout(function() { $scope.submitted = false; }, 1000);
                    }, (error) => {})
                } else {
                    CRUDFactory.add("Nationalities", $scope.nationality).then((result) => {
                        $scope.nationality = {}
                        $scope.submitted = true
                        $timeout(function() { $scope.submitted = false; }, 1000);
                    }, (error) => {})
                }
                $scope.reset();
            }
        }
        $scope.save = function() {
            $scope.edit = true
            if ($scope.addForm.$valid && !$scope.validName && !$scope.validPrefix) {
                if ($stateParams.id) {
                    CRUDFactory.edit("Nationalities", $scope.nationality, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $state.go('Nationalities')
                    }, (error) => {})
                } else {
                    CRUDFactory.add("Nationalities", $scope.nationality).then((result) => {
                        $scope.submitted = true
                        $state.go('Nationalities')
                    }, (error) => {})
                }
            }
        }
        $scope.redirect = function() {
            if ($scope.nationality != undefined) {
                if ($scope.nationality.NationalityName != "" ) {
                    bootbox.confirm("Are you sure you want to cancel without saving any changes ?", function(result) {
                        if (result) {
                            $state.go('Nationalities')
                        }
                    })
                } else {
                    $state.go('Nationalities')
                }
            } else {
                $state.go('Nationalities')
            }
        }
        $scope.reset = function () {
            $scope.validPrefix = false;
            $scope.validName = false;
            $scope.edit = false
            if ($stateParams.id) {
                CRUDFactory.get("Nationalities", $stateParams.id).then((result) => {
                    $scope.nationality = result.data;
                }, (error) => {})
            } else {
                $scope.nationality = {}
            }

             $scope.addForm.$setPristine();
             $scope.addForm.$setUntouched();

        }
        $scope.hasError = function(field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };
        $scope.isNamevalid = function(name) {
            var id = $stateParams.id > 0 ? $stateParams.id : 0;
            NationalityFactory.checkName(name, id).then((result) => {
                $scope.validName = result.data
            })
        }
        $scope.isPrefixvalid = function(prefix) {
            var id = $stateParams.id > 0 ? $stateParams.id : 0;
            if (prefix == undefined) { $scope.validPrefix = false } else {
                NationalityFactory.checkPrefix(prefix, id).then((result) => {
                    $scope.validPrefix = result.data
                })
            }
        }
    }
])