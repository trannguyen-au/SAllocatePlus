(function () {
    'use strict';

    angular.module('tna.sap.services')
    .factory('jobService', ['ajaxHelper', '$q', function (ajaxHelper, $q) {
        return {
            getJobsForCostCentre: getJobsForCostCentre,
            saveJobDetail: saveJobDetail,
            getJobMessageTemplate: getJobMessageTemplate
        }

        function getJobsForCostCentre(costCentre) {
            return ajaxHelper.get(resourceUrl.jobApi + "?costCentre=" + costCentre, "getJobsForCostCentre");
        }

        function saveJobDetail(jobDto) {
            if (jobDto.BookID == 0) {
                return ajaxHelper.post(resourceUrl.jobApi, {
                    job: jobDto
                }, "saveJobDetail");
            }

            return ajaxHelper.post(resourceUrl.jobApi + "/" + jobDto.BookID, {
                job: jobDto
            }, "saveJobDetail");
        }

        function getJobMessageTemplate(costCentre) {
            return ajaxHelper.get(resourceUrl.jobResource + "/GetEmailTemplate/" + costCentre);
        }
    }]);
})();