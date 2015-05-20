/* 
* 	(c) Copyright Marek Ledvina, Foriero Studo
*/

using UnityEngine;
using System.IO;
using System.Collections;
using CSharpSynth.Synthesis;
using CSharpSynth.Sequencer;
using CSharpSynth.Banks;
using CSharpSynth.Effects;
using System.Collections.Generic;

//[RequireComponent (typeof (AudioListener))]
public class CSharpSynthScript : MonoBehaviour {
	
	//RESOURC PATH TO BANK AND MIDI//
	public string gmBankPath = "GM Bank/gm.txt";
	public string midiPath = "Midis/carnival.mid";
	public bool playOnStart = false;
		
	public static volatile bool initialized = false;
	//static volatile bool locked = false;
	//static object locker = new object();
		
	static float[] buffer  = new float[0];
	
	static public StreamSynthesizer synth;
	static public float gain = 1f;
		
	static public MidiSequencer midiseq;
	static dBMeter dBmeter;
	
	static byte[] instruments = new byte[16];
	//static byte[] volume = new byte[16];
	
	public static void ShortMessage(byte Command, byte Data1, byte Data2){
		int chn = Command & 15; // channel //
		int cmn = Command >> 4; // command //
		switch(cmn){
		case 0x9:	/*NOTE ON*/
			if(initialized){
				if(Data2 == 0) synth.NoteOff(chn, Data1);
				else synth.NoteOn(chn, Data1, Data2, instruments[chn]); 
			}
		break;
		case 0x8:	/*NOTE OFF*/
			if(initialized){
				synth.NoteOff(chn, Data1);
			}
		break;
		case 0xC:	/*PROGRAM*/
			instruments[chn] = Data1;
		break;
		case 0xB: 	/*CONTROLER*/
			if(Data1 == 120) AllSoundOff();
		break;
		}
	}
	
	public static void AllSoundOff(){
		if(initialized){	
			synth.NoteOffAll(true);	
		}
	}
	
	public static MemoryStream LoadAssetBytes(string aResoucePath){
		TextAsset ta = Resources.Load(aResoucePath, typeof(TextAsset)) as TextAsset;
		if(ta != null){
		 return new MemoryStream(ta.bytes);	
		}
		Debug.LogError("RESOURCE FILE NOT FOUND : " + aResoucePath);
		return null;
	}
	
	//WARNING : THIS FUNCTION IS RUNNING ON DIFFERENT THREAD//
	void OnAudioFilterRead(float[] data,int channels) {
	    if(initialized){
			synth.GetNext(buffer);
		
			for (var i = 0; i < data.Length; ++i){
				data[i] = data[i] + buffer[i] * gain;
			}
		}
	}
		
	void CreateSynth(){
		synth = new StreamSynthesizer(44100, 2, 1024, 40, false);
	    buffer = new float[synth.BufferSize];
	    
		synth.LoadBank(gmBankPath);
		initialized = true;
	}
	
	void CreateMidi(){
		if(midiseq == null){
			midiseq = new MidiSequencer(synth);
		     
		    midiseq.NoteOnEvent += new MidiSequencer.NoteOnEventHandler(handleOn);
		    midiseq.NoteOffEvent += new MidiSequencer.NoteOffEventHandler(handleOff);
		} else {
			midiseq.Stop(true);	
		}
		
		if(!string.IsNullOrEmpty(midiPath)){
			midiseq.LoadMidi(midiPath,false);
	    	if(playOnStart) midiseq.Play();
		}	
	}
			
	void Awake(){
		if(!initialized) CreateSynth();
		CreateMidi();
	}
	
	void handleOn(int channel, int note, int velocity)
    {
        
    }
				
    void handleOff(int channel, int note)
    {

    }
}
