using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Locations
public enum ELocations
{
    nullLocation,
    Location1,
    Location2,
    Location3,
}

//Enemies
public enum EEnemies
{
    nullEnemy,
    Pig
}

public class EventManager : LTI.Singleton<EventManager>
{
    private QuestManager m_questManager;

    private ELocations m_lastLocation;

    private void Start()
    {
        instance = this;
        m_questManager = QuestManager.Instance;
    }

    public ELocations GetLastLocation()
    {
        return m_lastLocation;
    }

    //Set the last location and check all location quests.
    public void SetLastLocation(ELocations _location)
    {
        m_lastLocation = _location;
        //In all active quests
        foreach(Quest _q in m_questManager.GetQuests())
        {
            //If the location objective isnt empty.
            if(_q.m_locationObjectives.Capacity != 0)
            {
                //For every location objective
                foreach(LocationObjective _lo in _q.m_locationObjectives)
                {
                    //Check if the objective location is equal to current location.
                    if(_lo.GetLocation() == m_lastLocation)
                    {
                        _lo.SetIsComplete(true);
                        CheckCompletion(_q);
                    }
                }
            }
        }
    }

    public void CheckCompletion(Quest _q)
    {
        _q.CheckCompletion();
    }

}

