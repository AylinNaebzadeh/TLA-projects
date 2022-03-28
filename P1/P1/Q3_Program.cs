using System;
using System.Collections.Generic;
using System.Linq;

namespace P1_Q3
{
    class NFA
    {
        public List<State> states {get; set;}
        public List<Transition> transitions {get; set;}
        public List<char> symbols {get; set;}
        public NFA(List<State> _states, List<Transition> _transitions, List<char> _symbols)
        {
            states = _states;
            transitions = _transitions;
            symbols = _symbols;
        }
        public List<State> convertNFAtoDFA()
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
                states[i].name = states[i].name.Trim();
            }



            List<State> DFA = new List<State>();
            State Initial = new State();
            Initial.isInitial = true;
            Initial.isFinal = states[0].isFinal;
            Initial.name = states[0].name;

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
                        newName = newName + " " + newAdjStates[k].name;
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
            return DFA;
        } 
    }
    class Program
    {
        static void Q3_Main(string[] args)
        {
            /*
                The third part is to determine the number of states of  a minimized dfa
            */
            List<State> NFA = new List<State>();
            List<Transition> transitions = new List<Transition>();
            var states = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var sigma = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).Select(item =>  char.Parse(item)).ToList();
            var finals = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            long numberOfTransitions = Convert.ToInt64(Console.ReadLine());
            for (int i = 0; i < states.Count; i++)
            {
                State newState = new State();
                if (i == 0)
                {
                    newState.isInitial = true;
                }
                newState.name = states[i];
                NFA.Add(newState);
            }
            for (int i = 0; i < finals.Count; i++)
            {
                for (int j = 0; j < NFA.Count; j++)
                {
                    if (NFA[j].name == finals[i])
                    {
                        NFA[j].isFinal = true;
                    }
                }
            }
            for (int i = 0; i < numberOfTransitions; i++)
            {
                var t = Console.ReadLine().Split(',');
                Transition tr = new Transition();
                tr.symbol = Convert.ToChar(t[1]);
                for (int j = 0; j < NFA.Count; j++)
                {
                    if (NFA[j].name == t[0])
                    {
                        tr.start = NFA[j];
                    }
                    if (NFA[j].name == t[2])
                    {
                        tr.end = NFA[j];
                    }
                }
                transitions.Add(tr);
            }

            for (int i = 0; i < NFA.Count; i++)
            {
                for (int j = 0; j < transitions.Count; j++)
                {
                    if (NFA[i] == transitions[j].start)
                    {
                        NFA[i].transitions.Add(transitions[j]);
                    }
                }
            }
            NFA newNFA = new NFA(NFA, transitions, sigma);
            var dfaStates = newNFA.convertNFAtoDFA();
            DFA newDFA = new DFA(dfaStates, sigma);
            System.Console.WriteLine(newDFA.minimizeDFA());
        }
    }
}