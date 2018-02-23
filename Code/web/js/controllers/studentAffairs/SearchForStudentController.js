angular.module('enozomApp').controller('SearchForStudentController', ['$rootScope', '$state', '$scope', '$translate', '$filter', 'CRUDFactory',
    'gradeFactory', 'studentFactory', 'UsersFactory', '$q', 'settings', 'NgTableParams', 'GridService', 'StudentStatus',
    function($rootScope, $state, $scope, $translate, $filter, CRUDFactory, gradeFactory, studentFactory, UsersFactory, $q, settings, NgTableParams, GridService, StudentStatus) {
        $scope.students = [];
        $scope.edit = false;
        $scope.studentName = "";
        $scope.selectStudent = "";
        $scope.selectGrade = "".toString();
        $scope.selectSchool = "".toString();
        $scope.filterObject = {};
        $scope.filterObject.SearchObject = {};
        $scope.StudentStatus = StudentStatus
        $scope.StudentStatusId = StudentStatus.STUDENT
        $scope.Schools = CRUDFactory.getList("schools")
        $scope.currentUser = UsersFactory.getCurrentUser()
        $scope.SeachBtnPressed = false;
        $q.all([$scope.Schools, $scope.currentUser]).then((result) => {
            $scope.schoolData = result[0].data
            $rootScope.user = result[1].data;
            if ($rootScope.user.SchoolId) {
                $scope.selectSchool = $rootScope.user.SchoolId;
                $scope.disableSchool = true
                $scope.chooseSchoolId();
                $scope.initialView();
            }
        });

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
                    var OrderByParam = GridService.getSortExpression(orderBy, 'StudentCode');
                    var OrderByDirection = GridService.getSortDirection(orderBy);
                    $scope.filterObject.PageNumber = params.page();
                    $scope.filterObject.SortBy = OrderByParam;
                    $scope.filterObject.SortDirection = OrderByDirection;
                    $scope.filterObject.PageSize = settings.pageSize;
                    $scope.filterObject.SearchObject.SchoolId = $scope.selectSchool;
                    $scope.filterObject.SearchObject.GradeId = $scope.selectGrade;
                    // $scope.filterObject.SearchObject.ApplicantName = $scope.studentName;


                    $scope.splitName = $scope.studentName.split(" ")
                    if (!/[^a-zA-Z]/.test($scope.splitName[0])) //letters only
                    {
                        $scope.filterObject.SearchObject.FirstName = $scope.splitName[0]
                        if ($scope.splitName.length > 1) {
                            $scope.filterObject.SearchObject.FamilyName = $scope.splitName[$scope.splitName.length - 1]
                            $scope.filterObject.SearchObject.MiddleName = $scope.splitName.slice(1, $scope.splitName.length - 1).join(" ")
                        }
                        $scope.filterObject.SearchObject.ApplicationNo = null;
                        $scope.filterObject.SearchObject.StudentCode = null;
                    }
                    else {
                        $scope.filterObject.SearchObject.FirstName = null;
                        $scope.filterObject.SearchObject.ApplicationNo = $scope.splitName[0];
                        $scope.filterObject.SearchObject.StudentCode = $scope.splitName[0];
                    }

                    $scope.filterObject.SearchObject.StudentStatusId = $scope.StudentStatusId;
                    // ajax request to api
                    return studentFactory.searchForStudent($scope.filterObject).then((data) => {
                        params.total(data.data.TotalRecords);
                        return $scope.students = data.data.Results;
                    });
                }
            };
            $scope.studentTable = new NgTableParams(initialParams, initialSettings);
        }

        $scope.chooseSchoolId = function() {
            $scope.students = [];
            if ($scope.selectSchool == null)
                $scope.selectSchool = 0;
            if ($scope.selectSchool != "") {
                gradeFactory.gradeBySchoolId($scope.selectSchool).then((data) => { $scope.gradeData = data.data; }, (error) => { console.log("error at promise", error) });
                $scope.selectGrade = "".toString();
            } else {
                $scope.gradeData = [];
                $scope.selectGrade = "".toString();
            }
            if($scope.SeachBtnPressed == true && $scope.selectSchool !=null)
            {
                $scope.initialView();
            }
        };

        $scope.searchforStudent = function(isvalid) {
            $scope.edit = true;
            $scope.SeachBtnPressed = true;
            if (isvalid) {
                if ($scope.selectSchool == "") {
                    $scope.selectSchool = 0;
                }
                $scope.initialView();
            }
        };
        $scope.returnWithStudent = function() {
            $state.go('issueVoucher');

        }
        $scope.setStudent = function(student) {
            $scope.selectStudent = student;
            $scope.redirect();
        }
        $scope.redirect = function() {
            if ($scope.selectStudent != undefined && $scope.selectStudent != "") {
                $state.go('returnIssueVoucher', { id: $scope.selectStudent.StudentId });
            }
        }

        $scope.hasError = function(field, validation) {
            if (validation) {
                return ($scope.createForm[field].$dirty && $scope.createForm[field].$error[validation] && !$scope.createForm[field].$pristine) || ($scope.edit && $scope.createForm[field].$error[validation]);
            }
            return ($scope.createForm[field].$dirty && $scope.createForm[field].$invalid && !$scope.createForm[field].$pristine) || ($scope.edit && $scope.createForm[field].$invalid);
        };


    }
]);