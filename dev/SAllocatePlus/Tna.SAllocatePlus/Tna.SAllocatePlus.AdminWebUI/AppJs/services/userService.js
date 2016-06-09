(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('userService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getCostCentre: getCostCentre
        }

        function getCostCentre(writeAccess) {
            return ajaxHelper.get(resourceUrl.costCentreApi + "?writeAccess=" + writeAccess, "getCostCentre");
        }
    }]);
})();