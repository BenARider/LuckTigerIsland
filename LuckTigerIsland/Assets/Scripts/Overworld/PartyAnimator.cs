using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyAnimator : MonoBehaviour {
    public GameObject playerMain;
    public GameObject[] otherPlayers; //3 length
    

    float m_updateTimer = 0f;


    Vector3[,] lastPositions = new Vector3[3,10]; //10 is latest
    int[] directions = new int[4]; //right,down,up,-right


	// Use this for initialization
	void Start () {
        ResetVars();

    }

    public void ResetVars()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                lastPositions[i, j] = playerMain.transform.position;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            directions[i] = 0;
        }
        otherPlayers[0].transform.position = playerMain.transform.position;
        otherPlayers[1].transform.position = playerMain.transform.position;
        otherPlayers[2].transform.position = playerMain.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate () {
        m_updateTimer += Time.fixedDeltaTime*20f;
        if (m_updateTimer>=1.0f)
        {
            newPositions();
            m_updateTimer = 0f;
        }
        otherPlayers[0].transform.position = Vector3.Lerp(lastPositions[0, 1], lastPositions[0, 2], m_updateTimer);
        otherPlayers[1].transform.position = Vector3.Lerp(lastPositions[1, 1], lastPositions[1, 2], m_updateTimer);
        otherPlayers[2].transform.position = Vector3.Lerp(lastPositions[2, 1], lastPositions[2, 2], m_updateTimer);
        
    }

    void newPositions()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                lastPositions[i, j] = lastPositions[i, j + 1];
            }
        }
        lastPositions[0, 9] = playerMain.transform.position;
        lastPositions[1, 9] = otherPlayers[0].transform.position;
        lastPositions[2, 9] = otherPlayers[1].transform.position;
    }
}
