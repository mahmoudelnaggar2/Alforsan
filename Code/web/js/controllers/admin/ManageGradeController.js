angular.module('enozomApp').controller('editGradesController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', 'gradeFactory', '$stateParams', '$q', function($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, gradeFactory, $stateParams, $q) {

    $scope.isPositiveMessage = false;
    $scope.edit = false;
    $scope.isDisable = false;
    $scope.flag = true;
    if ($stateParams.id == undefined) {
        $scope.displayTitle = false;
        $scope.isPositiveMessage = false;
        // $scope.Grade.SchoolId = "".toString();
        $scope.currentUser = UsersFactory.getCurrentUser()
        $scope.schools = CRUDFactory.getList('Schools')

        $q.all([$scope.schools, $scope.currentUser]).then(result => {
            $scope.schoolData = result[0].data;
            $scope.user = result[1].data;
            console.log($scope.user)
            console.log($scope.schoolData)
            if ($scope.user.SchoolId != null) {
                $scope.Grade = {}
                $scope.Grade.SchoolId = $scope.user.SchoolId.toString()
                $scope.disableSchool = true
            }
        })

    } else {
        $scope.displayTitle = true;
        $scope.currentUser = UsersFactory.getCurrentUser()
        $scope.promise1 = CRUDFactory.getList('Schools');
        $scope.promise2 = CRUDFactory.get('Grade', $stateParams.id);
        $q.all([$scope.promise1, $scope.promise2, $scope.currentUser]).then((success) => {
            $scope.schoolData = success[0].data;
            $scope.Grade = success[1].data;
            $scope.user = success[2].data;
            $scope.Grade.SchoolId = $scope.Grade.SchoolId.toString();
            if ($scope.user.SchoolId != null) {
                $scope.disableSchool = true
                $scope.selectSchool = $scope.user.SchoolId.toString()
            }
        }, (error) => { console.log("error at promise", error) });
        $scope.selectCheck = function() {
            $scope.isOrderUnique();
            $scope.isGradeNameUnique();
        }
    }

    $scope.redirect = function() {
        $state.go('grades');
    }

    $scope.isPositiveNumber = function () {

        if ($scope.Grade.Order >= 1)
            $scope.isPositiveMessage = false;
        else
            $scope.isPositiveMessage = true;

    }

    $scope.saveFun = function(valid) {
        $scope.isDisable = true;
        $scope.edit = true;
        $scope.validOrder = false;
        if (valid && !$scope.gradeUniqueMessage && !$scope.orderUniqueMessage) {
           
            if ($stateParams.id) {
                $scope.Grade.GradeId = $stateParams.id;
                CRUDFactory.edit('Grade', $scope.Grade, $stateParams.id).then(function(success) {
                        $state.go('grades');
                    },
                    function (error) {
                        console.log("error", error);
                    });
                $scope.isDisable = false;
                
            } else {
                
                CRUDFactory.add('Grade', $scope.Grade).then(function(success) {
                        if (success.status == 201) {
                            $state.go('grades');
                        }
                    },
                    function(error) {
                        console.log("error", error);
                    });
            }
            $scope.isDisable = false;

        } else {
            $scope.isDisable = false;

            if ($scope.Grade.GradeName!=null)
                $scope.validOrder = true;
            else
                $scope.validOrder = false;

        }

    }

    $scope.continueFun = function(valid) {
        $scope.edit = true;
        if (valid && !$scope.gradeUniqueMessage && !$scope.orderUniqueMessage) {
            $scope.isDisable = true;
            if ($stateParams.id) {
                CRUDFactory.edit('Grade', $scope.Grade, $stateParams.id).then(function(success) {}, function(error) { console.log("error", error); });
            } else {
                CRUDFactory.add('Grade', $scope.Grade).then(function(success) {
                    $scope.Grade.GradeName = null;
                    $scope.Grade.Order = null;
                    },
                    function(error) {
                        console.log("error", error);
                    });
            }
            $scope.isDisable = false;
            $scope.flag = false;
        } else {
            $scope.validOrder = true;
            if ($scope.Grade.GradeName != null)
                $scope.validOrder = true;
            else
                $scope.validOrder = false;
           
        }
    }

    $scope.reset = function () {
        $scope.validOrder = false;
        $scope.edit = false;
        if ($stateParams.id) {
            CRUDFactory.get('Grade', $stateParams.id).then(function(data) {
                $scope.Grade = data.data;
                $scope.Grade.SchoolId = $scope.Grade.SchoolId.toString();
                if ($scope.Grade.GradeName) {
                    $scope.isGradeNameUnique();
                }
                if ($scope.Grade.Order) {
                    $scope.isOrderUnique();
                }
            }, function(error) { console.log("get by id"); });
        } else {
            $scope.Grade = {};
            $scope.gradeUniqueMessage = false;
            $scope.orderUniqueMessage = false;
        }
        $scope.validOrder = false;

       // $scope.createForm.$setValidity();
        $scope.createForm.$setPristine();
        $scope.createForm.$setUntouched();

    }

    $scope.isGradeNameUnique = function() {
        if ($stateParams.id) {
            if ($scope.Grade.SchoolId != undefined) {
                gradeFactory.gradeNameUbique($scope.Grade.GradeName, $scope.Grade.SchoolId, $stateParams.id).then(function(data) {
                        $scope.gradeUniqueMessage = data.data;
                    },
                    function(error) {
                        console.log("gradeUniquemessage error");
                    });
            }


        } else {
            if ($scope.Grade.SchoolId != undefined) {
                gradeFactory.gradeNameUbique($scope.Grade.GradeName, $scope.Grade.SchoolId, 0).then(function(data) {
                        $scope.gradeUniqueMessage = data.data;
                    },
                    function(error) {
                        console.log("gradeUniquemessage error");
                    });
            }

        }

    }


    $scope.isOrderUnique = function() {
        if ($stateParams.id) {
            if ($scope.Grade.Order != undefined && $scope.Grade.SchoolId != undefined) {
                gradeFactory.orderUbique($scope.Grade.Order, $scope.Grade.SchoolId, $stateParams.id).then(function(data) {
                    $scope.orderUniqueMessage = data.data;
                    },
                    function(error) {
                        console.log("gradeUniquemessage error");
                    });
            }


        } else {
            if ($scope.Grade.Order != undefined && $scope.Grade.SchoolId != undefined) {
                gradeFactory.orderUbique($scope.Grade.Order, $scope.Grade.SchoolId, 0).then(function(data) {
                        $scope.orderUniqueMessage = data.data;
                    },
                    function(error) {
                        console.log("gradeUniquemessage error");
                    });
            }

        }

    }

    $scope.selectCheck = function() {
        $scope.flag = true;
        if ($scope.Grade.GradeName) {
            $scope.isGradeNameUnique();
        }
        if ($scope.Grade.Order) {
            $scope.isOrderUnique();
        }
    }
    $scope.hasError = function(field, validation) {
        if (validation) {
            return ($scope.createForm[field].$dirty && $scope.createForm[field].$error[validation] && $scope.flag) || ($scope.edit && $scope.createForm[field].$error[validation] && $scope.flag);
        }
        return ($scope.createForm[field].$dirty && $scope.createForm[field].$invalid) || ($scope.edit && $scope.createForm[field].$invalid);
    };
}]);