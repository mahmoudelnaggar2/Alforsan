angular.module('enozomApp').controller('FeesTypesController', ['$state', '$scope', 'CRUDFactory', 'FeesType', function($state, $scope, CRUDFactory, FeesType) {

    CRUDFactory.getList("FeesTypes").then((result) => {
        $scope.feesTypes = result.data;
    }, (error) => {})

    $scope.delete = function(id) {
        if (id != FeesType.ADMISSIONFEES) {
            bootbox.confirm("Are you sure you want to delete this item?", function(result) {
                if (result) {
                    CRUDFactory.delete("FeesTypes", id).then((result) => {
                        CRUDFactory.getList("FeesTypes").then((result) => {
                            $scope.feesTypes = result.data;
                        }, (error) => {})
                    }, (error) => {})
                }
            })
        } else {
            bootbox.alert({
                message: "there is a relation,you cannot delete this record",
                buttons: {
                    ok: {
                        label: "cancel"
                    }
                },
                callback: function() {
                    return;
                }
            });
        }
    }
    $scope.edit = function(id) {
        $state.go("Edit Fees Type", { 'id': id });
    }

}]);