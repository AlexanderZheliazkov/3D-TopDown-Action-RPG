using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New game event", menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> gameEventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = gameEventListeners.Count - 1; i >= 0; i--)
        {
            gameEventListeners[i].OnEventRaised();
        }
    }

    public void AddListener(GameEventListener _listener)
    {
        if (gameEventListeners.Contains(_listener))
        {
            Debug.Log($"{this.name} already contains {_listener} as a listener and will not be added again!");
            return;
        }
        gameEventListeners.Add(_listener);
    }

    public void RemoveListener(GameEventListener _listener)
    {
        gameEventListeners.Remove(_listener);
    }
}
