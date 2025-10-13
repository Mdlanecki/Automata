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



while (true)
{
        Console.WriteLine("Enter word");

    String word = Console.ReadLine();

    Console.WriteLine(dFA.traverse(word));
}