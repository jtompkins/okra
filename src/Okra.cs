using System;
using System.Collections.Generic;

namespace okra {
    public class Okra {
        private readonly List<Container> _containers;
        private readonly Stack<Container> _contextStack;

        public Okra() {
            _containers = new List<Container>();
            _contextStack = new Queue<Container>();
        }

        private Container Current {
            get {
                if (_contextStack.Count == 0) {
                    throw new Exception("You can only call that inside of a container");
                }

                return _contextStack.Peek();
            }
        }

        #region Builders

        public void Describe(string description, Action func) {
            var container = new Container(description, ContainerTypes.Context);

            _containers.Add(container);

            _contextStack.Push(container);
            func();
            _contextStack.Pop();
        }

        public void Context(string when, Action func) {
            var container = new Container(when, ContainerTypes.Context);

            Current.AddContainer(container);

            _contextStack.Push(container);
            func();
            _contextStack.Pop();
        }

        public void When(string when, Action func) {
            Context(when, func);
        }

        public void Then(string when, Action func) {
            var test = new Test() {
                Description = when,
                TestFunc = func
            };

            Current.AddTest(test);
        }

        #endregion

        #region Modifiers

        public void BeforeAll(Action func) => Current.BeforeAll = func;
        public void AfterAll(Action func) => Current.AfterAll = func;
        public void JustBeforeEach(Action func) => Current.JustBeforeEach = func;
        public void BeforeEach(Action func) => Current.BeforeEach = func;
        public void AfterEach(Action func) => Current.AfterEach = func;

        #endregion
    }
}
