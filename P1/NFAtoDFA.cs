using System;
using System.Collections;
using System.Collections.Generic;

namespace P1
{
    public class NFAtoDFA
    {
        public ArrayList delete(ArrayList nodes, string var, 
                                List<Dictionary<string, ArrayList>> nfaTransitions)
        {
            ArrayList newSetOfNodes = new ArrayList();
            for (long i = 0; i < nodes.Count; i++)
            {
                Dictionary<string, ArrayList> tmp = nfaTransitions[(int)nodes[(int)i]];
                if (tmp.ContainsKey(var))
                {
                    newSetOfNodes.Add(tmp[var]);
                }
            }
            return union(newSetOfNodes);
        }

        private ArrayList union(ArrayList nodes)
        {
            ArrayList set = new ArrayList();
            for (long i = 0; i < nodes.Count; i++)
            {
                ArrayList tmp = (ArrayList)nodes[(int)i];
                for (long j = 0; j < tmp.Count; j++)
                {
                    if (!set.Contains(tmp[(int)j]))
                    {
                        set.Add(tmp[(int)j]);
                    }
                }
            }
            return set;
        }
        private ArrayList eClosure(int node, 
                    List<Dictionary<string, ArrayList>> nfaTransitions)
        {
            ArrayList set = new ArrayList();
            set.Add(node);
            int i = 0;
            while (i < set.Count)
            {
                Dictionary<string, ArrayList> tmp = nfaTransitions[(int)set[i]];
                if (tmp.ContainsKey("$"))
                {
                    ArrayList al = tmp["$"];
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
        private ArrayList eClosureList(ArrayList nodes, 
                    List<Dictionary<string, ArrayList>> nfaTransitions)
        {
            ArrayList combinedNodes = new ArrayList();
            for (int i = 0; i < nodes.Count; i++)
            {
                combinedNodes.Add(eClosure((int)nodes[i], nfaTransitions));
            }
            return union(combinedNodes);
        }
        private int isNewNodeNew(ArrayList newNodes, ArrayList dfaNodes)
        {
            newNodes.Sort();
            for (int i = 0; i < dfaNodes.Count; i++)
            {
                ArrayList tmp = (ArrayList)dfaNodes[i];
                tmp.Sort();
                if (newNodes.Equals(tmp))
                {
                    return i;
                }
            }
            return -1;
        }
        public static void ConvertNFAtoDFA()
        {}
    }
}