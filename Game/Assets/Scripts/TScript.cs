using UnityEngine;
using UnityEngine.SceneManagement;

public class TScript : MonoBehaviour
{
    public bool IsGoodT;

    private float timer;
    private float x;

    private void Start()
    {
        timer = 2;
        x = transform.localScale.x;
    }

    void Update()
    {
        if (HudController.Lost)
        {
            if (GameObject.Find("RealT(Clone)").transform.localScale.x == x && IsGoodT)
            {
                var t = GameObject.Find("RealT(Clone)");
                t.GetComponent<SpriteRenderer>().color = Color.white;
                t.transform.localScale *= 3;
            }

            timer -= Time.deltaTime;
            if (timer < 0 && IsGoodT)
            {
                HudController.Lives--;
                SceneManager.LoadScene("LevelLost");
            }

            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            //var touchPosition = Input.GetTouch(0).position;
            if (GetComponent<Collider2D>().bounds.Contains(mousePosition))// || GetComponent<Collider2D>().bounds.Contains(touchPosition))
            {
                Destroy(gameObject);
                if (IsGoodT)
                {
                    HudController.Score += 100 + (int)HudController.MainTimer * 100;
                    if (HudController.Score > PlayerPrefs.GetInt("High Score: ", 0))
                    {
                        PlayerPrefs.SetInt("High Score: ", HudController.Score);
                    }
                    SceneManager.LoadScene("LevelComplete");
                }
                else
                {
                    var t = GameObject.Find("RealT(Clone)");
                    t.GetComponent<SpriteRenderer>().color = Color.white;
                    t.transform.localScale *= 3;

                    HudController.Lost = true;
                }
            }
        }
    }
}
