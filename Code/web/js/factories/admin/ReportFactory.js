angular.module('enozomApp').factory('ReportFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', function ($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory) {
    return {
        StudentPrintOut: function (studentId) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "StudentReports/" + studentId, null, "arraybuffer");
        },
        InterViewReport: function (addedObject) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "StudentReports/Interview", addedObject, "arraybuffer");
        },
        AdmissionReport: function (admissionObj) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "StudentReports/Admission", admissionObj, "arraybuffer");
        },
        PreviousSchoolsReport: function (SchoolID) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "StudentReports/PreviousSchool/"+ SchoolID,null, "arraybuffer");
        },
        UnPaidFeesReport: function () {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "StudentReports/UnPaidFees",null, "arraybuffer");
        },
        PaidFeesReport: function (PaidFeesObj) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "StudentReports/PaidFees", PaidFeesObj, "arraybuffer");
        },
        VoucherReport: function (StudentVoucherID) {
            return RequestFactory.SendRequest('GET', appConfigs.apiBaseURL + "StudentReports/voucher/" + StudentVoucherID, null, "arraybuffer");
        },
        NewApplicantsReport: function (NewApplicantsObj) {
            return RequestFactory.SendRequest('POST', appConfigs.apiBaseURL + "StudentReports/NewApplicants", NewApplicantsObj, "arraybuffer");
        }
    }
}]);