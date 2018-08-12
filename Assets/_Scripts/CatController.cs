using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat {
	public class CatController : MonoBehaviour {
		//THIS IS THE CAT

		public GameObject paws;

		bool holdingItem = false;
		GameObject itemHovered;
		GameObject itemHeld;


		// Use this for initialization
		void Start () {
			Cursor.visible = false;
		}
		
		// Update is called once per frame
		void Update () {
			FollowMouse ();
			Action ();
		}


		void FollowMouse(){
			// THE CAT FOLLOWS THE MOUSE HEHE
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			this.transform.position = new Vector3 (mousePos.x, mousePos.y, -1);

		}

		void Action(){
			if (Input.GetMouseButtonDown(0)) {
				
				if (!holdingItem)
					PickItem ();
				else
					ReleaseItem ();
			}
		}

		void PickItem (){

			if (itemHovered == null)
				return;

			holdingItem = true;
			itemHeld = itemHovered;

			Vector3 pawsPostion = paws.transform.position;

			itemHeld.transform.position = new Vector3(pawsPostion.x, pawsPostion.y, pawsPostion.z - 1);
			itemHeld.transform.SetParent (paws.transform);
		}

		void ReleaseItem (){
			
			holdingItem = false;
			itemHeld.transform.SetParent (null);

			var grid = Cat.GameManager.instance.itemGrid;
			var cellPos = grid.WorldToCell(itemHeld.transform.position);

			itemHeld.transform.position = grid.GetCellCenterWorld (cellPos);

			itemHeld = null;
			itemHovered = null;
		}

		void OnTriggerStay2D (Collider2D col){
					
			if (col.gameObject.tag == "Item") {
				itemHovered = col.gameObject;
			}
		}

		void OnTriggerExit2D(Collider2D col){
			
			if (col.gameObject.tag == "Item") {
				itemHovered = null;
			}
		}
	}
}