using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    Questions[] questionslist;
    public Questions question1; public Questions question2; public Questions question3; public Questions question4; public Questions question5; public Questions question8; public Questions question9; public Questions question10; public Questions question11; public Questions question12; public Questions question13; public Questions question14; public Questions question15;
    public string question6 = "Which of the following figures will form a perfect square by merging with the adjacent figure?";
    public string question7 = "Which figure does not match the others?";

    public GameObject question6panel;
    public GameObject question7panel;
    public TMP_Text scoreText;
    public int score = 0;

    public int questionsAttended;
    public GameObject startButton;
    public GameObject scorePanel, questionPanel, answerPanel;
    public TMP_Text questionText, option1Text, option2Text, option3Text, option4Text;

    public string correctOpt;

    private void Awake()
    {
        question1 = new Questions("Which ratio is higher: 1/3 or 2/5?",
        "1/3,2/3,They are equal,I do not know", "2/3");
        question2 = new Questions("If Friday is the 4th day of the month, what day is the 14th of the month?",
        "Monday,Wednesday,Sunday,Friday", "Monday");
        question3 = new Questions("If 4 people say \"hello\" to each other, how many times will the word \"hello\" be said?",
        "4,3,12,13", "12");
        question4 = new Questions("Care este rezultatul impartirii lui 67 la o patrime apoi insumat cu 32 ?",
        "168,268,300,312", "300");
        question5 = new Questions("If a man weighs 75 of his own weight plus 25 kilograms, what weight does he have?",
        "96,100,86,75", "96");
        question8 = new Questions("What number continues the sequence: 0, 1, 2, 4, 6, 9, 12, 16,…",
        "9,20,18,21", "20");
        question9 = new Questions("Every year, Kevin's parents put a number of coins in his wallet equal to the age he is. There are currently 28 coins in Kevin's wallet. How old is Kevin?",
        "8,7,12,28", "7");
        question10 = new Questions("What number continues the sequence: 0, 1, 1, 2, 3, 5, 8,…",
         "10,12,13,21", "13");
        question11 = new Questions("Continue the sequence: LM MN NO OP P",
         "Q,R,S,O", "Q");
        question12 = new Questions("Continue the sequence: bob bob zdr rdz 284",
         "sdr,214,482,bdr", "482");
        question13 = new Questions("A postman climbs 5 times a day to the 10th floor and 10 times to the 5th floor. If he had never descended to the ground floor, but would have always ascended, what floor would he have reached?+B9:B15",
         "200,100,10,5", "100");
        question14 = new Questions("Which of the following words is different from the others?",
         "Cat,Squirrel,Hen,Dog", "Hen");
        question15 = new Questions("A snail climbs a 3 meter tree during the day and slides at night 2. After how many days does it reach the top of the tree which is 10 meters high?",
         "8,9,10,7", "8");
        questionslist = new Questions[] {
            question1, question2, question3, question4, question5, question8,
            question9, question10, question11, question12, question13, question14, question15 };
    }
    private void Start()
    {
        questionsAttended = 0;
    }
    public void startQuiz()
    {
        startButton.SetActive(false);
        scorePanel.SetActive(true);
        questionPanel.SetActive(true);
        answerPanel.SetActive(true);
    }

    public void getSelectedAnswer(TMP_Text selectedAnswer)
    {
        questionsAttended++;
        if (questionsAttended < 13)
        {

            correctOpt = questionslist[questionsAttended - 1].correctAns;
            if (selectedAnswer.text == correctOpt)
            {
                score += 10;
                scoreText.text = score.ToString();
            }
            char delimeter = ',';
            var options = questionslist[questionsAttended].answers;
            var optli = options.Split(delimeter);
            questionText.text = questionslist[questionsAttended].question;
            option1Text.text = optli[0];
            option2Text.text = optli[1];
            option3Text.text = optli[2];
            option4Text.text = optli[3];
        }
        else
        {
            if (questionsAttended == 13)
            {
                questionText.text = question6;
                answerPanel.SetActive(false);
                question6panel.SetActive(true);
                // answer jo aega wo param me aega func k, wo print kr k check karna, sahi wala likh kr hi match kr k score ko database me fatt se phekna
            }
            else if (questionsAttended == 14)
            {
                questionText.text = question7;
                question6panel.SetActive(false);
                question7panel.SetActive(true);
            }
        }
    }
}

