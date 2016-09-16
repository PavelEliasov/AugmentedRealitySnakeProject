using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    [SerializeField]
    GameObject cameraMain;
    [SerializeField]
    GameObject plane;
    [SerializeField]
    GameObject food;
    
    WallScript [] walls;//obstacle array for enable/disable

    //float _width;
    //float _height;
    float _cameraHeight;

    int score;//score in game
    [SerializeField]
    int scoreStep;
    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject diePanel;

    [Header("FoodSpawnLimits")]
    [SerializeField]
    Transform _upSpawnLimit;
    [SerializeField]
    Transform _downSpawnLimit;
    [SerializeField]
    Transform _leftSpawnLimit;
    [SerializeField]
    Transform _rightSpawnLimit;

    Vector3 spawnVector;
	// Use this for initialization
	void Start () {

        ObjectPool.CreatePool(food,2);
       
        walls = FindObjectsOfType<WallScript>();

        foreach (WallScript wall in walls) {
            wall.gameObject.SetActive(GameManager.Instance.isObstacle);
        }
        SetSize(GameManager.Instance.size);//check size of main plane

        //For Non-augmentReality mode
        _cameraHeight = cameraMain.transform.position.y;


	}

    void SetSize(SizeState size) {//check size of main plane

        switch (size) {
            case SizeState.Small:

                break;
            case SizeState.Normal:
                cameraMain.transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y*1.5f, cameraMain.transform.position.z);//For Non augmentReality mode
                plane.transform.localScale = new Vector3(plane.transform.localScale.x*1.5f, plane.transform.localScale.y, plane.transform.localScale.z*1.5f);
                break;
            case SizeState.Big:
                cameraMain.transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y * 2f, cameraMain.transform.position.z);//For Non augmentReality mode
                plane.transform.localScale = new Vector3(plane.transform.localScale.x * 2f, plane.transform.localScale.y, plane.transform.localScale.z * 2f);
                break;
        }

    }


   public void SpawnFood() {
        AddScore();
        spawnVector = new Vector3(Random.Range(_leftSpawnLimit.position.x,_rightSpawnLimit.transform.position.x),
                                  0,
                                  Random.Range(_downSpawnLimit.position.z, _upSpawnLimit.transform.position.z));
      //  Debug.Log(spawnVector);
      var _food=ObjectPool.Spawn(food,spawnVector);
    }



    public void AddScore() {
        score += scoreStep;
        scoreText.text = score.ToString();
    }

    public void Die() {
        diePanel.SetActive(true);
        GameManager.Instance.MaxScore = score;
       // Time.timeScale = 0;
    }

    public void TryAgain() {
        SceneManager.LoadScene("2");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }

    void SeparateVerticeses() {


    }
}
