using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public GameObject thedoor;

void OnTriggerEnter ( Collider obj  ){ // key�� �� ��������� ������
	thedoor.GetComponent<Animation>().Play("open");
}

void OnTriggerExit ( Collider obj  ){
	thedoor.GetComponent<Animation>().Play("close");
}
}