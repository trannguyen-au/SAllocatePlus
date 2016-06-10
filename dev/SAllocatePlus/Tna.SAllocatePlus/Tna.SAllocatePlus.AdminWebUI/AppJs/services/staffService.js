(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('staffService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getStaffsForCostCentre: getStaffsForCostCentre,
            getStaff: getStaff
        }

        function getStaffsForCostCentre(costCentre) {
            return ajaxHelper.get(resourceUrl.staffApi + "?costCentre=" + costCentre, "getStaffsForCostCentre");
        }

        function getStaff(id) {
            return ajaxHelper.get(resourceUrl.staffApi + "/" + id, "getStaff");
        }
    }]);
})();