(function () {
    'use strict';

    angular
        .module('jobApp.controllers')
    .controller('jobListCtrl', ['$scope', '$rootScope', function ($scope, $rootScope) {
        $rootScope.AppTitle = "Job list";
    }]);

})();