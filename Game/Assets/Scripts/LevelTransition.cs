using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private float _timer = 3;

    void Start()
    {
        
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            IndicatorController.IndicatorTimer = 1;
            SceneManager.LoadScene("Game");
        }
    }
}
