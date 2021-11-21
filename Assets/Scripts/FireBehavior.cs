using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    public Vector3 moveFireUp = new Vector3(0, .001f, 0);


    // Start is called before the first frame update
    void Start()
    {
        UIScripts UI = GetComponent<UIScripts>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FireSpreading());
    }

    public IEnumerator FireSpreading()
    {
        if (!UIScripts.gameIsPaused)
        {
            this.transform.position += moveFireUp;
            yield return new WaitForSeconds(30f);
        }

    }
}
