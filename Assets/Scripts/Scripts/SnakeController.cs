using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour {
    [SerializeField]
    int maxLenght;
    [SerializeField]
    int startLength;

    public GameObject head_Of_Snake;
    public GameObject part_Of_Snake;

    List<GameObject> parts_Of_Snake = new List<GameObject>();
    List<Transform> partsTransform = new List<Transform>();//transform of parts for cache
    ArrayList angles1;//resizable array for angles

    Vector3 pos;//start position of snake head

    [SerializeField]
    float _interpolateStep;
    
    void Start() {
        _interpolateStep *= GameManager.Instance.Speed * 0.2f;

        angles1 = new ArrayList(startLength);
       
        pos = head_Of_Snake.transform.localPosition;

        Quaternion headQuaternion = Quaternion.Euler(head_Of_Snake.transform.localEulerAngles);

        ObjectPool.CreatePool(part_Of_Snake,maxLenght);
        
        for (int i = 0; i < startLength; ++i) {// Create start view of snake
            angles1.Add(null);
            pos += Vector3.back*2;
            // parts_Of_Snake.Add(Instantiate(part_Of_Snake, pos, headQuaternion) as GameObject);
            parts_Of_Snake.Add(ObjectPool.Spawn(part_Of_Snake));

            parts_Of_Snake[i].transform.SetParent(this.gameObject.transform);

            partsTransform.Add(parts_Of_Snake[i].transform);

        }
       // Debug.Log(angles1[0]);

    }
    
    void Update() {
       
        MoveParts();
    }

    void MoveParts() {
    
        partsTransform[0].position = Vector3.Lerp(partsTransform[0].position, head_Of_Snake.transform.position, _interpolateStep);
        angles1[0] = Vector3.Angle(Vector3.forward, partsTransform[0].position - head_Of_Snake.transform.position);
        partsTransform[0].eulerAngles = partsTransform[0].position.x < head_Of_Snake.transform.position.x ? new Vector3(0, -(float)angles1[0], 0) : new Vector3(0, (float)angles1[0], 0);

        for (int i = 1; i < parts_Of_Snake.Count; ++i) {
            partsTransform[i].position = Vector3.Lerp(partsTransform[i].position, partsTransform[i-1].position, _interpolateStep);

            angles1[i] = Vector3.Angle(Vector3.forward, partsTransform[i-1].position - partsTransform[i].position);

            parts_Of_Snake[i].transform.eulerAngles = partsTransform[i-1].position.x < partsTransform[i].position.x ? new Vector3(0, -(float)angles1[i], 0) : new Vector3(0, (float)angles1[i], 0);


        }


    }

   public void AddSegment() {
        if ((parts_Of_Snake.Count - 1)>=maxLenght) {
            return;
        }
        var newpart = ObjectPool.Spawn(part_Of_Snake, parts_Of_Snake[parts_Of_Snake.Count - 1].transform.position, parts_Of_Snake[parts_Of_Snake.Count - 1].transform.localRotation);
        partsTransform.Add(newpart.transform);
        parts_Of_Snake.Add(newpart);
        angles1.Add(null);
    }

}
