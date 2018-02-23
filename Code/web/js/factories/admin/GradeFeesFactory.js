angular.module('enozomApp').factory('gradeFeeFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        GradeFeesBySchoolId: function (schoolId, yearId, JoiningYearID) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "GradeFees/GradeFeesBySchoolId?schoolId=" + schoolId + "&yearId=" + yearId + "&JoiningYearID=" + JoiningYearID)
        },
        FeeForStudentvoucher: function (studentId, gradeId, studingYearId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "GradeFees/FeeForStudentvoucher?studentId=" + studentId + "&gradeId=" + gradeId + "&studingYearId=" + studingYearId)
        }
    }
}]);