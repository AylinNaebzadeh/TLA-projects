using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P1
{
    public class NFA
    {
        public enum Constants
        {
            Landa = '$',
            None = '\0'
        }
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
                    (Alphabets.Contains(transition.Symbol) || transition.Symbol == (char)Constants.Landa);
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
            if (Accept(initialState, input))
            {
                return "Accepted";
            }
            return "Rejected";
        }

        private bool Accept(string currentState, string input)
        {
            bool result = false;
            if (input.Length == 0)
            {
                if (finalStates.Contains(currentState))
                {
                    return true;
                }
                return false;
            }
            else if (input.Length > 0)
            {
                foreach (var t in Transitions)
                {
                    if (t.startState == currentState && t.Symbol == input[0] && result == false)
                    {
                        result = Accept(t.endState, input.Substring(1));
                    }
                    else if (t.startState == currentState && t.Symbol == (char)Constants.Landa && result == false)
                    {
                        result = Accept(t.endState, input);
                    }
                }   
                return result;
            }
            return result;
        }
    }
}