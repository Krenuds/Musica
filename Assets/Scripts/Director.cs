using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	public static Director instance;
	
	private GUIText _clockGUI;
	
	private float _time;
	
	void Awake()
	{
		instance = this;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {	
		
	}
}
