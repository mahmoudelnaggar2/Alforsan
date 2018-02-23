angular.module('enozomApp').controller('NewApplicantController', ['$state', '$scope', 'CRUDFactory', 'StudentsFactory', 'StudentStatus', 'UsersFactory', '$rootScope', '$q',
    function($state, $scope, CRUDFactory, StudentsFactory, StudentStatus, UsersFactory, $rootScope, $q) {
        $scope.applicant = { ApplicationDate: new Date() }
        $scope.edit = false;
        $scope.submitted = false
        $scope.disableSchool = false
        $scope.freeAdmission = false;

        
        $scope.yearPromise = CRUDFactory.getList("Year")

        $scope.Schools = CRUDFactory.getList("schools")
        $scope.currentUser = UsersFactory.getCurrentUser()
        $q.all([$scope.Schools, $scope.currentUser, $scope.yearPromise]).then((result) => {
            $scope.schools = result[0].data
            $rootScope.user = result[1].data;
            $scope.years = result[2].data;
            $scope.years.forEach(function (item) {
                if (item.IsCurrent == true) {
                    $scope.applicant.JoiningYearId = item.YearId;
                
                }
            });
            if ($rootScope.user.SchoolId) {
                $scope.applicant.SchoolId = $rootScope.user.SchoolId
                $scope.disableSchool = true
                $scope.onSelected($scope.applicant.SchoolId)
            }
        })

        $scope.Discounts = CRUDFactory.getList("DiscountsTypes")
        $scope.currentUser = UsersFactory.getCurrentUser()
        $q.all([$scope.Discounts, $scope.currentUser]).then((result) => {
            $scope.discounts = result[0].data
            //$rootScope.user = result[1].data;
            //if ($rootScope.user.DiscountsTypeID) {
            //    $scope.applicant.SchoolId = $rootScope.user.SchoolId
            //    $scope.disableSchool = true
            //    $scope.onSelected($scope.applicant.SchoolId)
            //}
        })

        $scope.save = function () {
            $scope.edit = true;
            $scope.splitName = $scope.applicant.ApplicantName.split(" ")
            $scope.applicant.FirstName = $scope.splitName[0]
            if ($scope.splitName.length > 1) {
                $scope.applicant.FamilyName = $scope.splitName[$scope.splitName.length - 1]
                $scope.applicant.MiddleName = $scope.splitName.slice(1, $scope.splitName.length - 1).join(" ")
            }
            debugger;
            if ($scope.addForm.$valid && !$scope.validNumber==true) { //submit form if valid
                $scope.applicant.StudentStatusId = StudentStatus.APPLICATIONFEES;
                CRUDFactory.add("students", $scope.applicant).then((result) => {
                    $scope.submitted = true
                    $state.go("dashboard")
                }, (error) => { })
            }
            else {
                bootbox.alert({
                    message: 'Invalid Form!',
                    buttons: {
                        ok: {
                            label: 'close'
                        }
                    },
                    callback: function () {
                        return;
                    }
                });
            }
            
        }
        $scope.reset = function () {
            $scope.applicant = { ApplicationDate: new Date() }
          //  $scope.addForm.$setValidity();
            $scope.addForm.$setPristine();
            $scope.addForm.$setUntouched();
            $scope.validNumber = false;
            $scope.edit = false;
        };
        $scope.onSelected = function(id) {
            if (id == undefined)
                id = 0
            StudentsFactory.getGrades(id).then((result) => {
                $scope.grades = result.data
            }, (error) => {})
        };

        $scope.isNumbervalid = function(number) {
            var id = $scope.applicant.StudentId > 0 ? $scope.applicant.StudentId : 0;
            StudentsFactory.checkApplicationNo(number, id).then((result) => {
                $scope.validNumber = result.data
            })
        }
        $scope.hasError = function(field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };
    }
]);