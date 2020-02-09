using System;
using System.Collections.Generic;

namespace okra {
    public class Test {
        public string Description { get; }
        public Action Func { get; }

        public Test(string description, Action func) {
            Description = description;
            Func = func;
        }

        public Queue<Action> Before { get; } = new Queue<Action>();
        public Queue<Action> After { get; } = new Queue<Action>();
        public Queue<Action> JustBefore { get; } = new Queue<Action>();
    }
}
