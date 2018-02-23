angular.module('enozomApp').controller('ManageYearsController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', 'yearFactory', '$stateParams', '$q', function($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, yearFactory, $stateParams, $q) {

    $scope.isPositiveMessage = false;
    $scope.isDisable = false;
    $scope.edit = false;
    $scope.validYear = false;
    if ($stateParams.id == undefined) {
        $scope.displayTitle = false;
        $scope.isCurrent = false;
    } else {
        $scope.displayTitle = true;
        CRUDFactory.get('Year', $stateParams.id).then(function(data) {
            $scope.Year = data.data;
        }, function(error) { console.log("get by id"); });


    }

    $scope.save = function(valid) {
        $scope.edit = true;
      //  $scope.validYear = false;

      //  if ($scope.Year.YearName != null && !$scope.yearNameUniqueMessage)
      //      $scope.validYear = false;
      //  else
      //      $scope.validYear = true; 
      //if ($scope.Year.ShortName != null && !$scope.shortNameUniqueMessage)
      //      $scope.validShortName = false;
      //  else
      //      $scope.validShortName = true;  

        console.log("valid", valid)
        console.log("!$scope.yearNameUniqueMessage", !$scope.yearNameUniqueMessage)
        console.log("!$scope.shortNameUniqueMessage", !$scope.shortNameUniqueMessage)
        if (valid && !$scope.yearNameUniqueMessage && !$scope.shortNameUniqueMessage) {
            $scope.isDisable = true;
            if ($stateParams.id == undefined) {
                CRUDFactory.add('Year', $scope.Year).then(function(data) {
                    $state.go("years");
                }, function(error) { console.log("error save data"); })
                $scope.isDisable = false;
            } else {
                $scope.Year.YearId = $stateParams.id;
                CRUDFactory.edit('Year', $scope.Year, $stateParams.id).then(function(data) {
                    $state.go("years");
                }, function(error) { console.log("error save data"); });
                $scope.isDisable = false;
            }
        } else {
            $scope.isDisable = false;
        }
    }

    $scope.saveAndContinue = function (valid) {
        $scope.edit = true;
        if (valid && !$scope.yearNameUniqueMessage && !$scope.shortNameUniqueMessage) {
            $scope.isDisable = true;
            if ($stateParams.id == undefined) {
                CRUDFactory.add('Year', $scope.Year).then(function (data) {
                    $scope.Year = {};
                }, function (error) { console.log("error save data"); });
                $scope.isDisable = false;

            } else {
                $scope.Year.GradeId = $stateParams.id;
                CRUDFactory.edit('Year', $scope.Year, $stateParams.id).then(function (data) { }, function (error) { console.log("error save data"); });
                $scope.isDisable = false;
            }
            $scope.reset();
        } else {
            $scope.isDisable = false;
        }
       
    }
    $scope.redirect = function() {

        $state.go("years");
    }

    $scope.reset = function () {
        $scope.isPositiveMessage = false;
        $scope.yearNameUniqueMessage = false;
        $scope.shortNameUniqueMessage = false;

        $scope.edit = false;
        if ($stateParams.id) {
            CRUDFactory.get('Year', $stateParams.id).then(function(data) {
                $scope.Year = data.data;
                if ($scope.Year.YearName) {
                    $scope.yearNameUnique();
                }
                if ($scope.Year.ShortName) {
                    $scope.shortNameUnique();
                }
            }, function(error) { console.log("get by id"); });
        } else {
            $scope.Year = 
            {
                YearName: "",
                ShortName:"",
                ApplicationFees :null
            }
        }
      //  $scope.createForm.$setValidity();
        $scope.createForm.$setPristine();
        $scope.createForm.$setUntouched();

    }
    $scope.yearNameUnique = function() {
        if ($stateParams.id == undefined) {
            yearFactory.yearNameUnique($scope.Year.YearName, 0).then(function(data) {
                $scope.yearNameUniqueMessage = data.data;
            }, function(error) { console.log("error yearNameUnique"); });

        } else {
            yearFactory.yearNameUnique($scope.Year.YearName, $stateParams.id).then(function(data) {
                $scope.yearNameUniqueMessage = data.data;
            }, function(error) {
                console.log("error yearNameUnique");
            });
        }

    }
    $scope.shortNameUnique = function() {
        if ($stateParams.id == undefined) {
            if ($scope.Year.ShortName != undefined) {
                yearFactory.shortNameUnique($scope.Year.ShortName, 0).then(function(data) {
                    $scope.shortNameUniqueMessage = data.data;
                }, function(error) { console.log("error shortNameUnique"); });
            }
        } else {
            if ($scope.Year.ShortName != undefined) {
                yearFactory.shortNameUnique($scope.Year.ShortName, $stateParams.id).then(function(data) {
                    $scope.shortNameUniqueMessage = data.data;
                }, function(error) { console.log("error shortNameUnique"); });
            }
        }

        if ($scope.Year.ShortName == true)
            $scope.validShortName = true;
        else
            $scope.validShortName = false;

    }

    $scope.isPositiveNumber = function() {
        if ($scope.Year.ShortName >= 1) {
            $scope.isPositiveMessage = false;
        } else {

            $scope.isPositiveMessage = true;
        }
    }
    $scope.hasError = function(field, validation) {
        if (validation) {
            
            return ($scope.createForm[field].$dirty && $scope.createForm[field].$error[validation] && !$scope.createForm[field].$pristine) || ($scope.edit && $scope.createForm[field].$error[validation]);
        } 
        return ($scope.createForm[field].$dirty && $scope.createForm[field].$invalid && !$scope.createForm[field].$pristine) || ($scope.edit && $scope.createForm[field].$invalid);
    };
}]);