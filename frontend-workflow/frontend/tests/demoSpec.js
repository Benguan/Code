"use strict";

describe("DemoCtrl", function() {
  beforeEach(angular.mock.module("demo"));

  it("demo has data", angular.mock.inject(function($controller) {
    var ctrl = $controller("DemoCtrl", {});
    expect(ctrl.answer).toEqual("?");
  }));
});
