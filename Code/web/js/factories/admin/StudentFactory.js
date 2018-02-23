angular.module('enozomApp').factory('studentFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        getStudentByGradeIDAndSchollId: function(schoolId, gradeId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/GetStudentBySchoolIdAndGradeId?schoolId=" + schoolId + "&gradeId=" + gradeId)
        },
        transferTostudent: function(addedObject) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "Students/TransferToStudent", addedObject, null);
        },
        searchForStudent: function(filterObject) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "Students/SearchForStudent", filterObject, null);
        },
        StudentByCode: function(code) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Students/StudentByCode?code=" + code)
        },
        getAllStudentsInterView: function (filterObject) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "Students/GetAllStudentsInterView", filterObject, null);
        },
        UpdateStudentInterViewStatus: function (filterObject) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "Students/UpdateStudentInterViewStatus", filterObject, null);
    }
        
    }
}]);