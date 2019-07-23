(function () {
    'use strict';

    angular.module('treeEditApp')
        .controller('TreeViewController',
            [
                '$scope', 'TreeItemProvider', function ($scope, TreeItemProvider) {

                    $scope.data = [];

                    TreeItemProvider.get().then(function (response) {
                        $scope.data = response.data;
                    });


                    $scope.treeOptions = {
                        dropped: function (ev) {
                            var parentNodeId = null;
                            if (ev.dest.nodesScope.$nodeScope)
                                parentNodeId = ev.dest.nodesScope.$nodeScope.$modelValue.id;

                            var nodeId = ev.source.nodeScope.$modelValue.id;

                            TreeItemProvider.update(nodeId, parentNodeId).then(function (response) {
                                if (response.status != 200) {
                                    //TODO: revert action
                                }
                            });
                        }
                    };

                    $scope.newSubItem = function (scope) {
                        var nodeData = scope.$modelValue;
                        TreeItemProvider.add(nodeData.id).then(function (response) {
                            if (response.status == 200) {
                                var node = {
                                    id: response.data.id,
                                    title: response.data.title,
                                    nodes: []
                                };
                                nodeData.nodes.push(node);
                            }
                        });

                    };

                    $scope.deleteNode = function (scope) {
                        var nodeData = scope.$modelValue;
                        TreeItemProvider.delete(nodeData.id).then(function (response) {
                            if (response.status == 200) {
                                scope.remove();

                            }
                        });

                    };

                    $scope.newNode = function () {
                        TreeItemProvider.add().then(function (response) {
                            if (response.status == 200) {
                                var node = {
                                    id: response.data.id,
                                    title: response.data.title,
                                    nodes: []
                                };
                                $scope.data.push(node);
                            }
                        });
                    };
                }
            ]);
})();