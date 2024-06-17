using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ChangingImage : MonoBehaviour
{
    public Sprite[] cutsceneimages;
    public int cutscenestate;
    public Animator animator;
    public Image imagetoshow;
    public IntroCutscene intro;

    public void SwitchImageState()
    {

        if (intro.dialogueSet[1] == intro.dialogueSet[3])
        {
            animator.SetTrigger("SecondState");
        }



    }
}
