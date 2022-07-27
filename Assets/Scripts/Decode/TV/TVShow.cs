using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;
using UnityEngine.UI;

public class TVShow : DecodeBaseInput
{
    public TVLetter letter;
    public Animator animator;
    public SpriteRenderer image;

    private bool is_changing = false;
    // Update is called once per frame
    void Update()
    {
        if (is_changing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            letter.TVAdd();
            StartCoroutine(ChangeTVShow());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            letter.TVSub();
            StartCoroutine(ChangeTVShow());
        }
    }

    IEnumerator ChangeTVShow()
    {
        is_changing = true;
        animator.speed = 0;
        animator.transform.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
        image.color = new Color(0,0,0,1);

        yield return new WaitForSeconds(0.5f);

        animator.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        image.color = new Color(0.5f, 0.5f, 0.5f, 0.4f);
        animator.speed = 1;
        is_changing = false;
    }
}
