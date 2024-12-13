using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementsChara : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform spriteTransform;
    public Animator animator;

    private Vector3 movement;
    private SpriteRenderer spriteRenderer;

    public bool canMove = true; 

    void Start()
    {
        spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {

        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal");

            movement = new Vector3(moveX, 0f, 0f).normalized;

            transform.position += movement * moveSpeed * Time.deltaTime;

            animator.SetFloat("speed", Mathf.Abs(moveX));

            if (movement.x != 0)
            {
                spriteRenderer.flipX = movement.x < 0;
            }
        }
        else
        {

            animator.SetFloat("speed", 0f);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}

