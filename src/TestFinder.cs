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


        }

        public void VisitContainer(Container container) {
            throw new System.NotImplementedException();
        }

        public void VisitTest(Test test) {
            throw new System.NotImplementedException();
        }
    }
}
