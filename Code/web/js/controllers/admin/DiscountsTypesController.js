angular.module('enozomApp').controller('DiscountsTypesController', ['$state', '$scope', 'CRUDFactory', function ($state, $scope, CRUDFactory) {

    CRUDFactory.getList("DiscountsTypes").then((result) => {
        $scope.discountsTypes = result.data;
    }, (error) => { })

    $scope.delete = function (id) {
        bootbox.confirm("Are you sure you want to delete this item?", function (result) {
            if (result) {
                CRUDFactory.delete("DiscountsTypes", id).then((result) => {
                    CRUDFactory.getList("DiscountsTypes").then((result) => {
                        $scope.discountsTypes = result.data;
                    }, (error) => { })
                }, (error) => { })
            }
        })
    }
    $scope.edit = function (id) { 
        $state.go("Edit Discounts Type", { 'id': id });
    }

}]);