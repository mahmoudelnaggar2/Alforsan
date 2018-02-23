angular.module('enozomApp').controller('ManageFeesTypesController', ['$state', '$scope', 'CRUDFactory', '$stateParams', 'FeesTypesFactory', '$timeout',
    function($state, $scope, CRUDFactory, $stateParams, FeesTypesFactory, $timeout) {
        $scope.pageTitle = $state.current.data.pageTitle
        $scope.feestype = { IsMandatory: true, IsBank: true, IsPayOnce: false, AllowDiscounts: false }
        $scope.submitted = false
        if ($stateParams.id) {
            CRUDFactory.get("FeesTypes", $stateParams.id).then((result) => {
                $scope.feestype = result.data;
            }, (error) => {})
        }

        $scope.continue = function() {
            $scope.edit = true
            if ($scope.addForm.$valid && !$scope.validName) {
                if ($stateParams.id) {
                    CRUDFactory.edit("FeesTypes", $scope.feestype, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $timeout(function() { $scope.submitted = false; }, 1000);
                    }, (error) => {})
                } else {
                    CRUDFactory.add("FeesTypes", $scope.feestype).then((result) => {
                        $scope.feestype = { IsMandatory: true, IsBank: true }
                        $scope.submitted = true
                        $timeout(function() { $scope.submitted = false; }, 1000);
                    }, (error) => {})
                }
                $scope.reset();
            }
        }

        $scope.cancel = function() {
            bootbox.confirm("Are you sure you want to cancel without saving any changes ?", function(result) {
                if (result) {
                    $state.go('fees_types')
                }
            })
        }

        $scope.save = function() {
            $scope.edit = true
            if ($scope.addForm.$valid && !$scope.validName) {
                if ($stateParams.id) {
                    CRUDFactory.edit("FeesTypes", $scope.feestype, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $state.go('fees_types')
                    }, (error) => {})
                } else {
                    CRUDFactory.add("FeesTypes", $scope.feestype).then((result) => {
                        $scope.submitted = true
                        $state.go('fees_types')
                    }, (error) => {})
                }
            }
        }

        $scope.reset = function () {
            $scope.edit = false;
            if ($stateParams.id) {
                CRUDFactory.get("FeesTypes", $stateParams.id).then((result) => {
                    $scope.feestype = result.data;
                }, (error) => {})
            } else {
                $scope.feestype = { IsMandatory: true, IsBank: true, IsPayOnce: false, AllowDiscounts: false }
            }
           // $scope.addForm.$setValidity();
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
            FeesTypesFactory.checkName(name, id).then((result) => {
                $scope.validName = result.data
            })
        }

    }
])