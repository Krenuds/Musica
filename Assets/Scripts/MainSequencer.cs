using UnityEngine;
using System.Collections;

public class MainSequencer : MonoBehaviour {
	
	
	private int _currentBeat = 1;
	//private int _currentBar = 1;
	private Vector3 _sequencerHead;
	private RaycastHit[] objectsHit;
	private Transform playhead;
	private bool _firstNote = true;
	
	public enum TypeOfNote
	{
		sixteenthTriplet,
		sixteenth,
		eighthTriplet,
		eightTh,
		quarterTriplet,
		quarter,
		halfTriplet,
		half,
		wholeTriplet,
		whole
	}
	public TypeOfNote noteType;
	
	public int beatsPerMeasure = 8;
	public float playheadLength = 4.5f;
	public int noteDistance = 1;
	
	void Start () {
		playhead = transform.FindChild("Playhead");
		_sequencerHead = playhead.transform.localPosition;
	}
	
	void FixedUpdate () {
		if (MasterClock.clockIsRunning && _firstNote)
		{
			TryToPlayNote();
			_firstNote = false;
		}
			
		if(MasterClock.instance.measureTriggers[(int)noteType])
		{
			Debug.Log ("Tick");
			if(_currentBeat < beatsPerMeasure)
			{
				playhead.transform.localPosition += NextNote();
				_currentBeat++;				
			}else{
				_currentBeat = 1;
				playhead.transform.localPosition = _sequencerHead;
			}
			TryToPlayNote();		
		}	
	}
	
	private void TryToPlayNote()
	{
		objectsHit = Physics.RaycastAll(playhead.position, transform.forward, playheadLength);
		{
			foreach (RaycastHit hitObject in objectsHit)
			{
				Debug.Log (hitObject.transform.name);
				if (hitObject.transform.tag == "Note")
				{
					hitObject.transform.SendMessage("PlayNote");
				}
			}
		}		
	}
	
	private Vector3 NextNote()
	{
		return Vector3.right * noteDistance;
	}
}
