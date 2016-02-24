using UnityEngine;
using System.Collections;

public class InteractableAnimScript : MonoBehaviour {

    [SerializeField] Animator[] animators;
    [SerializeField] string[] animationNames;
    bool _play;

    public void PlayAnimation()
    {
        Debug.Log("triggered");
        for (int i =0; i < animators.Length; i++)
        {
            animators[i].SetBool(animationNames[i],true);
        }
    }
}
