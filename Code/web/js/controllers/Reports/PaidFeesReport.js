angular.module('enozomApp').controller('PaidFeesReportController', ['$state', '$scope', 'CRUDFactory', 'ReportFactory', '$q', '$rootScope', 'DownLoadService',
    function ($state, $scope, CRUDFactory, ReportFactory, $q, $rootScope, DownLoadService) {

        $scope.PaidFeesObj = { DateTo: new Date()};
        $scope.edit = false;

        $scope.hasError = function (field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };

        $scope.printPaidFeesReport = function (isValid) {
            $scope.edit = true;
            if (isValid) {

                $scope.edit = false;
                $scope.promise1 = ReportFactory.PaidFeesReport($scope.PaidFeesObj);
                $q.all([$scope.promise1]).then((success) => {
                    $scope.dataFile = success[0].data;
                    DownLoadService.downLoadPdf($scope.dataFile, success[0].headers().filename);
                }, (error) => { console.log("error at promise", error) });
            }

        };

    }
]);