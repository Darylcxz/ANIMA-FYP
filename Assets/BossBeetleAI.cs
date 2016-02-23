using UnityEngine;
using System.Collections;

public class BossBeetleAI : MonoBehaviour {
    [SerializeField] enum BossStates
    {
        INGROUND,
        IDLE,
        WALKING,
        ATTACK,
        STUNNED,
        PRYING,
        HIT,
        DEATH
    }
    BossStates BeetleLogic;
    Animator BossAnim;

    float gameTime;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void StateMachine()
    {
        switch(BeetleLogic)
        {
            case BossStates.INGROUND:
                break;
            case BossStates.IDLE:
                break;
            case BossStates.ATTACK:
                break;
            case BossStates.STUNNED:
                break;
            case BossStates.PRYING:
                break;
            case BossStates.HIT:
                break;
            case BossStates.DEATH:
                break;
        }
    }

    void ChangeState(float timeBeforeStateChange, BossStates nextState)
    {
        if(gameTime > timeBeforeStateChange)
        {
            BeetleLogic = nextState;
            gameTime = 0;
        }
    }
}
