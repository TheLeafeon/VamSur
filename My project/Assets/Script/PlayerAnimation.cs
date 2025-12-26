using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void SetTrigger(string parName)
    {
        anim.SetTrigger(parName);
    }
    public void SetBool(string parName, bool value)
    {
        anim.SetBool(parName, value);
    }
    public void SetFloat(string parName, float value)
    {
        anim.SetFloat(parName, value);
    }
}
