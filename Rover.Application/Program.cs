using Microsoft.Extensions.DependencyInjection;
using RobotRover.Domain;
using System;

namespace RobotRover.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup DI
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IRover, Rover>()
            .BuildServiceProvider();

            // Instantiate Navigation object
            var rover = serviceProvider.GetService<IRover>();

            // Set inital position
            rover.SetPosition(10, 10, "N");
            Console.WriteLine("Rover Starting position: [{0},{1},{2}]\n", rover.Position.X, rover.Position.Y, (char)rover.Position.Direction);

            // Input move
            Console.WriteLine("Move Rover (enter move):");
            var move = Console.ReadLine().ToUpper();

            // Exit on "0" otherwise keep accepting commands
            while (move != "0")
            {
                // Make move
                rover.Move(move);

                // Display commands
                foreach (var c in rover.Commands)
                    Console.WriteLine(c);
                
                // Display end position
                Console.WriteLine("Rover End position: [{0},{1},{2}]\n\n", rover.Position.X, rover.Position.Y, (char)rover.Position.Direction);

                // Input another move
                Console.WriteLine("Move Rover (enter move):");
                move = Console.ReadLine().ToUpper();
            }
        }
    }
}
