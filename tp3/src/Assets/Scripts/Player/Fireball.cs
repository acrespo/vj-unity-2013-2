using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
	public Transform fragments;										// Place the fractured fireball model
	public float fragmentParticleMinSizeModification = 0.75f;		// Change the particle minimum size for fragments
	public float fragmentParticleMaxSizeModification = 0.75f;		// Change the particle maximum size for fragments
	public float fragmentParticleMaxEmissionModification = 5.0f;	// Change the particle maximum emission for fragments
	public float fragmentParticleMinEmissionModification = 5.0f;	// Change the particle minimum emission for fragments
	public float fragmentParticleMaxEnergyModification = 0.5f;		// Change the particle maximum energy for fragments
	public float fragmentParticleMinEnergyModification = 0.5f;		// Change the particle minimum energy for fragments
	public float fragmentParticleSizeGrow = -0.125f;				// Change the particle grow for fragments
	public float waitForRemoveCollider = 1;							// Delay before removing collider
	public float waitForDestroy = 2;								// Delay before removing fireball
	public float explosiveForce = 350;								// How much random force applied to each fragment
	public bool showFractures = true;								// Show/Hide fragments
	public int maxFragmentsWithParticles = 20;						// How many of the fragments should have particle system attached
	public float selfExplode = 100;									// Delay before explode the fireball even if it does not collide with anything 
	public bool inheritVelocity;									// Fragments will have the velocity of the fireball when added
	public float fractureDelay = 0.1f;								// Delay before explosion
	public PhysicMaterial physicMat;								// Physical material attached to each fragment
	public AudioSource explosionSound;								// Sound to play when fireball explodes
	public float explosionRadius = 1;								// Radius of the hit test
	public float  explosionDamage = 500;							// Damage
	
	private int counter;
	private bool exploded;
	
	IEnumerator Start ()
	{
		yield return new WaitForSeconds (selfExplode);
		StartCoroutine ("triggerExplosion");
	}
	
	void OnCollisionEnter (Collision collision)
	{   
		StartCoroutine ("triggerExplosion");
	}
	
	IEnumerator triggerExplosion ()
	{
		yield return new WaitForSeconds(fractureDelay);
		Destroy (collider);
		if (transform.FindChild ("fireball") != null) {
			Destroy (transform.FindChild ("fireball").gameObject);
		}
		StartCoroutine ("explode");
		exploded = true;
	}
	
	IEnumerator explode ()
	{
		if (!exploded) {
			if (explosionSound != null) {
				AudioSource.PlayClipAtPoint (explosionSound.clip, transform.position);
			}
			DamageCloseObjects ();
			Transform fragmentd = (Transform)Instantiate (fragments, transform.position, transform.rotation);
			foreach (Transform child in fragmentd.FindChild("fragments")) {
				counter ++;
				child.gameObject.AddComponent ("MeshCollider");
				child.gameObject.AddComponent ("Rigidbody");
				if (physicMat != null) {
					child.gameObject.collider.material = physicMat;
				}
				if (counter <= maxFragmentsWithParticles && transform.FindChild ("particles") != null) {
					Transform fP = (Transform)Instantiate (transform.FindChild ("particles"), child.transform.position, child.transform.rotation);
					fP.particleEmitter.minSize = fP.particleEmitter.minSize * fragmentParticleMinSizeModification;
					fP.particleEmitter.maxSize = fP.particleEmitter.minSize * fragmentParticleMaxSizeModification;
					fP.particleEmitter.maxEmission = fP.particleEmitter.maxEmission * fragmentParticleMaxEmissionModification;
					fP.particleEmitter.minEmission = fP.particleEmitter.minEmission * fragmentParticleMinEmissionModification;
					fP.particleEmitter.maxEnergy = fP.particleEmitter.minEnergy * fragmentParticleMaxEnergyModification;
					fP.particleEmitter.minEnergy = fP.particleEmitter.minEnergy * fragmentParticleMinEnergyModification;
					fP.GetComponent<ParticleAnimator> ().sizeGrow = fragmentParticleSizeGrow;
					fP.parent = child;
					if (inheritVelocity) {
						child.rigidbody.velocity = transform.rigidbody.velocity;
					}
				}
				child.rigidbody.AddForce (Random.Range (-explosiveForce, explosiveForce), Random.Range (-explosiveForce, explosiveForce), Random.Range (-explosiveForce, explosiveForce));
				child.rigidbody.AddTorque (Random.Range (-explosiveForce, explosiveForce), Random.Range (-explosiveForce, explosiveForce), Random.Range (-explosiveForce, explosiveForce));
				if (!showFractures) {
					child.renderer.enabled = false;
				}
			}
			if (transform.FindChild ("particles") != null) {
				transform.FindChild ("particles").particleEmitter.emit = false;
			}
			yield return new WaitForSeconds (waitForRemoveCollider);
			foreach (Transform child in fragmentd.FindChild("fragments")) {  
				Destroy (child.GetComponent<MeshCollider> ());
			}
			yield return new WaitForSeconds (waitForDestroy);
			if (transform.FindChild ("Billboard GameObjects") != null) {
				Destroy (transform.FindChild ("Billboard GameObjects").gameObject);
			}
			Destroy (fragmentd.gameObject);
			Destroy (transform.gameObject);
		}
	}
	
	void DamageCloseObjects ()
	{
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, explosionRadius);
		for (int i = 0; i < hitColliders.Length; i++) {
			Enemy obj = hitColliders [i].gameObject.GetComponent<Enemy> ();
			if (obj != null) {
				obj.Damage (explosionDamage);
			}
		}
	}
}
