using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using activity_00_tap_26_27.Core.Events;
using GameLibrary;

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
            _gameManager.SetSelectionIndex(0);
        }

        public void Exit()
        {
            
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
            int max_choice = 2;

            if( action == GameActionType.NAVIGATE_DOWN)
            {
                if(current_index < max_choice - 1) // si pas sur le dernier choix on descent
                {
                    _gameManager.SetSelectionIndex(current_index + 1);
                }
            }

            else if(action == GameActionType.NAVIGATE_UP)
            {
                if(current_index >0)//si pas sur la premiere option on monte
                {
                    _gameManager.SetSelectionIndex(current_index - 1);
                }
            }

            else if (action == GameActionType.CONFIRM)
            {
                if(current_index == 0) // le joueur choisis play
                {
                    _gameManager.SetSelectionIndex(-1);
                    _gameManager.ChangeState(new ExplorationState(_gameManager));//quitte ecran titre pour aller dans jeu
                }
                else if(current_index == 1)
                {
                    _gameManager.SetShouldQuit(true);
                }
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
