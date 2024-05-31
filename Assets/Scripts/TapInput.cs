using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TapInput : MonoBehaviour
{
    public GameObject flyMissedImage;
    public float animationDuration = .75f;
    
    private int flyCoinBalance;

    private ScoreManager scoreManager;
    private MainMenuManager mainMenuManager;
    private AudioManager audioManager;


    private void Start()
    {
        flyCoinBalance = PlayerPrefs.GetInt("FlyCoinBalance", 0);

        scoreManager = FindObjectOfType<ScoreManager>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                
                Destroy(gameObject);
                scoreManager.IncrementRoundScore();
                PlayerPrefs.SetInt("FlyCoinBalance", PlayerPrefs.GetInt("FlyCoinBalance") + 1);
            }



            // Convert mouse position to canvas position
            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<Canvas>().transform as RectTransform,
                Input.mousePosition,
                GetComponent<Canvas>().worldCamera,
                out canvasPos
            );

            // Instantiate tap animation prefab
            GameObject tapAnimation = Instantiate(flyMissedImage, canvasPos, Quaternion.identity);

            // Set the parent of the image to the TapAnimationManager
            tapAnimation.transform.SetParent(transform);

            // Start the coroutine to remove the animation after a delay
            StartCoroutine(RemoveAnimationAfterDelay(tapAnimation));
        }
    }

    private IEnumerator RemoveAnimationAfterDelay(GameObject animation)
    {
        yield return new WaitForSeconds(animationDuration);
        Destroy(animation);
    }
}
