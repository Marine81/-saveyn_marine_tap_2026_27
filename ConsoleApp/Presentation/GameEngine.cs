using System.Diagnostics;
using System;
using activity_00_tap_26_27.Core;
using activity_00_tap_26_27.Core.Events;
using activity_00_tap_26_27.Core.Components;
using activity_00_tap_26_27.Core.States;

namespace activity_00_tap_26_27.Presentation
{
    public class GameEngine
    {
        private LogFileWriter _fileWriter = new LogFileWriter("debug.log");

        private const float FIXED_FRAME_TIME = 20 / 1000.0f;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly ConsoleRenderManager _renderManager = new ConsoleRenderManager();

        private EventManager _eventManager = new EventManager();
        private GameManager _gameManager;

        private LogManager _logManager;

        public void Run()
        {
            _logManager = new LogManager(_eventManager, _fileWriter);
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
                _eventManager.ProcessEvents();
                _gameManager.Update(elapsed_time);

                Render();

                last_time = loop_start_time;
            }

            // Console.WriteLine("Goodbye!");
            _renderManager.Draw(0, 0, "GoodBye", ConsoleColor.Cyan, ConsoleColor.Black);
            _renderManager.Render();
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
                    case ConsoleKey.DownArrow:
                    {
                        event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.NAVIGATE_DOWN));
                        break;
                    }

                    case ConsoleKey.UpArrow:
                    {
                        event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.NAVIGATE_UP));
                        break;
                    }

                    case ConsoleKey.Enter:
                    {
                        event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.CONFIRM));

                        break;
                    }
            }
            
        }

        private void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo player_command = Console.ReadKey(true);

                SendTranslatedKey(player_command.Key,_eventManager);
            }
        }
       
                    

        private void Render()
        {
            IState current_state = _gameManager.GetCurrentState();

            int selected_index = _gameManager.GetSelectionIndex();

           
            if (current_state is TitleState)
            {
                _renderManager.Draw(0, 0, "Game", ConsoleColor.Blue, ConsoleColor.Black);

                ConsoleColor background_color = ConsoleColor.Black;
                if (selected_index == 0)
                {
                    background_color = ConsoleColor.DarkGreen; //vert si selectione                }
                }

                _renderManager.Draw(0, 1, "Enter to play", ConsoleColor.White, background_color);
                background_color = ConsoleColor.Black;

                if (selected_index == 1)
                {
                    background_color = ConsoleColor.DarkGreen;
                }

                _renderManager.Draw(0, 3, "Quit", ConsoleColor.White,background_color);

            }
            else if(current_state is ExplorationState)
            {
                _renderManager.Draw(0, 0, "Game in progress...\n", ConsoleColor.Magenta, ConsoleColor.Black);
                LocationComponent current_location = _gameManager.GetCurrentLocation();
                _renderManager.Draw(0, 1, $"Exploring {current_location.GetLocationName()}\n", ConsoleColor.Cyan, ConsoleColor.Black);

                for (int index = 0; index < current_location.GetConnectionCount(); index++) 
                {
                    Connection connection = current_location.GetConnection(index);
                    LocationComponent destination = connection.GetDestination();

                    ConsoleColor text_color = ConsoleColor.White;
                    ConsoleColor background_color = ConsoleColor.Black;

                    if (index == selected_index) //fait en sorte que ca s'affiche en vert
                    {
                        background_color = ConsoleColor.DarkGreen;
                    }
                    _renderManager.Draw(0, 2 + index, $"{index + 1}. {destination.GetLocationName()} : Distance : {connection.GetDistance()} ", text_color, background_color);
                }

                MessageComponent message = current_location.GetOwner().GetComponent<MessageComponent>(); // regarde si il y a component message

                if (message != null) //verifie si il possede message
                {
                    _renderManager.Draw(0, 5, $"[PANNEAU] : {message.GetMessage()}", ConsoleColor.Yellow, ConsoleColor.Black);
                }
            }

            _renderManager.Render();
        }

        private float GetCurrentTime()
        {
            return _stopwatch.ElapsedMilliseconds / 1000.0f;
        }
    }
}