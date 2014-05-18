(function () {

    'use strict';

    var controllerId = 'AppCtrl';

    angular.module('poaApp').controller(controllerId,
        ['$scope', '$http', AppCtrl]);


    
    function AppCtrl($scope, $http) {
        var section_ = 1;
        var agent_id = $("#Agent_ID").val();
       

       //document.writeln(agent_id);
        //$scope.init = init;
       // $scope.show = show;
        //$scope.cancel = cancel; 
       // $scope.item = {}
       // function show()
       // {
            var principal_id = $("#Principal_ID").val();
            $http({ method: 'GET', url: '/Agent/GetUserByID', params: { id: principal_id } }).
               success(function (data, status, headers, config) {
                   // this callback will be called asynchronously
                   // when the response is available 
                   $scope.item = data;
               }).
               error(function (data, status, headers, config) {
                   // called asynchronously if an error occurs
                   // or server returns response with an error status.
                   //alert(status);
               });
      //  }

        $scope.section_ = function (id) {
            section_ = id;
        };

        $scope.is = function (id) {
            return section_ == id;
        };

       
       
    }
})();