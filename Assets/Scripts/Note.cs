using UnityEngine;
using System.Collections;

public class Note: MonoBehaviour {
	public int note = 60;
	public int volume = 127;
	public float duration = 0.2f;
	
	public GameObject noteInfo;
	
	
	
	void Start(){
		CSharpSynthScript.ShortMessage((byte)(0xC << 4 | 1), 11, 0);
		noteInfo.renderer.enabled = false;
	}
	
	//void OnCollisionEnter(Collision aCollision){
		//PlayNote();

	
	
	void OnMouseEnter(){
		
		noteInfo.renderer.enabled = true;
	}
	
	void OnMouseExit(){
		noteInfo.renderer.enabled = false;
		
	}
	
	IEnumerator NoteOffEnumerator(){
		yield return new WaitForSeconds(duration);
		// Sending Note OFF on channel 1 //
		CSharpSynthScript.ShortMessage((byte)(0x8 << 4 | 1), (byte)note, 0);
	}
	
	public void PlayNote()
	{
		CSharpSynthScript.ShortMessage((byte)(0x9 << 4 | 1), (byte)note, 127);
		StartCoroutine(NoteOffEnumerator());		
	}
	
}
