using System;
using System.Collections;
using System.Collections.Generic;
/*
    I will change every node to an integer 
    For example:
        q0 -> 0
        q1 -> 1
        q2 -> 2
        ...
*/

namespace P1
{
    public class NFAtoDFA
    {
        public List<int> delete(List<int> states, string var, 
                                List<Dictionary<string, List<int>>> nfaTransitions)
        {
            List<List<int>> newSetOfNodes = new List<List<int>>();
            for (int i = 0; i < states.Count; i++)
            {
                Dictionary<string, List<int>> tmp = nfaTransitions[states[i]];
                if (tmp.ContainsKey(var))
                {
                    newSetOfNodes.Add(tmp[var]);
                }
            }
            return union(newSetOfNodes);
        }

        private List<int> union(List<List<int>> states)
        {
            List<int> set = new List<int>();
            for (int i = 0; i < states.Count; i++)
            {
                List<int> tmp = states[i];
                for (int j = 0; j < tmp.Count; j++)
                {
                    if (!set.Contains(tmp[j]))
                    {
                        set.Add(tmp[j]);
                    }
                }
            }
            return set;
        }
        private List<int> eClosure(int state, 
                    List<Dictionary<string, List<int>>> nfaTransitions)
        {
            List<int> set = new List<int>();
            set.Add(state);
            int i = 0;
            while (i < set.Count)
            {
                Dictionary<string, List<int>> tmp = nfaTransitions[set[i]];
                if (tmp.ContainsKey("$"))
                {
                    List<int> al = tmp["$"];
                    for (int j = 0; j < al.Count; j++)
                    {
                        if (!set.Contains(al[j]))
                        {
                            set.Add(al[j]);
                        }
                    }
                }
                i++;
            }
            return set;
        }
        private List<int> eClosureList(List<int> states, 
                    List<Dictionary<string, List<int>>> nfaTransitions)
        {
            List<List<int>> combinedNodes = new List<List<int>>();
            for (int i = 0; i < states.Count; i++)
            {
                combinedNodes.Add(eClosure(states[i], nfaTransitions));
            }
            return union(combinedNodes);
        }
        private int isNewNodeNew(List<int> newNodes, List<List<int>> dfaNodes)
        {
            newNodes.Sort();
            for (int i = 0; i < dfaNodes.Count; i++)
            {
                List<int> tmp = dfaNodes[i];
                tmp.Sort();
                if (newNodes.Equals(tmp))
                {
                    return i;
                }
            }
            return -1;
        }
        public void ConvertNFAtoDFA(int nfaCount, int varCount, 
                                            List<int> nfaNodes, List<string> variables, 
                                            int nfaInitialNode, List<int> nfaFinalNodes,
                                            List<Dictionary<string, List<int>>> nfaTransitions)
        {
            List<List<int>> dfaNodes = new List<List<int>>();
            List<Dictionary<string, int>> dfaTransitions = new List<Dictionary<string, int>>();
            List<int> dfaFinalNodes = new List<int>();
            int dfaCount = 0;
            List<int> firstDFAnode = eClosure(nfaInitialNode, nfaTransitions);
            dfaNodes.Add(firstDFAnode);
            dfaCount++;
            int j = 0;
            while (j < dfaCount)
            {
                Dictionary<string, int> newNodeDict = new Dictionary<string, int>();
                for (int i = 0; i < varCount; i++)
                {
                    List<int> newNode = eClosureList(delete(dfaNodes[j], variables[i], nfaTransitions), nfaTransitions);
                    int nodePointingTo = isNewNodeNew(newNode, dfaNodes);
                    if (nodePointingTo == -1)
                    {
                        dfaNodes.Add(newNode);
                        newNodeDict.Add((string)variables[i], dfaCount);
                        dfaCount++;
                    }
                    else
                    {
                        newNodeDict.Add((string)variables[i], nodePointingTo);
                    }
                    dfaTransitions.Add(newNodeDict);
                    j++;
                }
            }
            for (int i = 0; i < nfaFinalNodes.Count; i++)
            {
                for (int k = 0; k < dfaNodes.Count; k++)
                {
                    List<int> tmp = dfaNodes[k];
                    if (tmp.Contains(nfaFinalNodes[i]))
                    {
                        dfaFinalNodes.Add(k);
                        continue;
                    }
                }
            }
        }
    }
}