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
}