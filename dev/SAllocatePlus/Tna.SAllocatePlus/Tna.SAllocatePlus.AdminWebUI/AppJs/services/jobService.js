(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('jobService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getJobsForRegion : getJobsForRegion
        }

        function getJobsForRegion(region) {

        }
    }]);
})();