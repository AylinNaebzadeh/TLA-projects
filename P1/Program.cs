using System;
using System.Collections.Generic;
using System.Linq;

namespace P1
{
    class Program
    {
        static void Main(string[] args)
        {
            var states = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var sigma = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).Select(item =>  char.Parse(item)).ToList();
            var final = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            long numberOfTransitions = Convert.ToInt64(Console.ReadLine());
            var Delta = new List<Transition>();
            for (long i = 0; i < numberOfTransitions; i++)
            {
                var t = Console.ReadLine().Split(',');
                Delta.Add(new Transition(t[0], char.Parse(t[1]), t[2]));
            }
            string input = Console.ReadLine();

            NFA nfa = new NFA(states, sigma, Delta, states[0], final);
            System.Console.WriteLine(nfa.isAccepted(input));
        }
    }
}
