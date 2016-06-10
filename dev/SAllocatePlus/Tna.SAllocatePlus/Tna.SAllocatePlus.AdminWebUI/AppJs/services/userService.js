(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('userService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getCostCentre: getCostCentre,
            getAllRole: getAllRole,
            getAllCostCentre: getAllCostCentre
        }

        function getCostCentre(writeAccess) {
            return ajaxHelper.get(resourceUrl.costCentreApi + "/" + writeAccess, "getCostCentre");
        }
        function getAllRole() {
            return ajaxHelper.get(resourceUrl.roleApi, "getAllRole");
        }
        function getAllCostCentre() {
            return ajaxHelper.get(resourceUrl.costCentreApi, "getAllCostCentre");
        }
    }]);
})();