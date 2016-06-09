(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('staffService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getStaffsForCostCentre: getStaffsForCostCentre
        }

        function getStaffsForCostCentre(costCentre) {
            return ajaxHelper.get(resourceUrl.staffApi + "?costCentre=" + costCentre, "getStaffsForCostCentre");
        }
    }]);
})();