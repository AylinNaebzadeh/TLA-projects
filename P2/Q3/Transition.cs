namespace Q3
{
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
}