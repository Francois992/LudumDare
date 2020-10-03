using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timer = 15f;
    public int nbOfTimeLoops = 15;


    

    IEnumerator UpdateTimer()
    {
        //takingAway = true;

        string minutes = ((int)timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");

        //timerText.text = minutes + ":" + seconds;
        yield return new WaitForSeconds(1);
        timer -= 1;
        //takingAwaky = false;
    }
}
