using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core.Events;

namespace activity_00_tap_26_27.Core.States
{
    public class TitleState : IState
    {
        //reagit a confirm et escape

        private GameManager _gameManager;

        public TitleState (GameManager game_manager) // le constructeur permet a l'etat de connaitre le gamemanager
        {
            _gameManager = game_manager;    
        }

        public void Enter()
        {
            _gameManager.GetEventManager().RegisterToEvent<GameActionGameEvent>(HandleInput);
        }

        public void Exit()
        {
            _gameManager.GetEventManager().UnregisterFromEvent<GameActionGameEvent>(HandleInput);
        }

        public void FixedUpdate(float fixed_elapsed_time)
        {
            throw new NotImplementedException();
        }

        public void HandleInput(IGameEvent base_event)
        {
            GameActionGameEvent game_event = (GameActionGameEvent)base_event;
            GameActionType action = game_event.GetActionType();

            if (action == GameActionType.CONFIRM)
            {
                _gameManager.ChangeState(new ExplorationState(_gameManager));//quitte ecran titre pour aller dans jeu
            }

            else if (action == GameActionType.CANCEL)
            {
                _gameManager.SetShouldQuit(true);
            }
        }

        public void Update(float elapsed_time)
        {
            throw new NotImplementedException();
        }
    }
}
