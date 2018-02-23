angular.module('enozomApp').controller('GradesController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', 'NgTableParams',
    'gradeFactory', 'RequestFactory', 'GridService', 'settings', '$q',
    function($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, NgTableParams, gradeFactory, RequestFactory, GridService, settings, $q) {

        $scope.selectSchool = 0;
        $scope.filterObject = {};
        $scope.filterObject.SearchObject = {};
        $scope.selectSchool = "".toString();

        $scope.currentUser = UsersFactory.getCurrentUser()
        $scope.schools = CRUDFactory.getList('Schools')

        $q.all([$scope.schools, $scope.currentUser]).then(result => {
            $scope.schoolData = result[0].data;
            $scope.user = result[1].data;
            if ($scope.user.SchoolId != null) {
                $scope.Grade = {}
                $scope.selectSchool = $scope.user.SchoolId.toString()
                $scope.disableSchool = true
            }
            $scope.initialView();
        })

        $scope.initialView = function() {

            var initialParams = {
                count: settings.pageSize // initial page size
            };
            var initialSettings = {
                // page size buttons (right set of buttons in demo)
                counts: [],
                // determines the pager buttons (left set of buttons in demo)
                paginationMaxBlocks: 13,
                paginationMinBlocks: 2,
                getData: function(params) {
                    var orderBy = params.orderBy();
                    var OrderByParam = GridService.getSortExpression(orderBy, 'GradeName');
                    var OrderByDirection = GridService.getSortDirection(orderBy);
                    $scope.filterObject.PageNumber = params.page();
                    $scope.filterObject.SortBy = OrderByParam;
                    $scope.filterObject.SortDirection = OrderByDirection;
                    $scope.filterObject.PageSize = settings.pageSize;
                    $scope.filterObject.SearchObject.SchoolId = $scope.selectSchool;
                    // ajax request to api
                    return CRUDFactory.getPaginatedList("Grade", $scope.filterObject).then(function(result) {
                        params.total(result.data.TotalRecords); // recal. page nav controls
                        return $scope.grades = result.data.Results;
                    });
                }
            };

            $scope.gradesTable = new NgTableParams(initialParams, initialSettings);
        }


        $scope.deleteGrade = function(id, name) {
            bootbox.confirm("Are you sure you want to delete " + name + " Grade?", function(result) {
                if (result) {
                    CRUDFactory.delete('Grade', id).then(function(succ) {
                            if (succ.status == 200) {
                                $scope.initialView();
                            }
                        },
                        function(error) { console.log("delete error") });
                }
            });
        }

        $scope.editGrade = function(Id) {
            $state.go('edit grade', { id: Id });
        }

        $scope.filterList = function() {
            $scope.filterObject.SearchObject.SchoolId = $scope.selectSchool;
            $scope.initialView();
        }




    }
]);