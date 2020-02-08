﻿using System;

namespace okra {
    public class ATest : Okra {
        public ATest() {
            Describe("a thing", () => {
                Console.WriteLine("describe");

                BeforeAll(() => Console.WriteLine("BeforeAll"));
                AfterAll(() => Console.WriteLine("AfterAll"));

                BeforeEach(() => Console.WriteLine("BeforeEach"));

                When("a thing does another thing", () => {
                    Console.WriteLine("when");

                    JustBeforeEach(() => Console.WriteLine("JustBeforeEach"));

                    Then("another thing happens", () => {
                        //normally we'd assert here, but I don't know how that works
                        // yet, so we're just going to write something to the console
                        // so we know this func got called.
                        Console.WriteLine("then");
                    });
                });
            });
        }
    }
}
