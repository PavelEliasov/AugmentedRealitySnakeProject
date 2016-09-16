using UnityEngine;
using System.Collections;

public class HeadControll : MonoBehaviour {
   
    public  enum Direction {
        up,
        down,
        left,
        right
    }
    SnakeController controller;

    SceneController scenecontroller;

    public Direction direct = Direction.up;

   
    float speed = 1;

    Transform headTransform;

	void Start () {
        scenecontroller = FindObjectOfType<SceneController>();
        controller = FindObjectOfType<SnakeController>();
        headTransform = GetComponent<Transform>();
        speed = GameManager.Instance.Speed;
	}
	
	void Update () {
        Move();
	}

    void Move() {
        switch (direct) {
            case Direction.up:
                //   headTransform.position = Vector3.MoveTowards(headTransform.position,new Vector3 (headTransform.position.x,0,headTransform.position.z+1),0.5f);
                headTransform.position = headTransform.position + Vector3.forward* Time.deltaTime*speed;
                headTransform.eulerAngles = new Vector3(0, (Mathf.MoveTowardsAngle(headTransform.eulerAngles.y, 0, speed*1.5f)), 0);
                break;
            case Direction.down:
                headTransform.position = headTransform.position + Vector3.back * Time.deltaTime*speed;
                headTransform.eulerAngles = new Vector3(0, (Mathf.MoveTowardsAngle(headTransform.eulerAngles.y, 180, speed * 1.5f)), 0);
                break;
            case Direction.left:
                headTransform.position = headTransform.position + Vector3.left * Time.deltaTime*speed;
                headTransform.eulerAngles = new Vector3(0, (Mathf.MoveTowardsAngle(headTransform.eulerAngles.y,-90, speed * 1.5f)), 0 );
                break;
            case Direction.right:
                headTransform.position = headTransform.position + Vector3.right * Time.deltaTime*speed;
                headTransform.eulerAngles = new Vector3(0, (Mathf.MoveTowardsAngle(headTransform.eulerAngles.y, 90, speed * 1.5f)), 0);
                break;

        }
    }

    public void ButtonUp() {
        if (direct==Direction.down) {
            return;
        }
        direct = Direction.up;

    }
    public void ButtonDown() {
        if (direct == Direction.up) {
            return;
        }
        direct = Direction.down;

    }
    public void ButtonLeft() {
        if (direct == Direction.right) {
            return;
        }

        direct = Direction.left;
    }
    public void ButtonRight() {
        if (direct == Direction.left) {
            return;
        }
        direct = Direction.right;

    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag=="Food") {
            ObjectPool.Recycle(other.gameObject);
            controller.AddSegment();
            scenecontroller.SpawnFood();

            Debug.Log("Food");
        }
        if (other.gameObject.tag=="SnakeBody") {
            scenecontroller.Die();
            Debug.Log("Die");
            this.enabled = false;
        }
        if (other.gameObject.tag == "Wall") {
            scenecontroller.Die();
            Debug.Log("Die");
            this.enabled = false;
        }
    }
}
