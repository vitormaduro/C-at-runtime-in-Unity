using UnityEngine;

public class GunAddOn : MonoBehaviour {
	void Start() {
		GetComponent<GunController>().timePerShot = 0f;
	}
}