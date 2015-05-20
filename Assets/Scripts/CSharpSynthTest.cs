using UnityEngine;
using System.Collections;

public class CSharpSynthTest : MonoBehaviour {
	
	void OnMouseEnter(){
		CSharpSynthScript.midiseq.Play();
	}
	
	void OnMouseExit(){
		CSharpSynthScript.midiseq.Stop(false);
	}
}
