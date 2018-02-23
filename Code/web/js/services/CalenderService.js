angular.module('enozomApp').service('CalenderService', function () {
    getDateToday = function () {

        var fulClientDateObj= new Date();

        var dateOnly=  new Date(fulClientDateObj.toDateString());

        return dateOnly;
    }

    return {
        getDateToday: getDateToday
    };
   
}); 