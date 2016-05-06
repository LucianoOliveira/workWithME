﻿var WorkWithMeApp = angular.module("WorkWithMeApp", ["ngRoute", "ngResource"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: ListCtrl, templateUrl: 'list.html' }).
            when('/new', { controller: CreateCtrl, templateUrl: 'details.html' }).
            when('/edit/:editId', { controller: EditCtrl, templateUrl: 'details.html' }).
            otherwise({ redirectTo: '/' });
    });

WorkWithMeApp.factory('Projects', function ($resource) {
    return $resource('/api/projects/:projectId', { projectId: '@id' }, { update: { method: 'PUT' } });
});

var EditCtrl = function ($scope, $location, $routeParams, Projects) {
    $scope.action = "Update";
    var id = $routeParams.editId;
    $scope.item = Projects.get({ id: id });

    $scope.save = function () {
        Projects.update({id: id}, $scope.item, function () {
            $location.path('/');
        });
    };

};

var CreateCtrl = function ($scope, $location, Projects) {
    $scope.action = "Add";
    $scope.save = function () {
        Projects.save($scope.item, function () {
            $location.path('/');
        });
    };
};

var ListCtrl = function ($scope, $location, Projects) {
    $scope.search = function () {
        Projects.query({
            q: $scope.query,
            sort: $scope.sort_order,
            desc: $scope.is_desc,
            offset: $scope.offset,
            limit: $scope.limit
        },
            function (data) {
                $scope.more = data.length === 20;
                $scope.items = $scope.items.concat(data);

        });
    };

    $scope.sort = function (col) {
        if ($scope.sort_order === col) {
            $scope.is_desc = !$scope.is_desc;
        }
        else {
            $scope.sort_order = col;
            $scope.is_desc = true;
        }
        
        $scope.reset();
    };

    $scope.show_more = function () {
        $scope.offset += $scope.limit;
        $scope.search();
    };

    $scope.has_more = function () {
        return $scope.more;
    };


    $scope.reset = function () {
        $scope.limit = 20;
        $scope.offset = 0;
        $scope.items = [];
        $scope.more = true;
        $scope.search();
    };

    $scope.delete = function () {
        var id = this.item.projectId;
        Projects.delete({ projectId: id }, function(){
            $('#item_'+id).fadeOut();
        });
    };

    $scope.sort_order = "priority";
    $scope.is_desc = true;

    $scope.reset();
};