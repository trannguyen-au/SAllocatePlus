(function () {
    'use strict';

    angular
        .module('tna.sap.controllers')
        .filter('sendEmailStaffFilter', function () {
            return function (list, isFilter) {
                if (!isFilter || !list || list.length == 0) return list;
                return list.filter(function (item) {
                    return item.selected;
                });
            };
        })
    .controller('jobSendEmailCtrl', ['$scope', '$rootScope', '$routeParams', '$location', 'userService', 'staffService', 'jobService', '$timeout', function (s, $rootScope, $routeParams, $location, userService, staffService, jobService, $timeout) {
        if (typeof ($rootScope.SelectedJobs) == 'undefined' || $rootScope.SelectedJobs.length == 0) {
            $location.url("/" + $routeParams.cc ? $routeParams.cc : "");
        }

        s.listFilter = {
            StaffCostCentre: $routeParams.cc
        };

        s.costCentreList = [];
        s.staffList = [];

        function initData() {
            userService.getCostCentre(true)
            .then(function (result) {
                s.costCentreList = result;
                console.log(result);
            }, function (error) {
                console.log(error);
            });

            s.showStaff();
        }

        s.model = {
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

        s.showStaff = function () {
            staffService.getStaffsForCostCentre(s.listFilter.StaffCostCentre)
                .then(function (result) {
                    s.staffList = result;
                    l(result);
                }, function (error) {
                    l(error);
                });
        };

        s.send = function () {
            var jobIdList = [], staffIdList = [];
            $rootScope.SelectedJobs.forEach(function (item) {
                jobIdList.push(item.BookID);
            });
            s.staffList.filter(function (staff) {
                return staff.selected;
            })
            .forEach(function (staff) {
                staffIdList.push(staff.StaffID);
            });
            jobService.sendJobEmail(jobIdList, staffIdList, $routeParams.cc, s.emailMessage)
            .then(function (result) {
                $location.url('/' + $routeParams.cc);
                console.log(result);
            });
        };

        s.previewMessage = function () {
            jobService.getJobMessageTemplate(s.listFilter.StaffCostCentre)
            .then(function (template) {
                var jobContent = "";
                var count = 1;
                $rootScope.SelectedJobs.forEach(function (job) {
                    jobContent += "Job #" + count + " :\n";
                    jobContent += "Site: " + job.SiteName + "\n";
                    jobContent += "Address location: " + job.SiteAddress + "\n";
                    jobContent += "Team size: " + job.StaffRequired + "\n";
                    jobContent += "Date: " + job.JobDate + " time: " + job.JobTime + "\n";
                    if (job.SupervisorName) {
                        jobContent += "Your supervisor is: " + job.SupervisorName;
                    }
                    jobContent += "Detail information: " + job.JobDetails + "\n";
                    if (count < $rootScope.SelectedJobs.length) {
                        jobContent += "\n\n";
                        count++;
                    }
                    
                });

                s.emailMessage = template.Content.replace(/\{JobList\}/, jobContent);
                s.showEmailCompose = true;
            });
        };



        // load staff list based on value of cost centre
        

        initData();
    }]);

})();