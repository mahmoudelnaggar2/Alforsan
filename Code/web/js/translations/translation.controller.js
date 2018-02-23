(function () {
    angular.module('app.translation').controller('TranslationController', ['$scope', '$rootScope', '$translate', function ($scope, $rootScope, $translate) {
        $scope.language = "English";
        $scope.changeLang = function () {
            if ($scope.language == "English") {
                $scope.language = "عربي";
                $translate.use('ar');
            }
            else {
                $scope.language = "English";
                $translate.use('en');
            }     
            $rootScope.$state.go($rootScope.$state.current, [], {
                location: true, inherit: true, relative: $rootScope.$state.$current, notify: true, reload: true
            });
        };
    }]);
})();