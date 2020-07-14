using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D bc;
    //					 0		1		  2		  3		   4
    private enum State {idle, running, jumping, falling, flying}
    private State state = State.idle;
    private Collider2D playerCollider;
    
    private void Sart(){
    	rb 	= GetComponent<Rigidbody2D>();
    	animator = GetComponent<Animator>();
    }

    void Update(){ 

    	float horizontal = Input.GetAxis("Horizontal");

    	if(horizontal<0){
        	rb.velocity =  new Vector2(-3.5f,rb.velocity.y);
        	transform.localScale = new Vector2(-1, 1);        	
    	}

    	else if(horizontal>0){
    		rb.velocity =  new Vector2(3.5f,rb.velocity.y);
        	transform.localScale = new Vector2(1, 1);
    	}

        if(Input.GetKeyDown(KeyCode.Space)){
        	if(state == State.jumping){
        		state = State.flying;
        		rb.velocity = new Vector2(rb.velocity.x, 13);
        	}
        	else if(state == State.falling){
        		state = State.flying;
        		rb.velocity = new Vector2(rb.velocity.x, 13);
        	}
        	else{
        		state = State.jumping;
        		rb.velocity = new Vector2(rb.velocity.x, 13);
        	}
		}

		setState();
		animator.SetInteger("State", (int)state);
	}

	void setState(){

		if(state == State.jumping && rb.velocity.y < 0.5f){
			state = State.falling;
		}

		else if(state == State.flying && rb.velocity.y < 0.5f){
			state = State.falling;
		}

		else if(rb.velocity.y == 0.0f && Mathf.Abs(rb.velocity.x) > 0.3f){
			state = State.running;
		}

		else if(state == State.falling && rb.velocity.y == 0.0f){
			if(rb.velocity.x < 0.3f){
				state = State.idle;
			}
			else{
				state = State.running;
			}
		}

		else if(state == State.running && rb.velocity.x < 0.3f){
			state = State.idle;
		}
	}
}
