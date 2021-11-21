using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipTrap : MonoBehaviour
{
    public Vector3 moveWhipAround = new Vector3(0, 0, .005f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(moveWhip());
    }

    private IEnumerator moveWhip()
    {
        this.transform.Rotate(0, 0, -.1f);
        yield return new WaitForSeconds(1f);
    }
}
