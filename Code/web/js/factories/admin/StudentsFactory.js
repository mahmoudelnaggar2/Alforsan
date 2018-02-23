angular.module('enozomApp').factory('StudentsFactory', ['$http', 'appConfigs', 'RequestFactory', function($http, appConfigs, RequestFactory) {
    return {
        checkApplicationNo: function(no, id) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/ApplicationNoExists?id=" + id + "&code=" + no, null, null)
        },
        getGrades: function(schoolId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Grade/GradeBySchoolId?SchoolId=" + schoolId, null, null)
        },
        StudentByGradeIDAndSchollId: function(studentId, schoolId, gradeId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/StudentBySchoolIdAndGradeId?studentId=" + studentId + "&schoolId=" + schoolId + "&gradeId=" + gradeId)
        },
        UpgradeStudent: function(students) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "Students/UpgradeStudent", students, null)
        },
        validationStudentStatus: function(studentId, toStatus) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/ValidationStudentStatus/" + studentId + "/" + toStatus);
        },
        activateStudent: function(studentId, isActive) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/ActivateStudent/" + studentId + "/" + isActive);
        },
        AddStudent: function (ApplicantObj, FreeAdmission) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "Students/AddStudent/" + FreeAdmission, ApplicantObj)
        },
        NumberOfStudentsFromPreviousSchools: function (SchoolID) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/StudentsFromPreviousSchools/" + SchoolID)
        }

    }
}])