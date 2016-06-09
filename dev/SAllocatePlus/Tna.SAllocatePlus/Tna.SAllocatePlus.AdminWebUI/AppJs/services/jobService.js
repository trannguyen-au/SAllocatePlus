(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('jobService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getJobsForCostCentre: getJobsForCostCentre
        }

        function getJobsForCostCentre(costCentre) {
            return ajaxHelper.get(resourceUrl.jobApi + "?costCentre=" + costCentre, "getJobsForCostCentre");
        }
    }]);
})();