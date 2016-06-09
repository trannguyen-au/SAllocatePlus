(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
    .controller('jobListCtrl', ['$scope', '$rootScope', 'jobService', 'userService', function ($scope, $rootScope, jobService, userService) {
        $rootScope.AppTitle = "Job list";

        var jl = this;

        jl.listFilter = {
            JobCostCentre: undefined,
            Keyword: undefined
        };

        function initData() {
            userService.getCostCentre(true)
            .then(function (result) {
                jl.costCentreList = result;
                console.log(result);
            }, function (error) {
                console.log(error);
            });
        }

        jl.showJob = function() {
            jobService.getJobsForCostCentre(jl.listFilter.JobCostCentre)
            .then(function (result) {
                jl.jobList = result;
                console.log(result);
            }, function (error) {
                console.log(error);
            });
        }

        initData();

        
    }]);

})();