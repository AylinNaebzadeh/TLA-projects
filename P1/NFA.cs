using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P1
{
    public class NFA
    {
        public List<string> States = new List<string>();
        public List<char> Alphabets = new List<char>();
        public List<Transition> Transitions = new List<Transition>();
        public string initialState;
        public List<string> finalStates = new List<string>();
        public NFA(List<string> _states, 
                    List<char> _alphabets, 
                    List<Transition> _transitions, 
                    string _initial, List<string> _finals)
        {
            States = _states;
            Alphabets = _alphabets;
            AddTransition(_transitions);
            AddInitialState(_initial);
            AddFinalState(_finals);
        }
        private void AddTransition(List<Transition> transitions)
        {
            foreach (var t in transitions.Where(ValidTransition))
            {
                Transitions.Add(t);
            }
        }

        private bool ValidTransition(Transition transition)
        {
            return States.Contains(transition.startState) &&
                    States.Contains(transition.endState) &&
                    Alphabets.Contains(transition.Symbol);
        }
        private void AddInitialState(string state)
        {
            if (state != null && States.Contains(state))
            {
                initialState = state;
            }
        }
        private void AddFinalState(List<string> finals)
        {
            foreach (var f in finals.Where(f => States.Contains(f)))
            {
                finalStates.Add(f);
            }
        }
        public string isAccepted(string input)
        {
            if (Accept(initialState, input, new StringBuilder()))
            {
                return "Accepted";
            }
            return "Rejected";
        }

        private bool Accept(string currentState, string input, StringBuilder steps)
        {
            if (input.Length > 0)
            {
                var transitions = GetAllTransitions(currentState, input[0]);
                foreach (var t in transitions)
                {
                    var currentStep = new StringBuilder(steps.ToString() + t);
                    if (Accept(t.endState, input.Substring(1), currentStep))
                    {
                        return true;
                    }
                }
                return false;
            }
            if (finalStates.Contains(currentState))
            {
                return true;
            }
            return false;
        }

        private List<Transition> GetAllTransitions(string currentState, char symbol)
        {
            return Transitions.FindAll( t => t.startState == currentState && t.Symbol == symbol);
        }
    }
}