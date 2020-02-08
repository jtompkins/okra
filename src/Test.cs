using System;

namespace okra {
    public class Test : ITestNode {
        public string Description { get; set; }
        public Action TestFunc { get; set; }

        public void Accept(IVisitor visitor) {
            visitor.VisitTest(this);
        }
    }
}
