(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
    .controller('jobListCtrl', ['$scope', '$rootScope', '$routeParams', '$location', 'jobService', 'userService', function (s, $rootScope, $routeParams,$location, jobService, userService) {
        $rootScope.AppTitle = "Job list";

        s.listFilter = {
            JobCostCentre: undefined,
            Keyword: undefined
        };

        function initData() {
            userService.getCostCentre(true)
            .then(function (result) {
                s.costCentreList = result;
                console.log(result);
            }, function (error) {
                console.log(error);
            });
        }

        s.showJob = function() {
            jobService.getJobsForCostCentre(s.listFilter.JobCostCentre)
            .then(function (result) {
                s.jobList = result;
                console.log(result);
            }, function (error) {
                console.log(error);
            });
        }

        if ($routeParams != null && typeof ($routeParams.cc) != 'undefined') {
            s.listFilter.JobCostCentre = $routeParams.cc;
            s.showJob();
        }

        s.sendEmail = function () {
            var jobList = s.jobList.filter(function (j) {
                return j.selected;
            });
            if (jobList.length == 0) {
                alert('Please select a job first');
                return;
            }

            $rootScope.SelectedJobs = jobList;

            $location.url('/sendEmail/' + s.listFilter.JobCostCentre);
        }

        initData();

        
    }]);

})();