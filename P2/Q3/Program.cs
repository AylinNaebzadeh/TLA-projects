using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3
{
    class TuringMachine
    {
        string finalStates {get; set;}
        List<Transition> transitions = new List<Transition>();
        List<string> inputs = new List<string>(); 
        public TuringMachine(string _finalStates, List<Transition> _transitions, List<string> _inputs)
        {
            finalStates = _finalStates;
            transitions = _transitions;
            inputs = _inputs;
        }

        public List<string> isAccepted()
        {
            List<string> result = new List<string>();
            /* Create a tape and add blank characters to it*/
            string[] tape = new string[50000];

            bool mine = false;

            for (int i = 0; i < inputs.Count; i++)
            {
                
                string currentState = "1";

                int len = inputs[i].Split('0').Length;
                inputs[i] = "1010101010" + inputs[i] + "0101010101";
                tape = inputs[i].Split('0');
                int headPosition = "1010101010".Split('0').Length - 1;
                while (headPosition < headPosition + 1 + len + headPosition + 1)
                {
                    mine = false;
                    for (int j = 0; j < transitions.Count; j++)
                    {
                        if (transitions[j].start == currentState && (transitions[j].inputCh == tape[headPosition] || (transitions[j].inputCh == "1" && tape[headPosition] == "")))
                        {
                            currentState = transitions[j].end;
                            tape[headPosition] = transitions[j].outputCh;
                            if (transitions[j].direction == "1")
                            {
                                headPosition--;
                            }
                            else
                            {
                                headPosition++;
                            }
                            mine = true;
                            break;
                        }
                    }

                    if (currentState == finalStates)
                    {
                        Console.WriteLine("Accepted");
                        break;
                    }

                    if (!mine)
                    {
                        Console.WriteLine("Rejected");
                        break;
                    }
                }


            }

            return result;
        }
    }
    class Transition
    {   /*every delta function is in the form T(state, char) = (state, char, L/R) */
        public string start {get; set;}
        public string inputCh {get; set;}
        public string end {get; set;}
        public string outputCh {get; set;}
        public string direction {get; set;}

        public Transition(string _start, string _inputCh, string _end, string _outputCh, string _direction)
        {
            start = _start;
            inputCh = _inputCh;
            end = _end;
            outputCh = _outputCh;
            direction = _direction;
        }
    }
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
