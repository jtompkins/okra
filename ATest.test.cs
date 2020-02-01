using System;

namespace ginkgo_cs {
    public class ATest : Ginkgo {
        ATest() {
            BeforeAll(() => {
                System.Console.WriteLine("BeforeAll");
            });

            AfterAll(() => {
                System.Console.WriteLine("AfterAll");
            });

            Describe("describe", () => {
                System.Console.WriteLine("describe");

                JustBeforeEach(() => {
                    System.Console.WriteLine("JustBeforeEach");
                });

                When("when", () => {
                    System.Console.WriteLine("when");

                    BeforeEach(() => {
                        System.Console.WriteLine("BeforeEach");
                    });

                    Then("then", () => {
                        System.Console.WriteLine("Then");
                    });
                });
            });
        }
    }
}
