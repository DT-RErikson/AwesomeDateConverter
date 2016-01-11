var app = angular.module('dateapp', ['ngRoute']);

app.config(['$routeProvider',
			'$locationProvider',
			'$httpProvider',
	function ($routeProvider,
				$locationProvider,
				$httpProvider) {
	    $routeProvider
            .when('/', {
                controller: 'dateCtrl',
                templateUrl: '/home/date'
            })
	}]);

app.controller('dateCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.dateToConvert = '';
    $scope.utcDate = '';
    $scope.waiting = false;
    $scope.errorMessage = '';

    $scope.convertDate = function ($event) {

        if ($scope.dateForm.$invalid === true) {
            $scope.errorMessage = 'There is an error with the entered date.  Make sure the date is greater than 01/01/1903 and less than 12/31/9999';
            $event.preventDefault();
        }
        else {
            $scope.waiting = true;
            $scope.utcDate = '';
            var currentDate = moment($scope.dateToConvert)
            var p = $http.post('/api/dateconvert', currentDate)
            p.then(function (res) {
                $scope.waiting = false;
                $scope.errorMessage = '';
                $scope.utcDate = res.data.converted;
                $scope.localDate = res.data.posted;
            })
            .catch(function (err) {
                $scope.errorMessage = 'An error occured while retrieving the requested UTC Date';
            })
        }
        $scope.utcDate = '';
    }
}]);