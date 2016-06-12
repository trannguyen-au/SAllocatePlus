// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'tna.sap.common','tna.sap.controllers', 'tna.sap.services', 'wn.ajax-helper'])

.run(function($ionicPlatform, $rootScope, $ionicModal, userService) {
  $ionicPlatform.ready(function() {
    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
    if (window.cordova && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);

    }
    if (window.StatusBar) {
      // org.apache.cordova.statusbar required
      StatusBar.styleDefault();
    }

    if(!$rootScope.user) {
      $ionicModal.fromTemplateUrl('templates/login.html', {
        scope: $rootScope
      }).then(function(modal) {

        $rootScope.loginData = {
          username : 'EL744787',
          password : '123456'
        };

        $rootScope.modal = modal;
        $rootScope.modal.show();
      });
    }

    $rootScope.EVENT_USER_LOGIN = "User.Login";

    $rootScope.doLogin = function() {
      if(empty($rootScope.loginData.username)) {
        alert('Please enter user name');
      }
      else if(empty($rootScope.loginData.password)) {
        alert('Please enter password');
      }

      userService.validateLogin($rootScope.loginData.username, $rootScope.loginData.password)
        .then(function(result) {
          $rootScope.user = result;
          $rootScope.modal.hide();

          // broadcast event
          $rootScope.$emit($rootScope.EVENT_USER_LOGIN);

          console.log(result);
        });
    };
  });
})
  .constant('ApiEndpoint', {
    url: 'http://192.168.1.153:8100/apiproxy'
    // uncomment below line when deploy / testing on device
    //url: 'http://ec2-52-62-96-249.ap-southeast-2.compute.amazonaws.com/app_dev.php'
  })
.config(function($stateProvider, $urlRouterProvider) {
  $stateProvider
    .state('app', {
    url: '/app',
    abstract: true,
    templateUrl: 'templates/menu.html',
    controller: 'mainCtrl'
  })

    .state('app.jobList', {
      url: '/job',
      views: {
        'menuContent': {
          templateUrl: 'templates/jobList.html',
          controller: 'jobListCtrl'
        }
      }
    })

  .state('app.jobDetail', {
    url: '/job/:id',
    views: {
      'menuContent': {
        templateUrl: 'templates/jobDetail.html',
        controller: 'jobDetailCtrl'
      }
    }
  })

  .state('app.confirmedJobs', {
      url: '/confirmedJobs',
      views: {
        'menuContent': {
          templateUrl: 'templates/confirmedJobList.html',
          controller: 'confirmedJobCtrl'
        }
      }
    })
    .state('app.playlists', {
      url: '/playlists',
      views: {
        'menuContent': {
          templateUrl: 'templates/playlists.html',
          controller: 'PlaylistsCtrl'
        }
      }
    })

  .state('app.single', {
    url: '/playlists/:playlistId',
    views: {
      'menuContent': {
        templateUrl: 'templates/playlist.html',
        controller: 'PlaylistCtrl'
      }
    }
  });
  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('/app/job');
});
