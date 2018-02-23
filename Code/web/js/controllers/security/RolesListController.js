angular.module('enozomApp').controller('RolesListController', ['$rootScope', '$scope', 'CRUDFactory', 'settings', '$state', '$translate', 'NgTableParams', 'GridService',
    function($rootScope, $scope, CRUDFactory, settings, $state, $translate, NgTableParams, GridService) {
        //------------------------------Roles LIST PAGE---------------------------//
        $scope.filterObject = {};
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
                    var OrderByParam = GridService.getSortExpression(orderBy, 'RoleName');
                    var OrderByDirection = GridService.getSortDirection(orderBy);
                    $scope.filterObject.PageNumber = params.page();
                    $scope.filterObject.SortBy = OrderByParam;
                    $scope.filterObject.SortDirection = OrderByDirection;
                    // ajax request to api
                    return CRUDFactory.getPaginatedList("Roles", $scope.filterObject).then(function(result) {
                        params.total(result.data.TotalRecords); // recal. page nav controls
                        return result.data.Results;
                    });
                }
            };

            $scope.tableParams = new NgTableParams(initialParams, initialSettings);
        }

        $scope.editRow = function(row) {
            $state.go('RoleEdit', { 'id': row.RoleID });
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


                        CRUDFactory.delete("Roles", row.RoleID).then((result) => {
                            $scope.getList();
                        }, (error) => {

                            
                        })

                        //CRUDFactory.delete("Roles", row.RoleID).success(function(data, status, headers, config) {
                        //    $scope.getList();
                        //});
                }
            });
        }

        $scope.getList();
    }
]);