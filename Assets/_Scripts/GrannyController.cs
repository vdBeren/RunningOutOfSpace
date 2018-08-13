using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat{
	public class GrannyController : MonoBehaviour {

		public float moveSpeed = 0.1f;
		private Vector2 destination;

		private SpriteRenderer grannySpriteRenderer;

		public Granny_States currentState { get; set; }

		public enum Granny_States{
			Waiting,
			Moving,
			Falling
		}

		// Use this for initialization
		void Start () {

			grannySpriteRenderer = this.GetComponent<SpriteRenderer> ();

			Wait ();
		}
		
		// Update is called once per frame
		void Update () {


			if (currentState == Granny_States.Moving) {
				Move ();
			} 
				
		}

		public void Wait(){
			this.currentState = Granny_States.Waiting;
		}

		void Move(){
			
			var nextPosition = Vector2.MoveTowards (this.transform.position, destination, moveSpeed * Time.deltaTime);
			this.transform.position = nextPosition;

			if ((Vector2) this.transform.position == (Vector2) destination) {
				Wait ();
			}
		}

		void Fall(){
			
		}
			
		public void SpawnGranny(Vector2 start, Vector2 destination, bool flip){
			this.transform.position = start;
			this.destination = destination;

			this.grannySpriteRenderer.flipX = flip;

			this.currentState = GrannyController.Granny_States.Moving;

		}


		void OnTriggerEnter2D (Collider2D col){

			if (col.gameObject.tag == "Item") {
				currentState = Granny_States.Falling;
			}
				
		}

		void OnTriggerExit2D(Collider2D col){

			if (col.gameObject.tag == "Item") {

			}
				

		}
	}
}
