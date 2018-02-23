
 angular.module('enozomApp').controller('admissionReportsController', ['$state', '$scope', 'CRUDFactory', 'ReportFactory', 'UsersFactory', '$q', '$rootScope', 'DownLoadService', 'StudentsFactory',
 function ($state, $scope, CRUDFactory, ReportFactory, UsersFactory, $q, $rootScope, DownLoadService, StudentsFactory) {

     $scope.AdmissionObj = { DateFrom: new Date(), DateTo: new Date(), SchoolId:null };
     $scope.edit = false;

     $scope.Schools = CRUDFactory.getList("schools");
     $scope.currentUser = UsersFactory.getCurrentUser();
     $q.all([$scope.Schools, $scope.currentUser]).then((result) => {
     $scope.schools = result[0].data
     $rootScope.user = result[1].data;
     if ($rootScope.user.SchoolId) {
        $scope.selectSchool = $rootScope.user.SchoolId;
        $scope.disableSchool = true;
                        }
            });

                    $scope.hasError = function (field, validation) {
                        if (validation) {
                            return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
                        }
                        return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
                    };

                    $scope.printAdmissionReports = function (isValid) {
                        $scope.edit = true;
                        if (isValid) {
                            console.log("$scope.AdmissionObj", $scope.AdmissionObj);
                            $scope.edit = false;
                            $scope.promise1 = ReportFactory.AdmissionReport($scope.AdmissionObj);
                            $q.all([$scope.promise1]).then((success) => {
                                $scope.dataFile = success[0].data;
                                console.log("$scope.dataFile", $scope.dataFile);
                                     DownLoadService.downLoadPdf($scope.dataFile, success[0].headers().filename);
                            }, (error) => { console.log("error at promise", error) });
                        }
                    };
                }
            ]);