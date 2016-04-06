using UnityEngine;
using System.Collections;

public class GameStateMachine : MonoBehaviour {
    
    private static GameStateMachine _instance;
    private int score = 0;    
    
    void Awake() {
        if(null == _instance) {
            _instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }
    
    private static GameStateMachine safeGet() {
        if(null == _instance) {
            GameStateMachine instance = GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>();
            DontDestroyOnLoad(instance);
            return instance;
        } else {
            return _instance;
        }
    }
    
    static public void addScore(int pointsGot) {
        safeGet().score += pointsGot;
        Debug.Log("new score: "+safeGet().score);
    } 
    
}
