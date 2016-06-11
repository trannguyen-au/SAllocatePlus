/**
 * Created by Wery7 on 11/06/2016.
 */
angular.module('tna.sap.controllers')
  .controller('jobDetailCtrl', function($scope, $rootScope, $ionicModal, $location, $timeout, userService, $stateParams) {
    $scope.title = "test";

    function getJobDetail() { // private method
      if(!$rootScope.jobList) return;
      $scope.job = $rootScope.jobList.filter(function(item) {
        return item.BookID == $stateParams.id
      })[0];

      console.log($scope.job);
    }

    function loadJobList() { // private method
      userService.loadNewJobData()
        .then(function(newList) {
          $rootScope.jobList = newList;


          getJobDetail();
        }, function(error) {
          console.log(error);
        });
    }

    getJobDetail();

    // refresh after user login
    $rootScope.$on($rootScope.EVENT_USER_LOGIN, function() {
      loadJobList();
    });

    $scope.setAvailable = function(value) {
      userService.setAvailable($scope.job.BookID, value)
        .then(function(result) {

          // refresh the list
          loadJobList();

          $location.url('/app/job');
        }, function(error) {
          console.log(error);
        });
    }
  });
