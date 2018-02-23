angular.module('enozomApp').controller('InterViewsController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', 'NgTableParams', 'settings', 'GridService', '$filter', '$q', 'gradeFactory', 'StudentStatusEnum', 'RoleEnum', '$stateParams', 'DownLoadService', 'ReportFactory', 'studentFactory',
function ($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, NgTableParams, settings, GridService, $filter, $q, gradeFactory, StudentStatusEnum, RoleEnum, $stateParams, DownLoadService, ReportFactory, studentFactory) {

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

    $q.all([$scope.schoolPromise]).then((success) => {
        $scope.schoolData = success[0].data;
    }, (error) => { console.log("error at promise", error) });

    UsersFactory.getCurrentUser().success(function (data, status, headers, config) {
        $rootScope.user = data;
        if ($rootScope.user.SchoolId) {
            $scope.filterObject.SearchObject.SchoolId = $rootScope.user.SchoolId;
            $scope.disableSchool = true;
            $scope.selectSchoool();
        }
        $scope.getList();

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
                 
                OrderByParam = GridService.getSortExpression(orderBy, 'StudentId');
                OrderByDirection = GridService.getSortDirection('DESC');
                $scope.filterObject.PageNumber = params.page();
                $scope.filterObject.SortBy = OrderByParam;
                $scope.filterObject.SortDirection = OrderByDirection;
                $scope.filterObject.PageSize = settings.pageSize;
                $scope.filterObject.SearchObject.DateTimeFrom = new $filter('date')($scope.dateFrom, settings.ServerFormat);
                $scope.filterObject.SearchObject.DateTimeTo = new $filter('date')($scope.dateTo, settings.ServerFormat);
                return studentFactory.getAllStudentsInterView($scope.filterObject).then(function (result) {
                    params.total(result.data.TotalRecords);
                    return $scope.applicants = result.data.Results;
                });
            }
        }
        $scope.ApplicantsTable = new NgTableParams(initialParams, initialSettings);
    };

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
        $state.go('SetInterViewStatus', { id: ai })
    };
    $('input[name="dateRange"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format(settings.calenderFormat) + ' - ' + picker.endDate.format(settings.calenderFormat));
        $scope.dateFrom = picker.startDate._d;
        $scope.dateTo = picker.endDate._d;
    });

    $('input[name="dateRange"]').on('cancel.daterangepicker', function (ev, picker) {
        $('input[name="dateRange"]').val('');
    });

    $scope.getList();

}]);