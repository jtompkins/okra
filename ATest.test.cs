using System;

namespace okra {
    public class ATest : Okra {
        public ATest() {
            Describe("a thing", () => {
                BeforeAll(() => Console.WriteLine("BeforeAll"));
                AfterAll(() => Console.WriteLine("AfterAll"));

                BeforeEach(() => Console.WriteLine("BeforeEach"));
                AfterEach(() => Console.WriteLine("After Each 1"));

                When("a thing does another thing", () => {
                    JustBeforeEach(() => Console.WriteLine("JustBeforeEach"));
                    AfterEach(() => Console.WriteLine("After Each 2"));

                    Then("another thing happens", () => {
                        //normally we'd assert here, but I don't know how that works
                        // yet, so we're just going to write something to the console
                        // so we know this func got called.
                        Console.WriteLine("Then 1");
                    });

                    Then("a way different thing happens", () => {
                        //normally we'd assert here, but I don't know how that works
                        // yet, so we're just going to write something to the console
                        // so we know this func got called.
                        Console.WriteLine("Then 2");
                    });
                });
            });
        }
    }
}
