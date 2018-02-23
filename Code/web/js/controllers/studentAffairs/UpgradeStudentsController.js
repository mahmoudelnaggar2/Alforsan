angular.module('enozomApp').controller('UpgradeStudentsController', ['$document', '$rootScope', '$state', '$scope', '$filter', 'StudentsFactory', 'CRUDFactory', 'UsersFactory', '$q', 'studentFinancialFactory',
    function($document, $rootScope, $state, $scope, $filter, StudentsFactory, CRUDFactory, UsersFactory, $q, studentFinancialFactory) {
        $scope.school = 0
        $scope.grade = 0
        $scope.students = [];
        $scope.selectList = []
        $scope.UpgradeStudents = []
        $scope.selectedAll = false

        $scope.Schools = CRUDFactory.getList("schools")
        $scope.currentUser = UsersFactory.getCurrentUser()
        $q.all([$scope.Schools, $scope.currentUser]).then((result) => {
            $scope.schools = result[0].data
            $rootScope.user = result[1].data;
            if ($rootScope.user.SchoolId) {
                $scope.school = $rootScope.user.SchoolId
                $scope.disableSchool = true
                $scope.updateGrade()
            }
        })
        $scope.updateGrade = function() {
            if ($scope.grade == null)
                $scope.grade = 0
            if ($scope.school == null)
                $scope.school = 0
            StudentsFactory.getGrades($scope.school).then((result) => {
                $scope.grades = result.data
            })
        }
        $scope.getStudents = function() {
            if ($scope.grade == null)
                $scope.grade = 0
            if ($scope.school == null)
                $scope.school = 0
            StudentsFactory.StudentByGradeIDAndSchollId(0, $scope.school, $scope.grade).then((result) => {
                $scope.students = result.data
            })
        }
        $scope.upgradeStudents = function() {
            $scope.checkCheckBox();

            for (var key in $scope.selectList) {
                $scope.students.forEach(function(item) {
                    if (item.StudentId == key && $scope.selectList[key] == true) {
                        $scope.UpgradeStudents.push(item);
                    }
                });
            }
            if (!$scope.checkflag) {
                StudentsFactory.UpgradeStudent($scope.UpgradeStudents).then((data) => {
                    if ($scope.grades[$scope.grades.length - 1].GradeId == $scope.UpgradeStudents[0].GradeId) {
                        angular.element($document[0].getElementsByClassName('successMessage'))[0].innerHTML = "can't upgrade student in Last grade"
                    }
                    $scope.UpgradeStudents = [];
                    $scope.students = []
                    $scope.getStudents();
                    $scope.selectList = []
                }, (error) => {});

            }

        }
        $scope.cancel = function() {
            $state.go('dashboard')
        }

        $scope.checkAll = function(event) {
            if (event.target.checked) {
                $scope.selectedAll = true;
            } else {
                $scope.selectedAll = false;
            }
            angular.forEach($scope.students, function(student) {
                $scope.selectList[student.StudentId] = $scope.selectedAll;
            });
            $scope.checkCheckBox();

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
        };
    }
]);