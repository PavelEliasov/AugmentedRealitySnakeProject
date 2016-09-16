using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {
    SceneController _scenecontroller;
	// Use this for initialization
	void Start () {
        _scenecontroller = FindObjectOfType<SceneController>();
	}
	
	

    void OnTriggerEnter(Collider other) {//ReInstantiate food when it coords equal walls coords
        if (other.gameObject.tag=="Wall") {
            ObjectPool.Recycle(this.gameObject);
            _scenecontroller.SpawnFood();
        }
    }
}
