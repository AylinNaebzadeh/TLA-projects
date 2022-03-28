using System.Collections.Generic;

namespace P1_Q3
{
    public class State
    {
        public bool isFinal;
        public bool isInitial;
        public string name;
        public string groupName;
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
        public List<State> findReachableStates(List<State> dfa, List<State> visited, State current)
        {
            visited.Add(current);
            for (int i = 0; i < dfaSymbols.Count; i++)
            {
                State next = current.transitions[i].end;
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    visited = findReachableStates(dfa, visited, next);
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
        public int minimizeDFA()
        {
            equivalenceStates finals = new equivalenceStates();
            equivalenceStates notfinals = new equivalenceStates();
            List<equivalenceStates> groups = new List<equivalenceStates>();

            List<State> can_be_reached = new List<State>();
            can_be_reached = findReachableStates(dfaStates, can_be_reached, dfaStates[0]);

            for (int i = 0; i < dfaStates.Count; i++)
            {
                if (!can_be_reached.Contains(dfaStates[i]))
                {
                    dfaStates.RemoveAt(i);
                }
            }

            for (int i = 0; i < dfaStates.Count; i++)
            {
                if (dfaStates[i].isFinal)
                {
                    finals.states.Add(dfaStates[i]);
                    dfaStates[i].groupName = "g0";
                }
                else
                {
                    notfinals.states.Add(dfaStates[i]);
                    dfaStates[i].groupName = "g1";
                }
            }

            finals.name = "g0";
            notfinals.name = "g1";

            groups.Add(finals);
            groups.Add(notfinals);

            bool over = false;
            bool has_same_name = false;
            List<string> resultGroups = new List<string>();
            string finalGroupName;
            string holdName;
            while (!over)
            {
                int counter = groups.Count;
                for (int i = 0; i < counter; i++)
                {
                    for (int j = 0; j < groups[i].states.Count; j++)
                    {
                        has_same_name = false;
                        for (int k = 0; k < dfaSymbols.Count; k++)
                        {
                            finalGroupName = groups[i].states[j].transitions[k].end.groupName;
                            resultGroups.Add(finalGroupName);
                        }

                        holdName = "";
                        holdName = groups[i].states[j].groupName;
                        for (int k = 0; k < dfaSymbols.Count; k++)
                        {
                            holdName += resultGroups[k];
                        }
                        resultGroups.Clear();

                        for (int k = 0; k < groups.Count; k++)
                        {
                            if (holdName == groups[k].name)
                            {
                                groups[k].states.Add(groups[i].states[j]);
                                has_same_name = true;
                            }
                        }

                        if (!has_same_name)
                        {
                            equivalenceStates tmp = new equivalenceStates();
                            tmp.name = holdName;
                            tmp.states.Add(groups[i].states[j]);
                            groups.Add(tmp);
                        }
                    }
                }
                if (groups.Count == counter * 2)
                {
                    over = true;
                    for (int i = 0; i < counter; i++)
                    {
                        groups.RemoveAt(groups.Count - 1);
                    }
                }
                else
                {
                    for (int i = 0; i < counter; i++)
                    {
                        groups.RemoveAt(0);
                    }
                }
                for (int i = 0; i < groups.Count; i++)
                {
                    for (int j = 0; j < groups[i].states.Count; j++)
                    {
                        groups[i].states[j].groupName = groups[i].name;
                    }
                }
            }

            groups = grouping(groups, dfaStates);

            for (int i = 0; i < groups.Count; i++)
            {
                for (int j = 0; j < groups[i].states.Count; j++)
                {
                    groups[i].states[j].groupName = groups[i].name;
                }
            }
            return groups.Count;
        }
    }
}