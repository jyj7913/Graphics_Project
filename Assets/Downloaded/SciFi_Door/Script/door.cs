using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public GameObject thedoor;

	void OnTriggerEnter ( Collider obj  ){ // key�� �� ��������� ������
		if (obj.tag == "Player")
		{
			thedoor.GetComponent<Animation>().Play("open");
		}
	}

	void OnTriggerExit ( Collider obj  ){
		if (obj.tag == "Player")
		{
			thedoor.GetComponent<Animation>().Play("close");
		}
	}
}