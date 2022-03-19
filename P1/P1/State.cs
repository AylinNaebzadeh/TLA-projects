using System.Collections.Generic;

namespace P1_Q2
{
    public class State
    {
        public bool isFinal;
        public bool isInitial;
        public string name;
        public List<Transition> transitions = new List<Transition>();
        public State(){}
        public State(bool _isFinal, bool _isInitial, string _name)
        {
            isFinal = _isFinal;
            isInitial = _isInitial;
            name = _name;
        }

    }
}