angular.module('enozomApp').controller('NationalitiesController', ['$state', '$scope', '$q', 'CRUDFactory', 'UsersFactory', function ($state, $scope,$q, CRUDFactory, UsersFactory) {

    
    $scope.schoolPromise = CRUDFactory.getList('Nationalities');
    $scope.currentUserPromise = UsersFactory.getCurrentUser()
    $q.all([$scope.schoolPromise, $scope.currentUserPromise]).then((success) => {
        $scope.Nationalities = success[0].data;
        $scope.user = success[1].data;
   
    }, (error) => { console.log("error at promise", error) }); 
     
    $scope.delete = function(id) {
        bootbox.confirm("Are you sure you want to delete this item?", function(result) {
            if (result) {
                CRUDFactory.delete("Nationalities", id).then((result) => {
                    CRUDFactory.getList("Nationalities").then((result) => {
                        $scope.Nationalities = result.data;
                    }, (error) => {

                    })
                }, (error) => {

                    bootbox.alert({
                        message: "school can't be deleted as it's related to grade",
                        buttons: {
                            ok: {
                                label: "cancel"
                            }
                        },
                        callback: function () {
                            return;
                        }
                    });
                })
            }
        })

    }
    $scope.edit = function (id) {
        $state.go("Edit Nationality", { 'id': id });
    }
}]);