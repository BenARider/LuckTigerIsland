using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Locations
public enum ELocations
{
    Location1,
    Location2,
    Location3,
}

public enum EEnemies
{
    Pig
}

public class EventManager : MonoBehaviour
{

    public static EventManager instance;

    private QuestManager m_questManager = QuestManager.instance;

    private ELocations m_lastLocation;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There were two " + gameObject.name + "s present.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public ELocations GetLastLocation()
    {
        return m_lastLocation;
    }
    public void SetLastLocation(ELocations _location)
    {
        m_lastLocation = _location;
    }
}
