using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour
{
    public Vector3 moveWallLeft = new Vector3(-.01f, 0, 0);
    public Vector3 moveWallRight = new Vector3(.01f, 0, 0);
    public GameObject leftWall;
    public GameObject rightWall;


    // Start is called before the first frame update
    void Start()
    {
        UIScripts UI = GetComponent<UIScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftWall.transform.position.x > -40)
        {
            StartCoroutine(WallLeft());
        }
        if (rightWall.transform.position.x < 25)
        {
            StartCoroutine(WallRight());
        }

    }

    public IEnumerator WallLeft()
    {
        if (!UIScripts.gameIsPaused)
        {
            leftWall.transform.position += moveWallLeft;
            yield return new WaitForSeconds(30f);
        }

    }

    public IEnumerator WallRight()
    {
        if (!UIScripts.gameIsPaused)
        {
            rightWall.transform.position += moveWallRight;
            yield return new WaitForSeconds(30f);
        }

    }
}
