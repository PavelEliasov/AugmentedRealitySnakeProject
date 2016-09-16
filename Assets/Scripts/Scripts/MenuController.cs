using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Toggle obstacleToggle;
    [SerializeField]
    Slider speedSlider;
    [SerializeField]
    Text speedVAlue;
    [SerializeField]
    Text highScore;
    // Use this for initialization
    void Start () {
        speedVAlue.text = GameManager.Instance.Speed.ToString();
        highScore.text = GameManager.Instance.MaxScore.ToString();
    }


    public void NewGame() {
        SceneManager.LoadScene("2");
    }

    public void ObstacleOnOff() {// Toggle Method
        GameManager.Instance.isObstacle = obstacleToggle.isOn;

    }

    public void SpeedSlider() {//Slider Method

        GameManager.Instance.Speed = speedSlider.value;
        speedVAlue.text = GameManager.Instance.Speed.ToString();
       // Debug.Log();
    }

    public void SmallMode() {// Select Small Mode
        GameManager.Instance.size = SizeState.Small;
        NewGame();
    }

    public void NormalMode() {
        GameManager.Instance.size = SizeState.Normal;
        NewGame();
    }

    public void BigMode() {
        GameManager.Instance.size = SizeState.Big;
        NewGame();
    }

    public void Exit() {
        Application.Quit();
    }

}
