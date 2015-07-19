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
	
	private PlayerModifiers currentMultiplier;
	
	[SerializeField] PlatformController currentGround;

	Vector3 initialPosition;
	
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Start() {
		initialPosition	= transform.position;
	}
	
	void Update () 
	{
		if (transform.position.y < -10) {
			WorldController.instance.Reset();
			Debug.Log("Out of the game");
		}
	}
	
	void FixedUpdate()
	{
//		float horizontal = Input.GetAxis("Horizontal");
//		move (horizontal);
//		
//		if (jump)
//		{
//			Jump();
//		}
//		2280X1440
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1, 1 << LayerMask.NameToLayer("Ground"));
		if (hit.collider != null) {
			float distance = Mathf.Abs(hit.point.y - transform.position.y);
			if (distance <= 0.41) {
				PlatformController controller = hit.collider.GetComponent<PlatformController>() ?? hit.collider.GetComponentInParent<PlatformController>();
				if (controller != currentGround) {
					currentGround = controller;
				
					controller.Touch();
					ApplyModifier(controller.playerModifier);
					grounded = true;
				}
			}
		} else {
			grounded = false;
			currentGround = null;
			ResetMultiplier();
		}
	}
	public void Move(float value){
		if (value * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * value * moveForce);
		
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
	}
	public void Jump(){
		if (grounded) {
			rb2d.AddForce (new Vector2 (0f, jumpForce + (rb2d.velocity.x * 70)));
			jump = false;
			grounded = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Coin") {
			col.gameObject.SetActive(false);
			WorldController.instance.AddCoin();
		} else if (col.tag == "Enemy") {
			col.gameObject.SetActive(false);
			Debug.Log("Killed");
			WorldController.instance.Reset();
		} else if (col.tag == "EndOfGame") {
			Debug.Log("EndOfGame");
			Application.LoadLevel(1);
		}
	}

	void ApplyModifier(PlayerModifiers multi) {
		currentMultiplier = multi;
		switch (currentMultiplier) {
		case PlayerModifiers.longJump : {
			jumpForce *= 1.5f; 
			break;		
		}
		}
	}
	
	void ResetMultiplier() {
		if (currentMultiplier != PlayerModifiers.none) {
			switch (currentMultiplier) {
			case PlayerModifiers.longJump : {
				jumpForce /= 1.5f; 
				break;	
			}
			}
			currentMultiplier = PlayerModifiers.none;
		}
	}
	
	public void Reset() {
		transform.position = initialPosition;
		rb2d.velocity = Vector2.zero;
	}
}