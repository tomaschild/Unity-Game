using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D bc;
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;
    private Collider2D playerCollider;
    
    private void Sart(){
    	rb 	= GetComponent<Rigidbody2D>();
    	animator = GetComponent<Animator>();
    }

    void Update(){ 

    	float horizontal = Input.GetAxis("Horizontal");

    	if(horizontal<0){
        	rb.velocity =  new Vector2(-5,rb.velocity.y);
        	transform.localScale = new Vector2(-1, 1);
    	}

    	else if(horizontal>0){
    		rb.velocity =  new Vector2(5,rb.velocity.y);
        	transform.localScale = new Vector2(1, 1);
    	}

        if(Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0.0f){
        	rb.velocity = new Vector2(rb.velocity.x, 7);
        	state = State.jumping;
		}

        StateSwitch();
        animator.SetInteger("State", (int)state);
	}

	private void StateSwitch(){

		if(state == State.jumping && rb.velocity.y < 0.0f){			
			print("Cayendo");
			state = State.falling;
		}

		else if(Mathf.Abs(rb.velocity.x)>0.3f){
			state = State.running;
		}

		else if(rb.velocity.y < 0.1f){
			state = State.idle;
		}
	}
}