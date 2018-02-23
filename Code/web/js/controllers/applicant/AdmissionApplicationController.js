angular.module('enozomApp').controller('AdmissionApplicationController', ['$rootScope', '$state', '$scope', '$translate', '$filter', 'CRUDFactory', '$q', 'StudentsFactory',
    '$stateParams', 'settings', 'ParentStatus', 'InterviewStatus', 'StudentStatus', 'Custody', '$timeout', 'UsersFactory', 'ReportFactory', 'DownLoadService', 'Religion', 'Nationality',
    function($rootScope, $state, $scope, $translate, $filter, CRUDFactory, $q, StudentsFactory, $stateParams, settings, ParentStatus,
        InterviewStatus, StudentStatus, Custody, $timeout, UsersFactory, ReportFactory, DownLoadService, Religion, Nationality) {
        ////////////// Enums/////////////////
        $scope.InterviewStatus = InterviewStatus
        $scope.ParentStatus = ParentStatus
        $scope.StudentStatus = StudentStatus

        //////////// Member Data ///////////////////
        $scope.edit = false;
        $scope.siblingCheck = false
        $scope.validNumber = false
        $scope.interviewCheck = false
        $scope.submitted = false
        $scope.tab1 = true
        $scope.isStudent = true
        $scope.disableName = false
        $scope.mobilePattern = "^[0-9]*$"
        $scope.grades = []
        $scope.allSiblings = []
        $scope.fromDate = {};
        $scope.fromDate.date = new Date();
        $scope.nationality = CRUDFactory.getList("Nationalities")
        $scope.religion = CRUDFactory.getList("Religions")
        $scope.gender = CRUDFactory.getList("Gender")
        $scope.student = CRUDFactory.get("Students", $stateParams.id)
        $scope.school = CRUDFactory.getList("schools")
        $scope.parentsStatus = CRUDFactory.getList("ParentStatus")
        $scope.childCustody = CRUDFactory.getList("Custody")
        $scope.interviewStatus = CRUDFactory.getList("InterviewStatus")

            ///////////////////// Get current user and check if have access to student///////////////////////////////////
        $scope.currentUser = UsersFactory.getCurrentUser()
        $scope.yearPromise = CRUDFactory.getList("Year")
        $scope.LanguagesPromise = CRUDFactory.getList("Languages")

         
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* Get Data from Database*/
        $q.all([$scope.nationality, $scope.religion, $scope.gender, $scope.student, $scope.school, $scope.parentsStatus, $scope.childCustody, $scope.interviewStatus, $scope.currentUser, $scope.yearPromise, $scope.LanguagesPromise])
            .then((result) => {
                $scope.nationalities = result[0].data
                $scope.religions = result[1].data
                $scope.genders = result[2].data

                $scope.applicant = result[3].data
                 
                $scope.schools = result[4].data
                $scope.parentsStatuses = result[5].data
                $scope.custodys = result[6].data
                $scope.contacts = $scope.applicant.Emergencies
                $scope.interviewStatuses = result[7].data
                $rootScope.user = result[8].data;


                $scope.years = result[9].data;

                $scope.Languages = result[10].data;


                StudentsFactory.getGrades($scope.applicant.SchoolId).then((result) => {
                    $scope.studentGrades = result.data
                }, (error) => {})
                if ($rootScope.user.SchoolId != $scope.applicant.SchoolId && $rootScope.user.SchoolId != null) {
                    $state.go('dashboard')
                }
                $scope.defaultValue()
            }, (error) => {
                if (error.status == 404) {
                    $state.go('dashboard')
                }
            });
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*default value to bind data correctly */
        $scope.defaultValue = function () {
            if ($scope.applicant.InterviewStatusId == InterviewStatus.ACCEPTED && $scope.applicant.StudentStatusId == StudentStatus.STUDENT) {
                $scope.isStudent = false;
                $scope.disableName = true;
            }
            //if($scope.applicant.DateOfBirth != null)
            //   $scope.applicant.DateOfBirth = $scope.applicant.DateOfBirth.split('T')[0];
            if ($scope.applicant.DateOfBirth != null && $scope.applicant.DateOfBirth != undefined) {
                $scope.applicant.DateOfBirth = new Date($scope.applicant.DateOfBirth)
            }
            if ($scope.applicant.Father == null)
            {
                $scope.applicant.Father = {
                    'Name': $scope.applicant.MiddleName
                }
            }
           
            $scope.applicant.ApplicationNo = parseInt($scope.applicant.ApplicationNo)
            $scope.applicant.ApplicationDate = new Date($scope.applicant.ApplicationDate)
          //  $scope.applicant.DateOfBirth = new Date($scope.applicant.DateOfBirth)
            if ($scope.applicant.Emergencies.length == 0)
                $scope.contacts = [{}]
            $scope.siblings = $scope.applicant.Siblings
            if ($scope.applicant.Siblings.length == 0) {
                $scope.siblingCheck = false
                $scope.siblings = [{}]
            } else {
                if ($scope.applicant.Siblings.length > 1)
                    $scope.siblingRemoved = false
                $scope.siblingCheck = true
                var SiblingGrades = []
                var SiblingNames = []
                var siblings = []
                $scope.applicant.Siblings.forEach(function(value) {
                    siblings.push(CRUDFactory.get("Students", value.SiblingStudentId))
                });
                $q.all(siblings).then((result) => {
                    result.forEach(function(value, index) {
                        $scope.siblings[index] = value.data
                        SiblingGrades.push(StudentsFactory.getGrades(value.data.SchoolId))
                        if (value.data.GradeId != undefined)
                            SiblingNames.push(StudentsFactory.StudentByGradeIDAndSchollId($scope.applicant.StudentId, value.data.SchoolId, value.data.GradeId))
                        $q.all(SiblingGrades).then((result) => {
                            result.forEach(function(value, index) {
                                $scope.grades[index] = []
                                $scope.grades[index] = value.data
                            })
                        })
                        $q.all(SiblingNames).then((result) => {
                            result.forEach(function(value, index) {
                                $scope.allSiblings[index] = value.data
                            })
                        })
                    })
                });
            }
            if ($scope.applicant.Emergencies.length > 1)
                $scope.emergencyRemoved = false
            if ($scope.applicant.InterviewDate != null) {
                $scope.interviewCheck = true
                $scope.fromDate.date = new Date($scope.applicant.InterviewDate);
            }
            $scope.applicant.Mother = $scope.applicant.Mother || {}
            $scope.applicant.Father = $scope.applicant.Father || {}

            if ($scope.applicant.ParentStatusId == null || $scope.applicant.ParentStatusId == ParentStatus.Married)
                $scope.parentsStatus = ParentStatus.Married
            else
                $scope.parentsStatus = ParentStatus.Divorced
            $scope.applicant.GrandparentsLiveWithTheFamily = $scope.applicant.GrandparentsLiveWithTheFamily || false
            $scope.applicant.ParentStatusId = $scope.applicant.ParentStatusId || ParentStatus.Married
            $scope.applicant.CustodyId = $scope.applicant.CustodyId || Custody.MOTHER
            $scope.applicant.Transportation = $scope.applicant.Transportation || false
            $scope.applicant.MedicalIssues = $scope.applicant.MedicalIssues || false
            $scope.applicant.RegularMedication = $scope.applicant.RegularMedication || false
            $scope.applicant.ReligionId = $scope.applicant.ReligionId || Religion.Muslim
            $scope.applicant.NationalityId = $scope.applicant.NationalityId || Nationality.Egyptian
            $scope.applicant.Father.NationalityId = $scope.applicant.Father.NationalityId || Nationality.Egyptian
            $scope.applicant.Mother.NationalityId = $scope.applicant.Mother.NationalityId || Nationality.Egyptian
        };
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* start Add new buttons add and remove for basic info taab and addtional info tabs */
        //siblings and Emergency Contacts
        $scope.siblingRemoved = true;
        $scope.emergencyRemoved = true;
        $scope.addNew = function(typeKey, num) {
            if ($scope[typeKey].length + 1 <= num) {
                $scope[typeKey].push({ 'StudentId': parseInt($stateParams.id) });
                if (typeKey == 'contacts') {

                    $scope.emergencyDisabled = false;
                } else {
                    $scope.siblingDisabled = false;
                }

            }
            if ($scope[typeKey].length + 1 >= num + 1) {
                if (typeKey == 'contacts') {
                    $scope.emergencyDisabled = true;
                } else {
                    $scope.siblingDisabled = true;
                }

            }
            if ($scope[typeKey].length > 1) {
                if (typeKey == 'contacts') {
                    $scope.emergencyRemoved = false;
                } else {
                    $scope.siblingRemoved = false;
                }
            }
        };
        $scope.remove = function(typeKey, index) {
            if ($scope[typeKey].length + 1 > 1) {
                $scope[typeKey].splice(index, 1)
                if (typeKey == 'contacts') {
                    $scope.emergencyDisabled = false;
                } else {
                    $scope.grades.splice(index, 1)
                    $scope.allSiblings.splice(index, 1)
                    $scope.siblingDisabled = false;
                }
            }
            if ($scope[typeKey].length == 1) {
                if (typeKey == 'contacts') {
                    $scope.emergencyRemoved = true;
                } else {
                    $scope.siblingRemoved = true;
                }
            }
        };

        $scope.birthDateMask = function () { 
            var v = this.value;
            if (v.match(/^\d{2}$/) !== null) {
                this.value = v + '/';
            } else if (v.match(/^\d{2}\/\d{2}$/) !== null) {
                this.value = v + '/';
            }
        }

        /* End Add new buttons add and remove for basic info taab and addtional info tabs */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* Action Buttons */
        $scope.reset = function() {
            CRUDFactory.get("Students", $stateParams.id).then((result) => {
           
                $scope.applicant = result.data
                $scope.defaultValue()
            })
        }
        $scope.back = function() {
            $state.go('dashboard')
        }
        $scope.save = function () {
            if ($scope.siblingCheck) {
                $scope.siblings.forEach(function(value, index) {
                    $scope.applicant.Siblings[index] = { StudentId: $scope.applicant.StudentId, SiblingStudentId: value.StudentId }
                })
            } else {
                $scope.applicant.Siblings = []
            }
            if ($scope.contacts[0].ContactName == undefined)
                $scope.applicant.Emergencies = []
            else {
                $scope.contacts[0].StudentId = parseInt($stateParams.id)
                $scope.applicant.Emergencies = $scope.contacts
            }
            if ($scope.interviewCheck)
                $scope.applicant.InterviewDate = $filter('date')($scope.fromDate.date, settings.ServerFormat)
            else
                $scope.applicant.InterviewDate = null
            if ($scope.applicant.ParentStatusId == ParentStatus.Married)
                $scope.applicant.CustodyId = null
            if ($scope.FamilyForm.$valid && $scope.interviewCheck == false) {
                $scope.applicant.StudentStatusId = StudentStatus.APPLICATIONNOTENTERED
            } else if ($scope.FamilyForm.$valid && !$scope.validNumber && $scope.interviewCheck && $scope.applicant.InterviewDate != null && $scope.applicant.InterviewStatusId == null) {
                $scope.applicant.StudentStatusId = StudentStatus.WAITINGINTERVIEW
            } else if ($scope.FamilyForm.$valid && !$scope.validNumber && $scope.interviewCheck && $scope.applicant.InterviewDate != null && $scope.applicant.InterviewStatusId != null && $scope.applicant.StudentStatusId < StudentStatus.STUDENT) {
                $scope.applicant.StudentStatusId = StudentStatus.FORMCOMPLETED
            } else if ($scope.FamilyForm.$valid && !$scope.validNumber && $scope.interviewCheck && $scope.applicant.InterviewDate != null && $scope.applicant.InterviewStatusId == InterviewStatus.ACCEPTED && $scope.applicant.StudentStatusId == StudentStatus.STUDENT) {
                $scope.applicant.StudentStatusId = StudentStatus.STUDENT
            }
            $scope.defaultValue();
            if ($scope.BasicForm.grade.$valid) {
                $scope.submitted = true
                
                if ($scope.applicant.DateOfBirth != null && $scope.applicant.DateOfBirth!=undefined) {
                    $scope.applicant.DateOfBirth = $filter('date')($scope.applicant.DateOfBirth, settings.ServerFormat);
                }
                
                $scope.RefineStudentLanguages();
                CRUDFactory.edit("Students", $scope.applicant, $scope.applicant.StudentId).then((result) => {
                    if ($scope.applicant.DateOfBirth != null && $scope.applicant.DateOfBirth != undefined) {
                        $scope.applicant.DateOfBirth = new Date($scope.applicant.DateOfBirth)
                    }
                    $timeout(function() { $scope.submitted = false }, 1000);
                }, (error) => {})
            }
        };
        $scope.RefineStudentLanguages = function () {
            var StudentLanguages = [];
            for (var i = 0; i < $scope.applicant.StudentLanguages.length; i++) {
                if ($scope.applicant.StudentLanguages[i] != undefined && ($scope.applicant.StudentLanguages[i] == true
                   ||
                   $scope.applicant.StudentLanguages[i].LanguageId != undefined)) {
                    if (i > 0 && $scope.applicant.StudentLanguages[i].LanguageId == undefined) {
                        StudentLanguages.push({ LanguageId: i, StudentId: $scope.applicant.StudentId })
                    }
                    else if ($scope.applicant.StudentLanguages[i].LanguageId != undefined) {
                        StudentLanguages.push({ LanguageId: $scope.applicant.StudentLanguages[i].LanguageId, StudentId: $scope.applicant.StudentId })
                    }
                }
            }
            $scope.applicant.StudentLanguages = StudentLanguages;
        }

        $scope.checkBoxClick = function (langId, IsChecked) {
            if (IsChecked == false) {
                $scope.applicant.StudentLanguages.splice(langId, 1);
            } else if (IsChecked == undefined || typeof IsChecked === 'object') {
                 
                var index = $scope.applicant.StudentLanguages.indexOf($filter('filter')($scope.applicant.StudentLanguages, { LanguageId: langId })[0]);
                $scope.applicant.StudentLanguages.splice(index, 1);
            }
        }
       
        $scope.finish = function() {
            $scope.edit = true;
            if (($scope.FamilyForm.$valid && $scope.BasicForm.$valid && $scope.additionalForm.$valid) && !$scope.validNumber) {
                $scope.save();
                $state.go('dashboard')
            } else {
               
                if (!$scope.BasicForm.$valid)
                    $scope.tabChange('1')
                else if (!$scope.additionalForm.$valid)
                    $scope.tabChange('2')
                else if (!$scope.FamilyForm.$valid)
                    $scope.tabChange('3')
                angular.element('input.ng-invalid:first').focus();
            }
        }
        $scope.downloadReport = function() {
            $scope.promise1 = ReportFactory.StudentPrintOut($scope.applicant.StudentId);
            $q.all([$scope.promise1]).then((data) => {
                $scope.dataFile = data[0].data;
                DownLoadService.downLoadPdf($scope.dataFile, data[0].headers().filename);
            }, (error) => { console.log("error at promise", error) });
        }

        /* End of Buttons */
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* Validation Form */

        $scope.hasError = function(field, validation, form) {
            if (form != 'FamilyForm' && form != 'additionalForm') {
                if (validation) {
                    return ($scope.BasicForm[field].$dirty && $scope.BasicForm[field].$error[validation]) || ($scope.edit && $scope.BasicForm[field].$error[validation]);
                }
                return ($scope.BasicForm[field].$dirty && $scope.BasicForm[field].$invalid) || ($scope.edit && $scope.BasicForm[field].$invalid);
            } else if (form == 'additionalForm') {
                if (validation) {
                    return ($scope.additionalForm[field].$dirty && $scope.additionalForm[field].$error[validation]) || ($scope.edit && $scope.additionalForm[field].$error[validation]);
                }
                return ($scope.additionalForm[field].$dirty && $scope.additionalForm[field].$invalid) || ($scope.edit && $scope.additionalForm[field].$invalid);
            } else {
                if (validation) {
                    return ($scope.FamilyForm[field].$dirty && $scope.FamilyForm[field].$error[validation]) || ($scope.edit && $scope.FamilyForm[field].$error[validation]);
                }
                return ($scope.FamilyForm[field].$dirty && $scope.FamilyForm[field].$invalid) || ($scope.edit && $scope.FamilyForm[field].$invalid);
            }

        };
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////  tabs Changing ///////////////////////////////
        $scope.tabChange = function(tab) {
            $scope.tab1 = false
            $scope.tab2 = false
            $scope.tab3 = false
            $scope.tab4 = false
            switch (tab) {
                case '1':
                    $scope.tab1 = true
                    break;
                case '2':
                    $scope.tab2 = true
                    break;
                case '3':
                    $scope.tab3 = true
                    break;
                case '4':
                    $scope.tab4 = true
                    break;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*  Start Basic info Method  */
        $scope.isNumbervalid = function(number) {
            var id = $scope.applicant.StudentId > 0 ? $scope.applicant.StudentId : 0;
            StudentsFactory.checkApplicationNo(number, id).then((result) => {
                $scope.validNumber = result.data
            })
        };
        $scope.updateGrade = function(siblingObj, index) {
            var item = siblingObj.SchoolId;
            if ($scope.applicant.Siblings[index] != undefined) {
                $scope.applicant.Siblings[index].GradeId = null
                $scope.applicant.Siblings[index].SiblingStudentId = null
            }
            StudentsFactory.getGrades(item).then((result) => {
                $scope.grades[index] = result.data;
            }, (error) => {})

        }
        $scope.updateSiblingNames = function(siblingObj, index) {
            if ($scope.applicant.Siblings[index] != undefined) {
                $scope.applicant.Siblings[index].SiblingStudentId = null
            }
            if (siblingObj.GradeId != undefined) {
                StudentsFactory.StudentByGradeIDAndSchollId($scope.applicant.StudentId, siblingObj.SchoolId, siblingObj.GradeId).then((result) => {
                    $scope.allSiblings[index] = []
                    $scope.allSiblings[index] = result.data
                }, (error) => {})
            } else {
                siblingObj.StudentId = null
                $scope.allSiblings[index] = []
            }


        }
        $scope.checkSibling = function(event) {
            if (event.target.checked) {
                $scope.siblingCheck = true
            } else {
                $scope.siblingCheck = false
            }
        };
        $scope.homeNo = function(number) {
            $scope.applicant.Mother.HomeNumber = number
        };


        $scope.IsLanguageChecked = function (LanguageId) {
           return $filter('filter')($scope.applicant.StudentLanguages, { LanguageId: LanguageId })[0]!=undefined;
        }
        /*End of basic info*/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* start Family info tab */
        //parents status
        $scope.updateStatus = function(status) {
            $scope.parentsStatus = status;
        };
        $scope.Address = function(address) {
            $scope.applicant.Mother.HomeAddress = address
        };
        $scope.isActivate = function(active) {
                if (!active) {
                    $scope.applicant.IsSuspend = true;
                } else {
                    $scope.applicant.IsSuspend = false;
                }
                StudentsFactory.activateStudent($scope.applicant.StudentId, $scope.applicant.IsSuspend).then(function(data) {}, function(error) {});
            }
            /* End Family info tab */
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* start interview admission info tab */
        $scope.checkInterview = function(event) {
            if (event.target.checked) {
                $scope.interviewCheck = true
            } else {
                $scope.interviewCheck = false
                $scope.fromDate.date = new Date()
                $scope.applicant.InterviewDate = null
                $scope.applicant.InterviewStatusId = null
                $scope.applicant.InterviewComments = null
            }
        };

        /* Date and time format */
        //date time

        $scope.fullDateTime = function() {
            var date = $filter('date')($scope.fromDate.date, settings.dateFormat) + '  ' + $filter('date')($scope.fromDate.date, 'shortTime');
            return date;

        };
        /* end of date time format */
        /* end interview admission info tab */
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
]);