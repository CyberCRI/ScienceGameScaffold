using UnityEngine;
using System.Collections;

public class GenericQuizUI : MonoBehaviour {

    private static GenericQuizUI _instance = null;
    
	public GameObject leftPopUp;
    public GameObject rightInfos;
    
    void Awake() {
        if(null == _instance) {
            _instance = this;
        } else {
            Destroy(this);
        }       
    }
    
    void Start() {
        GameStateMachine.updateScore();
    }
    
    private static GenericQuizUI safeGet() {
        if(null == _instance) {
            GenericQuizUI instance = GameObject.Find("GenericUI").GetComponent<GenericQuizUI>();
            return instance;
        } else {
            return _instance;
        }
    }
    
    public static void open() {
        safeGet().leftPopUp.active = true;
        safeGet().rightInfos.active = true;
    }
    
    public static void close() {
        safeGet().leftPopUp.active = false;
        safeGet().rightInfos.active = false;        
    }
}
