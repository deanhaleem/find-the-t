using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public static float IndicatorTimer;

    public Sprite NewSprite;
    public GameObject GameObject;

    void Start()
    {
        // set up timer
        if (GameController.DifficultyLevel == GameController.Difficulty.Easy)
        {
            IndicatorTimer = Random.Range(2f, 4f);
        }
        else if (GameController.DifficultyLevel == GameController.Difficulty.Medium)
        {
            IndicatorTimer = Random.Range(2f, 4f);
        }
        else
        {
            IndicatorTimer = Random.Range(1.5f, 3f);
        }
    }

    void Update()
    {
        IndicatorTimer -= Time.deltaTime;

        if (IndicatorTimer < 1)
        {
            GetComponent<SpriteRenderer>().sprite = NewSprite;
        }

        if (IndicatorTimer < 0)
        {
            Destroy(GameObject);
        }
    }
}
