using UnityEngine;
using System.Collections;

public class FallOnTrigger : MonoBehaviour
{
	public float fallDelay = 1f;
	public float floatDelay = 10f;

	private bool interactedWith = false;
	private float timer = 0f;
	private System.Random rnd = new System.Random();
	private UnityEngine.Vector3 height;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			interactedWith = true;
	}

	void Update()
	{
		height = GetComponent<Transform>().position;
		if (interactedWith)
		{
			timer += Time.deltaTime;
			if (timer > fallDelay)
			{
				GetComponent<Rigidbody>().useGravity = true;
				GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		//Reset();
	}

    private void Reset()
    {
		//timer += Time.deltaTime;
		//if (timer > floatDelay)
        //{
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().isKinematic = true;
			//GetComponent<Transform>().position = height;
			transform.position = height * Time.deltaTime;
			interactedWith = false;
		//}
	}
}