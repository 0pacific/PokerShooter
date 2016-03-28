using UnityEngine;
using System.Collections;
// パーティクルを自動消去（Loopなしのみ)
public class AutoDestroyEffect2 : MonoBehaviour {

	void Start () {
		// Loopしないパーティクルのみ
		ParticleSystem particle = GetComponent<ParticleSystem> ();
		Destroy (this.gameObject, particle.duration);
	}
}
