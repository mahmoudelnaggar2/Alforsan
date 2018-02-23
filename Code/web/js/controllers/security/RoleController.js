angular.module('enozomApp').controller('RoleController', ['$rootScope', '$scope', 'CRUDFactory', '$location', '$stateParams', 'RolesFactory', '$translate', '$state',
    function($rootScope, $scope, CRUDFactory, $location, $stateParams, RolesFactory, $translate, $state) {
        //Global Variables
        $scope.role = {};
        $scope.role.RoleRights = []
        $scope.pageTitle = $state.current.data.pageTitle
        $scope.FeaturesRights = [];
        $rootScope.submitted = false;
        $scope.edit = false;
        $scope.RightsCount = 0
            //Decalre Controllers Method
        $scope.save = function() {
            $rootScope.submitted = true;
            $scope.edit = true; console.log("ROLE",$scope.role)
            if ($scope.addForm.$valid) { //submit form if valid
                //if edit
                if ($stateParams.id) {
                    CRUDFactory.edit("Roles", $scope.role, $scope.role.RoleID).success(function(data, status, headers, config) {
                        //   $location.path('roles.html');
                        $state.go('roles');
                    });
                } else { //if add
                    CRUDFactory.add("Roles", $scope.role).success(function(data, status, headers, config) {
                        $state.go('roles');
                    });

                }
            } else {
                $scope.submitted = false;
            }
        };
        $scope.redirect = function() {
            $state.go('roles');
        };
        $scope.hasError = function(field, validation) {
            if (validation) {
                return ($scope.addForm[field].$dirty && $scope.addForm[field].$error[validation]) || ($scope.edit && $scope.addForm[field].$error[validation]);
            }
            return ($scope.addForm[field].$dirty && $scope.addForm[field].$invalid) || ($scope.edit && $scope.addForm[field].$invalid);
        };

        $scope.getRightSelection = function(id) {
            if ($scope.role.RoleRights) {
                var rightItem = $.grep($scope.role.RoleRights, function(e) { return e.RightID == id; });
                if (rightItem.length == 0)
                    return false;
                else
                    return true;
            } else {
                return false;
            }
        }
        $scope.changeRight = function(id) {
            var rightItem = $.grep($scope.role.RoleRights, function(e) { return e.RightID == id; });
            if (rightItem.length == 0) {
                $scope.role.RoleRights.push({ RoleID: -1, RightID: id });
            } else if (rightItem.length != 0) {
                var index = $scope.role.RoleRights.indexOf(rightItem[0]);
                $scope.role.RoleRights[index].RoleID = 0;
            }
        };

        $scope.checkAll = function(event) { 
            if (event.target.checked) {
                $scope.role.RoleRights = []
                $scope.FeaturesRights.forEach(function(value) {
                    value.Rights.forEach(function(data) {
                        $scope.role.RoleRights.push({ RoleID: -1, RightID: data.RightID });
                    })
                });
            } else {
                if ($stateParams.id) { //Update Existing Item
                    RolesFactory.getRole($stateParams.id).success(function(data, status, headers, config) {
                        if (data) { 
                            $scope.role = data;
                            $scope.UserRights = data.RoleRights.length
                                //     $scope.role.RoleRights = data.RoleRights
                                //ON LOAD CODE
                            getdata();
                        }
                    });
                } else { //Create New Item
                    $scope.role.RoleRights = []
                    getdata();
                }

            }
        }

        function getdata() {
            // get Features and Rights
            RolesFactory.getFeaturesRights("roles").success(function(data, status, headers, config) {
                $scope.FeaturesRights = data;
                data.forEach(function(element) {
                    $scope.RightsCount += element.Rights.length
                });
                if ($scope.UserRights == $scope.RightsCount) {
                    $scope.checked = true
                } else {
                    $scope.checked = false
                };
            }).error(function(data, status, headers, config) {
                raiseerror();
            });

        };
        if ($stateParams.id) { //Update Existing Item
            RolesFactory.getRole($stateParams.id).success(function(data, status, headers, config) {
                if (data) {
                    $scope.role = data;
                    $scope.UserRights = data.RoleRights.length
                        //ON LOAD CODE
                    getdata();
                }
            });
        } else { //Create New Item
            getdata();
        }


        $scope.isNamevalid = function (name) { 
            var id = $stateParams.id > 0 ? $stateParams.id : 0;
            RolesFactory.checkName(name, id).then((result) => {
                $scope.validName = result.data
            })
        }
        //END



    }
]);