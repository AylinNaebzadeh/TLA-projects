namespace P1
{
    public class Transition
    {
        public string startState {get; set;}
        public char Symbol {get; set;}
        public string endState {get; set;}
        public Transition(string _start, char _symbol, string _end)
        {
            startState = _start;
            Symbol = _symbol;
            endState = _end;
        }
        public override string ToString()
        {
            return $"{startState},{Symbol},{endState}";
        }
    }
}