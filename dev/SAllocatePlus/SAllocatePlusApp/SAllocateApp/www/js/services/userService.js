/**
 * Created by Wery7 on 11/06/2016.
 */

(function () {
  'use strict';

  angular.module('tna.sap.services')
    .factory('userService', ['$rootScope','ajaxHelper',  '$q', 'ApiEndpoint', '$timeout', function ($rootScope, ajaxHelper, $q, ApiEndpoint, $timeout) {
      return {
        validateLogin: validateLogin,
        loadNewJobData : loadNewJobData,
        setAvailable : setAvailable,
        loadConfirmedJobData : loadConfirmedJobData
      };

      function validateLogin(username, password) {
        return ajaxHelper.post(ApiEndpoint.url + "/Login", {
          UserName : username,
          Password:password
        }, "getCostCentre");
      }

      function loadNewJobData() {
        if(!$rootScope.user || !$rootScope.user.StaffID) {
          return fakeResponse();
        }

        return ajaxHelper.get(ApiEndpoint.url + "/Job/"+$rootScope.user.StaffID,
          "loadNewJobData");
      }

      function loadConfirmedJobData() {
        if(!$rootScope.user || !$rootScope.user.StaffID) {
          return fakeResponse();
        }

        return ajaxHelper.get(ApiEndpoint.url + "/Job/"+$rootScope.user.StaffID,
          "loadNewJobData");
      }

      function setAvailable(jobId, value) {
        return ajaxHelper.post(ApiEndpoint.url + "/JobAvailability/"+$rootScope.user.StaffID, {
          BookID : jobId,
          IsAvailable:value,
          StaffID : $rootScope.user.StaffID
        }, "setAvailable");
      }

      function fakeResponse() {
        var defer =$q.defer();
        $timeout(function() {
          defer.reject();
        }, 500);
        return defer.promise;
      }

    }]);
})();
