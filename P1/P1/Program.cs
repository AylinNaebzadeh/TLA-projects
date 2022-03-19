using System;
using System.Collections.Generic;
using System.Linq;

namespace P1_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<State> NFA = new List<State>();
            List<Transition> transitions = new List<Transition>();
            /*
                The second part is to convert the NFA to DFA
            */
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
            NFAtoDFA obj = new NFAtoDFA(NFA, transitions, sigma);
            
            System.Console.WriteLine(obj.convertNFAtoDFA());
        }
    }
}