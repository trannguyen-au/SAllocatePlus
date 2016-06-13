angular.module('tna.sap.controllers')
  .controller('jobListCtrl', function($scope, $rootScope, $ionicModal, $timeout, userService) {
    $scope.title = "test";

    $scope.loadJobList = function() {
      userService.loadNewJobData()
        .then(function(newList) {
          $rootScope.jobList = newList;
        }, function(error) {
          console.log(error);
        });
    };

    $scope.doRefresh = function() {
      $scope.loadJobList();
    };

    $scope.loadJobList();

    // refresh after user login
    $rootScope.$on($rootScope.EVENT_USER_LOGIN, function() {
      $scope.loadJobList();
    });

  });
