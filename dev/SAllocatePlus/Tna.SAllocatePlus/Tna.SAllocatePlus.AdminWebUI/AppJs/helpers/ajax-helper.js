angular.module('wn.ajax-helper', [])
.factory('ajaxHelper', ['$http', '$q', '$cacheFactory', '$rootScope', function ajaxHelperFactory($http, $q, $cacheFactory, $rootScope) {
    var ajaxHelper = {
        handleServerError : function(data, status) {},
        Debug : false,
        cache: false,
        init: function ($rootScope) {
            this.cache = $cacheFactory('ajaxHelperCache');
        },
        AjaxWatcher: [],
        get: function (url, watcherId, isCache) {
            var $this = this;
            var deferred = $q.defer();
            if (isCache && this.cache.get(url)) {
                deferred.resolve(this.cache.get(url));
            }
            else {
                var watcher = this.FindWatcher(watcherId);
                watcher.loading = true;
                $http.get(url, {
                    cache: isCache
                })
                .success(function (data, status, header) {
                    if ($this.Debug) console.log({ url: url, watcherId: watcherId, data: data, status:status, header:header });
                    watcher.loading = false;
                    if (data.Message != undefined && data.Message != null) {
                        if (data.IsSuccess) {

                            if (isCache) {
                                $this.cache.put(url, data.Data);
                            }

                            deferred.resolve(data.Data);
                        }
                        else {
                            $this.handleServerError(data);
                            deferred.reject(data);
                        }
                    }
                    else {
                        if (isCache) {
                            $this.cache.put(url, data);
                        }

                        deferred.resolve(data);
                    }
                })
                .error(function (data, status) {
                    watcher.loading = false;
                    deferred.reject(data);
                    $this.handleServerError(data, status);
                });
            }

            return deferred.promise;
        },
        post: function (url, dataInput, watcherId, isShowMessage, isCache) {
            var $this = this;
            var deferred = $q.defer();
            if (isCache && this.cache.get("post" + url)) {
                deferred.resolve(this.cache.get("post" + url));
            }
            else {
                var watcher = this.FindWatcher(watcherId);
                watcher.loading = true;
                $http.post(url, JSON.stringify(dataInput), {
                    cache: isCache,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (data, status, header) {
                    if ($this.Debug) console.log({ url: url, watcherId: watcherId, data: data, status: status, header: header });
                    watcher.loading = false;
                    if (data.Message != undefined && data.Message != null) {
                        if (data.IsSuccess) {

                            if (isCache) {
                                $this.cache.put("post" + url, data.Data);
                            }

                            deferred.resolve(data.Data);
                        }
                        else {
                            $this.handleServerError(data);
                            deferred.reject(data);
                        }
                    }
                    else {
                        if (isCache) {
                            $this.cache.put("post" + url, data);
                        }

                        deferred.resolve(data);
                    }
                })
                .error(function (data, status) {
                    watcher.loading = false;
                    deferred.reject(data);
                    $this.handleServerError(data, status);
                });
            }

            return deferred.promise;
        },
        FindWatcher: function (watcherId) {
            for (var i = 0; i < this.AjaxWatcher.length; i++) {
                if (this.AjaxWatcher[i].id == watcherId) {
                    return this.AjaxWatcher[i];
                }
            }

            // if not found, create a new watcher
            var watcher = {
                id: watcherId,
                loading: false
            };
            this.AjaxWatcher.push(watcher);
            return watcher;
        },
        clearCache : function(key) {
            this.cache.remove(key);
        },
        registerScope: function($scope) {
            $scope.FindWatcher = this.FindWatcher;
            $scope.AjaxWatcher = this.AjaxWatcher;
            if (typeof ($scope.handleServerError) != 'undefined') {
                this.handleServerError = $scope.handleServerError;
            }
        }
    };

    ajaxHelper.init();
    ajaxHelper.registerScope($rootScope);

    return ajaxHelper;
}]);