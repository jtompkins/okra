using System;

namespace okra {
    public class ATest : Okra {
        public ATest() {
            Describe("a thing", () => {
                string myCoolString = "";

                BeforeAll(() => Console.WriteLine("BeforeAll"));
                AfterAll(() => Console.WriteLine("AfterAll"));

                BeforeEach(() => myCoolString = "beforeEach");
                AfterEach(() => Console.WriteLine("After Each 1"));

                When("a thing does another thing", () => {
                    JustBeforeEach(() => myCoolString = "justBeforeEach");
                    AfterEach(() => Console.WriteLine("After Each 2"));

                    Then("another thing happens", () => {
                        //normally we'd assert here, but I don't know how that works
                        // yet, so we're just going to write something to the console
                        // so we know this func got called.
                        Console.WriteLine(myCoolString);
                    });

                    Then("a way different thing happens", () => {
                        //normally we'd assert here, but I don't know how that works
                        // yet, so we're just going to write something to the console
                        // so we know this func got called.
                        Console.WriteLine(myCoolString);
                    });
                });
            });
        }
    }
}
