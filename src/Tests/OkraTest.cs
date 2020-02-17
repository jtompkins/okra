using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace okra.Tests {
    public class OkraTest : Okra {
        private class BasicTest : Okra {
            public BasicTest() {
                Describe("BasicTest", () => { });
            }
        }

        private class TestWithModifiers : Okra {
            public string ModifierOutput { get; set; }

            public TestWithModifiers() {
                Describe("TestWithModifiers", () => {
                    BeforeEach(() => ModifierOutput = "BeforeEach");
                    BeforeAll(() => ModifierOutput = "BeforeAll");
                    AfterEach(() => ModifierOutput = "AfterEach");
                    AfterAll(() => ModifierOutput = "AfterAll");
                });
            }
        }

        private class TestWithTests : Okra {
            public string TestOutput { get; set; }

            public TestWithTests() {
                Describe("TestWithTests", () => It("does a thing", () => TestOutput = "test"));
            }
        }

        private class TestWithNestedContexts : Okra {
            public TestWithNestedContexts() {
                Describe("TestWithNestedContexts", () => {
                    When("parent context", () => {
                        When("child context", () => { });
                    });
                });
            }
        }

        private class TestWithModifiersAndTests : Okra {
            public static Action SampleJustBeforeEach = () => { };
            public static Action SampleOuterBeforeEach = () => { };
            public static Action SampleInnerBeforeEach = () => { };
            public static Action SampleOuterAfterEach = () => { };
            public static Action SampleInnerAfterEach = () => { };

            public TestWithModifiersAndTests() {
                Describe("a test with modifiers and tests", () => {
                    BeforeEach(SampleOuterBeforeEach);
                    AfterEach(SampleOuterAfterEach);

                    Context("a context", () => {
                        BeforeEach(SampleInnerBeforeEach);
                        JustBeforeEach(SampleJustBeforeEach);
                        AfterEach(SampleInnerAfterEach);

                        It("does a thing", () => { });
                    });
                });
            }
        }

        public OkraTest() {
            Describe("Okra", () => {
                When("a test class is defined", () => {
                    Then("a 'describe' container is created with the correct description", () => {
                        var subject = new BasicTest().Containers.First();
                        subject.ContainerType.ShouldBe(ContainerTypes.Description);
                        subject.Description.ShouldBe("BasicTest");
                    });

                    When("a container defines before and after modifiers", () => {
                        TestWithModifiers testClass = null;
                        Container subject = null;

                        BeforeEach(() => {
                            testClass = new TestWithModifiers();
                            subject = testClass.Containers.First();
                        });

                        When("the test has a BeforeEach", () => {
                            Then("the container is assigned the BeforeEach func", () => {
                                subject.BeforeEach.Invoke();
                                testClass.ModifierOutput.ShouldBe("BeforeEach");
                            });
                        });

                        When("the test has a BeforeAll", () => {
                            Then("the container is assigned the BeforeAll func", () => {
                                subject.BeforeAll.Invoke();
                                testClass.ModifierOutput.ShouldBe("BeforeAll");
                            });
                        });

                        When("the test has a AfterEach", () => {
                            Then("the container is assigned the AfterEach func", () => {
                                subject.AfterEach.Invoke();
                                testClass.ModifierOutput.ShouldBe("AfterEach");
                            });
                        });

                        When("the test has a AfterAll", () => {
                            Then("the container is assigned the AfterAll func", () => {
                                subject.AfterAll.Invoke();
                                testClass.ModifierOutput.ShouldBe("AfterAll");
                            });
                        });
                    });
                });

                When("the test class contains tests", () => {
                    Then("the container has a reference to the test", () => {
                        var testContainer = new TestWithTests().Containers.First();
                        testContainer.Tests.Count().ShouldBe(1);

                        var subject = testContainer.Tests.First();
                        subject.Description.ShouldBe("does a thing");
                    });
                });

                When("the test class has nested contexts", () => {
                    Then("the top-level container has a reference to the child container", () => {
                        var testClass = new TestWithNestedContexts();
                        testClass.Containers.Count().ShouldBe(1);

                        var describeContainer = new TestWithNestedContexts().Containers.First();
                        describeContainer.Containers.Count().ShouldBe(1);

                        var parentContainer = describeContainer.Containers.First();
                        parentContainer.Containers.Count().ShouldBe(1);
                        parentContainer.Description.ShouldBe("parent context");

                        var childContainer = parentContainer.Containers.First();
                        childContainer.Description.ShouldBe("child context");
                    });
                });

                When("the test class has contexts with modifiers and tests", () => {
                    Test subject = null;

                    BeforeEach(() => subject = new TestWithModifiersAndTests().Containers.First().Containers.First().Tests.First());

                    It("orders the before each modifiers correctly", () => {
                        subject.Description.ShouldBe("does a thing");

                        var first = subject.Before.Dequeue();
                        first.ShouldBe(TestWithModifiersAndTests.SampleInnerBeforeEach);

                        var second = subject.Before.Dequeue();
                        second.ShouldBe(TestWithModifiersAndTests.SampleOuterBeforeEach);
                    });

                    It("orders the after each modifiers correctly", () => {
                        subject.Description.ShouldBe("does a thing");

                        var first = subject.After.Dequeue();
                        first.ShouldBe(TestWithModifiersAndTests.SampleInnerAfterEach);

                        var second = subject.After.Dequeue();
                        second.ShouldBe(TestWithModifiersAndTests.SampleOuterAfterEach);
                    });

                    It("orders the just before each modifiers correctly", () => {
                        subject.Description.ShouldBe("does a thing");

                        var first = subject.JustBefore.Dequeue();
                        first.ShouldBe(TestWithModifiersAndTests.SampleJustBeforeEach);
                    });
                });
            });
        }
    }
}
