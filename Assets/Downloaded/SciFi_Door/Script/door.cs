using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public GameObject thedoor;

void OnTriggerEnter ( Collider obj  ){ // key를 다 모았을때만 열리게
	thedoor.GetComponent<Animation>().Play("open");
}

void OnTriggerExit ( Collider obj  ){
	thedoor.GetComponent<Animation>().Play("close");
}
}