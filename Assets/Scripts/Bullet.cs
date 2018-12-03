using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	GameObject target;
	// Use this for initialization
	public float speed;
	public int damg;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Target is dead kill your self ;)
		if(target == null){
			Destroy(gameObject);
			return;
		}else{

			transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed * Time.deltaTime);

		}

		if(Vector3.Distance(transform.position,target.transform.position) < 2){
			Destroy(gameObject);
			BadGuyBase bg = target.GetComponent<BadGuyBase>();
			bg.takeDamg(damg);
			//Create boom animation here

		}



	}

	public void setTarget(GameObject target){
		this.target = target;
	}
}
