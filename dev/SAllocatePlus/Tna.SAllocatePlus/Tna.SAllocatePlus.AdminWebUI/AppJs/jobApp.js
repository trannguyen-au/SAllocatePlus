(function () {
    'use strict';

    angular.module('jobApp.services', ['wn.ajax-helper']);
    angular.module('jobApp.controllers', ['jobApp.services']);
    

    angular
        .module('jobApp', ['ngRoute', 'wn.ajax-helper',
            'jobApp.controllers', 'jobApp.services'])
        .config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
            $routeProvider.when('/', {
                templateUrl: '/AppJs/templates/jobList.html',
                controller: 'jobListCtrl',
                controllerAs: 'jl'
            })
            .otherwise({
                redirectTo: '/'
            });

            $httpProvider.interceptors.push(function () {
                return {
                    'response': function (response) {
                        if (response.status == 401) {
                            window.location.href = "~/Home/Login";
                        }

                        if (response.data.hasOwnProperty('d')) {
                            if (typeof (response.data.d) == "string") {
                                response.data.d = response.data.d.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1');
                                response.data = eval('(' + response.data.d + ')');
                            }

                            response.data = response.data.d;
                        }
                        return response;
                    }
                }
            });
        }])
    .run(['$rootScope', function ($rootScope) {
        $rootScope.AppTitle = 'Job App';
    }]);

})();