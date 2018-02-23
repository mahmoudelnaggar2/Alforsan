angular.module('enozomApp').factory('UploadFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', 'appConfigs', 'RequestFactory', 'Upload', function ($http, $rootScope, settings, $cookieStore, $location, appConfigs, RequestFactory, Upload) {
        return {
            upload: function (file, path) {
                var data = {path: path};
                return RequestFactory.upload('POST', appConfigs.apiBaseURL + 'Upload?path=' + path, data, file, null);
            }

        }
    }]);

