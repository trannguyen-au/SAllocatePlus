/**
 * Created by Wery7 on 11/06/2016.
 */
angular.module('tna.sap.controllers')
  .controller('confirmedJobCtrl', function($scope, $rootScope, $ionicModal, $timeout, userService) {
    $scope.loadJobList = function () { // private method
      userService.loadConfirmedJobData()
        .then(function(newList) {
          $scope.confirmedJobList = newList;
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
