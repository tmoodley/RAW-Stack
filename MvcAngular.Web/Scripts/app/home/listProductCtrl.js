
angular
    .module('myApp.ctrl.listProduct', [])
    .controller('listProductCtrl', [
        '$scope',
        '$location',
        'productService',
        function ($scope, $location, productService) {

            $scope.product = [];
            $scope.viewProduct = function(id) {
                $location.path("/detail/" + id);
            };
            $scope.editProduct = function (id) {
                $location.path("/edit/" + id);
            };
            $scope.deleteProduct = function (id) {
                $location.path("/delete/" + id);
            };
            $scope.createProduct = function () {
                $location.path("/create/");
            };

            productService
                .getProduct()
                .success(function (data, status, headers, config) {
                    var newvar = data.rows;
                    console.log(newvar);
                    $scope.products = newvar;
                });

            $scope.navigationManager.setListPage();

        }]);
