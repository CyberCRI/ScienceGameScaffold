using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateMachine : MonoBehaviour {
    
    private static GameStateMachine _instance;
    private int score = 0;    
    public Text scoreText;
    
    void Awake() {
        if(null == _instance) {
            _instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
        updateScore();
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
        GameStateMachine gsm = safeGet();
        gsm.score += pointsGot;
        updateScore();
    } 
    
    static public void updateScore() {
        GameStateMachine gsm = safeGet();
        if(null == gsm.scoreText) {
            gsm.scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        gsm.scoreText.text = gsm.score+" points";
    }
    
    static public int getScore() {
        return safeGet().score;
    }
}
