using System.Collections.Generic;
using System;
using GameLibrary;

namespace activity_00_tap_26_27.Core.Events
{
    public class EventManager
    {
        private readonly Dictionary<Type, List<Action<IGameEvent>>> _eventTypeTable = new Dictionary<Type, List<Action<IGameEvent>>>();
        private Queue<IGameEvent> _eventQueue = new Queue<IGameEvent>();

        public void RegisterToEvent<TYPE>(Action<IGameEvent> action) where TYPE : IGameEvent //ecoute les touches du clavier
        {
            Type event_type = typeof(TYPE);

            if (!_eventTypeTable.ContainsKey(event_type))
            {
                _eventTypeTable.Add(event_type, new List<Action<IGameEvent>>());
            }

            _eventTypeTable[event_type].Add(action);
        }

        public void UnregisterFromEvent<TYPE>(Action<IGameEvent> action) where TYPE : IGameEvent //Se desabonne quand objet est detruit
        {
            Type event_type = typeof(TYPE);

            if (_eventTypeTable.ContainsKey(event_type))
            {
                _eventTypeTable[event_type].Remove(action);
            }
        }

        public void TriggerDelayedEvent(IGameEvent game_event) //prend l'evenement et le place dans file d'attente
        {
            _eventQueue.Enqueue(game_event);
        }

        public void TriggerEvent(IGameEvent game_event)// execute les action immediatement en prevenant tous ceux abonne
        {
            Type event_type = game_event.GetType();

            if (_eventTypeTable.ContainsKey(event_type))
            {
                for (int event_index = 0; event_index < _eventTypeTable[event_type].Count; event_index++)
                {
                    Action<IGameEvent> action = _eventTypeTable[event_type][event_index];
                    action(game_event);
                }
            }
        }

        public void ProcessEvents() //declenche les evenement de la file d'attente
        {
            while (_eventQueue.Count > 0)
            {
                IGameEvent game_event = _eventQueue.Dequeue();
                Type event_type = game_event.GetType();
                if (_eventTypeTable.TryGetValue(game_event.GetType(), out List<Action<IGameEvent>> actions))
                {
                    foreach(Action<IGameEvent> action in actions)
                    {
                        action(game_event);
                    }
                }
            }
        }
    }
}