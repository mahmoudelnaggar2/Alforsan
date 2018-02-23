angular.module('enozomApp').controller('ManageSchoolController', ['$scope', '$stateParams', '$state', 'CRUDFactory', 'SchoolsFactory', '$timeout',
    function($scope, $stateParams, $state, CRUDFactory, SchoolsFactory, $timeout) {
        $scope.pageTitle = $state.current.data.pageTitle
        $scope.submitted = false
        if ($stateParams.id) {
            CRUDFactory.get("schools", $stateParams.id).then((result) => {
                $scope.school = result.data;
            }, (error) => {})
        }

        $scope.continue = function() {
            $scope.edit = true
            if ($scope.addForm.$valid && !$scope.validName && !$scope.validPrefix) {
                if ($stateParams.id) {
                    CRUDFactory.edit("schools", $scope.school, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $timeout(function() { $scope.submitted = false; }, 1000);
                    }, (error) => {})
                } else {
                    CRUDFactory.add("schools", $scope.school).then((result) => {
                        $scope.school = {}
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
                    CRUDFactory.edit("schools", $scope.school, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $state.go('schools')
                    }, (error) => {})
                } else {
                    CRUDFactory.add("schools", $scope.school).then((result) => {
                        $scope.submitted = true
                        $state.go('schools')
                    }, (error) => {})
                }
            }
        }
        $scope.redirect = function() {
            if ($scope.school != undefined) {
                if ($scope.school.SchoolName != "" || $scope.school.Prefix != null) {
                    bootbox.confirm("Are you sure you want to cancel without saving any changes ?", function(result) {
                        if (result) {
                            $state.go('schools')
                        }
                    })
                } else {
                    $state.go('schools')
                }
            } else {
                $state.go('schools')
            }
        }
        $scope.reset = function () {
            $scope.validPrefix = false;
            $scope.validName = false;
            $scope.edit = false
            if ($stateParams.id) {
                CRUDFactory.get("schools", $stateParams.id).then((result) => {
                    $scope.school = result.data;
                }, (error) => {})
            } else {
                $scope.school = {}
            }

            //$scope.addForm.$setValidity();
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
            SchoolsFactory.checkName(name, id).then((result) => {
                $scope.validName = result.data
            })
        }
        $scope.isPrefixvalid = function(prefix) {
            var id = $stateParams.id > 0 ? $stateParams.id : 0;
            if (prefix == undefined) { $scope.validPrefix = false } else {
                SchoolsFactory.checkPrefix(prefix, id).then((result) => {
                    $scope.validPrefix = result.data
                })
            }
        }
    }
])