using System;
using System.Collections.Generic;

namespace okra {
    public interface ITestNode {
        void Accept(IVisitor visitor);
    }

    public enum ContainerTypes {
        Description,
        Context
    }

    public class Container : ITestNode {
        private readonly List<Container> _containers;
        private readonly List<Test> _tests;

        public IEnumerable<Container> Containers {
            get {
                return _containers;
            }
        }

        public IEnumerable<Test> Tests {
            get {
                return _tests;
            }
        }

        public Container(string description, ContainerTypes type) {
            _containers = new List<Container>();
            _tests = new List<Test>();

            this.Description = description;
            this.ContainerType = type;
        }

        public string Description { get; set; }
        public ContainerTypes ContainerType { get; set; }

        public Action BeforeAll { get; set; }
        public Action AfterAll { get; set; }

        public Action BeforeEach { get; set; }
        public Action JustBeforeEach { get; set; }
        public Action AfterEach { get; set; }

        public void AddContainer(Container container) {
            _containers.Add(container);
        }

        public void AddTest(Test test) {
            _tests.Add(test);
        }

        public void Accept(IVisitor visitor) {
            visitor.VisitContainer(this);
        }
    }
}
