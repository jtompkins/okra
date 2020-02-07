using System;
using System.Collections.Generic;

namespace okra {
    public enum ContainerTypes {
        Description,
        Context
    }

    public class Container {
        public string Description { get; set; }
        public ContainerTypes ContainerType { get; set; }

        public Action BeforeAll { get; set; }
        public Action AfterAll { get; set; }

        public Action BeforeEach { get; set; }
        public Action JustBeforeEach { get; set; }
        public Action AfterEach { get; set; }

        public IList<Container> Containers { get; set; }

        public IList<Test> Tests { get; set; }

        public void AddContainer(Container container) {
            this.Containers.Add(container);
        }

        public void AddTest(Test test) {
            this.Tests.Add(test);
        }
    }

    public class Test {
        public string Description { get; set; }
        public Action TestFunc { get; set; }
    }

    public class Okra {
        private readonly IList<Container> _containers;

        private readonly Queue<Container> _contextStack;

        public Okra() {
            _containers = new List<Container>();
            _contextStack = new Queue<Container>();
        }

        private Container _current {
            get {
                if (_contextStack.Count == 0) {
                    throw new Exception("You can only call that inside of a container");
                }

                return _contextStack.Peek();
            }
        }

        #region Builders

        public void Describe(string thing, Action func) {
            var container = new Container() {
                Description = thing,
                ContainerType = ContainerTypes.Context
            };

            _contextStack.Enqueue(container);
            func();
            _contextStack.Dequeue();
            _containers.Add(container);
        }

        public void Context(string when, Action func) {
            var container = new Container() {
                Description = when,
                ContainerType = ContainerTypes.Context
            };

            _current.AddContainer(container);

            _contextStack.Enqueue(container);
            func();
            _contextStack.Dequeue();
        }

        public void When(string when, Action func) {
            Context(when, func);
        }

        public void Then(string when, Action func) {

        }

        #endregion

        #region Modifiers

        public void BeforeAll(Action func) => _current.BeforeAll = func;
        public void AfterAll(Action func) => _current.AfterAll = func;
        public void JustBeforeEach(Action func) => _current.JustBeforeEach = func;
        public void BeforeEach(Action func) => _current.BeforeEach = func;
        public void AfterEach(Action func) => _current.AfterEach = func;

        #endregion

        public void Run() {

        }
    }
}
