(function () {
    'use strict';

    var controllerId = 'editTitleController';

    angular.module('poaApp').controller(controllerId,
        ['$scope', '$http', editTitleController]);

    function editTitleController($scope, $http) {

        $scope.editing = false;
        $scope.init = init;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.edit = edit;
       // $scope.title

        function init(title) {
            $scope.originalTitle = angular.extend({}, title);
            $scope.title = title;
            
        }

        function edit() {
            $scope.editing = true;
            
        }

        function save() {
            alert($scope.title.titleID + " " + $scope.title.titleName + " " + $scope.title.titleDescription);
            $http.post("/Title/Edit", $scope.title)
                .success(function (data) {
                    if (data.success !== true) {
                        alert("There was a problem saving to the server: " + data.errorMessage);
                        return;
                    }

                    $scope.originalTitle = angular.extend({}, $scope.title);

                    $scope.editing = false;
                })
                .error(function () {
                    alert("There was a problem saving the title.  Please try again.");
                });
        }

        function cancel() {
            angular.extend($scope.title, $scope.originalTitle);
            $scope.editing = false;
        }
    }
})();
