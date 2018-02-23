angular.module('enozomApp').controller('DashboardController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', 'NgTableParams', 'settings', 'GridService', '$filter', '$q', 'gradeFactory', 'StudentStatusEnum', 'RoleEnum', '$stateParams', 'DownLoadService', 'ReportFactory',
    function ($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, NgTableParams, settings, GridService, $filter, $q, gradeFactory, StudentStatusEnum, RoleEnum, $stateParams, DownLoadService, ReportFactory) {

        $scope.filterObject = {};
        $scope.filterObject.SearchObject = {};
        $scope.filterObject.SearchObject.ApplicationDate = null;
        $scope.filterObject.SearchObject.CheckedInterviewStatus = [];
        $scope.dateFrom = "";
        $scope.dateTo = "";
        $scope.upgradeApllicantStatus = "";
        $scope.upgradeApllicant = {};
        $scope.studentStatusName = "";
        $scope.showDependOnRole = false;
        $scope.hideStudentStatus = true;

        $scope.schoolPromise = CRUDFactory.getList('Schools');
        $scope.studentStatusPromise = CRUDFactory.getList('StudentStatus');
        $scope.interviewStatusPromise = CRUDFactory.getList('InterviewStatus');


        $q.all([$scope.schoolPromise, $scope.studentStatusPromise, $scope.interviewStatusPromise]).then((success) => {
            $scope.schoolData = success[0].data;
            $scope.studentStatusData = success[1].data;
            $scope.interViewStatusData = success[2].data;

            for (var i = 0; i < $scope.studentStatusData.length ; i++) {
                $scope.studentStatusData[i].Checked = false;
            }
            for (var i = 0; i < $scope.interViewStatusData.length ; i++) {
                $scope.interViewStatusData[i].Checked = false;
            }
        }, (error) => { console.log("error at promise", error) });

        UsersFactory.getCurrentUser().success(function (data, status, headers, config) {
            $rootScope.user = data;
            if ($rootScope.user.SchoolId) {
                $scope.filterObject.SearchObject.SchoolId = $rootScope.user.SchoolId;
                $scope.disableSchool = true;
                $scope.selectSchoool();
            }

            $scope.getList();
            if (data.RoleID == RoleEnum.Accountant) {
                $scope.showDependOnRole = true;
            }
        });


        $scope.selectSchoool = function () {
            var id = $scope.filterObject.SearchObject.SchoolId;
            if (id != null) {
                gradeFactory.gradeBySchoolId(id).then(function (data) { $scope.gradeData = data.data; }, function () { })
            } else {
                $scope.gradeData = 0;
            }
        }

        $scope.getList = function () {

            var arr = $scope.studentStatusData;
            if (arr)
                $scope.filterObject.SearchObject.StudentStatusDTO = $scope.studentStatusData;

            if ($stateParams.statusId) {
                $scope.hideStudentStatus = false;
                $scope.filterObject.SearchObject.StudentStatusId = StudentStatusEnum.Student;
            }
            var initialParams = {
                count: settings.pageSize
            };
            var initialSettings = {
                counts: [],
                paginationMaxBlocks: 13,
                paginationMinBlocks: 2,
                getData: function (params) {
                    var orderBy = params.orderBy();
                    var OrderByParam = null
                    var OrderByDirection = null

                    //if ($stateParams.statusId)
                    //{
                    //    OrderByParam = GridService.getSortExpression(orderBy, 'FirstName');
                    //    OrderByDirection = GridService.getSortDirection('+');
                    //}
                    //else
                    //{
                    //   OrderByParam = GridService.getSortExpression(orderBy, 'ApplicationDate');
                    //   OrderByDirection = GridService.getSortDirection('DESC');
                    //}

                    OrderByParam = GridService.getSortExpression(orderBy, 'StudentId');
                    OrderByDirection = GridService.getSortDirection('DESC');
                    $scope.filterObject.PageNumber = params.page();
                    $scope.filterObject.SortBy = OrderByParam;
                    $scope.filterObject.SortDirection = OrderByDirection;
                    $scope.filterObject.PageSize = settings.pageSize;
                    $scope.filterObject.SearchObject.DateTimeFrom = new $filter('date')($scope.dateFrom, settings.ServerFormat);
                    $scope.filterObject.SearchObject.DateTimeTo = new $filter('date')($scope.dateTo, settings.ServerFormat);
                    return CRUDFactory.getPaginatedList("Students", $scope.filterObject).then(function (result) {
                        params.total(result.data.TotalRecords);
                        return $scope.applicants = result.data.Results;
                    });
                }
            }
            $scope.ApplicantsTable = new NgTableParams(initialParams, initialSettings);
        };

        $scope.toggleInterviewStatusCheck = function (interViewStatus) {
            if ($scope.filterObject.SearchObject.CheckedInterviewStatus.indexOf(interViewStatus) === -1) {
                $scope.filterObject.SearchObject.CheckedInterviewStatus.push(interViewStatus);
            } else {
                $scope.filterObject.SearchObject.CheckedInterviewStatus.splice($scope.filterObject.SearchObject.CheckedInterviewStatus.indexOf(interViewStatus), 1);
            }
        }
        $scope.clearSearch = function () {
            $scope.filterObject.SearchObject = {};
            $scope.filterObject.SearchObject.SchoolId = $rootScope.user.SchoolId;
            $scope.selectSchoool();
            $('input[name="dateRange"]').val('');
            $scope.gradeData = 0;
            $scope.dateFrom = "";
            $scope.dateTo = "";
            $scope.getList();
        };

        $scope.go = function (ai) {
            $state.go('AdmissionApp', { id: ai })
        };

        $('input[name="dateRange"]').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format(settings.calenderFormat) + ' - ' + picker.endDate.format(settings.calenderFormat));
            $scope.dateFrom = picker.startDate._d;
            $scope.dateTo = picker.endDate._d;
        });

        $('input[name="dateRange"]').on('cancel.daterangepicker', function (ev, picker) {
            $('input[name="dateRange"]').val('');
        });

        $scope.changeStatus = function (student) {
            $scope.upgradeApllicant = student;
            if (StudentStatusEnum.ApplicationWithParent == $scope.upgradeApllicant.StudentStatus.StudentStatusId) {
                $scope.studentStatusName = Object.keys(StudentStatusEnum)[2];
                $scope.upgradeApllicant.StudentStatusId = StudentStatusEnum.ApplicationNotEntered;
                $scope.confirmUpgrade();
            } else if (StudentStatusEnum.ApplicationFees == $scope.upgradeApllicant.StudentStatus.StudentStatusId) {
                $scope.studentStatusName = Object.keys(StudentStatusEnum)[1];
                $scope.upgradeApllicant.StudentStatusId = StudentStatusEnum.ApplicationWithParent;
                $scope.confirmUpgrade();
            } else {

                $scope.go(student.StudentId);
            }
        };
        $scope.confirmUpgrade = function () {
            bootbox.confirm("Are you sure you want to upgrade applicant status to " + $scope.studentStatusName, function (result) {
                if (result) {
                    $scope.upgradeApllicant.StudentStatus = {};
                    CRUDFactory.edit('Students', $scope.upgradeApllicant, $scope.upgradeApllicant.StudentId).then(function (success) {
                        $scope.upgradeApllicant = {};
                        $scope.studentStatusName = "";
                        $scope.getList();
                    },
                        function (error) { console.log("upgrade  error") });
                } else {
                    $scope.upgradeApllicant = {};
                    $scope.studentStatusName = "";
                }
            });

        };
        $scope.downloadReport = function (id) {
            $scope.promise1 = ReportFactory.StudentPrintOut(id);
            $q.all([$scope.promise1]).then((data) => {
                $scope.dataFile = data[0].data;
                DownLoadService.downLoadPdf($scope.dataFile, data[0].headers().filename);
            }, (error) => { console.log("error at promise", error) });
        }

        $scope.getList();
    }
]);