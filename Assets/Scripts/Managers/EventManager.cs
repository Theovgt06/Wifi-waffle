using System;
using System.Collections.Generic;

// Types d'�v�nements possibles dans notre jeu
public enum GameEventType
{
    GameStart,
    GameOver,
    EnemyKilled,
    PlayerDamaged,
    AmmoCollected,
    LifeCollected,
}

// Classe statique pour g�rer les événements globaux
public static class EventManager
{
    // Dictionnaire stockant les diff�rents �v�nements et leurs abonn�s
    private static Dictionary<GameEventType, Action<object>> eventDictionary =
        new Dictionary<GameEventType, Action<object>>();

    // S'abonner � un �v�nement
    public static void Subscribe(GameEventType eventType, Action<object> listener)
    {
        eventDictionary.TryAdd(eventType, null);

        eventDictionary[eventType] += listener;
    }

    // Se d�sabonner d'un �v�nement
    public static void Unsubscribe(GameEventType eventType, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
        {
            eventDictionary[eventType] -= listener;
        }
    }

    // D�clencher un �v�nement
    public static void TriggerEvent(GameEventType eventType, object data = null)
    {
        if (eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
        {
            eventDictionary[eventType].Invoke(data);
        }
    }
}