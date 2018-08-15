using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Cat {
	public class CatController : MonoBehaviour {
		
		//THIS IS THE CAT

		public GameObject paws;
		public Sprite[] catSprites;
		public BoxCollider2D catCollider;

		bool holdingItem = false;
		bool hoveringBlock = false;
		GameObject itemHovered;
		GameObject itemHeld;

		Vector2 itemHeldSize;

		SpriteRenderer catSprite;


		// Use this for initialization
		void Start () {
			catSprite = this.GetComponent<SpriteRenderer> ();
			catSprite.sprite = catSprites [0];

			itemHeldSize = new Vector2 (0, 0);

		}
		
		// Update is called once per frame
		void Update () {
			FollowMouse ();
			CheckForInputs ();
		}


		void FollowMouse(){
			// THE CAT FOLLOWS THE MOUSE HEHE
			Cursor.visible = false;
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			this.transform.position = new Vector3 (mousePos.x + itemHeldSize.x/2*0, mousePos.y - itemHeldSize.y*0, -1); //TODO: FIX THIS MOUSE POSITIONING THING

		}

		void CheckForInputs(){
			if (Input.GetMouseButtonDown(0)) {
				
				if (!holdingItem)
					CatchItem ();
				else
					ReleaseItem ();
			}
		}

		void CatchItem (){

			if (itemHovered == null)
				return;

			catSprite.sprite = catSprites [1];

			holdingItem = true;
			itemHeld = itemHovered;

			itemHeldSize = itemHeld.GetComponent<SpriteRenderer> ().bounds.size;

			Vector3 pawsPostion = paws.transform.position;

			itemHeld.transform.position = new Vector3(pawsPostion.x - itemHeldSize.x/4, pawsPostion.y + itemHeldSize.y/4, pawsPostion.z - 1);
			itemHeld.transform.SetParent (paws.transform);
			catCollider.enabled = false;
		}

		void ReleaseItem (){
			
			if (itemHovered != null || hoveringBlock) {
				// TODO: INSERT FEEDBACK FOR CELL ALREADY IN USE
				this.catSprite.DOColor(new Color(1,0,0), 0.2f).OnComplete(()=>{
					this.catSprite.DOColor(new Color(1,1,1), 0.1f);
				});
				return;
			}

			catSprite.sprite = catSprites [0];

			holdingItem = false;
			itemHeld.transform.SetParent (null);

			var grid = Cat.GameManager.instance.itemGrid;
		//	var itemCellPos = grid.WorldToCell(itemHeld.transform.position);
			var itemCellPos = grid.WorldToCell (new Vector3(itemHeld.transform.position.x + itemHeldSize.x/3.0f, itemHeld.transform.position.y + itemHeldSize.y/3.0f, itemHeld.transform.position.z));

			catCollider.enabled = true;

			var cs = grid.cellSize;

			var gridCellPos = grid.GetCellCenterWorld (itemCellPos);

			itemHeld.transform.position = gridCellPos;
			itemHeld.transform.position = new Vector3 (itemHeld.transform.position.x - cs.x/2, itemHeld.transform.position.y - cs.y/2, itemHeld.transform.position.z);

			itemHeldSize = new Vector2 (0, 0);
			itemHeld = null;
			itemHovered = null;
			

		}

		void OnTriggerStay2D (Collider2D col){
					
			if (col.gameObject.tag == "Item") {
				itemHovered = col.gameObject;
			}

			if (col.gameObject.tag == "Block") {
				hoveringBlock = true;
			}
		}

		void OnTriggerExit2D(Collider2D col){
			
			if (col.gameObject.tag == "Item") {
				itemHovered = null;
			}

			if (col.gameObject.tag == "Block") {
				hoveringBlock = false;
			}

		}
	}
}