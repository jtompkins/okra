using System;

namespace ginkgo_cs {
    public class Assertion {
        public void ToThrow<T>() {

        }
    }
    public class Ginkgo {
        public void BeforeAll(Action func) { }

        public void JustBeforeEach(Action func) { }

        public void BeforeEach(Action func) { }

        public void AfterEach(Action func) { }

        public void AfterAll(Action func) { }

        public void Describe(string thing, Action func) {

        }

        public void Context(string when, Action func) {

        }

        public void When(string when, Action func) {

        }

        public void Then(string when, Action func) {

        }

        public Assertion Expect(Action func) {
            return new Assertion();
        }
    }
}
