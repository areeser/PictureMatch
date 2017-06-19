using UnityEngine;
using System.Collections;
using System;

public class RandomPictures : MonoBehaviour {

    public GameObject[] pictures = new GameObject[9];
    public GameObject[] currPictures = new GameObject[9];
    public int nullIndex;

    public Camera cam;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < pictures.Length; ++i) {
            int picPos = UnityEngine.Random.Range(0, 9);
            while (currPictures[picPos] != null) {
                picPos = UnityEngine.Random.Range(0, 9);
            }
            currPictures[picPos] = pictures[i];
            Instantiate(currPictures[picPos], new Vector3(picPos % 3 * 2, picPos / 3 * 2, 0), Quaternion.identity);
            if (currPictures[picPos].tag == "Null") {
                nullIndex = picPos;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            //Debug.Log("Mouse");
            if (hit.collider != null)
            {
                //Debug.Log("Hit");
                GameObject empty = GameObject.FindGameObjectWithTag("Null");
                //Debug.Log(nullIndex);
                //Debug.Log(Math.Abs((int)currPictures[nullIndex].transform.position.x - (int)hit.transform.position.x) + " " +
                   // Math.Abs((int)currPictures[nullIndex].transform.position.y - (int)hit.transform.position.y));
                if ((Math.Abs((int) empty.transform.position.x - (int) hit.transform.position.x) == 2 &&
                        empty.transform.position.y == hit.transform.position.y) ||
                        (Math.Abs((int) empty.transform.position.y - (int)hit.transform.position.y) == 2 &&
                        empty.transform.position.x == hit.transform.position.x)) {
                   // Debug.Log(hit.transform.gameObject.tag);
                    for (int i = 0; i < currPictures.Length; ++i) {
                        //Debug.Log(currPictures[i].tag + " " + i);
                        if (hit.transform.gameObject.tag == currPictures[i].tag) {
                            GameObject tempGO = currPictures[nullIndex];
                            currPictures[nullIndex] = currPictures[i];
                            currPictures[i] = tempGO;
                            nullIndex = i;
                            break;
                        }
                    }
                    Vector3 temp = hit.transform.position;
                    hit.transform.position = empty.transform.position;
                    empty.transform.position = temp;
                }
        
            }
            bool solved = true;
            for (int i = 0; i < pictures.Length; ++i) {
                if (!(pictures[i].tag == currPictures[i].tag)) {
                    solved = false;
                }
            }
            if (solved) {
                Debug.Log("You Win!!!!");
                //TODO load win scene
            }
        }
	}
}
