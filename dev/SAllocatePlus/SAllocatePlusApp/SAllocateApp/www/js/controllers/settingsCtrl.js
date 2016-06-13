/**
 * Created by Wery7 on 11/06/2016.
 */
angular.module('tna.sap.controllers')
  .controller('settingsCtrl', function($scope, $rootScope, $ionicModal, $timeout, userService, ApiEndpoint) {
    $scope.ApiEndpoint = ApiEndpoint;

    $scope.save = function() {
      ApiEndpoint.url = $scope.ApiEndpoint.url;
      alert("New end point is saved");
    };
  });
