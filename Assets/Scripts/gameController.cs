using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    //variables
    public Button[] buttons;

    private int chance = 0;
    private bool init = true;
    private int playerScore = 0;
    private int opponentScore = 0;
   
    // Update is called once per frame
    void Update()
    { 
        //If player won last match, first chance goes to Player.
        if (playerScore >= opponentScore && init)
        {
            show("Player has the first move.");

            //Clear all contents, and make all buttons interactable
            for (int i = 1; i <= 9; i++)
            {
                GameObject.Find("Button (" + i.ToString() + ")").GetComponentInChildren<Text>().text = "-";
                buttons[i-1].interactable = true;
            }
            init = false;

            //Make all buttons clickable and keep checking which button was pressed.
            for (int i = 0; i < buttons.Length; i++)
            {
                //In order to prevent Closure Problem caused by using register variables in call functions.
                int closureIndex = i;

                //Listen to all buttons
                buttons[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
            }
        }

        //If opponent won the last match
        else if (init && opponentScore > playerScore )
        {
            show("Computer has the first move.");
            chance = 1;
            //Clear all contents, and make all buttons interactable
            for (int i = 1; i <= 9; i++)
            {
                GameObject.Find("Button (" + i.ToString() + ")").GetComponentInChildren<Text>().text = "-";
                buttons[i-1].interactable = true;
            }
            init = false;            
        }

        do_this();
    }

    //On player clicked buttons, make them Non-interactable, and increment chance.
    public void TaskOnClick(int index)
    {
        buttons[index].GetComponentInChildren<Text>().text = "X";
        buttons[index].interactable = false;
        show("Chance changed to Opponent.");
        chance = 1;
    }

    private void do_this()
    {
        //If it's Opponent chance, do random choice for Computer.
        if(chance == 1)
        {
            while (true)
            {
                int r = Random.Range(0, 8);
                Debug.Log("Random: " + r);
                if (buttons[r].interactable)
                {
                    buttons[r].GetComponentInChildren<Text>().text = "O";
                    buttons[r].interactable = false;

                    show("Chance changed to Player.");
                    chance = 0;
                    break;
                }
            }
        }

        //Checking for win condition
        if (check_win() == 1)
        {
            show("Player has won.");
            playerScore += 1;
            GameObject.Find("playerScore").GetComponentInChildren<Text>().text = playerScore.ToString();

            StartCoroutine("wait");
            chance = 1;
            init = true;
        }
        else if (check_win() == 2)
        {
            show("Opponent has won.");
            
            opponentScore += 1;
            GameObject.Find("opponentScore").GetComponentInChildren<Text>().text = opponentScore.ToString();

            StartCoroutine("wait");
            chance = 2;
            init = true;
        }
    }
    private int check_win()
    {
        //PLAYER
        //Checking Positions Horizontal
        if ((buttons[0].GetComponentInChildren<Text>().text == "X" &&
            buttons[1].GetComponentInChildren<Text>().text == "X" &&
            buttons[2].GetComponentInChildren<Text>().text == "X") ||
            (buttons[3].GetComponentInChildren<Text>().text == "X" &&
            buttons[4].GetComponentInChildren<Text>().text == "X" &&
            buttons[5].GetComponentInChildren<Text>().text == "X") ||
            (buttons[6].GetComponentInChildren<Text>().text == "X" &&
            buttons[7].GetComponentInChildren<Text>().text == "X" &&
            buttons[8].GetComponentInChildren<Text>().text == "X"))
            return 1;

        //Checking Positions Vertical
        else if ((buttons[0].GetComponentInChildren<Text>().text == "X" &&
            buttons[3].GetComponentInChildren<Text>().text == "X" &&
            buttons[6].GetComponentInChildren<Text>().text == "X") ||
            (buttons[1].GetComponentInChildren<Text>().text == "X" &&
            buttons[4].GetComponentInChildren<Text>().text == "X" &&
            buttons[7].GetComponentInChildren<Text>().text == "X") ||
            (buttons[2].GetComponentInChildren<Text>().text == "X" &&
            buttons[5].GetComponentInChildren<Text>().text == "X" &&
            buttons[8].GetComponentInChildren<Text>().text == "X"))
            return 1;

        //Checking cross directional
        else if ((buttons[0].GetComponentInChildren<Text>().text == "X" &&
            buttons[4].GetComponentInChildren<Text>().text == "X" &&
            buttons[8].GetComponentInChildren<Text>().text == "X") ||
            (buttons[2].GetComponentInChildren<Text>().text == "X" &&
            buttons[4].GetComponentInChildren<Text>().text == "X" &&
            buttons[6].GetComponentInChildren<Text>().text == "X"))
            return 1;

        //OPPONENT
        //Checking Positions Horizontal
        if ((buttons[0].GetComponentInChildren<Text>().text == "O" &&
            buttons[1].GetComponentInChildren<Text>().text == "O" &&
            buttons[2].GetComponentInChildren<Text>().text == "O") ||
            (buttons[3].GetComponentInChildren<Text>().text == "O" &&
            buttons[4].GetComponentInChildren<Text>().text == "O" &&
            buttons[5].GetComponentInChildren<Text>().text == "O") ||
            (buttons[6].GetComponentInChildren<Text>().text == "O" &&
            buttons[7].GetComponentInChildren<Text>().text == "O" &&
            buttons[8].GetComponentInChildren<Text>().text == "O"))
            return 2;

        //Checking Positions Vertical
        else if ((buttons[0].GetComponentInChildren<Text>().text == "O" &&
            buttons[3].GetComponentInChildren<Text>().text == "O" &&
            buttons[6].GetComponentInChildren<Text>().text == "O") ||
            (buttons[1].GetComponentInChildren<Text>().text == "O" &&
            buttons[4].GetComponentInChildren<Text>().text == "O" &&
            buttons[7].GetComponentInChildren<Text>().text == "O") ||
            (buttons[2].GetComponentInChildren<Text>().text == "O" &&
            buttons[5].GetComponentInChildren<Text>().text == "O" &&
            buttons[8].GetComponentInChildren<Text>().text == "O"))
            return 2;

        //Checking cross directional
        else if ((buttons[0].GetComponentInChildren<Text>().text == "O" &&
            buttons[4].GetComponentInChildren<Text>().text == "O" &&
            buttons[8].GetComponentInChildren<Text>().text == "O") ||
            (buttons[2].GetComponentInChildren<Text>().text == "O" &&
            buttons[4].GetComponentInChildren<Text>().text == "O" &&
            buttons[6].GetComponentInChildren<Text>().text == "O"))
            return 2;

        else
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
