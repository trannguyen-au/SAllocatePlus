(function () {
    'use strict';

    angular
        .module('staffApp', ['ngRoute', 'wn.ajax-helper', 'tna.sap.common',
            'tna.sap.controllers', 'tna.sap.services', 'ui.bootstrap', 'mgcrea.ngStrap'])
        .config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
            $routeProvider.when('/', {
                templateUrl: '/AppJs/templates/staffList.html',
                controller: 'staffListCtrl'
            }).when('/:cc', {
                templateUrl: '/AppJs/templates/staffList.html',
                controller: 'staffListCtrl'
            }).when('/create/:cc', {
                templateUrl: '/AppJs/templates/staffDetail.html',
                controller: 'staffDetailCtrl'
            }).when('/edit/:cc/:id', {
                templateUrl: '/AppJs/templates/staffDetail.html',
                controller: 'staffDetailCtrl'
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

    }]);

})();