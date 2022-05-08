using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;

    Animator animator;

    [SerializeField] float horizontalSpeed;
    bool move = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!gameManager.isStartMoving && StackSystem.instance.gameState != StackSystem.GameState.finished)
        {
            animator.Play("Idle");
            return;
        }
        else if (gameManager.isStartMoving && StackSystem.instance.gameState == StackSystem.GameState.playing)
        {
            animator.Play("Running");
        }
        else
        {
            animator.Play("Sit");
        }

        if (StackSystem.instance.gameState == StackSystem.GameState.playing)
        {
            transform.position += transform.forward * Time.deltaTime * 7;
            Vector3 horizontal;
            horizontal = new Vector3(Input.GetAxis("Horizontal"), 0);
            transform.Translate(horizontal.x * horizontalSpeed * Time.deltaTime, 0, 0);
            float a = transform.position.x;
            a = Mathf.Clamp(a, -4.25f, 4.25f);
            transform.position = new Vector3(a, transform.position.y, transform.position.z);
        }


#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);
            horizontal = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontal * horizontalSpeed * Time.deltaTime, 0, forwardSpeed * Time.deltaTime));

            if (finger.phase == TouchPhase.Began)
            {
                horizontal = Input.GetAxis("Horizontal");
                transform.Translate(new Vector3(horizontal * horizontalSpeed * Time.deltaTime, 0, forwardSpeed * Time.deltaTime));
            }
            if (finger.phase == TouchPhase.Moved)
            {
                horizontal = Input.GetAxis("Horizontal");
                transform.Translate(new Vector3(horizontal * horizontalSpeed * Time.deltaTime, 0, forwardSpeed * Time.deltaTime));
            }
        }
#endif
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Multiplier")
        {

        }

    }
}
