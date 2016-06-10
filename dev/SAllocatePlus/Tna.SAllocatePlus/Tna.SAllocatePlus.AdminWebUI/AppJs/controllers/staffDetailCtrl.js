(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
    .controller('staffDetailCtrl', ['$scope', '$rootScope', '$routeParams', 'staffService', 'userService', function (s, rs, rp, staffService, userService) {
        s.param = {
            CostCentre: null,
            StaffID: null
        };

        s.blankModel = {
            StaffCostCentre: null,
            Active: true,
            Email: '',
            FirstName: '',
            SurName: '',
            StaffID: 0,
            AccessList: [],
            RoleList: [],
            UserName:''
        };

        if (rp != null && typeof (rp.id) !== 'undefined') {

            s.param.CostCentre = rp.cc;
            s.param.StaffID = rp.id;

            staffService.getStaff(rp.id)
            .then(function (result) {
                s.model = result;
                l(result);
            }, function (error) {
                l(error);
            });
        }
        else {
            s.model = angular.copy(model);
        }
        
        s.getFullName = function () {
            return s.model?s.model.FirstName + " " + s.model.SurName:"";
        };

        function initData() {
            userService.getCostCentre(true)
            .then(function (result) {
                s.costCentreList = result;
                l(result);
            }, function (error) {
                l(error);
            });

            userService.getAllCostCentre()
            .then(function (result) {
                s.allCostCentres = result;
                l(result);
            }, function (error) {
                l(error);
            });

            userService.getAllRole()
            .then(function (result) {
                s.allRoles = result;
            }, function (error) {
                l(error);
            });
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

        s.isAdmin = function () {
            if (!s.model) return false;

            return s.model.RoleList.filter(function (role) {
                return role == "Administrator";
            }).length > 0;
        };

        s.canAccess = function (costCentre) {
            if (!s.model) return false;
            return s.model.AccessList.filter(function (item) {
                return item.CostCentreCode == costCentre.CostCentreCode;
            }).length > 0;
        };

        s.isInRole = function (role) {
            if (!s.model) return false;
            return s.model.RoleList.filter(function (item) {
                return item == role.RoleName;
            }).length > 0;
        };

        initData();


    }]);

})();