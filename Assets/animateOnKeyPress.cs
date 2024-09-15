using UnityEngine;

public class AnimateOnKeyPress : MonoBehaviour
{
    private Animator animator;
    public string animationName = "opening"; // Name of the animation state to play
    public KeyCode keyToPress = KeyCode.Space; // Default key to trigger the animation

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            animator.Play(animationName);
        }
    }

    public void StartAnimation()
    {
        animator.Play(animationName);
    }
}
