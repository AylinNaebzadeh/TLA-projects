using System.Text;
/* useful links:
* https://github.com/tiena2cva/CalculatorC-/blob/master/Calculator/Expression.cs
* https://github.com/dipiash/Calculator/blob/master/Calculator/RevPolNotation.cs
* https://github.com/Pochatkin/MyRep/blob/master/CSharp/Calculator/Calculator/PostfixNotation.cs
* https://github.com/cychmps426211/C_sharp/blob/master/Calculator/Calculator/Program.cs
* https://github.com/Andegawen/Calculator/tree/master/Calculator
* 
*/
namespace Q2
{
    internal class Calculator
    {
        public string equation{get; set;}

        public Calculator(string input)
        {
            equation = input;
        }

        public string[] parsingInput()
        {
            List<string> tmp = new List<string>();
            int start = 0;
            while (start <=  equation.Length - 1)
            {
                
                if (char.IsDigit(equation[start]))
                {
                    var builder = new StringBuilder();
                    while (start < equation.Length && (equation[start] == '.' || char.IsDigit(equation[start])))
                    {
                        builder.Append(equation[start]);
                        start++;
                    }
                    tmp.Add(builder.ToString());
                }
                // --------------------------------------check for arithmatic operations
                else if (equation[start] == '+' || equation[start] == '*' || equation[start] == '/' || equation[start] == '^' || equation[start] == '(' || equation[start] == ')')
                {
                    tmp.Add(equation[start].ToString());
                    start++;
                    continue;
                }
                // -------------------------------------- check for negation
                else if (equation[start] == '-')
                {
                    if (char.IsDigit(equation[start + 1]))
                    {
                        if (char.IsDigit(tmp[tmp.Count - 1][tmp[tmp.Count - 1].Length - 1]) || tmp[tmp.Count - 1][tmp[tmp.Count - 1].Length - 1] == ')')
                        {
                            tmp.Add("-");
                            start++;
                        }
                        else
                        {
                            var builder = new StringBuilder();
                            builder.Append("-");
                            start++;
                            while (start < equation.Length && (char.IsDigit(equation[start]) || equation[start] == '.'))
                            {
                                builder.Append(equation[start]);
                                start++;
                            }
                            tmp.Add(builder.ToString());
                        }
                    }
                    else
                    {
                        tmp.Add("-");
                        start++;
                    }
                }
                // --------------------------------------
                else if (equation[start] == 'l' && equation[start + 1] == 'n')
                {
                    tmp.Add("ln");
                    start += 2;
                }

                else if (equation[start] == '-' && equation[start + 1] == 'l' && equation[start + 2] == 'n')
                {
                    tmp.Add("-ln");
                    start += 3;
                }
                // --------------------------------------
                else if (equation[start] == 's' && equation[start + 1] == 'i' && equation[start + 2] == 'n')
                {
                    tmp.Add("sin");
                    start += 3;
                }

                else if (equation[start] == '-' && equation[start + 1] == 's' && equation[start + 2] == 'i' && equation[start + 3] == 'n')
                {
                    tmp.Add("-sin");
                    start += 4;
                }
                // --------------------------------------
                else if (equation[start] == 'c' && equation[start + 1] == 'o' && equation[start + 2] == 's')
                {
                    tmp.Add("cos");
                    start += 3;
                }

                else if (equation[start] == '-' && equation[start + 1] == 'c' && equation[start + 2] == 'o' && equation[start + 3] == 's')
                {
                    tmp.Add("-cos");
                    start += 4;
                }
                // -------------------------------------
                else if (equation[start] == 't' && equation[start + 1] == 'a' && equation[start + 2] == 'n')
                {
                    tmp.Add("tan");
                    start += 3;
                }

                else if (equation[start] == '-' && equation[start + 1] == 't' && equation[start + 2] == 'a' && equation[start + 3] == 'n')
                {
                    tmp.Add("-tan");
                    start += 4;
                }
                // ---------------------------------------
                else if (equation[start] == 'a' && equation[start + 1] == 'b' && equation[start + 2] == 'c')
                {
                    tmp.Add("abc");
                    start += 3;
                }

                else if (equation[start] == '-' && equation[start + 1] == 'a' && equation[start + 2] == 'b' && equation[start + 3] == 'c')
                {
                    tmp.Add("-abc");
                    start += 4;
                }
                // --------------------------------------
                else if (equation[start] == 'e' && equation[start + 1] == 'x' && equation[start + 2] == 'p')
                {
                    tmp.Add("exp");
                    start += 3;
                }

                else if (equation[start] == '-' && equation[start + 1] == 'e' && equation[start + 2] == 'x' && equation[start + 3] == 'p')
                {
                    tmp.Add("-exp");
                    start += 4;
                }
                // --------------------------------------
                else if (equation[start] == 's' && equation[start + 1] == 'q' && equation[start + 2] == 'r' && equation[start + 3] == 't')
                {
                    tmp.Add("sqrt");
                    start += 4;
                }

                else if (equation[start] == '-' && equation[start + 1] == 's' && equation[start + 2] == 'q' && equation[start + 3] == 'r' && equation[start + 4] == 't')
                {
                    tmp.Add("-sqrt");
                    start += 5;
                }
                // --------------------------------------else for space
                else
                {
                    start++;
                }
            }
            return tmp.ToArray();
        }
        public string[] convertToPostFixNotation()
        {
            List<string> outputSeperated = new List<string>();
            Stack<string> PDA = new Stack<string>();
            ListSupport operations = new ListSupport();
            var seperatedEquation = parsingInput();
            foreach (string c in seperatedEquation)
            {
                if (operations.Contains(c))
                {
                    while (PDA.Count != 0 && getPrioriry(PDA.Peek()) >= getPrioriry(c) && PDA.Peek() != "(")
                    {
                        outputSeperated.Add(PDA.Pop());
                    }
                    PDA.Push(c);
                }
                else if (c == "(")
                {
                    PDA.Push("(");
                }
                else if (c == ")")
                {
                    while (PDA.Count != 0 && PDA.Peek() != "(")
                    {
                        outputSeperated.Add(PDA.Pop());
                    }
                    if (PDA.Count == 0)
                    {
                        throw new Exception("Stack is Empty:(");
                    }
                    else
                    {
                        PDA.Pop();
                    }
                }
                else
                {
                    outputSeperated.Add(c);
                }
            }
            while (PDA.Count != 0)
            {
                if (PDA.Peek() == "(")
                {
                    throw new Exception("Paranthesis check fails");
                }
                outputSeperated.Add(PDA.Pop());
            }
            return outputSeperated.ToArray();
        }

