using System;
using System.Collections.Generic;

using VM_OS_Project;

Console.WriteLine("Multi-Program OS Prototype has been started...\n\n\n");
Console.WriteLine("Main Menu:\n");
Console.WriteLine("Launch a program: start [program_name]\n");
Console.WriteLine("Stop the OS: exit\n");
Console.WriteLine("Change program running mode (step/continuous): run_mode [step/cont]\n");

string input = Console.ReadLine();

if (!string.IsNullOrEmpty(input) && input.StartsWith("start"))
{
    string[] parts = input.Split(' ');
    if (parts.Length == 2)
    {
        string programName = parts[1];
        try
        {
            ProgramLoader loader = new ProgramLoader();
            List<string> commands = loader.LoadProgramFromHDD("hdd.txt", programName);
            Console.WriteLine("Program is starting\n");
            foreach (var cmd in commands)
                Console.WriteLine(cmd);
        }
        catch (Exception exc)
        {
            Console.WriteLine("Exception: " + exc.Message);
        }
    }
    

}
else if (input == "test")
{
    TestVM.RunBasicTest();
}
else if (input == "exit")
{
    Console.WriteLine("Shutting down the OS...\n");
    Environment.Exit(0); // Exits the application (does nothing right now)
    //Application.Exit();
}
else if (input == "run_mode step")
{
    Console.WriteLine("Program running mode changed to STEP");
}
else if (input == "run_mode cont")
{
    Console.WriteLine("Program running mode changed to CONTINUOUS");
}
else
{
    Console.WriteLine("Unrecognized command entered... Please try again");
}
