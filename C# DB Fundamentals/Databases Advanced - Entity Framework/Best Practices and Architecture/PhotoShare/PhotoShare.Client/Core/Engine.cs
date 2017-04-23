﻿namespace PhotoShare.Client.Core
{
    using System;

    public class Engine
    {
        private readonly CommandDispatcher _commandDispatcher;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this._commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    var result = _commandDispatcher.DispatchCommand(data);
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
