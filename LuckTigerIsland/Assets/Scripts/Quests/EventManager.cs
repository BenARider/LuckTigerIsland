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

public class EventManager : LTI.Singleton<EventManager>
{
    private QuestManager m_questManager = QuestManager.Instance;

    private ELocations m_lastLocation;

    private void Start()
    {
        instance = this;
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
