var demo = angular.module("demo", []);

demo.controller("DemoCtrl", function() {
  this.answer = "?";
  this.calc = function() {
    this.answer = 1 + 1;
  };
});

demo.directive("taskList", function() {
  return {
    restrict: "E",
    template: require("./template/task-list.html"),
    controller: ["$scope", function($scope) {
      $scope.tasks = [
        {title: "Write some code"},
        {title: "Charge cellphone"},
        {title: "Make phone call"},
      ];
    }]
  };
});
