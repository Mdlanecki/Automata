
using Automata;
using System.Diagnostics.Tracing;
using System.Text;
using System.IO;

Console.WriteLine("Enter number of states");
int numStates = int.Parse(Console.ReadLine());
Console.WriteLine("Enter the alphabet (as a string of characters without spaces)");
List<char> alphabet = Console.ReadLine().ToList();

List<State> states = new List<State>();

for (int i = 1; i <= numStates; i++)
{
    string stateName = $"q{i}";
    bool isInitial = i == 1; 
    states.Add(new State(stateName, new Dictionary<char, State>(), isInitial));
}

foreach(var state in states)
{
    foreach (char symbol in alphabet)
    {
        Console.WriteLine("State: " + state.Name + " symbol: " + symbol.ToString() + ", Add state jumped to by symbol");
        string targetStateName = Console.ReadLine();
        State targetState = states.Find(s => s.Name == targetStateName);   
        state.Transitions[symbol] = targetState;
    }

    Console.WriteLine("Is " + state.Name + " an accepting state? (y/n)");
    Char response = Char.Parse(Console.ReadLine());

    if (response == 'y' || response == 'Y')
    {
        state.isAcceptingState = true;
    }
}


DFA dFA = new DFA(
    states,
    alphabet,
    states.Find(s => s.isInitialState),
    states.Where(s => s.isAcceptingState).ToList()

);

Console.WriteLine("Enter word");

String word = Console.ReadLine();

Console.WriteLine(dFA.traverse(word));




// Function to generate DOT representation of the DFA
string GenerateDot(List<State> states, List<char> alphabet)
{
    StringBuilder sb = new StringBuilder();
    sb.AppendLine("digraph DFA {");
    sb.AppendLine("    rankdir=LR;"); // left to right
    sb.AppendLine("    node [shape = circle];");

    // Mark accepting states with doublecircle
    foreach (var state in states)
    {
        if (state.isAcceptingState)
            sb.AppendLine($"    {state.Name} [shape=doublecircle];");
    }

    // Invisible start node pointing to the initial state
    sb.AppendLine("    start [shape=point];");
    var initialState = states.Find(s => s.isInitialState);
    sb.AppendLine($"    start -> {initialState.Name};");

    // Add transitions
    foreach (var state in states)
    {
        foreach (var kvp in state.Transitions)
        {
            char symbol = kvp.Key;
            State targetState = kvp.Value;
            sb.AppendLine($"    {state.Name} -> {targetState.Name} [label=\"{symbol}\"];");
        }
    }

    sb.AppendLine("}");
    return sb.ToString();
}

// Usage
string dot = GenerateDot(states, alphabet);
File.WriteAllText("dfa.dot", dot);

Console.WriteLine("DOT file generated as dfa.dot. You can convert it to PNG using Graphviz: dot -Tpng dfa.dot -o dfa.png");