        private static int getPrioriry(string s)
        {
            switch (s)
            {
					case "+":
					case "-":
						return 1;
					case "*":
					case "/":
						return 2;
					case "^":
						return 3;
                    case "sin":
                    case "cos":
                    case "tan":
                    case "sqrt":
                    case "abs":
                    case "ln":
                    case "exp":
                    case "-sin":
                    case "-cos":
                    case "-tan":
                    case "-sqrt":
                    case "-abs":
                    case "-ln":
                    case "-exp":
                        return 4;
					default:
						return 5;
            }
        }

        public double Evaluate()
        {
            Stack<string> stack = new Stack<string>();
			Queue<string> queue = new Queue<string>(convertToPostFixNotation());
            string to_be_checked = queue.Dequeue();
            ListSupport operators = new ListSupport();
            
            while (queue.Count >= 0)
            {
                if (!operators.Contains(to_be_checked) && to_be_checked != "(" && to_be_checked != ")")
                {
                    stack.Push(to_be_checked);
                    to_be_checked = queue.Dequeue();
                }
                else
                {
                    double to_be_returned = 0;
                    switch (to_be_checked)
                    {
                        case "+":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    to_be_returned = a + b;
                                    break;
                                }
                        case "-":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    to_be_returned = b - a;
                                    break;
                                }
                        case "*":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    to_be_returned = a * b;
                                    break;
                                }
                        case "/":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    if (a == 0)
                                    {
                                        throw new Exception("Divisor can not be zero");
                                    }
                                    to_be_returned = b / a;
                                    break;
                                }
                        case "^":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    to_be_returned = Math.Pow(b, a);
                                    break;
                                }
                        case "sin":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = Math.Sin(a);
                                    break;
                                }
                        case "cos":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = Math.Cos(a);
                                    break;
                                }
                        case "tan":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = Math.Tan(a);
                                    break;
                                }
                        case "sqrt":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    if (a < 0)
                                    {
                                        throw new Exception("value can not be less than zero");
                                    }
                                    to_be_returned = Math.Sqrt(a);
                                    break;
                                }
                        case "abs":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = Math.Abs(a);
                                    break;
                                }
                        case "ln":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    if (a <= 0)
                                    {
                                        throw new Exception("The value can not be less than or equal zero");
                                    }
                                    to_be_returned = Math.Log10(a);
                                    break;
                                }
                        case "exp":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = Math.Exp(a);
                                    break;
                                }
                        case "-sin":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = -Math.Sin(a);
                                    break;
                                }
                        case "-cos":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = -Math.Cos(a);
                                    break;
                                }
                        case "-tan":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = -Math.Tan(a);
                                    break;
                                }
                        case "-sqrt":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    if (a < 0)
                                    {
                                        throw new Exception("value can not be less than zero");
                                    }
                                    to_be_returned = -Math.Sqrt(a);
                                    break;
                                }
                        case "-abs":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = -Math.Abs(a);
                                    break;
                                }
                        case "-ln":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    if (a <= 0)
                                    {
                                        throw new Exception("The value can not be less than or equal zero");
                                    }
                                    to_be_returned = -Math.Log10(a);
                                    break;
                                }
                        case "-exp":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    to_be_returned = -Math.Exp(a);
                                    break;
                                }
                        // default:
                        // {
                        //     stack.Push(to_be_checked);
                        //     break;
                        // }
                                    
                    }
                    stack.Push(to_be_returned.ToString());
                    if (queue.Count > 0)
                    {
                        to_be_checked = queue.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return stack.Count == 1 ? Convert.ToDouble(stack.Pop())   : throw new Exception("evaluation fails");
        }
    }

    public class ListSupport : List<string>
	{
		public ListSupport()
		{
			//operator
			Add("+");
			Add("-");
			Add("*");
			Add("/");
			Add("^");
			//function
			Add("sin");
			Add("cos");
			Add("tan");
            Add("sqrt");
            Add("abs");
            Add("ln");
            Add("exp");
            //-function
			Add("-sin");
			Add("-cos");
			Add("-tan");
            Add("-sqrt");
            Add("-abs");
            Add("-ln");
            Add("-exp");
		}
	}
}