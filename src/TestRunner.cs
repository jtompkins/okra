using System.Collections.Generic;

namespace okra {
    public class TestRunner {
        private readonly IEnumerable<Okra> _tests;

        public TestRunner(IEnumerable<Okra> tests) {
            _tests = tests;
        }

        public void Run() {
            // do a depth-first search of the test, looking for
            // test metadata; for this first version, we'll just
            // log to the console when we "discover" each node in
            // the test tree.
            foreach (var test in _tests) {
                foreach (var container in test.Containers) {
                    DiscoverTests(container);
                }
            }
        }

        public void DiscoverTests(Container container) {
            if (container == null) {
                return;
            }

            container.BeforeAll?.Invoke();

            foreach (var test in container.Tests) {
                EvaluateTest(test);
            }

            foreach (var child in container.Containers) {
                DiscoverTests(child);
            }

            container.AfterAll?.Invoke();
        }

        public void EvaluateTest(Test test) {
            foreach (var before in test.Before) {
                before();
            }

            foreach (var justBefore in test.JustBefore) {
                justBefore();
            }

            test.Func();

            foreach (var after in test.After) {
                after();
            }
        }
    }
}
