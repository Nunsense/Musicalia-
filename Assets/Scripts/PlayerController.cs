using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	
	[SerializeField] private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;
	
	private int currentMultiplier;
	
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
			PlatformController controller = coll.gameObject.GetComponent<PlatformController>();
			controller.Touch();
			ApplyModifier(controller.Modifier);
		}
	}
	
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = false;
			ResetMultiplier();
		}
	}
	
	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		
		anim.SetFloat("Speed", Mathf.Abs(horizontal));
		
		if (horizontal * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * horizontal * moveForce);
		
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		
		if (horizontal > 0 && !facingRight)
			Flip();
		else if (horizontal < 0 && facingRight)
			Flip();
		
		if (jump)
		{
			anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void ApplyModifier(int multi) {
		currentMultiplier = multi;
		switch (currentMultiplier) {
		case 1 : {
			jumpForce *= 2; break;		
		}
		}
	}
	
	void ResetMultiplier() {
		switch (currentMultiplier) {
		case 1 : {
			jumpForce = 2; break;	
		}
		}
		currentMultiplier = -1;
	}
}