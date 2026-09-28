using Anecs.Core;

if (args is ["version"])
{
    Console.WriteLine($"anecs-cli 0.1.0 | policy {P0ActionSelector.PolicyVersion}");
    return;
}

Console.WriteLine("ANECS reference CLI");
Console.WriteLine("Usage: anecs version");
