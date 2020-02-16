using System;
using System.Text.RegularExpressions;

namespace okra {
    public class SimpleTestFormatter : ITestFormatter {
        private int _indentationLevel = 0;

        private int _passingTests = 0;
        private int _failingTests = 0;

        private const string INDENT_STRING = "  ";

        private void WriteIndent() {
            for (var i = 0; i < _indentationLevel; i++) {
                Console.Write(INDENT_STRING);
            }
        }

        public void StartContainer(Container container) {
            WriteIndent();

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(container.Description);
            _indentationLevel++;
        }

        public void FinishContainer(Container container) {
            _indentationLevel--;
        }

        public void TestSuccessful(Test test) {
            _passingTests++;

            WriteIndent();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("✓\t");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(test.Description);
        }

        public void TestFailed(Test test, string failureMessage) {
            _failingTests++;

            WriteIndent();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("✘\t");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{test.Description}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(failureMessage);
        }

        public void TestingStarted() {
            Console.WriteLine("");
        }

        public void TestingComplete() {
            Console.WriteLine("");

            if (_failingTests == 0) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" PASS ");
            } else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" FAIL ");
            }

            Console.WriteLine("");
        }
    }
}
