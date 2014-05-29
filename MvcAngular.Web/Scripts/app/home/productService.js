
angular
    .module('myApp.service.products', [])
    .factory('productService', [
        '$http',
        function($http) {

            return {
                getProduct: function () {
                    return $http({
                        method: 'GET',
                        url: '/api/product'
                    });
                },

                createProduct: function (person) {
                    return $http({
                        method: 'POST',
                        url: '/api/product',
                        data: person
                    });
                },

                readProduct: function (personId) {
                    return $http({
                        method: 'GET',
                        url: '/api/product/' + personId
                    });
                },

                updateProduct: function (person) {
                    return $http({
                        method: 'PUT',
                        url: '/api/product',
                        data: person
                    });
                },

                deleteProduct: function (personId) {
                    return $http({
                        method: 'DELETE',
                        url: '/api/product/' + personId
                    });
                }
            };
        }]);
