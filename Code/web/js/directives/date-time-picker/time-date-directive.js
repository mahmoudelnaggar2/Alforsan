enozomApp.directive('customtimepicker',['$document', function($document){

return {
  restrict: 'E',
  require: '?ngModel',
  scope: {
    choices: '=',
    selected: '=',
    hstep:'=',
    mstep:'=',
    ismeridian:'='
  },
  templateUrl: 'js/directives/date-time-picker/time-date-tpl.html',
  replace: true,
  link: function(scope, element, attr){
    
      scope.isPopupVisible = false;

      scope.toggleSelect = function(){
        scope.isPopupVisible = !scope.isPopupVisible;
      }

      $document.bind('click', function(event){
        var isClickedElementChildOfPopup = element
          .find(event.target)
          .length > 0;
          
        if (isClickedElementChildOfPopup)
          return;
          
        scope.isPopupVisible = false;
        scope.$apply();
      });
  }
};
}]);