(function() {

    'use strict';

    angular.module('treeEditApp')
        .factory('TreeItemProvider',
            [
                '$http', function($http) {
                    var TreeItemProvider = function() {

                        TreeItemProvider.prototype.get = function() {
                            return $http.get('api/Tree');
                        };
                        TreeItemProvider.prototype.add = function (parentId){
                            return $http.post('api/Tree?parentId=' + parentId);
                        };
                        TreeItemProvider.prototype.update = function (nodeId, parentId) {
                            return $http.put('api/Tree?id=' + nodeId + '&parentId=' + parentId);
                        };
                        TreeItemProvider.prototype.delete = function (nodeId) {
                            return $http.delete('api/Tree?id=' + nodeId);
                        };
                    };

                    return new TreeItemProvider();
                }
            ]);
})();