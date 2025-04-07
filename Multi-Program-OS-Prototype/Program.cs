
Console.WriteLine("Multi-Program OS Prototype has been started...\n\n\n");
Console.WriteLine("Main Menu:\n");
Console.WriteLine("Launch a program: start [program_name]\n");
Console.WriteLine("Stop the OS: exit\n");
Console.WriteLine("Change program running mode (step/continuous): run_mode [step/cont]\n");

string input = Console.ReadLine();

if (input == "start")
{
    Console.WriteLine("Program is starting\n");

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
