using System.Collections.Generic;
using System.Diagnostics;
using System;
using activity_00_tap_26_27.Components;
using activity_00_tap_26_27.Events;

namespace activity_00_tap_26_27
{
    public class GameEngine
    {
        private const float FIXED_FRAME_TIME = 20 / 1000.0f;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        private readonly ConsoleRenderManager _renderManager = new ConsoleRenderManager();

        private bool _shouldQuit = false;


        EventManager _eventManager = new EventManager();
        private LogManager _logManager;

        public void Run()
        {
            _logManager = new LogManager(_eventManager);
            _stopwatch.Start();
            float lag = 0.0f;
            float last_time = GetCurrentTime();

            GameObject gameObjectTest = new GameObject("test");
            RegisterGameObjectGameEvent registerEvent = new RegisterGameObjectGameEvent(gameObjectTest);
            _eventManager.TriggerDelayedEvent(registerEvent);
            //creation gameobject test

            while (!_shouldQuit)
            {
                float loop_start_time = GetCurrentTime();
                float elapsed_time = loop_start_time - last_time;
                lag += elapsed_time;

                ProcessInput();
               
                while (lag >= FIXED_FRAME_TIME)
                {
                    FixedUpdate(FIXED_FRAME_TIME);
                    lag -= FIXED_FRAME_TIME;
                }
               
                Update(elapsed_time);

                Render();

                _eventManager.ProcessEvents();

                last_time = loop_start_time;
            }

            Console.WriteLine("Goodbye!");
        }

        private void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo player_command = Console.ReadKey(true);

                if (player_command.Key == ConsoleKey.Escape)
                {
                    _shouldQuit = true;
                }
            }
        }

        private void FixedUpdate(float fixed_elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.FixedUpdate(fixed_elapsed_time);
                }
            }
        }

        private void Update(float elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.Update(elapsed_time);
                }
            }
        }

        private void Render()
        {
            _renderManager.Draw(0,0, "Game in progress...\n", ConsoleColor.Magenta);
            _renderManager.Render();
        }

        private float GetCurrentTime()
        {
            return _stopwatch.ElapsedMilliseconds / 1000.0f;
        }
    }
}