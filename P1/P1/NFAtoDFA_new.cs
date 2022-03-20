using System.Collections.Generic;

namespace P1_Q2
{
    public class NFAtoDFA
    {
        public List<State> states {get; set;}
        public List<Transition> transitions {get; set;}
        public List<char> symbols {get; set;}
        public NFAtoDFA(List<State> _states, List<Transition> _transitions, List<char> _symbols)
        {
            states = _states;
            transitions = _transitions;
            symbols = _symbols;
        }
        public int convertNFAtoDFA()
        {
            List<State> newStates = new List<State>();
            /* Initialize every state */
            for (int i = 0; i < states.Count; i++)
            {
                State tmpState = new State();
                tmpState.isFinal = states[i].isFinal;
                tmpState.isInitial = states[i].isInitial;
                for (int j = 0; j < states[i].transitions.Count; j++)
                {
                    Transition tmpTr = new Transition();
                    tmpTr.start = states[i].transitions[j].start;
                    tmpTr.end = states[i].transitions[j].end;
                    tmpTr.symbol = states[i].transitions[j].symbol;
                    tmpState.transitions.Add(tmpTr);
                }
                tmpState.name = states[i].name;
                newStates.Add(tmpState);
            }
            /* Now we should look for landa transitions */
            for (int i = 0; i < states.Count; i++)
            {
                for (int j = 0; j < states[i].transitions.Count; j++)
                {
                    if (states[i].transitions[j].symbol == '$')
                    {
                        states[i].name = states[i].name + " " + states[i].transitions[j].end.name;
                    }
                }
                /* Is it neccesary? */
                states[i].name = states[i].name.Trim();
            }

            for (int i = states.Count - 1; i >= 0; i--)
            {
                for (int j = states[i].transitions.Count - 1; j >= 0; j--)
                {
                    if (states[i].transitions[j].symbol == '$')
                    {
                        states[i].transitions.Remove(states[i].transitions[j]);
                    }
                }
            }

            List<State> DFA = new List<State>();
            State Initial = new State(states[0].isFinal, true, states[0].name);
            DFA.Add(Initial);

            for (int i = 0; i < DFA.Count; i++)
            {
                for (int j = 0; j < symbols.Count; j++)
                {
                    State tmp = new State();
                    tmp.isFinal = false;
                    List<State> newAdjStates = new List<State>();

                    var statesName = DFA[i].name.Trim().Split(' ');
                    
                    for (int k = 0; k < statesName.Length; k++)
                    {
                        for (int m = 0; m < newStates.Count; m++)
                        {
                            for (int n = 0; n < newStates[m].transitions.Count; n++)
                            {
                                if (newStates[m].name ==  statesName[k] && (char)newStates[m].transitions[n].symbol == symbols[j])
                                {
                                    newAdjStates.Add(newStates[m].transitions[n].end);
                                }
                            }
                        }
                    }
                    string newName = "";
                    for (int k = 0; k < newAdjStates.Count; k++)
                    {
                        newName += " " + newAdjStates[k].name;
                    }
                    for (int k = 0; k < newAdjStates.Count; k++)
                    {
                        if (newAdjStates[k].isFinal)
                        {
                            tmp.isFinal = true;
                        }
                    }

                    tmp.name = newName;
                    bool exist = false;
                    int index = -1;
                    for (int k = 0; k <= i; k++)
                    {
                        if (DFA[k].name.Trim() == newName.Trim())
                        {
                            exist = true;
                            index = k;
                        }
                    }

                    Transition trDFA = new Transition();
                    if (exist)
                    {
                        trDFA.start = DFA[i];
                        trDFA.symbol = symbols[j];
                        trDFA.end = DFA[index];
                        DFA[i].transitions.Add(trDFA);
                    }
                    else
                    {
                        trDFA.start = DFA[i];
                        trDFA.symbol = symbols[j];
                        trDFA.end = tmp;
                        DFA.Add(tmp);
                        DFA[i].transitions.Add(trDFA);
                    }
                }
            }
            return DFA.Count;
        } 
    }
}