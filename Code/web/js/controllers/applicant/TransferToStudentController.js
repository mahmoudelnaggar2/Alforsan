angular.module('enozomApp').controller('TransferToStudentController', ['$rootScope', '$state', '$scope', '$translate', '$filter', 'gradeFactory', 'CRUDFactory', 'studentFactory', 'UsersFactory', '$q',
    function($rootScope, $state, $scope, $translate, $filter, gradeFactory, CRUDFactory, studentFactory, UsersFactory, $q) {

        $scope.selectList = [];
        $scope.transferToStudentList = [];
        $scope.selectAll = false;
        $scope.isDisabled = false;
        $scope.disableSchool = false;
        $scope.checkflag = false; 
        $scope.Selectflag = false;

        $scope.Schools = CRUDFactory.getList("schools")
        $scope.currentUser = UsersFactory.getCurrentUser()
        $q.all([$scope.Schools, $scope.currentUser]).then((result) => {
            $scope.schoolData = result[0].data
            $rootScope.user = result[1].data;
            if ($rootScope.user.SchoolId) {
                $scope.selectSchool = $rootScope.user.SchoolId
                $scope.disableSchool = true
                $scope.chooseSchoolId()
            }
        })

        $scope.chooseSchoolId = function() {
            $scope.applicants = {};
            $scope.selectAll = false;
            if ($scope.selectSchool != "" && $scope.selectSchool != null) {
                gradeFactory.gradeBySchoolId($scope.selectSchool).then((data) => { $scope.gradeData = data.data; }, (error) => { console.log("error at promise", error) });
                $scope.selectGrade = "".toString();
            } else {
                $scope.gradeData = [];
                $scope.selectGrade = "".toString();
            }
        };
        $scope.chooseGrade = function() {
            if ($scope.selectGrade != "" && $scope.selectSchool != null) { 
                studentFactory.getStudentByGradeIDAndSchollId($scope.selectSchool, $scope.selectGrade).then((data) => { 
                    $scope.applicants = data.data;
                    (error) => {}
                })
            } else {
                $scope.applicants = {};
                $scope.selectGrade = "".toString();
                $scope.selectAll = false;
            }
        };
        $scope.transferToStudent = function() {
            $scope.CheckBalance = false
            if ($scope.selectGrade != undefined && $scope.selectGrade != null && $scope.selectGrade != "" && $scope.selectSchool != undefined && $scope.selectSchool != null && $scope.selectSchool != "")
            {
                $scope.checkCheckBox();
                for (var key in $scope.selectList) {
                    $scope.applicants.forEach(function(item) {
                        if (item.StudentId == key && $scope.selectList[key] == true) {
                            if (item.Balance == 0) {
                                $scope.transferToStudentList.push(item);
                            }
                        }
                    });
                }
                if (!$scope.checkflag) {
                    $scope.isDisabled = true;
                    studentFactory.transferTostudent($scope.transferToStudentList).then((data) => {
                        $scope.chooseGrade();
                        $scope.transferToStudentList = [];
                        $scope.isDisabled = false;
                    }, (error) => { console.log("error", error); });

                }
            }
            else {
                $scope.Selectflag = true;
                $scope.checkflag = false;
            }
        };

        $scope.CheckAll = (event) => {
            if (event.target.checked) {
                flag = true;
            } else {
                flag = false;
            }
            angular.forEach($scope.applicants, function(student) {
                if (student.Balance == 0)
                    $scope.selectList[student.StudentId] = flag;
            });
            $scope.checkCheckBox();
        };
        $scope.redirect = function() {
            $state.go('dashboard');
        };

        $scope.checkCheckBox = function() {
            var count = 0;
            if ($scope.selectList.length == 0) {
                $scope.checkflag = true;
            }
            $scope.selectList.forEach(function(check) {
                if (check) {
                    count += 1;
                }
            });
            if (count > 0) {
                $scope.checkflag = false;
            } else {
                $scope.checkflag = true;
            }
        };

        $scope.checkBoxClick = function(event) {
            $scope.checkCheckBox();
        }

        $scope.checkBalance = function(Balance) {
            if (Balance != 0)
                return 'Balance must be zero'
            else
                return ''
        }

    }
]);