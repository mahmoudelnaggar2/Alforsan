angular.module('enozomApp').controller('PreviousSchoolsReportsController', ['$state', '$scope', 'CRUDFactory', 'ReportFactory', 'UsersFactory', '$q', '$rootScope', 'DownLoadService', 'StudentsFactory',
    function ($state, $scope, CRUDFactory, ReportFactory, UsersFactory, $q, $rootScope, DownLoadService, StudentsFactory) {

        $scope.SchoolId = null;
        $scope.edit = false;
        $scope.disableSchool = false;

        $scope.Schools = CRUDFactory.getList("schools");

        $scope.currentUser = UsersFactory.getCurrentUser();
        $q.all([$scope.Schools, $scope.currentUser]).then((result) => {
            $scope.schools = result[0].data
            $rootScope.user = result[1].data;
            if ($rootScope.user.SchoolId) {
                $scope.selectSchool = $rootScope.user.SchoolId;
                $scope.SchoolId = $scope.selectSchool;
                $scope.disableSchool = true;
            }
        });

        $scope.hasError = function (field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };

        $scope.PreviousSchooldReport = function (isValid) {
            $scope.edit = true;
            if (isValid) {
                $scope.edit = false;
                //Check count of students transferred from previuos schools
                StudentsFactory.NumberOfStudentsFromPreviousSchools($scope.SchoolId).then((result) => {
                    if(result.data >0)
                    {
                        $scope.promise1 = ReportFactory.PreviousSchoolsReport($scope.SchoolId);
                        $q.all([$scope.promise1]).then((success) => {
                            $scope.dataFile = success[0].data;
                            DownLoadService.downLoadPdf($scope.dataFile, success[0].headers().filename);
                        }, (error) => { console.log("error at promise", error) });
                    }
                    else
                    {
                        bootbox.alert("This School has no Students transferred from previous schools")
                    }
                }, (error) => { })

            }

        };

    }
]);