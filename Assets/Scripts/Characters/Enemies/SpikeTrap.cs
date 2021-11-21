using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    private bool canHurtYou;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        SpikeToggle();
    }

    private IEnumerator SpikeToggle()
    {
        spriteRenderer.material.color = Color.red;
        canHurtYou = true;
        yield return new WaitForSeconds(2f);
        canHurtYou = false;
        spriteRenderer.material.color = Color.white;

    }


}
