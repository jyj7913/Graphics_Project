using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public GameObject thedoor;
	private AudioSource openAudio;
	//public AudioClip openSound;
	void Start()
	{
		openAudio = GetComponent<AudioSource>();
	}
	void OnTriggerEnter ( Collider obj  ){ // key�� �� ��������� ������
		if (obj.tag == "Player")
		{
			thedoor.GetComponent<Animation>().Play("open");
			openAudio.Play();
		}
	}

	void OnTriggerExit ( Collider obj  ){
		if (obj.tag == "Player")
		{
			thedoor.GetComponent<Animation>().Play("close");
			openAudio.Play();
		}
	}
}