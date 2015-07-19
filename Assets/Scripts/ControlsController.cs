using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsController : MonoBehaviour {

	private Vector2 touchOrigin = -Vector2.one;
	private PlayerController player;
	float horizontal;



	void Awake(){
		player = FindObjectOfType<PlayerController>();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		 horizontal = 0f;     //Used to store the horizontal move direction.
		
		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

			if (Input.GetButtonDown("Jump"))
			{
				player.Jump();
			}
			//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
			horizontal = Input.GetAxis ("Horizontal");
			player.Move(horizontal);

		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		
		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0)
		{
			if(Input.touchCount > 1)
				player.Jump();
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];
			
			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began)
			{
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}
			
			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (touchOrigin.x >= 0)
			{
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;
				
				//Calculate the difference between the beginning and end of the touch on the x axis.
				float x = touchEnd.x - touchOrigin.x;

				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.

				horizontal = x > 0 ? 1 : -1;
			}
		}
		
		#endif //End of mobile platform dependendent compilation section started above with #elif
		//Check if we have a non-zero value for horizontal or vertical
		if(horizontal != 0)
		{
			player.Move (horizontal);
		}
	}
}
