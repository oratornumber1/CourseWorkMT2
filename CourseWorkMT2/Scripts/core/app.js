var AddCategoryController = (function () {
    function AddCategoryController() {
    }
    return AddCategoryController;
}());
var AllCategoryController = (function () {
    function AllCategoryController() {
    }
    return AllCategoryController;
}());
var MainController = (function () {
    function MainController() {
    }
    return MainController;
}());
var crudApp = angular.module('crudApp', []);
// configure our routes
crudApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
        templateUrl: 'pages/home.html',
        controller: 'MainController'
    })
        .when('/addCategory', {
        templateUrl: 'pages/categories/add.html',
        controller: 'AddCategoryController'
    })
        .when('/allCategory', {
        templateUrl: 'pages/categories/all.html',
        controller: 'AllCategoryController'
    });
});
crudApp.controller("AllCategoryController", AllCategoryController);
crudApp.controller("AddCategoryController", AddCategoryController);
crudApp.controller("MainController", MainController);
//# sourceMappingURL=app.js.map