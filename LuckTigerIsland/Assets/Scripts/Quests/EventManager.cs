using System.Collections;
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

}

//Enemies
public enum EEnemies
{
    nullEnemy,
    Pig, //861 final boss pig
    Goblin,
    Bandit,
    Tiger,
    Skeleton,
    Knight,
    Lizardman,
    SkeletonBoss, //SpriteSheet 734/735. TinytinyHeroes armies
    MinotaurSkeleton  //838
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

    //Set the last location and check all location quests.
    public void SetBattleLocation(List<EEnemies> _enemies)
    {
        m_lastBattle = _enemies;
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
                                CheckCompletion(_q);
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
                        _io.DecreaseCurrentAmount(_amount);
                        if (_io.GetCurrentAmount() <= 0)
                        {
                            _io.SetIsComplete(true);
                            CheckCompletion(_q);
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

    public void CheckCompletion(Quest _q)
    {
        _q.CheckCompletion();
    }

}

