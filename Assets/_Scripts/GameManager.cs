using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat{
	public class GameManager : MonoBehaviour {

		public Grid itemGrid;
		public GrannyController grannyController;
		public GameObject grannySpawnPosLeft;
		public GameObject grannySpawnPosRight;
		public GameObject grannyDestinationPosLeft;
		public GameObject grannyDestinationPosRight;

		public float maxGrannyTimer = 5.0f;
		private float currentGrannyTimer;
		private bool grannySpawning = true;
		private bool direction = true;


		public float maxSpawnTimer = 5.0f;
		private float currentSpawnRate;

		public GameObject[] ItemList; 

		public static GameManager instance = null;

		void Awake () {

			if (instance == null)
				instance = this;

			DontDestroyOnLoad (this.gameObject);

		}

		void Start (){
			currentGrannyTimer = maxGrannyTimer;
			currentSpawnRate = maxSpawnTimer;

		}
		
		// Update is called once per frame
		void Update () {

			if (grannyController.currentState == GrannyController.Granny_States.Waiting && grannySpawning)
				StartCoroutine (GrannySpawn());
			
			StartCoroutine (ItemSpawn ());

		}

		IEnumerator GrannySpawn(){

			grannySpawning = false;

			yield return new WaitForSeconds (currentGrannyTimer);

			Vector2 start;
			Vector2 destination;
			bool flip;

			direction = !direction;

			if (direction) {
				start = grannySpawnPosLeft.transform.position;
				destination = grannyDestinationPosRight.transform.position;
				flip = false;
			}
			else{
				start = grannySpawnPosRight.transform.position;
				destination = grannyDestinationPosLeft.transform.position;
				flip = true;
			}

			grannyController.SpawnGranny(start, destination, flip);
			grannySpawning = true;

		}

		IEnumerator ItemSpawn (){


			yield return new WaitForSeconds (currentSpawnRate);

		}
	}
}
