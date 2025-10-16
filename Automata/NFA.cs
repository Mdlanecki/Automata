using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

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
        public string Traverse(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (!Alphabet.Contains(word[i]))
                {
                    return "Word contains symbols not in the alphabet";
                }
            }

            var q = new Queue<(State state, int iWord)>();
            var visited = new HashSet<(State state, int iWord)>();

            q.Enqueue((InitialState, 0));

            while (q.Count > 0)
            {
                var node = q.Dequeue();
                var currentState = node.state;
                var iWord = node.iWord;

                if (iWord == word.Length && AcceptingStates.Contains(currentState))
                {
                    return "ACCEPT";
                }

                visited.Add((currentState, iWord));

                foreach(var transition in currentState.Transitions)
                {
                    char symbol = transition.Key;
                    List<State> nextStates = transition.Value;
                    if (iWord < word.Length && word[iWord] == symbol)
                    {
                        foreach (var nextState in nextStates)
                        {
                            if (!visited.Contains((nextState, iWord + 1)))
                            {
                                q.Enqueue((nextState, iWord + 1));
                            }
                        }
                    }
                }

            }
            return "REJECT";
        }
    }
}
