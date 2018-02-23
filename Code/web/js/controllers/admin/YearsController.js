angular.module('enozomApp').controller('YearsController', ['$rootScope', '$state', '$scope', '$translate', 'CRUDFactory', 'UsersFactory', 'NgTableParams', 'yearFactory', 'settings', function($rootScope, $state, $scope, $translate, CRUDFactory, UsersFactory, NgTableParams, yearFactory, settings) {


    $scope.initial = function() {

        var initialParams = {
            count: settings.pageSize // initial page size
        };
        var initialSettings = {
            counts: [],
            getData: function(params) {
                return CRUDFactory.getList('Year').then(function(content) {
                    return $scope.years = content.data;
                }, function(error) {
                    console.log("error");
                });
            }
        };
        $scope.yearsTable = new NgTableParams(initialParams, initialSettings);
    }

    $scope.editYear = function(Id) {
        $state.go("Edit Year", { id: Id });
    }

    $scope.deleteYear = function(id, name) {
        bootbox.confirm("Are you sure you want to delete " + name + " Grade?", function(result) {
            if (result) {
                CRUDFactory.delete('Year', id).then(function(succ) {
                        if (succ.status == 200) {
                            $scope.initial();
                        }
                    },
                    function(error) { console.log("delete error") });
            }
        });
    }

    $scope.initial();

}]);