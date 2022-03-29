using System;
using System.Collections.Generic;
using System.Linq;

namespace P1_Q2
{
    public class creation
    {
        public static NFA createNFA()
        {
            var states = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var alphabets = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).Select(item =>  char.Parse(item)).ToList();
            /*
                The format of the below dictionary will be like this:
                    q0_a : [], q0_b : [], q0_$ : [], q1_a : [], q1_b : [], q1_$ : [], ...
            */
            Dictionary<string, List<string>> transitionMapping = new Dictionary<string, List<string>>();
            foreach (var state in states)
            {
                foreach (var alphabet in alphabets)
                {
                    transitionMapping[state + "_" + alphabet] = new List<string>();
                }
                transitionMapping[state + "_" + "$"] = new List<string>();
            }

            var finalStates = Console.ReadLine().Split(',', '{', '}').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            int transitionCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < transitionCount; i++)
            {
                var transition = Console.ReadLine().Split(',');
                string firstState = transition[0];
                string nextState = transition[2];
                string symbol = transition[1];
                if (symbol == "$")
                {
                    transitionMapping[firstState + "_" + "$"].Add(nextState);
                }
                else
                {
                    transitionMapping[firstState + "_" + symbol].Add(nextState);
                }
            }


            NFA newNFA = new NFA(states, finalStates, transitionMapping, alphabets);
            return newNFA;
        }
    }
    public class NFA
    {
        private List<string> states = new List<string>();
        private List<string> finalStates = new List<string>();
        private Dictionary<string, List<string>> transitions = new Dictionary<string, List<string>>();
        private List<char> alphabets = new List<char>();
        private string initialState;
        private  Dictionary<string, List<string>>  lambdaTransitions = new Dictionary<string, List<string>>();

        public NFA(List<string> _states, List<string> _finalStates, Dictionary<string, List<string>> _transitions, List<char> _alphabets)
        {
            this.states = _states;
            this.finalStates = _finalStates;
            this.transitions = _transitions;
            this.alphabets = _alphabets;
            this.initialState = _states[0];
            foreach (var state in states)
            {
                findPathsWithLambda(state);
            }
        }

        private void findPathsWithLambda(string state)
        {
            List<string> checkState = new List<string>();
            List<string> possibleStates = new List<string>{state};
            // var tmp
            for (int i = 0; i < possibleStates.Count; i++)
            {
                var item = possibleStates[i];
                // if (checkState.SequenceEqual(possibleStates))
                // {
                //     break;
                // }
                var newStates = transitions[item + "_" + "$"];
                foreach (var newState in newStates)
                {
                    if (!possibleStates.Contains(newState))
                    {
                        possibleStates.Add(newState);
                    }
                }
                checkState.Add(item);
                lambdaTransitions[state] = possibleStates;
            }
        }

        public int createEquivalentDFA()
        {
            List<string> startStatesList = this.lambdaTransitions[this.initialState];
            startStatesList.Sort();

            List<List<string>> statesList = new List<List<string>>();
            statesList.Add(startStatesList);

            List<Tuple<List<string>, char, List<string>>> transitionsList = new List<Tuple<List<string>, char, List<string>>>();

            for (int i = 0; i < statesList.Count; i++)
            {
                var item = statesList[i];
                foreach (var alphabet in this.alphabets)
                {
                    List<string> newState = new List<string>();
                    foreach (var state in item)
                    {
                        List<string> addStates = this.transitions[state + "_" + alphabet];
                        foreach (var innerState in addStates)
                        {
                            if (!newState.Contains(innerState))
                            {
                                newState.Add(innerState);
                            }
                        }
                    }

                    for (int j = 0; j < newState.Count; j++)
                    {
                        var state = newState[j];
                        List<string> lambdaStates = this.lambdaTransitions[state];
                        foreach (var innerState in lambdaStates)
                        {
                            if (!newState.Contains(innerState))
                            {
                                newState.Add(innerState);
                            }
                        }
                    }
                    newState.Sort();

                    var transition = Tuple.Create(item, alphabet, newState);

                    if (newState.Count != 0)
                    {
                        transitionsList.Add(transition);
                    }

                    if (newState.Count != 0 && !statesList.Any(list => list.SequenceEqual(newState)))
                    {
                        statesList.Add(newState);
                    }
                }
            }

            List<List<string>> dfaFinalStates = new List<List<string>>();

            var nfaFinalStates = this.finalStates;

            foreach (var item in statesList)
            {
                foreach (var state in item)
                {
                    if (nfaFinalStates.Contains(state))
                    {
                        dfaFinalStates.Add(item);
                        break;
                    }
                }
            }
            int result = statesList.Count;
            if (transitionsList.Count != statesList.Count * 2)
            {
                result += 1;
                statesList.Add(new List<string>{"Trap"});
            }
            return result;
        }
    }
}