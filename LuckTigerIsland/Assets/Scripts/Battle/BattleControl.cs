using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleControl : MonoBehaviour {


    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    public bool loadSpeeds;
    public bool sortSpeeds;
    //public BattlePlayer[] m_players;
    //public Enemy_AI[] m_enemies;
    public static int currentTurn;
	public static string willDamage; //state n for no damage taken, state y for damage taken. Alternate state could indicate invulnerable or other in future.
	public static int currentDamage;
	public static int currentTarget;
	public static string side;
	public static int totalFighters = 8; //will be used to check when to reset the turn timer and stuff.

    private float Player1Speed;
    private float Player2Speed;
    private float Player3Speed;
    private float Player4Speed;

    private float Enemy1Speed;
    private float Enemy2Speed;
    private float Enemy3Speed;
    private float Enemy4Speed;

    // Use this for initialization
    void Start () {
		currentTurn = 1;
		willDamage = "n";
		currentDamage = 0;
		currentTarget = 0;
		Debug.Log("Setup complete");
		NewTurn();

        List<float> baseSpeedList = new List<float>(8) { Player1Speed, Player2Speed, Player3Speed, Player4Speed, Enemy1Speed, Enemy2Speed, Enemy3Speed, Enemy4Speed };


        loadSpeed();
        sortSpeed(baseSpeedList);

        List<float> sortedSpeedList = baseSpeedList;
	}
	/// <summary>
	/// Resets back to beginning of the turn order.
	/// Currently only resets turn order to begining. So the order is currently P/E/P/E/P/E/P/E with 8 things available.
	/// </summary>
	void NewTurn()
	{
        Debug.Log("New turn started");
	}
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(willDamage + " " + currentDamage + "On target: " + side + " " + currentTarget);
		if (currentTurn > 8)
		{
            NewTurn();
		}
	}

    bool loadSpeed() //add later a error handeler that wouls throw an error if this returned false
    {
     
        Player1Speed = Player1.GetComponent<BattleEntity>().GetSpeed();
        Player2Speed = Player2.GetComponent<BattleEntity>().GetSpeed();
        Player3Speed = Player3.GetComponent<BattleEntity>().GetSpeed();
        Player4Speed = Player4.GetComponent<BattleEntity>().GetSpeed();

        Enemy1Speed = Enemy1.GetComponent<BattleEntity>().GetSpeed();
        Enemy2Speed = Enemy2.GetComponent<BattleEntity>().GetSpeed();
        Enemy3Speed = Enemy3.GetComponent<BattleEntity>().GetSpeed();
        Enemy4Speed = Enemy4.GetComponent<BattleEntity>().GetSpeed();

        return true;
    }

    bool sortSpeed(List<float> baseSpeedList)
    {

        baseSpeedList.Sort();

        for (int i=0; i< baseSpeedList.Count;i++)
        {
            Debug.Log(baseSpeedList[i]);
        }

        return true;
    }
}