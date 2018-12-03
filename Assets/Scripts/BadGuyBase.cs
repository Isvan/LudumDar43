using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BadGuyBase : MonoBehaviour {

	public int hp;

	Controller cont;
	NavMeshAgent agent;
	// Use this for initialization
	Rigidbody rb;
	bool huntMode;
	void Start () {
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
		agent.destination = GameObject.FindWithTag("Base").transform.position;
		huntMode = false;
		cont = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
	}
	

	// Update is called once per frame
	void Update () {

			


		if(agent.isActiveAndEnabled){

			if(Vector3.Distance(agent.destination,rb.position) < 3){

				if(!huntMode){ ///Reached base
					huntMode = true;
					setVilTarget();
				}else{ //Reach Vil
					setVilTarget();

				}

			}

				setVilTarget();
			
		}
	}

	void OnTriggerEnter(Collider col){

		if(col.GetComponent<UnitBase>() != null){
			Destroy(col.gameObject);
			cont.vilKilled();

			if(huntMode){
				setVilTarget();
			}

		}

	}

	public void takeDamg(int damg){

		hp -= damg;

		if(hp <= 0){
			cont.baddieKilled();
			Destroy(gameObject);
		}

	}

	void setVilTarget(){

		GameObject[] vils = GameObject.FindGameObjectsWithTag("Vil");
		
		if(vils.Length == 0 || agent == null || !agent.isOnNavMesh){
			return;
		}

		float distance = float.MaxValue;
		GameObject closest = null;

		for(int i = 0;i < vils.Length;i++){

			if(Vector3.Distance(vils[i].transform.position,rb.position) < distance){
				closest = vils[i];
			}

		}


		if(closest != null){
			agent.destination = closest.transform.position;
		}


	}

}
