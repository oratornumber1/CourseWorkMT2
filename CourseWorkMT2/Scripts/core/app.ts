class AddCategoryController {
    constructor() {
    }
}

class AllCategoryController {
    constructor() {
    }
}

class MainController {
    constructor() {
    }
}


var crudApp = angular.module('crudApp', []);
// configure our routes
crudApp.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: 'pages/home.html',
            controller: 'MainController'
        })

        // route for the about page
        .when('/addCategory', {
            templateUrl: 'pages/categories/add.html',
            controller: 'AddCategoryController'
        })

        // route for the contact page
        .when('/allCategory', {
            templateUrl: 'pages/categories/all.html',
            controller: 'AllCategoryController'
        });
});
crudApp.controller("AllCategoryController", AllCategoryController);
crudApp.controller("AddCategoryController", AddCategoryController);
crudApp.controller("MainController", MainController);

