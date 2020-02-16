namespace okra {
    public interface ITestFormatter {
        public void StartContainer(Container container);
        public void FinishContainer(Container container);

        public void TestSuccessful(Test test);
        public void TestFailed(Test test, string failureMessage);

        public void TestingStarted();
        public void TestingComplete();
    }
}
