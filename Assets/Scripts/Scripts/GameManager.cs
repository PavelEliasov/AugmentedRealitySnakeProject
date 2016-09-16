using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    int _maxScore;

    static GameManager _instance;

    float _speed;//snake speed

    public bool isObstacle;

    [SerializeField]
    float minSpeedValue;

    public SizeState size=SizeState.Small;
     
    public static GameManager Instance {
        get  {
            if (_instance==null) {
                _instance = FindObjectOfType<GameManager>();
                if (_instance==null) {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }
            return _instance;
        }

        set   {
            _instance = value;
        }
    }

    public float Speed  {
        get {
            if (_speed<minSpeedValue) {
                _speed = minSpeedValue;
            }
            return _speed;
        }

        set {
            _speed = value;
        }
    }

    public int MaxScore {
        get  {

            return _maxScore;
        }

        set {
            if (value > _maxScore) {
                _maxScore = value;
                PlayerPrefs.SetInt("MaxScore",_maxScore);//serialize Maxscore
            }
            
        }
    }

    void Awake() {
        _maxScore = PlayerPrefs.GetInt("MaxScore");
        
        if (FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
        }
        

    }
	



 
}
