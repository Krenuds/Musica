using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour {
	

	public static UserInterface instance;
	private GUIText _clockDisplay;
	private MouseLook mouseLookX;
	private MouseLook mouseLookY;
	
	void Awake()
	{
		instance =  this;
	}
	
	void Start()
	{
		_clockDisplay = GameObject.FindGameObjectWithTag("MainClock").GetComponent<GUIText>();
		Screen.showCursor = false;
		Screen.lockCursor = true;
		mouseLookY =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
		mouseLookX =  GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
	}
	void Update()
	{
		
		//This goes on the player script!
		_clockDisplay.text = MasterClock.instance._Time.ToString();
		if(Input.GetButton("FreeLook"))
		{
			Screen.showCursor = true;
			Screen.lockCursor = false;
			mouseLookY.enabled = false;
			mouseLookX.enabled = false;

		}else
		{
			Screen.lockCursor = true;
			Screen.showCursor = false;
			mouseLookY.enabled = true;
			mouseLookX.enabled = true;
		}
	}
	
	void OnGUI()
	{
        if (GUI.Button(new Rect(10, 70, 50, 30), "Stop")) {
			MasterClock.instance.StopTime();
		}

        if (GUI.Button(new Rect(70, 70, 50, 30), "Start")) {
			MasterClock.instance.StartTime();	
		}

        if (GUI.Button(new Rect(130, 70, 50, 30), "Reset")) {
			MasterClock.instance.ResetTime();
		}
	}
}
