angular.module('tna.sap.controllers')
  .controller('jobListCtrl', function($scope, $rootScope, $ionicModal, $timeout, userService) {
    $scope.title = "test";

    function loadJobList() { // private method
      userService.loadNewJobData()
        .then(function(newList) {
          $rootScope.jobList = newList;
        }, function(error) {
          console.log(error);
        });
    }

    loadJobList();

    // refresh after user login
    $rootScope.$on($rootScope.EVENT_USER_LOGIN, function() {
      loadJobList();
    });

  });
