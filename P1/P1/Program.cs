using System;
using System.Collections.Generic;
using System.Linq;

namespace P1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                The first part is to check whether a string is accepted by finite automata or not
            */
            // var states = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            // var sigma = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).Select(item =>  char.Parse(item)).ToList();
            // var final = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            // long numberOfTransitions = Convert.ToInt64(Console.ReadLine());
            // var Delta = new List<Transition>();
            // for (long i = 0; i < numberOfTransitions; i++)
            // {
            //     var t = Console.ReadLine().Split(',');
            //     Delta.Add(new Transition(t[0], char.Parse(t[1]), t[2]));
            // }
            // string input = Console.ReadLine();

            // NFA nfa = new NFA(states, sigma, Delta, states[0], final);
            // System.Console.WriteLine(nfa.isAccepted(input));
            /*
                The second part is to find the equivalent DFA of an NFA, and print the number of DFA states
            */
            var nfaStates = Console.ReadLine().Split(',','q','{','}').Where(x => !string.IsNullOrWhiteSpace(x)).Select(item => int.Parse(item)).ToList();
            int nfaInitialState = nfaStates[0];
            var variables = Console.ReadLine().Split(',','{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            variables.Add("$");
            var nfaFinalStates = Console.ReadLine().Split(',', 'q', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).Select(item => int.Parse(item)).ToList();
            int transitionsCount = Convert.ToInt32(Console.ReadLine());
            List<Dictionary<string, List<int>>> nfaTransitions = new List<Dictionary<string, List<int>>>();
            /*
                We first initialize the matrix with empty Dictionaries, their count is equal to states count
                nfaTransitions[0] -> q0
                nfaTransitions[1] -> q1
                nfaTransitions[2] -> q2
                ...
            */
            for (int i = 0; i < nfaStates.Count; i++)
            {
                nfaTransitions.Add(new Dictionary<string, List<int>>());
            }
            for (int i = 0; i < transitionsCount; i++)
            {
                var t = Console.ReadLine().Split(',', 'q').ToList();
                if (!nfaTransitions[int.Parse(t[0])].ContainsKey(t[1]))
                {
                    nfaTransitions[int.Parse(t[0])][t[1]] = new List<int>();
                }
                nfaTransitions[int.Parse(t[0])][t[1]].Add(int.Parse(t[2]));
            }
            NFAtoDFA nFAtoDFA = new NFAtoDFA();
            
            System.Console.WriteLine(nFAtoDFA.ConvertNFAtoDFA(nfaStates.Count, variables.Count, nfaStates, variables, nfaStates[0], nfaFinalStates, nfaTransitions));
        }
    }
}
