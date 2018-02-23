angular.module('enozomApp').controller('SchoolsController', ['$state', '$scope', '$q', 'CRUDFactory', 'UsersFactory', function ($state, $scope,$q, CRUDFactory, UsersFactory) {

    //CRUDFactory.getList("schools").then((result) => {
    //    $scope.schools = result.data;
    //    for (var i = 0; i < $scope.schools.length;i++)
    //    {
    //        $scope.schools[i].canEdit = $scope.canEdit($scope.schools.SchoolId)
    //    }
    //    console.log($scope.schools)
    //}, (error) => {})
 
    $scope.schoolPromise = CRUDFactory.getList('schools');
    $scope.currentUserPromise = UsersFactory.getCurrentUser()
    $q.all([$scope.schoolPromise, $scope.currentUserPromise]).then((success) => {
        $scope.schools = success[0].data;
        $scope.user = success[1].data;
  
        if ($scope.user.SchoolId != null) {
            for (var i = 0; i < $scope.schools.length; i++)
            {
                $scope.schools[i].canEdit= $scope.canEdit($scope.schools[i].SchoolId)
            }
        }
    }, (error) => { console.log("error at promise", error) });


    $scope.canEdit = function(schoolID)
    {
        if ($scope.user.SchoolId == schoolID)
            return false;
        else
            return true;
    }



    $scope.delete = function(id) {
        bootbox.confirm("Are you sure you want to delete this item?", function(result) {
            if (result) {
                CRUDFactory.delete("schools", id).then((result) => {
                    CRUDFactory.getList("schools").then((result) => {
                        $scope.schools = result.data;
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
    $scope.edit = function(id) {
        $state.go("Edit School", { 'id': id });
    }
}]);