namespace okra.Tests {
    public class TestRunnerTest : Okra {
        public TestRunnerTest() {
            Describe("TestRunner", () => {
                When("it is passed a test class", () => {
                    It("runs the tests", () => {

                    });

                    When("there are before all modifiers", () => {
                        It("runs the modifiers before it runs the tests", () => {

                        });
                    });

                    When("there are after all modifiers", () => {
                        It("runs the modifiers after it runs the tests", () => {

                        });
                    });

                    When("there are before each modifiers on the tests", () => {
                        It("runs the modifiers before each test", () => {

                        });
                    });

                    When("there are just before each modifiers on the tests", () => {
                        It("runs the modifiers before each test", () => {

                        });

                        It("runs the just before each modifiers after the before each modifiers", () => {

                        });
                    });

                    When("there are after each modifiers on the tests", () => {
                        It("runs the modifiers after each test", () => {

                        });

                        When("the test fails or throws an exception", () => {
                            It("still runs the after modifiers", () => {

                            });
                        });
                    });
                });

                When("it is passed multiple test classes", () => {
                    It("discovers the tests in each test class", () => {

                    });
                });
            });
        }
    }
}
