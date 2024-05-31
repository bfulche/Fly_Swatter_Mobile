using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text flyCoinBalanceText;

    private const string FlyCoinBalanceKey = "FlyCoinBalance";
    private int flyCoinBalance = 0;

    private void Start()
    {
        //Check if the fly coin balance is already stored in PlayerPrefs
        if (PlayerPrefs.HasKey(FlyCoinBalanceKey))
        {
            //Retrieve the fly coin balance from PlayerPrefs
            flyCoinBalance = PlayerPrefs.GetInt(FlyCoinBalanceKey);
        }
        else
        {
            //Create the fly coin balance in PlayerPrefs with an initial value of zero
            PlayerPrefs.SetInt(FlyCoinBalanceKey, flyCoinBalance);
        }

        //Update the UI text with the current fly coin balance
        UpdateFlyCoinBalanceText();
        
    }


    private void UpdateFlyCoinBalanceText()
    {
        //Update the UI text with the currenct fly coin balance
        flyCoinBalanceText.text = "Fly Coin Balance: " + flyCoinBalance.ToString();
    }

    public void UpdateFlyCoinBalance(int amount)
    {
        //Update the fly coin balance
        flyCoinBalance += amount;

        //UPdate the UI text with the updated fly coin balance
        UpdateFlyCoinBalanceText();

        //Store the fly coin balance in PlayerPrefs
        PlayerPrefs.SetInt(FlyCoinBalanceKey, flyCoinBalance);
    }



    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
