using System;
using System.Collections.Generic;

namespace okra {
    public class Program {
        public static void Main(string[] args) {
            var runner = new TestRunner(new List<Okra>() { new ATest() }, new SimpleTestFormatter());

            runner.Run();
        }
    }
}
