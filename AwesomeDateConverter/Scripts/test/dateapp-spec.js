describe('Date Convert App Example', function () {

    beforeEach(module('dateapp'));

    var scope, httpBackend, createController;

    describe('invalidDates', function () {
        beforeEach(inject(function ($rootScope, $httpBackend, $controller, $http) {
            scope = $rootScope.$new();
            scope.dateForm = {};
            scope.dateForm.$invalid = true;
            httpBackend = $httpBackend;

            mockEvent = new Event('convertDate');
            spyOn(mockEvent, 'preventDefault');

            createController = function () {
                return $controller('dateCtrl', {
                    '$scope': scope,
                    '$http': $http
                });
            };
        }));

        it('Should return an error message when the form is not valid', function () {
            scope.dateForm.$invalid === true;
            var controller = createController();
            event = scope.$emit("convertDate");
            scope.convertDate(event);

            expect(scope.errorMessage).toEqual('There is an error with the entered date.  Make sure the date is greater than 01/01/1903 and less than 12/31/9999');
        });
    });

    describe('validDates', function () {
        beforeEach(inject(function ($rootScope, $httpBackend, $controller, $http) {
            scope = $rootScope.$new();
            scope.dateForm = {};
            scope.dateForm.$invalid = false;
            httpBackend = $httpBackend;

            mockEvent = new Event('convertDate');
            spyOn(mockEvent, 'preventDefault');

            createController = function () {
                return $controller('dateCtrl', {
                    '$scope': scope,
                    '$http': $http
                });
            };
        }));

        it('Should return an error message when the form is not valid', function () {
            scope.dateForm.$invalid === true;
            var controller = createController();
            scope.dateToConvert = '1989-01-28T01:28:00';
            event = scope.$emit("convertDate");
            scope.convertDate(event);

            httpBackend.expectPOST('/api/dateconvert', moment(scope.dateToConvert)).respond(200, {posted: '1989-01-28T01:28:00', converted: '1989-01-28T08:28:00Z' });
            expect(scope.waiting).toEqual(true);
            httpBackend.flush();
            expect(scope.waiting).toEqual(false);
            expect(scope.errorMessage).toEqual('');
            expect(scope.localDate).toEqual('1989-01-28T01:28:00');
            expect(scope.utcDate).toEqual('1989-01-28T08:28:00Z');
        });
    });
});