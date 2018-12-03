using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeTower : MonoBehaviour {

	public float range;
	public float fireRate;
	float reload;
	float realoadTime;
	
	public Vector3 fireOffSet;

	public GameObject bulletPrefab;
	// Use this for initialization
	GameObject target;
	
	
	void Start () {
		realoadTime = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		
		reload += fireRate *Time.deltaTime;

		if(target == null){
			findTarget();
		}

		if(reload > realoadTime && target != null){
			reload = 0;
			//Fire Bullet
			GameObject spawnedBullet = (GameObject) Instantiate(bulletPrefab,transform.position + fireOffSet,new Quaternion(0,0,0,0));
			Bullet shot = spawnedBullet.GetComponent<Bullet>();
			shot.setTarget(target);		

			//Create fire animation here
			//Create sound effect

		}

		

	}

		void findTarget(){

		GameObject[] vils = GameObject.FindGameObjectsWithTag("BadGuy");
		
		if(vils.Length == 0){
			target = null;
			return;
		}

		float distance = float.MaxValue;
		GameObject closest = null;

		for(int i = 0;i < vils.Length;i++){
			float newDist = Vector3.Distance(vils[i].transform.position,gameObject.transform.position);
			if(newDist < distance && newDist < range){
				closest = vils[i];
				distance = newDist;
			}

		}

		if(closest != null){
			target = closest;
		}


	}

}
