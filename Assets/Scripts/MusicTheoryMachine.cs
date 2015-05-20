using UnityEngine;
using System.Collections;

public class MusicTheoryMachine : MonoBehaviour {
	
	private string[] _noteNames;
	private string[] _masterNoteList;

	private int _rangeOfNotes = 127; 
//	private int _wholeStep = 2;
//	private int _halfStep = 1;
	private int _noteRangeIndicator = 0;
	
	public string[] notesList;
	public string noteToPlay;
	
	public string[] AllNotes
	{
		get {return _masterNoteList;}
	}
	

	void Awake() 
	{
		_noteNames = new string[]
		{
			"A",
			"A#/Bb",
			"B",
			"C",
			"C#/Db",
			"D",
			"D#/Eb",
			"E",
			"F",
			"F#/Gb",
			"G",
			"G#/Ab",
		};
		_masterNoteList = new string[_rangeOfNotes - 1];
	}
	
	void Start () {
		DisplayNotesInDebug();
		GenerateMasterNoteList();
		notesList = _masterNoteList;
	}
	

	void FixedUpdate () {
	
	}
	
	public void DisplayNotesInDebug()
	{
		foreach (string note in _noteNames)
			Debug.Log (note + "\n");		
	}
	
	public void GenerateMasterNoteList()
	{
		int noteIndex = 0;
		for (int mI = 0; mI < _masterNoteList.Length; mI++)
		{
			if (noteIndex > 11) 
			{
				noteIndex = 0;
				_noteRangeIndicator++;
			}
			//Assign The Note to mI in master notes array
			_masterNoteList[mI] = _noteNames[noteIndex] + _noteRangeIndicator.ToString();

			Debug.Log(_masterNoteList[mI].ToString());
			noteIndex++;
		}
	}
}
