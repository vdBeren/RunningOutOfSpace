using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat{
	public class GameManager : MonoBehaviour {

		public Grid itemGrid;

		public static GameManager instance = null;

		// Use this for initialization
		void Start () {

			if (instance == null)
				instance = this;

			DontDestroyOnLoad (this.gameObject);

		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
