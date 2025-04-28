using UnityEngine;
using System.Collections;

public class FallOnTrigger : MonoBehaviour
{
	public GameManager gamemanager;
	private int oldCount;
	private int currentCount;

	public float fallDelay = 1f;
	public float floatDelay = 10f;

	private bool interactedWith = false;
	private float timer = 0f;
	private System.Random rnd = new System.Random();
	private UnityEngine.Vector3 height;

	private Vector3 originalPos;

    private void Start()
    {
		oldCount = currentCount = gamemanager.countNumber;
		originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		//alternatively, just: originalPos = gameObject.transform.position;
	}

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

		//Reset
		currentCount = gamemanager.countNumber;
		if (currentCount != oldCount)
        {
			oldCount = currentCount;
			Invoke("Reset", (float)1.1);
		}

	}

    public void Reset()
    {
		//timer += Time.deltaTime;
		//if (timer > floatDelay)
        //{
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().isKinematic = true;
			//GetComponent<Transform>().position = height;
			//transform.position = height * Time.deltaTime;
			gameObject.transform.position = originalPos;
			interactedWith = false;
		//}
	}
}