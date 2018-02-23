angular.module('enozomApp').service('LanguageService', function () {
    var lang;
    getLanguageKey = function () {
       return lang;
    },
    setLanguageKey = function (langKey) {
        lang = langKey;
        return lang;
    }
    return {
        getLanguageKey: getLanguageKey,
        setLanguageKey: setLanguageKey,
    };
});