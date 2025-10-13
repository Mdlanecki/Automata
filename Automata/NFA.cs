using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata
{
    public class NFA
    {
        public List<char> Alphabet { get; set; }
        public List<State> States { get; set; }
        public State InitialState { get; set; }
        public List<State> AcceptingStates { get; set; }
        public NFA(List<State> states, List<char> alphabet, State initialState, List<State> acceptingStates)
        {
            Alphabet = alphabet;
            States = states;
            InitialState = initialState;
            AcceptingStates = acceptingStates;
        }
        public String traverse(String word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (!Alphabet.Contains(word[i]) || word[i] != 'e')
                {
                    return "Word contains symbols not in the alphabet";
                }
            }

            State currentState = InitialState;

            for (int i = 0; i < word.Length; i++)
            {
                currentState = currentState.Transitions[word[i]];
            }
            if (this.AcceptingStates.Contains(currentState))
            {
                return "ACCEPT";
            }
            else
            {
                return "REJECT";
            }
        }
    }
}
