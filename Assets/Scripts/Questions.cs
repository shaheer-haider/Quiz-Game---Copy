using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions
{
    public string question;
    public string answers;
    public string correctAns;

    public Questions(string question, string answers, string correctAns)
    {
        this.question = question;
        this.answers = answers;
        this.correctAns = correctAns;
    }

}
