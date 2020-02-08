using System;
using System.Collections.Generic;
using System.Linq;

namespace okra {
    public interface IVisitor {
        void VisitContainer(Container container);
        void VisitTest(Test test);
    }

    public class TestVisitor : IVisitor {
        private readonly Okra _test;

        public TestVisitor(Okra test) {
            _test = test;
        }

        public void Discover() {
            // do a depth-first search of the test, looking for
            // test metadata; for this first version, we'll just
            // log to the console when we "discover" each node in
            // the test tree.

            var stack = new Stack<Container>();

            foreach (var container in _test.Containers) {
                stack.Push(container);
            }

            while (stack.Count > 0) {
                var current = stack.Pop();

                current.Accept(this);

                foreach (var test in current.Tests) {
                    test.Accept(this);
                }

                foreach (var container in current.Containers) {
                    stack.Push(container);
                }
            }
        }

        public void VisitContainer(Container container) {
            Console.WriteLine(container.Description);
        }

        public void VisitTest(Test test) {
            Console.WriteLine(test.Description);
        }
    }
}
