(function () {
    'use strict';

    angular.module('tna.sap.common', [])
    .filter('date', function () {
        return function (dateData) {
            if (typeof (dateData) == 'undefined' || dateData == null)
                return dateData;
            return moment(dateData).format('Y-MM-DD');
        }
    })
    .filter('time', function () {
        return function (timeData) {
            if (typeof (timeData) == 'undefined' || timeData == null)
                return timeData;
            if (timeData.length == 8) {
                return timeData.substr(0, 5);
            }
            return timeData;
        }
    });
})();