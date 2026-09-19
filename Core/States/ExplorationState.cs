using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core.Components;
using activity_00_tap_26_27.Core.Events;

namespace activity_00_tap_26_27.Core.States
{
    public class ExplorationState : IState
    {
        public GameManager _gameManager;

        public ExplorationState(GameManager game_manager)
        {
            _gameManager = game_manager;
        }
        public void Enter()
        {
            _gameManager.GetEventManager().RegisterToEvent<GameActionGameEvent>(HandleAction);
        }

        public void Exit()
        {
            _gameManager.GetEventManager().UnregisterFromEvent<GameActionGameEvent>(HandleAction);
        }

        public void FixedUpdate(float fixed_elapsed_time)
        {
            throw new NotImplementedException();
        }

        public void HandleAction(IGameEvent base_event)
        {
            GameActionGameEvent game_event = (GameActionGameEvent)base_event;
            GameActionType action = game_event.GetActionType();

            int current_index = _gameManager.GetSelectionIndex();
            int maxConnections = _gameManager.GetCurrentLocation().GetConnectionCount();


            if (action == GameActionType.NAVIGATE_DOWN)
            {
                if (current_index < maxConnections - 1) // verifie si la selection est a -1 (encore aucune destination selectione)
                {
                    _gameManager.SetSelectionIndex(current_index + 1); // met sur la premiere destination de la liste
                }
            }
            else if (action == GameActionType.NAVIGATE_UP)
            {
                if (current_index > 0)
                {
                    _gameManager.SetSelectionIndex(current_index - 1);
                }
            }

            else if (action == GameActionType.CONFIRM)// quand un index est deja selectione
            {
                if (current_index != -1 && current_index < maxConnections) //si index valid selectione
                {
                    LocationComponent current_location = _gameManager.GetCurrentLocation();
                    Connection chosenConnection = current_location.GetConnection(current_index);
                    LocationComponent new_location = chosenConnection.GetDestination();

                    _gameManager.SetCurrentLocation(new_location);
                    _gameManager.SetSelectionIndex(0);

                }
            }
        }

        public void Update(float elapsed_time)
        {
            throw new NotImplementedException();
        }
    }
}
