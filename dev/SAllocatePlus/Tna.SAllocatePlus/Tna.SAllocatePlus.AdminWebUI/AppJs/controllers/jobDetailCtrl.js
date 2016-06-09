(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
    .controller('jobDetailCtrl', ['$scope', '$rootScope', 'userService', 'staffService', '$timeout', function ($scope, $rootScope, userService, staffService, $timeout) {
        $rootScope.AppTitle = "Job Detail";

        $scope.jobStateList = [];
        $scope.costCentreList = [];
        $scope.staffList = [];

        function initData() {

            $scope.jobStateList = [
            { id: 1, value: "Inquiry" },
            { id: 2, value: "Tentative" },
            { id: 3, value: "Booked" },
            { id: 4, value: "Lock In" },
            ];

            userService.getCostCentre(true)
            .then(function (result) {
                $scope.costCentreList = result;
                console.log(result);
            }, function (error) {
                console.log(error);
            });
        }

        $scope.model = {
            BookID: 0,
            SiteName: '',
            SiteAddress: '',
            JobStage: 1,
            JobDate: moment().format('Y-MM-dd'),
            JobTime: '',
            StaffRequired: 1,
            JobDetails: "",
            JobCostCentre: '',
            JobSupervisor: ''
        };

        // load staff list based on value of cost centre
        $scope.$watch('model.JobCostCentre', function (newValue, oldValue) {

            if (!newValue || newValue == "") return;

            if ($scope.promiseStaffRefresh) {
                $timeout.cancel($scope.promiseStaffRefresh);
            }

            $scope.promiseStaffRefresh = $timeout(function () {
                staffService.getStaffsForCostCentre(newValue)
                .then(function (result) {
                    $scope.staffList = result;
                    console.log(result);
                }, function (error) {
                    console.log(error);
                });
            }, 500);
        });

        initData();
    }]);

})();