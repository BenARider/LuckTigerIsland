﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Locations
public enum ELocations
{
    nullLocation,
    Castleton,
    Elftown,
    City,
    Oceanview,
    Cliffside,
    SpiritCave,
    MtLarge,
    IslandBoat,

}

//Enemies
public enum EEnemies
{
    nullEnemy,
    PigBoss,
    Goblin,
    Farmer,
    Bandit,
    Knight,
    Wolf,
    Tiger,
    Troll,
    Birdman,
    Skeleton,
    Golem,
    Spirit,
    Warlock,
    ChickenBoss,
    TigerBoss,
    PorkBoss,
    MinotaurSkeleton
}

public class EventManager : LTI.Singleton<EventManager>
{
    private QuestManager m_questManager;

    private ELocations m_lastLocation;
    private List<EEnemies> m_lastBattle;

    private void Start()
    {
        instance = this;
        m_questManager = QuestManager.Instance;
    }

    public ELocations GetLastLocation()
    {
        return m_lastLocation;
    }

    public List<EEnemies> GetLastBattle()
    {
        return new List<EEnemies>(m_lastBattle);
    }

    //Set the last location and check all location quests.
    public void SetLastLocation(ELocations _location)
    {
        m_lastLocation = _location;
        //In all active quests
        foreach (Quest _q in m_questManager.GetQuests())
        {
            //If the location objective isnt empty.
            if (_q.m_locationObjectives.Capacity != 0)
            {
                //For every location objective
                foreach (LocationObjective _lo in _q.m_locationObjectives)
                {
                    //Check if the objective location is equal to current location.
                    if (_lo.GetLocation() == m_lastLocation)
                    {
                        _lo.SetIsComplete(true);
                        _q.CheckCompletion();
                    }
                }
            }
        }
    }

    //Set the last location and check all location quests.
    public void SetLastBattle(List<EEnemies> _enemies)
    {
        m_lastBattle = _enemies;


        foreach (EEnemies _e in m_lastBattle)
        {
            Debug.Log(_e);
        }


        //In all active quests
        foreach (Quest _q in m_questManager.GetQuests())
        {
            //If the kill objective isnt empty.
            if (_q.m_killObjectives.Capacity != 0)
            {
                //For every kill objective
                foreach (KillObjective _ko in _q.m_killObjectives)
                {
                    foreach (EEnemies _e in m_lastBattle)
                    {
                        //Check if the enemies killed are part of an objective
                        if (_ko.GetEnemy() == _e)
                        {
                            _ko.ReduceAmountRemaining();
                            if (_ko.GetAmountRemaining() <= 0)
                            {
                                _ko.SetIsComplete(true);
                                _q.CheckCompletion();
                            }
                        }
                    }
                }
            }
        }
    }

    public void ItemToInventory(InventoryObject _object, int _amount)
    {
        
        //In all active quests
        foreach (Quest _q in m_questManager.GetQuests())
        {
            //If the inventory objective isnt empty.
            if (_q.m_inventoryObjectives.Capacity != 0)
            {
                //For every inventory objective
                foreach (ItemObjective _io in _q.m_inventoryObjectives)
                {
                    //Check if the item picked up is part of the quest
                    if (_io.GetInvObject() == _object)
                    {
                        Debug.Log("Item picked up");
                        _io.DecreaseCurrentAmount(_amount);
                        Debug.Log("curr am: " + _io.GetCurrentAmount());
                        if (_io.GetCurrentAmount() <= 0)
                        {
                            Debug.Log("all picked up");
                            _io.SetIsComplete(true);
                            _q.CheckCompletion();
                        }
                    }
                }
            }
        }
    }

    //When an item is taken from the inventory, reduce 
    public void ItemFromInventory(InventoryObject _object, int _amount)
    {
        //In all active quests
        foreach (Quest _q in m_questManager.GetQuests())
        {
            //If the inventory objective isnt empty.
            if (_q.m_inventoryObjectives.Capacity != 0)
            {
                //For every inventory objective
                foreach (ItemObjective _io in _q.m_inventoryObjectives)
                {
                    //Check if the item picked up is part of the quest
                    if (_io.GetInvObject() == _object)
                    {
                        _io.DecreaseCurrentAmount(-_amount);
                        if (_io.GetCurrentAmount() >= _io.GetTotalAmount())
                        {
                            _io.SetCurrentAmount(_io.GetTotalAmount());
                        }
                    }
                }
            }
        }
    }

    public void CheckDialogueObj(NPCDialogue _dialogue)
    {
        //In all active quests
        foreach (Quest _q in m_questManager.GetQuests())
        {
            //If the inventory objective isnt empty.
            if (_q.m_dialogueObjectives.Capacity != 0)
            {
                //For every inventory objective
                foreach (DialogueObjective _do in _q.m_dialogueObjectives)
                {
                    //Check if the item picked up is part of the quest
                    if (_do.GetDialogue() == _dialogue)
                    {
                        _do.SetIsComplete(true);
                        _q.CheckCompletion();
                    }
                }
            }
        }
    }
}

