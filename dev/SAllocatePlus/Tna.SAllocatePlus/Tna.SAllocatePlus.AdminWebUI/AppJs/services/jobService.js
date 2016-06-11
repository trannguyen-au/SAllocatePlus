(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('jobService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getJobsForCostCentre: getJobsForCostCentre,
            saveJobDetail: saveJobDetail,
            getJobMessageTemplate: getJobMessageTemplate,
            sendJobEmail: sendJobEmail,
            getJobById: getJobById,
            getJobAvailability: getJobAvailability
        }

        function getJobAvailability(bookId) {
            return ajaxHelper.get(resourceUrl.jobApi + "/" + bookId + '/Availability', "getJobAvailability");
        }

        function getJobById(bookId) {
            return ajaxHelper.get(resourceUrl.jobApi + "/" + bookId, "getJobById");
        }

        function getJobsForCostCentre(costCentre) {
            return ajaxHelper.get(resourceUrl.jobApi + "?costCentre=" + costCentre, "getJobsForCostCentre");
        }

        function saveJobDetail(jobDto) {
            if (jobDto.BookID == 0) {
                return ajaxHelper.post(resourceUrl.jobApi, jobDto, "saveJobDetail");
            }

            return ajaxHelper.post(resourceUrl.jobApi + "/" + jobDto.BookID, jobDto, "saveJobDetail");
        }

        function getJobMessageTemplate(costCentre) {
            return ajaxHelper.get(resourceUrl.jobResource + "/GetEmailTemplate/" + costCentre);
        }

        function sendJobEmail(jobList, staffList, costCentre, content) {
            return ajaxHelper.post(resourceUrl.jobResource + "/SendEmail", {
                JobList: jobList,
                StaffList: staffList,
                CostCentre: costCentre,
                Content: content
            }, "sendJobEmail");
        }
    }]);
})();