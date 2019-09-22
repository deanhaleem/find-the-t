using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }
    public static Difficulty DifficultyLevel = Difficulty.Easy;

    public List<GameObject> BadTs;
    public GameObject GoodT;
    public GameObject Indicator;
    public int MaxTries;

    private readonly List<Bounds> _previousPositions = new List<Bounds>();
    private readonly Color[] _colors = new Color[6];
    private bool _update = true;
    private int _numOfEachBadT;
    private int _goalNumTs;

    void Start()
    {
        // set up indicator
        Instantiate(Indicator, Vector3.zero, Quaternion.identity);

        // set up colors
        _colors[0] = Color.cyan;
        _colors[1] = Color.red;
        _colors[2] = Color.green;
        _colors[3] = Color.blue;
        _colors[4] = Color.yellow;
        _colors[5] = Color.magenta;

        // set up number of Ts
        if (DifficultyLevel == Difficulty.Easy)
        {
            _numOfEachBadT = 3;
            _goalNumTs = 12;
        }
        else if (DifficultyLevel == Difficulty.Medium)
        {
            _numOfEachBadT = 3;
            _goalNumTs = 15;
        }
        else
        {
            _numOfEachBadT = 4;
            _goalNumTs = 20;
        }
    }

    void Update()
    {
        if (IndicatorController.IndicatorTimer > 0 || !_update)
        {
            return;
        }

        int totalTs = 0;
        int tries = 0;
        for (int i = 0; i < _numOfEachBadT; i++)
        {
            foreach (var t in BadTs)
            {
                tries = 0;
                var badTPosition = GetRandomPositionOnScreen();
                var tBounds = new Bounds(badTPosition, Vector3.one);

                while (_previousPositions.Exists(p => p.Intersects(tBounds)) && tries < MaxTries)
                {
                    badTPosition = GetRandomPositionOnScreen();
                    tBounds = new Bounds(badTPosition, Vector3.one);
                    tries++;
                }

                if (totalTs < _goalNumTs && tries < MaxTries)
                {
                    var badT = Instantiate(t, badTPosition, Quaternion.identity);
                    badT.GetComponent<SpriteRenderer>().color = _colors[Random.Range(0, _colors.Length)];

                    _previousPositions.Add(badT.GetComponent<Collider2D>().bounds);
                    totalTs++;
                }
            }
        }

        var goodTBounds = _previousPositions[0];
        var goodTPosition = GetRandomPositionOnScreen();

        var badTt = GameObject.FindGameObjectsWithTag("T").ToList().Find(t => t.GetComponent<Collider2D>().bounds.Intersects(goodTBounds));
        if (badTt != null)
        {
            goodTPosition = badTt.transform.position;
            Destroy(badTt);
        }

        var goodT = Instantiate(GoodT, goodTPosition, Quaternion.identity);
        goodT.GetComponent<SpriteRenderer>().color = _colors[Random.Range(0, _colors.Length)];

        _update = false;
    }

    private Vector3 GetRandomPositionOnScreen()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(30, Screen.width - 20), Random.Range(30, Screen.height - Screen.height / 4), 10));
    }
}
