using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*----------------- Reading Input ---------------------------------------------------------*/
            string[] rules = Console.ReadLine().Split(new string[] { "00" }, StringSplitOptions.None);
            
            int n = Convert.ToInt32(Console.ReadLine());
            List<string> toBeChecked = new List<string>();
            for (int i = 0; i < n; i++)
            {
                toBeChecked.Add(Console.ReadLine());
            }
            /*----------------- Transitions -----------------------------------------------------------*/
            List<Transition> transitions = new List<Transition>();
            foreach (var rule in rules)
            {
                var r = rule.Split('0');
                Transition transition = new Transition(r[0], r[1], r[2], r[3], r[4]);
                transitions.Add(transition);
            }
            /*------------------ Final State ---------------------------------------------------------*/
            List<string> tmp = new List<string>();
            foreach (var tr in transitions)
            {
                tmp.Add(tr.start);
                tmp.Add(tr.end);
            }
            string finalState = tmp.Distinct().Max();
            
            /*------------------- Call Turing Machine -------------------------------------------------*/
            TuringMachine TM = new TuringMachine(finalState, transitions, toBeChecked);


            var res = TM.isAccepted();
            for (int i = 0; i < res.Count; i++)
            {
                System.Console.WriteLine(res[i]);
            }
        }


    }
}
