using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {
    
    // normalization by number of expected answers: no
    // 1 point per correct chosen answer
    // summary at end
    
    // possible improvements:
    //  - load data from xml
    //  - add a level of abstraction when managing questions ie use list of list of strings
    //  - use composite objects to load when displaying an answer, containing image, sound, text...
    //  - add images
    
    

    public Text quizText;
    public Text summary;
    public GameObject validateButton;
    public GameObject continueButton;
    public int pointsPerCorrectAnswer = 1;
    
    private QuizState state = QuizState.START;
    public GameObject toggle;
    public RectTransform toggle1;
    public RectTransform toggle2;
    
    List<string> goodAnswers = new List<string>();
    List<Answer> answerList = new List<Answer>();
    
    
    private enum QuizState {
        START = 0,
        QUESTION1WAITINGANSWER = 1,
        QUESTION2PREPARATION = 2,
        QUESTION2WAITINGANSWER = 3,
        END = 4
    }
	
	// Update is called once per frame
	void Update () {
	   switch(state) {
           
           case QuizState.START:
            
            validateButton.active = true;
            summary.gameObject.active = false;
            continueButton.active = false;
            
            toggle1.gameObject.active = false;
            toggle2.gameObject.active = false;
            
            quizText.text = "Quelle est ta couleur préférée ?";
            
            prepareAnswers(new List<string>(){"Bleu", "Jaune", "Vert"});
            goodAnswers = new List<string>(){"Bleu", "Jaune"};
            
            state = QuizState.QUESTION1WAITINGANSWER;
           break;
           
            case QuizState.QUESTION1WAITINGANSWER:
            break;
            
            case QuizState.QUESTION2PREPARATION:
            quizText.text = "Quelle est ta couleur détestée ?"; 
            
            prepareAnswers(new List<string>(){"Violet", "Indigo", "Pourpre"});
            goodAnswers = new List<string>(){"Pourpre"};
            
            state = QuizState.QUESTION2WAITINGANSWER;           
            break;
           
            case QuizState.QUESTION2WAITINGANSWER:
            break;
            
            case QuizState.END:
            quizText.text = "Résumé";
            
            validateButton.active = false;
            summary.gameObject.active = true;
            continueButton.active = true;
             
            summary.text = "t'es trop fort";
             
            break;
       }
	}
    
    Answer prepareAnswer(string answerText, Answer previousAnswer = null) {
        GameObject answerGO = Instantiate(toggle);
        answerGO.transform.parent = this.gameObject.transform;
        Answer answer = answerGO.GetComponent<Answer>();
        answer.answerText.text = answerText;
        
        RectTransform answerRT = answer.GetComponent<RectTransform>();
        
        //case: all answers but the first
        if(null != previousAnswer) {       
            
            RectTransform previousAnswerRT = previousAnswer.GetComponent<RectTransform>();     
            
            float yShift = toggle1.transform.localPosition.y - toggle2.transform.localPosition.y;
            answerRT.transform.localPosition = new Vector3(
                previousAnswerRT.transform.localPosition.x, 
                previousAnswerRT.transform.localPosition.y - yShift,
                previousAnswerRT.transform.localPosition.z);
        
        //case: first anwser        
        } else { 
            answerRT.transform.localPosition = toggle1.transform.localPosition;
        }
        return answer;
    }
    
    void prepareAnswers(List<string> answers) {
        Answer previousAnswer = null;
        answerList.Clear();
        foreach (string answerText in answers) {
            previousAnswer = prepareAnswer(answerText, previousAnswer);
            answerList.Add(previousAnswer);
        }
    }
    
    // goes on to the next question
    public void onValidate() {        
        foreach (Answer answerObject in answerList)
        {
            if(answerObject.toggle.isOn) {
                foreach (string answerString in goodAnswers)
                {
                    if (answerString == answerObject.answerText.text) {
                        GameStateMachine.addScore(pointsPerCorrectAnswer);
                        break;
                    }
                }
            }
        }
        
        state++;
        clearAnswers();
    }
    
    void clearAnswers() {
        foreach(Answer answer in answerList) {
            Destroy(answer.gameObject);
        }
        answerList.Clear();
    }
    
    // exits quiz
    public void onContinue() {
        GenericQuizUI.close();
        state = QuizState.START;
    }
}

