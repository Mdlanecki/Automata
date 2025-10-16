using Automata;
using System.Diagnostics.Tracing;
using System.Text;
using System.IO;
using System.Security.Cryptography;

Console.WriteLine("Enter number of states");
int numStates = int.Parse(Console.ReadLine());
Console.WriteLine("Enter the alphabet (as a string of characters without spaces)");
List<char> alphabet = Console.ReadLine().ToList();

List<State> states = new List<State>();

for (int i = 1; i <= numStates; i++)
{
    string stateName = $"q{i}";
    bool isInitial = i == 1; 
    states.Add(new State(stateName, new Dictionary<char, List<State>>(), isInitial));
}


Console.WriteLine("Is the automata a DFA or NFA? (d/n)");
char automataType = Char.Parse(Console.ReadLine());

foreach (var state in states)
{
    foreach (char symbol in alphabet)
    {

        if (automataType == 'n' || automataType == 'N')
        {
            Console.WriteLine("State: " + state.Name + " symbol: " + symbol.ToString() + ", Add states jumped to by symbol by listing them with a comma");
            string targetStateNames = Console.ReadLine();

            if(targetStateNames != "")
            {
                // Split, trim, and find State objects
                var stateNames = targetStateNames.Split(',')
                                                 .Select(s => s.Trim())
                                                 .ToList();

                var targetStates = states.Where(s => stateNames.Contains(s.Name)).ToList();

                // Add to transitions
                state.Transitions.Add(symbol, targetStates);
            }     
        }
        else
        {
            Console.WriteLine("State: " + state.Name + " symbol: " + symbol.ToString() + ", Add state jumped to by symbol");
            string targetStateName = Console.ReadLine();
            State targetState = states.Find(s => s.Name == targetStateName);
            state.Transitions.Add(symbol, new List<State> { targetState });
        }
    }

    if (automataType == 'n' || automataType == 'N')
    {
        Console.WriteLine("Add epsilon transitions for state " + state.Name + " by listing target states with a comma, or leave blank for none");
        string epsilonTargetStateNames = Console.ReadLine();

        if(epsilonTargetStateNames != "")
        {
            var stateNames = epsilonTargetStateNames.Split(',')
                                                     .Select(s => s.Trim())
                                                     .ToList();
            var targetStates = states.Where(s => stateNames.Contains(s.Name)).ToList();
            state.Transitions.Add('e', targetStates);
        }

    }
    

    Console.WriteLine("Is " + state.Name + " an accepting state? (y/n)");
    Char response = Char.Parse(Console.ReadLine());

    if (response == 'y' || response == 'Y')
    {
        state.isAcceptingState = true;
    }
}


if(automataType == 'n' || automataType == 'N')
{
    NFA nFA = new NFA(
        states,
        alphabet,
        states.Find(s => s.isInitialState),
        states.Where(s => s.isAcceptingState).ToList()
        );

    while (true)
    {
        Console.WriteLine("Enter word");

        String word = Console.ReadLine();

        Console.WriteLine(nFA.Traverse(word));
    }
}
else
{
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
}





