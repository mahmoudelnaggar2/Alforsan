angular.module('enozomApp').controller('InterViewStatusController', ['$rootScope', '$state', '$scope', '$translate', '$filter', 'CRUDFactory', '$q', 
    '$stateParams', 'settings', 'ParentStatus', 'InterviewStatus', 'StudentStatus', 'Custody', '$timeout', 'UsersFactory', 'ReportFactory', 'DownLoadService', 'Religion', 'Nationality','studentFactory',
    function ($rootScope, $state, $scope, $translate, $filter, CRUDFactory, $q,  $stateParams, settings, ParentStatus,
        InterviewStatus, StudentStatus, Custody, $timeout, UsersFactory, ReportFactory, DownLoadService, Religion, Nationality, studentFactory) {

        $scope.interviewStatus = CRUDFactory.getList("InterviewStatus")
        $scope.currentUser = UsersFactory.getCurrentUser()
        $scope.studentDetail = CRUDFactory.get("Students", $stateParams.id)
        $scope.edit = false;
        $q.all([$scope.studentDetail, $scope.currentUser, $scope.interviewStatus])
            .then((result) => {
                $scope.applicant = result[0].data
                $rootScope.user = result[1].data;
                $scope.interviewStatuses = result[2].data

            });

        $scope.reset = function () {
            $scope.applicant.InterviewComments = '';
            $scope.applicant.InterviewStatusId = 0;
        }

        $scope.back = function () {
            $state.go('InterViews')
        }

        $scope.save = function () {
            $scope.isRequired = $scope.applicant.InterviewStatusId <= 0;
            if (!$scope.isRequired) {
                studentFactory.UpdateStudentInterViewStatus($scope.applicant).then((data) => {
                    if (data.data == true) {
                        $state.go('InterViews')
                    }
                });
            }
        }
        
        $scope.hasError = function (field, validation) {
            if ($scope.addForm[field] != undefined) {
                if (validation) {
                    return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
                }
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
            };

        }
    }
    
]);