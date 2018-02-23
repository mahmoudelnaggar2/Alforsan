(function () {
    angular.module('app.translation', ['pascalprecht.translate']);

    angular.module('app.translation').config(function ($translateProvider) {

        $translateProvider.translations('ar', arTranslations);

        $translateProvider.translations('en', enTranslations);
    });
})();