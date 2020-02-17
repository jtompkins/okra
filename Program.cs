using System;
using System.Collections.Generic;
using okra.Tests;

namespace okra {
    public class Program {
        public static void Main(string[] args) {
            var runner = new TestRunner(
                new List<Okra>() {
                    new OkraTest(),
                    new TestRunnerTest()
                },
                new SimpleTestFormatter()
            );

            runner.Run();
        }
    }
}
