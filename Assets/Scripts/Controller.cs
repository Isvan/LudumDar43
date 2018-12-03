using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	

public class Controller : MonoBehaviour {

	// Use this for initialization
	Camera cam;

	public GameObject baddiePrefab;
	public GameObject vilPrefab;

	int vilsLeft;

	public int startingVils;
	int vilsSacrificed;
	public int baddiesToSpawn;
	int baddiesLeft;
	int wave;

	public Text vilsLeftTxt;
	public Text waveTxt;
	

	public Material selecedMat;
	public Material sacrificeMat;
	public Material unSelecedMat;
	List<UnitBase> selected;
	public float mouseSensitivity;
	Vector3 CameraPos;
	void Start () {
		cam = GetComponent<Camera>();
		mouseSensitivity = 0.1f;
		vilsSacrificed = 0;
		selected = new List<UnitBase>();

		spawnVils(startingVils);
		//////baddiesLeft = baddiesToSpawn;
		vilsLeft = startingVils;
		vilsLeftTxt.text = "Villagers Left : " + vilsLeft.ToString();
		wave = 0;
		waveStart();

		
		

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			RaycastHit rayHit = new RaycastHit();

			bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayHit);
			if(hit){

				bool first = true;

				if(selected != null){
					foreach(UnitBase U in selected){


						if(U != null){
							
							if(U.marked){
								continue;
							}else if(first){
								U.setTargetTower(rayHit.point);
								U.GetComponent<MeshRenderer>().material = sacrificeMat;
								U.marked = true;
								U.sacType = 0;
								first = false;
							}
						}
					}
				}

			}
		}

		if(Input.GetKeyDown(KeyCode.Alpha2)){
			RaycastHit rayHit = new RaycastHit();

			bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayHit);
			if(hit){

				bool first = true;

				if(selected != null){
					foreach(UnitBase U in selected){


						if(U != null){
							
							if(U.marked){
								continue;
							}else if(first){
								U.setTargetTower(rayHit.point);
								U.GetComponent<MeshRenderer>().material = sacrificeMat;
								U.marked = true;
								U.sacType = 1;
								first = false;
							}
						}
					}
				}

			}
		}

		if(Input.GetKeyDown(KeyCode.Alpha3)){
			RaycastHit rayHit = new RaycastHit();

			bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayHit);
			if(hit){

				bool first = true;

				if(selected != null){
					foreach(UnitBase U in selected){


						if(U != null){
							
							if(U.marked){
								continue;
							}else if(first){
								U.setTargetTower(rayHit.point);
								U.GetComponent<MeshRenderer>().material = sacrificeMat;
								U.marked = true;
								U.sacType = 2;
								first = false;
							}
						}
					}
				}

			}
		}

		//Right Click
		if(Input.GetMouseButtonDown(1)){

			RaycastHit rayHit = new RaycastHit();

			bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayHit);
			if(hit){

				if(selected != null){
					foreach(UnitBase U in selected){
						if(U != null){

							if(U.marked){
								continue;
							}

							U.setTarget(rayHit.point);
						}
					}
				}

			}

		}


	}

	public void vilKilled(){
		vilsLeft -= 1;
		vilsLeftTxt.text = "Villagers Left : " + vilsLeft.ToString();
	
		if(vilsLeft <= 0){
			Debug.Log("Game over man!");
			
            SceneManager.LoadScene(0);
		}
	
	}

	public void vilsSacrifice(){
		vilsSacrificed += 1;
		vilKilled();
	}

	public void baddieKilled(){
		baddiesLeft -= 1;
		if(baddiesLeft <= 0){

			waveDone();
		}
	}

	public void waveStart(){
		wave++;
		waveTxt.text = "Wave : " + wave;
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("BadSpawn");

		for(int i = 0;i < baddiesToSpawn + vilsSacrificed;i++){
		Instantiate(baddiePrefab,spawns[Random.Range(0,spawns.Length)].transform.position +new Vector3(Random.Range(-40,40),0,Random.Range(-40,40)) ,new Quaternion(0,0,0,0));
		baddiesLeft++;
		}

	
	
	}

	public void waveDone(){
		Debug.Log("Wave done");

		Debug.Log("There are : " + vilsLeft + " vils left and " + vilsSacrificed + " sacrifced ");
		int vilsToSpawn = Mathf.RoundToInt(vilsLeft * 0.2f);
		spawnVils(1+vilsToSpawn);

		waveStart();
		baddiesToSpawn += 2;
		//baddiesToSpawn += vilsSacrificed;

	}

	public void spawnVils(int num){

		vilsLeft += num;
		vilsLeftTxt.text = "Villagers Left : " + vilsLeft.ToString();
		Vector3 spawnloc = GameObject.FindWithTag("Base").transform.position;

		for(int i = 0;i < num;i++){

			GameObject vil = Instantiate(vilPrefab,spawnloc+new Vector3(Random.Range(-40,40),0,Random.Range(-40,40)),new Quaternion(0,0,0,0));

			selected.Add(vil.GetComponent<UnitBase>());
		}

	}



}
