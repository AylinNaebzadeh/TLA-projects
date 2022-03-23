using System.Collections.Generic;

namespace P1_Q3
{
    public class State
    {
        public bool isFinal;
        public bool isInitial;
        public string name;
        public List<Transition> transitions = new List<Transition>();
    }
    public class equivalenceStates
    {
        public string name;
        public List<State> states = new List<State>();
    }
    public class Transition
    {
        public State start {get; set;}
        public State end {get; set;}
        public char symbol {get; set;}
    }
    public class DFA
    {
        public List<State> dfaStates {get; set;}
        public List<char> dfaSymbols {get; set;}
        public DFA(List<State> _states, List<char> _symbols)
        {
            dfaStates = _states;
            dfaSymbols = _symbols;
        }
        public List<State> findUnReachable(List<State> dfa, List<State> visited, State current)
        {
            visited.Add(current);
            for (int i = 0; i < dfaSymbols.Count; i++)
            {
                State next = current.transitions[i].end;
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    visited = findUnReachable(dfa, visited, next);
                }
            }
            return visited;
        }

        public List<equivalenceStates> grouping(List<equivalenceStates> groups, List<State> dfa)
        {
            string newGroupName = "";
            List<string> statesNames = new List<string>();
            for (int i = 0; i < groups.Count; i++)
            {
                newGroupName = "";
                statesNames.Clear();
                for (int j = 0; j < groups[i].states.Count; j++)
                {
                    if (!statesNames.Contains(groups[i].states[j].name))
                    {
                        if (groups[i].states[j].name == "" || groups[i].states[j].name == null)
                        {
                            if (!statesNames.Contains(""))
                            {
                                newGroupName += "Trap" +" ";
                                statesNames.Add("");
                            }
                        }
                        else
                        {
                            newGroupName += groups[i].states[j].name + " ";
                            statesNames.Add(groups[i].states[j].name);
                        }
                    }
                }
                groups[i].name = newGroupName;
            }
            return groups;
        }
        public void minimizeDFA()
        {}
    }
}