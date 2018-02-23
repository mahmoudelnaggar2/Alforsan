angular.module('enozomApp').factory('RequestFactory', ['$http', '$rootScope', 'settings', '$cookieStore', '$location', '$translate', 'appConfigs', 'Upload', '$state', '$timeout',
    function($http, $rootScope, settings, $cookieStore, $location, $translate, appConfigs, Upload, $state, $timeout) {
        return {
            SendRequest: function(method, url, data, responseType) {

                var header = {};
                header['method'] = method;
                header['url'] = url; //+ '?token=' + $cookieStore.get('key');

                if (data) {
                    header['data'] = data;
                }
                if (responseType) {
                    header['responseType'] = responseType;
                }
                var exceptionErrorMessage = $translate.instant('something_went_wrong') + "<br/>";

                $('.page-spinner-bar').removeClass("hide");
                var result = $http(header).success(function(data, status, headers, config) {
                    $('.page-spinner-bar').addClass("hide");
                    if (status == 201 || status == 204) {
                        $rootScope.submitted = false;
                        $rootScope.addSuccessful = true;
                        $timeout(function() {
                            $rootScope.addSuccessful = false;
                        }, 1000);
                        $rootScope.addSuccessful = true;
                    }
                }).error(function(data, status, headers, config) {
                    $('.page-spinner-bar').addClass("hide");
                    if (status === 401) {
                        // window.location = "login.html";
                        $state.go('login');
                    } else if (status === 400) {
                        $rootScope.submitted = false;
                        bootbox.alert({
                            message: $translate.instant('form_invalid'),
                            buttons: {
                                ok: {
                                    label: $translate.instant('close')
                                }
                            },
                            callback: function() {
                                return;
                            }
                        });
                    } else if (status === 409) {
                        $rootScope.submitted = false;
                        bootbox.alert({
                            message: "there is a relation,you cannot delete this record",
                            buttons: {
                                ok: {
                                    label: "cancel"
                                }
                            },
                            callback: function() {
                                return;
                            }
                        });
                    } else if (status === 500) {
                        $rootScope.submitted = false;
                        bootbox.alert({
                            message: exceptionErrorMessage,
                            buttons: {
                                ok: {
                                    label: $translate.instant('close')
                                }
                            },
                            callback: function() {
                                return;
                            }
                        });
                    }
                });
                return result;
            },
            upload: function(method, url, data, file, responseType) {

                var header = {};
                header['method'] = method;
                header['url'] = url; //+ '?token=' + $cookieStore.get('key');

                if (data) {
                    header['fields'] = data;
                }
                if (file) {
                    header['file'] = file;
                }
                if (responseType) {
                    header['responseType'] = responseType;
                }

                var result = Upload.upload(header).error(function(data, status, headers, config) {
                    if (status == 401)
                    //  window.location = "login.html";
                        $state.go('login');
                    else if (status == 500)
                        alert("upload failed");
                });
                return result;
            }
        }
    }
]);

enozomApp.factory('sessionInjector', ['$cookieStore', function($cookieStore) {
    var sessionInjector = {
        request: function(config) {
            // if (!SessionService.isAnonymus) {
            config.headers['cache'] = false;
            config.headers['foobar'] = new Date().getTime();
            config.headers['Authorization'] = 'Bearer ' + $cookieStore.get('key');
            //  }
            return config;
        }
    };
    return sessionInjector;
}]);