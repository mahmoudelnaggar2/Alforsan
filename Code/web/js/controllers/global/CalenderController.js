angular.module('enozomApp').controller('CalenderController', ['$rootScope', '$state', '$scope', '$log', 'settings', function($rootScope, $state, $scope, $log, settings) {
    var calVM = this;
    /////////////////////////////Date Picker Control Settings//////////////////////////////////////
    calVM.today = function() {
        calVM.dt = new Date();

    };
    calVM.today();
    calVM.clear = function() {
        calVM.dt = null;

    };
    calVM.open = function($event) {
        $event.preventDefault();
        $event.stopPropagation();
        calVM.opened = true;

    };

    calVM.dateOptions = {
        formatYear: 'yy',
        startingDay: 1,
        showWeeks: false 
    };

    calVM.format = settings.dateFormat;

    /////////////////////////////Time Picker Control Settings//////////////////////////////////////
    calVM.mytime = new Date();
    calVM.hstep = 1;
    calVM.mstep = 15;
    calVM.meridians = ['ص', 'م'];
    calVM.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30],

    };

    calVM.ismeridian = true;
    calVM.toggleMode = function() {
        calVM.ismeridian = !calVM.ismeridian;
    };

    calVM.update = function() {
        var d = new Date();
        d.setHours(14);
        d.setMinutes(0);
        calVM.mytime = d;
    };

    calVM.changed = function() {
        $log.log('Time changed to: ' + calVM.mytime);
    };

    calVM.clear = function() {
        calVM.mytime = null;
    };



}])