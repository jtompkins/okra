using System;

namespace okra {
    class Program {
        static void Main(string[] args) {
            var visitor = new TestVisitor(new ATest());

            visitor.Discover();
        }
    }
}
