using System;

namespace P2_Q1
{
    class Grammar
    {
        private List<List<string>> grammar;
        private bool isSimplified;
        private bool isInCNF;

        public Grammar(List<List<string>> grammar)
        {
            this.grammar = grammar;
            this.isSimplified = hasBeenSimplified();
            this.isInCNF = isInChomskyForm();
        }

        private bool hasBeenSimplified()
        {
            this.isSimplified = false;
            foreach (var item in this.grammar)
            {
                if (isSimplified == false && item.Contains("#"))
                {
                    isSimplified = true;
                }
            }
            return isSimplified;
        }

        private void Simplification()
        {
            while (this.hasBeenSimplified())
            {
                foreach (var item in this.grammar)
                {
                    foreach (var str in item)
                    {
                        if (str == "#")
                        {
                            item.Remove("#");
                            string to_be_deleted = item[0];
                            foreach (var obj in this.grammar)
                            {
                                for (int i = 1; i < obj.Count; i++)
                                {
                                    if (obj[i].Contains(to_be_deleted))
                                    {
                                        obj[i].Replace(to_be_deleted, string.Empty);
                                        obj.Add(obj[i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private bool isInChomskyForm()
        {
            this.isInCNF = true;
            foreach (var item in this.grammar)
            {
                foreach (var str in item)
                {
                    if (isInCNF = true && Char.IsUpper(str, 0) && str.Length > 1)
                    {
                        this.isInCNF = false;
                    }
                }
            }

            foreach (var item in this.grammar)
            {
                foreach (var str in item)
                {
                    if (isInCNF = true && Char.IsUpper(str, 0) && str.Count(ch => (ch == '>')) > 2)
                    {
                        this.isInCNF = false;
                    }
                }
            }
            return isInCNF;
        }

        private void convertToCNF()
        {
            this.Simplification();
            /**/
        }

        internal string isAcceptedByGrammar(string? inputString)
        {
            throw new NotImplementedException();
        }
    }
}