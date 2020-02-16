using System;
using System.Collections.Generic;

namespace okra {
    public class TestRunner {
        private readonly IEnumerable<Okra> _tests;
        private readonly ITestFormatter _formatter;

        public TestRunner(IEnumerable<Okra> tests, ITestFormatter formatter) {
            _tests = tests;
            _formatter = formatter;
        }

        public void Run() {
            // do a depth-first search of the test, looking for
            // test metadata; for this first version, we'll just
            // log to the console when we "discover" each node in
            // the test tree.
            _formatter?.TestingStarted();

            foreach (var test in _tests) {
                foreach (var container in test.Containers) {
                    DiscoverTests(container);
                }
            }

            _formatter?.TestingComplete();
        }

        public void DiscoverTests(Container container) {
            if (container == null) {
                return;
            }

            _formatter?.StartContainer(container);

            container.BeforeAll?.Invoke();

            foreach (var test in container.Tests) {
                EvaluateTest(test);
            }

            foreach (var child in container.Containers) {
                DiscoverTests(child);
            }

            container.AfterAll?.Invoke();

            _formatter?.FinishContainer(container);
        }

        public void EvaluateTest(Test test) {
            foreach (var before in test.Before) {
                before();
            }

            foreach (var justBefore in test.JustBefore) {
                justBefore();
            }

            try {
                test.Func();
                _formatter?.TestSuccessful(test);
            } catch (Exception ex) {
                _formatter?.TestFailed(test, ex.Message);
            } finally {
                foreach (var after in test.After) {
                    after();
                }
            }
        }
    }
}
