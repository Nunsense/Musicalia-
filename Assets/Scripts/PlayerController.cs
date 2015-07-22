using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	public float moveForce = 300f;
	public float maxSpeed = 5f;
	public float jumpForce = 1200f;
	
	[SerializeField] private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;
	private PlayerController player;
	
	private PlayerModifiers currentMultiplier;
	
	[SerializeField] PlatformController currentGround;

	Vector3 initialPosition;

	private Vector2 touchOrigin = -Vector2.one;
	
	void Awake () {
		anim   = GetComponent<Animator>();
		rb2d   = GetComponent<Rigidbody2D>();
		player = this;
	}
	
	void Start() {
		initialPosition	= transform.position;
	}
	
	void Update () {
		if (transform.position.y < -10) {
			WorldController.instance.Reset();
			Debug.Log("Out of the game");
		}

		PlayerKeyBinding();
	}
	
	void FixedUpdate() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1, 1 << LayerMask.NameToLayer("Ground"));
		if (hit.collider != null) {
			float distance = Mathf.Abs(hit.point.y - transform.position.y);
			if (distance <= 0.41) {
				PlatformController controller = hit.collider.GetComponent<PlatformController>() ?? hit.collider.GetComponentInParent<PlatformController>();
				if (controller != currentGround) {
					currentGround = controller;
				
					controller.Touch();
					ApplyModifier(controller.playerModifier);
					controller.SetColor(WorldController.instance.RandomColor());
					
					grounded = true;
				}
			}
		} else {
			grounded = false;
			currentGround = null;
			ResetMultiplier();
		}
	}// Fixed update

	public void Move(float value){
		if (value * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * value * moveForce * Time.deltaTime);
		
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
	}

	public void Jump() {
		if (grounded) {
			rb2d.AddForce (new Vector2 (0f, jumpForce + (rb2d.velocity.x * 70)));
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
			Application.LoadLevel("end_game");
		}
	}//OnTriggerEnter2D

	void ApplyModifier(PlayerModifiers multi) {
		currentMultiplier = multi;
		switch (currentMultiplier) {
		case PlayerModifiers.longJump : {
			jumpForce *= 1.5f; 
			break;		
		}
		}
	}//ApplyModifier
	
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
	}//ResetMultiplier
	
	public void Reset() {
		transform.position = initialPosition;
		rb2d.velocity = Vector2.zero;
	}

	void PlayerKeyBinding() {
//		horizontal = 0f;     //Used to store the horizontal move direction.
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal, 0f);
		
		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			player.Jump();
		};
		
		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		
		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0) {
			if(Input.touchCount > 1) {
				player.Jump();
			}
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];
			
			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began) {
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}
			
			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (touchOrigin.x >= 0) {
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;
				
				//Calculate the difference between the beginning and end of the touch on the x axis.
				float x = touchEnd.x - touchOrigin.x;
				
				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				
				moveHorizontal = x > 0 ? 1 : -1;
			}
		}
		
		#endif //End of mobile platform dependendent compilation section started above with #elif
		//Check if we have a non-zero value for horizontal or vertical
		player.Move(moveHorizontal);
	}//player key binding
}