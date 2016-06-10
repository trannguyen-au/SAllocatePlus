(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
    .controller('staffListCtrl', ['$scope', '$rootScope', '$routeParams', '$location', 'staffService', 'userService', function (s, $rootScope, $routeParams, $location, staffService, userService) {
        s.listFilter = {
            CostCentre: undefined,
            Keyword: undefined
        };

        function initData() {
            userService.getCostCentre(true)
            .then(function (result) {
                s.costCentreList = result;
                l(result);
            }, function (error) {
                l(error);
            });

            if ($routeParams != null && typeof ($routeParams.cc) != 'undefined') {
                s.listFilter.CostCentre = $routeParams.cc;
                s.showStaff();
            }
        }

        s.showStaff = function () {
            staffService.getStaffsForCostCentre(s.listFilter.CostCentre)
            .then(function (result) {
                s.staffList = result;
                l(result);
            }, function (error) {
                l(error);
            });
        }

        s.editStaff = function (id) {
            $location.url("/edit/" + s.listFilter.CostCentre + "/" + id);
        }

        s.sendMessage = function ($event) {
            alert('ok');
            $event.stopPropagation();
        }

        initData();


    }]);

})();