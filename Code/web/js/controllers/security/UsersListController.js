angular.module('enozomApp').controller('UsersListController', ['$rootScope', '$scope', 'CRUDFactory', 'settings', 'GridService', '$location', '$translate',
    'NgTableParams', '$state', 'UsersFactory', '$q',
    function($rootScope, $scope, CRUDFactory, settings, GridService, $location, $translate, NgTableParams, $state, UsersFactory, $q) {
        //---------------Variables-------------------------//
        $scope.rolesList = [];
        $scope.filterObject = {};
        $scope.filterObject.SearchObject = {}

        var currentUser = UsersFactory.getCurrentUser()
        $q.all([currentUser]).then((result) => {
                $scope.currentUser = result[0].data
                $scope.getList();
                $scope.getRoles();
            })
            //------------------------------User LIST PAGE---------------------------//
        $scope.getList = function() {

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
                    var OrderByParam = GridService.getSortExpression(orderBy, 'UserName');
                    var OrderByDirection = GridService.getSortDirection(orderBy);
                    $scope.filterObject.PageNumber = params.page();
                    $scope.filterObject.SortBy = OrderByParam;
                    $scope.filterObject.SortDirection = OrderByDirection;
                    $scope.filterObject.SearchObject.SchoolId = $scope.currentUser.SchoolId
                        // ajax request to api
                    return CRUDFactory.getPaginatedList("Users", $scope.filterObject).then(function(result) {
                        params.total(result.data.TotalRecords); // recal. page nav controls
                        return result.data.Results;
                    });
                }
            };

            $scope.tableParams = new NgTableParams(initialParams, initialSettings);
        }

        $scope.editRow = function(row) {
            //    $location.path('user/' + row.UserID);
            $state.go('UserEdit', { 'id': row.UserID })
        }

        $scope.deleteRow = function(row) {

            bootbox.confirm({
                message: $translate.instant('confirm'),
                buttons: {
                    confirm: {
                        label: $translate.instant('ok'),
                        className: "btn green"
                    },
                    cancel: {
                        label: $translate.instant('cancel'),
                        className: "bg-grey bg-font-grey"
                    }
                },
                callback: function(result) {
                    if (result)
                        CRUDFactory.delete("Users", row.UserID).success(function(data, status, headers, config) {
                            $scope.getList();
                        });
                }
            });
        }

        $scope.getRoles = function() {
            CRUDFactory.getList("Roles").success(function(data, status, headers, config) {
                $scope.rolesList = data;
            });

        };
        //-----------------------------Filter search and reset--------------------------//

        $scope.reset = function() {
            $scope.filterObject.SearchObject.UserName = null
            $scope.filterObject.SearchObject.FullName = null
            $scope.filterObject.SearchObject.RoleID = null
            $scope.getList();
        }

        $scope.search = function() {
            $scope.getList();
        }
    }
]);