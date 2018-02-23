angular.module('enozomApp').controller('UnPaidFeesReportController', ['$state', '$scope', 'CRUDFactory', 'ReportFactory', 'UsersFactory', '$q', '$rootScope', 'DownLoadService',
    function ($state, $scope, CRUDFactory, ReportFactory, UsersFactory, $q, $rootScope, DownLoadService) {

        //$scope.currentUser = UsersFactory.getCurrentUser();

        $scope.printUnPaidFeesReport = function () {

            $scope.promise1 = ReportFactory.UnPaidFeesReport();
            $q.all([$scope.promise1]).then((success) => {
                    $scope.dataFile = success[0].data;
                    DownLoadService.downLoadPdf($scope.dataFile, success[0].headers().filename);
                }, (error) => { console.log("error at promise", error) });

        };

    }
]);