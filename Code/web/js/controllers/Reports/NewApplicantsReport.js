angular.module('enozomApp').controller('NewApplicantsReportController', ['$state', '$scope', 'CRUDFactory', 'ReportFactory', 'UsersFactory', 'StudentsFactory', '$q', '$rootScope', 'DownLoadService',
    function ($state, $scope, CRUDFactory, ReportFactory, UsersFactory, StudentsFactory, $q, $rootScope, DownLoadService) {

        $scope.ReportObj = {}
        $scope.ReportObj.GradeId = null;
        $scope.NewApplicantsObj = {}
        $scope.edit = false;
        $scope.disableSchool = false;
        $scope.Schools = CRUDFactory.getList("schools");
        $scope.currentUser = UsersFactory.getCurrentUser();

        $q.all([$scope.Schools,$scope.currentUser]).then((result) => {
            $scope.schools = result[0].data;
            $rootScope.user = result[1].data;
    
            if ($rootScope.user.SchoolId) {
                $scope.ReportObj.SchoolId = $rootScope.user.SchoolId;
                $scope.onSelected($rootScope.user.SchoolId)
                $scope.disableSchool = true;
            }
        });

        $scope.printNewApplicantsReport = function (reportFormValid, IsPDF) {
            $scope.edit = true;
            if (reportFormValid)
            {
                $scope.NewApplicantsObj = {
                    SchoolId: $scope.ReportObj.SchoolId,
                    GradeId: $scope.ReportObj.GradeId,
                    UserId: $rootScope.user.UserID,
                    IsPDF: IsPDF
                };

                $scope.promise1 = ReportFactory.NewApplicantsReport($scope.NewApplicantsObj);
                $q.all([$scope.promise1]).then((success) => {
                    $scope.dataFile = success[0].data;
                    if(IsPDF)
                        DownLoadService.downLoadPdf($scope.dataFile, success[0].headers().filename);
                    else
                        DownLoadService.downLoadExcel($scope.dataFile, success[0].headers().filename);
                }, (error) => { console.log("error at promise", error) });
            }
        };

        $scope.onSelected = function (id) {
            if (id == undefined)
                id = 0
            StudentsFactory.getGrades(id).then((result) => {
                $scope.grades = result.data
            }, (error) => { })
        };

        $scope.hasError = function (field, validation) {
            if (validation) {
                return ($scope.reportForm[field].$dirty && $scope.reportForm[field].$error[validation]) || ($scope.edit && $scope.reportForm[field].$error[validation]);
            }
            return ($scope.reportForm[field].$dirty && $scope.reportForm[field].$invalid) || ($scope.edit && $scope.reportForm[field].$invalid);
        };

    }
]);