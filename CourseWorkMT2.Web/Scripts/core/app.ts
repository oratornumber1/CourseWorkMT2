class Category {
    CategoryID: number;
    CategoryName: string;
    Description: string;
    Picture: Uint8Array;
    constructor(name: string, description: string, id?: number) {
        if (id != null)
        {
            this.CategoryID = id;
        }
        
        this.CategoryName = name;
        this.Description = description;
    }

}

class AddCategoryController {
    constructor() {
    }
    create(name: string, description: string) {
        console.log('pressed');
        $.ajax({
            type: 'POST',
            url: "http://localhost:4897/Categories",
            data: new Category(name, description),
            dataType: 'json',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Content-Type", "application/json;odata=minimalmetadata");
                xhr.setRequestHeader("Accept", "application/json;odata=minimalmetadata");
                xhr.setRequestHeader("Accept-Charset", "UTF-8");
                // add say auth headers here for secure endpoints
            },
            success: function (newObj) {
            console.log("success")
                // job done
            },
            error: function (e) {
                console.log('fail');
                // nope, examine what server said
            }
        });
    }
}

class AllCategoryController {
    categories: Array<Category>;
    pageNum: number;
    searchString: string;
    constructor(private $scope: ng.IScope) {
        this.pageNum = 1;
        this.searchString = '';
        this.categories = new Array<Category>();

        $.getJSON("http://localhost:4897/Categories",
            cs => {
                let resultArr: Array<any> = cs['value'];
                resultArr.forEach(c => {
                    this.categories.push(new Category(c['CategoryName'], c['Description'], c['CategoryID']))
                });
                console.log(this.categories);
                this.$scope.$apply();
            });
    }
}
AllCategoryController.$inject = ['$scope'];

class MainController {
    constructor() {
    }
}

var crudApp = angular.module('crudApp', ['ngRoute']);
crudApp.config([<any>'$routeProvider', ($routeProvider: angular.route.IRouteProvider) => {
    $routeProvider
        .when('/', { templateUrl: 'pages/home.html', controller: MainController })
        .when('/addcategory', { templateUrl: 'pages/categories/add.html', controller: AddCategoryController })
        .when('/allcategory', { templateUrl: 'pages/categories/all.html', controller: AllCategoryController });
            //.otherwise({ redirectTo: '/' });
    }]);

crudApp.controller("AllCategoryController", AllCategoryController);
crudApp.controller("AddCategoryController", AddCategoryController);
crudApp.controller("MainController", MainController);

