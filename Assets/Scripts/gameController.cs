using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    //variables
    public Button[] buttons;

    private int chance = -1;
    private bool init = true;
    private int playerScore = 0;
    private int opponentScore = 0;
   
    // Update is called once per frame
    void Update()
    {
        if (playerScore >= opponentScore && init)
        {
            Debug.Log("Player has the first move.");
            show("Player has the first move.");
            for (int i = 1; i <= 9; i++)
            {
                GameObject.Find("Button (" + i.ToString() + ")").GetComponentInChildren<Text>().text = "-";
            }
            init = false;

            if (clicked())
            {

            }
        }
        else if(init && opponentScore > playerScore)
        {
            Debug.Log("Computer has the first move.");
            show("Computer has the first move.");
            for (int i = 1; i <= 9; i++)
            {
                GameObject.Find("Button (" + i.ToString() + ")").GetComponentInChildren<Text>().text = "-";
            }
            init = false;
        }
        if (check_win() == 1)
        {
            Debug.Log("Player has won.");
            show("Player has won.");
            int score = int.Parse(GameObject.Find("playerScore").GetComponent<Text>().text);
            score += 1;
            GameObject.Find("playerScore").GetComponent<Text>().text = score.ToString();
            init = true;
        }
        else if(check_win() == 2)
        {
            Debug.Log("Opponent has won.");
            show("Opponent has won.");
            int score = int.Parse(GameObject.Find("opponentScore").GetComponent<Text>().text);
            score += 1;
            GameObject.Find("opponentScore").GetComponent<Text>().text = score.ToString();
            init = true;
        }
    }

    private bool clicked()
    {
        return false;
    }

    private int check_win()
    {
        return 0;
    }
    private void show(string message)
    {
        StartCoroutine("wait");
        GameObject.Find("Info").GetComponentInChildren<Text>().text = message;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }
}
