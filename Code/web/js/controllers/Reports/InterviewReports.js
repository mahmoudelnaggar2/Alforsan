angular.module('enozomApp').controller('InterviewReportsController', ['$state', '$scope', 'CRUDFactory', 'ReportFactory', 'UsersFactory', '$q', '$rootScope', 'DownLoadService', 'StudentsFactory',
    function($state, $scope, CRUDFactory, ReportFactory, UsersFactory, $q, $rootScope, DownLoadService, StudentsFactory) {

        $scope.InterviewObj = { DateFrom: new Date(), DateTo: new Date(), SchoolId:null };
        $scope.edit = false;

        $scope.InterviewStatusesPromise = CRUDFactory.getList("InterviewStatus");

        $scope.Schools = CRUDFactory.getList("schools");
        $scope.currentUser = UsersFactory.getCurrentUser();
        $q.all([$scope.Schools,$scope.InterviewStatusesPromise, $scope.currentUser]).then((result) => {
            $scope.schools = result[0].data;
            $rootScope.InterviewStatuses = result[1].data;
            $rootScope.user = result[2].data;

            $scope.InterviewStatuses
            if ($rootScope.user.SchoolId) {
                $scope.selectSchool = $rootScope.user.SchoolId;
                $scope.InterviewObj.SchoolId = $scope.selectSchool;
                console.log($scope.selectSchool)
                $scope.onSelected($scope.selectSchool)
                $scope.disableSchool = true;
            }
        });

        $scope.hasError = function(field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };

        $scope.printInterviewReport = function(isValid) {
            $scope.edit = true;
            if (isValid) {
               
                $scope.edit = false;
                $scope.promise1 = ReportFactory.InterViewReport($scope.InterviewObj);
                $q.all([$scope.promise1]).then((success) => {
                    $scope.dataFile = success[0].data;
                    console.log("$scope.dataFile", $scope.dataFile);
                    DownLoadService.downLoadPdf($scope.dataFile, success[0].headers().filename);
                }, (error) => { console.log("error at promise", error) });
            }

        };
        $scope.onSelected = function(id) {
            if (id == undefined)
                id = 0
            StudentsFactory.getGrades(id).then((result) => {
                $scope.grades = result.data
            }, (error) => {})
        }

    }
]);