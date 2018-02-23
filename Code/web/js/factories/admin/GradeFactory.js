angular.module('enozomApp').factory('gradeFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        orderUbique: function(order, schoolId, gradeId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Grade/OrderUnique?order=" + order + "&schoolId=" + schoolId + "&gradeId=" + gradeId)
        },
        gradeNameUbique: function(gradeName, schoolId, gradeId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Grade/GradeNameUnique?gradeName=" + gradeName + "&schoolId=" + schoolId + "&gradeId=" + gradeId)
        },
        gradeBySchoolId: function(schoolId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "Grade/GradeBySchoolId?SchoolId=" + schoolId);
        }
    }
}]);