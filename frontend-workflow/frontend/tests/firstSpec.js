"use strict";

describe("First Suite", function() {
  var counter;

  beforeEach(function() {
    counter = 0;
  });

  it("increments value", function() {
    counter++;
    expect(counter).toEqual(1);
  });

  it("decrements value", function() {
    counter--;
    expect(counter).toEqual(-1);
  });

  xit("equal value", function() {
    expect(counter).toBe(0);
  });
});
