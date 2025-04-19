using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int floorsNumber = 0;
    [SerializeField] TextMeshProUGUI floorText;
    private string floorString;

    //public TMP_Text countText;
    public Button UpButton;
    public Button DownButton;
    public Button resetButton;

    public int countNumber = 0;
    public int currentFloor;

    public GameObject End_UI;

    void Start()
    {
        floorString = floorsNumber < 10 ? " " + floorsNumber.ToString() : floorsNumber.ToString();
        floorText.SetText(floorString);
        currentFloor = floorsNumber;
    }

    public void Count()
    {
        /* Description:
         * Keeps track of correct guesses and current floor number.
         * Checks for win condiction after each count. If floor 1 (lobby) is reached, call End().
         */

        countNumber++;
        //countText.SetText(countNumber.ToString());

        currentFloor--;
        floorString = currentFloor < 10 ? " " + currentFloor.ToString() : currentFloor.ToString();
        floorText.SetText((floorString).ToString());

        if (currentFloor == 1)
            End();
    }

    public void Reset()
    {
        /* Description:
         * Resets ony current floor number. 
         * Used when incorrect input is given
         */

        currentFloor = floorsNumber;
        floorText.SetText(floorsNumber.ToString());
    }

    public void FullReset()
    {
        /* Description:
         * Reset guess count and current floor number. 
         * Turns buttons interactable - Relevant since End() will disable button interaction. This turns it back on.
         */

        countNumber = 0;
        //countText.SetText(countNumber.ToString());

        currentFloor = floorsNumber;
        floorText.SetText(floorsNumber.ToString());

        DownButton.interactable = true;
        UpButton.interactable = true;

    }

    public void SwapButtonInteract()
    {
        /* Description:
         * If button interaction was off, turn it on,
         * If button interaction was on, turn it off
         */

        DownButton.interactable = !DownButton.interactable;
        UpButton.interactable = !UpButton.interactable;
    }

    public void End()
    {
        /* Description:
         * Called when win condition is true. Set buttons uninteractable (won't trigger any floor changes after this),
         * and set whatever is contained in End_UI active. This can be replaced/added to whatever scene/models show the Lobby has been reached.
         */

        DownButton.interactable = false;
        UpButton.interactable = false;

        End_UI.SetActive(true);
    }
}
