using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderAnimator : MonoBehaviour
{
    
    public Animator transition;    
    Image image;
    public static FaderAnimator i { get; private set; }

    private void Awake()
    {
        i = this;
        image = GetComponent<Image>();
        transition = GetComponent<Animator>();
    }

    public IEnumerator BattleTransition1()
    {
        image.gameObject.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        image.gameObject.SetActive(false);
    }  

}
