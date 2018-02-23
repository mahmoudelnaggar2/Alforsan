angular.module('enozomApp').controller('ManageDiscountsTypesController', ['$state', '$scope', 'CRUDFactory', '$stateParams', 'DicountsTypesFactory', '$timeout',
    function ($state, $scope, CRUDFactory, $stateParams, DiscountsTypesFactory, $timeout) { 
        $scope.pageTitle = $state.current.data.pageTitle
        $scope.discountstype = {  }
        $scope.submitted = false
        if ($stateParams.id) {
           
            CRUDFactory.get("DiscountsTypes", $stateParams.id).then((result) => {
                $scope.discountstype = result.data; 
            }, (error) => { })
        }

        $scope.continue = function () {
            $scope.edit = true
            if ($scope.addForm.$valid && !$scope.validName) {
                if ($stateParams.id) {
                    CRUDFactory.edit("DiscountsTypes", $scope.discountstype, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $timeout(function () { $scope.submitted = false; }, 1000);
                    }, (error) => { })
                } else {
                    CRUDFactory.add("DiscountsTypes", $scope.discountstype).then((result) => {
                        $scope.submitted = true
                        $timeout(function () { $scope.submitted = false; }, 1000);
                    }, (error) => { })
                }
                $scope.reset();
            }
        }

        $scope.redirect = function () {
            bootbox.confirm("Are you sure you want to cancel without saving any changes ?", function (result) {
                if (result) {
                    $state.go('discounts_types')
                }
            })
        }

        $scope.save = function () {
            $scope.edit = true
            $scope.isPositiveNumber();
            if ($scope.addForm.$valid && !$scope.validName && !$scope.isPositiveMessage) {
                if ($stateParams.id) {
                    CRUDFactory.edit("DiscountsTypes", $scope.discountstype, $stateParams.id).then((result) => {
                        $scope.submitted = true
                        $state.go('discounts_types')
                    }, (error) => { })
                } else {
                    CRUDFactory.add("DiscountsTypes", $scope.discountstype).then((result) => {
                        $scope.submitted = true
                        $state.go('discounts_types')
                    }, (error) => {
                    })
                }
            }
        }

        $scope.reset = function () {
            $scope.edit = false
            if ($stateParams.id) {
                CRUDFactory.get("DiscountsTypes", $stateParams.id).then((result) => {
                    $scope.discountstype = result.data;
                }, (error) => { })
            } else {
                $scope.discountstype = {
                    'DiscountName': null,
                    'DiscountPercentage' : null
                };
            }
            $scope.isPositiveMessage = false;
         //   $scope.addForm.$setValidity();
            $scope.addForm.$setPristine();
            $scope.addForm.$setUntouched();
        }

        $scope.hasError = function (field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };

        $scope.isNamevalid = function (name) {
            var id = $stateParams.id > 0 ? $stateParams.id : 0;
            DiscountsTypesFactory.checkName(name, id).then((result) => {
                $scope.validName = result.data
            })
        }
        $scope.isPositiveNumber = function () {
            if ($scope.discountstype.DiscountPercentage >= 1) {
                $scope.isPositiveMessage = false;
            } else {

                $scope.isPositiveMessage = true;
            }
        }

    }
])