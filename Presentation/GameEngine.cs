using System.Collections.Generic;
using System.Diagnostics;
using System;
using activity_00_tap_26_27.Components;
using activity_00_tap_26_27.Core;
using activity_00_tap_26_27.Core.Events;

namespace activity_00_tap_26_27.Presentation
{
    public class GameEngine
    {
        private const float FIXED_FRAME_TIME = 20 / 1000.0f;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly ConsoleRenderManager _renderManager = new ConsoleRenderManager();

        private EventManager _eventManager = new EventManager();
        private GameManager _gameManager;

        private LogManager _logManager;

        public void Run()
        {
            _logManager = new LogManager(_eventManager);
            _gameManager = new GameManager(_eventManager);
            _stopwatch.Start();
            float lag = 0.0f;
            float last_time = GetCurrentTime();

            while (!_gameManager.GetShouldQuit())
            {
                float loop_start_time = GetCurrentTime();
                float elapsed_time = loop_start_time - last_time;
                lag += elapsed_time;

                ProcessInput();
               
                while (lag >= FIXED_FRAME_TIME)
                {
                    _gameManager.FixedUpdate(FIXED_FRAME_TIME);
                    lag -= FIXED_FRAME_TIME;
                }
               
                _gameManager.Update(elapsed_time);

                Render();

                _eventManager.ProcessEvents();

                last_time = loop_start_time;
            }

            Console.WriteLine("Goodbye!");
        }

        private void SendTranslatedKey(ConsoleKey console_key, EventManager event_manager)
        {
           switch (console_key)
            {
                case ConsoleKey.Escape:
                {
                    event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.ESCAPE));
                        break;
                }
            }
        }

        private void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo player_command = Console.ReadKey(true);

                SendTranslatedKey(player_command.Key, _eventManager);

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