(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
    .controller('jobDetailCtrl', ['$scope', '$rootScope', '$routeParams', 'userService', 'staffService', 'jobService', '$timeout', function ($scope, $rootScope, $routeParams, userService, staffService, jobService, $timeout) {
        $rootScope.AppTitle = "Job Detail";

        $scope.param = {
            CostCentre: $routeParams.cc,
            BookID: null
        };

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
            JobDate: moment().format('Y-MM-D'),
            JobTime: '',
            StaffRequired: 1,
            JobDetails: "",
            JobCostCentre: '',
            JobSupervisor: ''
        };

        $scope.saveModel = function () {
            jobService.saveJobDetail($scope.model)
            .then(function (result) {
                alert("Save success");
                console.log(result);
            }, function (error) {
                console.log(error);
            });
        };

        if (typeof ($routeParams.id) != 'undefined') {
            $scope.param.BookID = $routeParams.id;
            // load job detail data
            jobService.getJobById($routeParams.id)
            .then(function (result) {
                $scope.model = result;

                jobService.getJobAvailability($routeParams.id)
                .then(function (aResult) {
                    $scope.availabilities = aResult;
                }, function (error) {
                    console.log(error);
                });

                console.log(result);
            }, function (error) {
                console.log(error);
            });
        }

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