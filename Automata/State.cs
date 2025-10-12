using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata
{
    public class State
    {
        public string Name { get; set; }

        public bool isInitialState { get; set; }

        public bool isAcceptingState { get; set; }

        public Dictionary<char, State> Transitions { get; set; }

        public State(string name, Dictionary<char, State> Transitions, bool isIniitalState = false, bool isAcceptingState = false) {
            Name = name;
            this.isInitialState = isIniitalState;
            this.Transitions = Transitions;
            this.isAcceptingState = isAcceptingState;
        }


        
    }
}
