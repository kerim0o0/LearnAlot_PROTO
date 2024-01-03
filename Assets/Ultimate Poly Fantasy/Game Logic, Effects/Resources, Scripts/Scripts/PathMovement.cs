using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UltimatePolyFantasy {
	public class PathMovement : MonoBehaviour {

		public WaypointPath pathToFollow;

		public int currentWayPointID = 0;
		public float moveSpeed;
		public float reach = 1.0f;
		public float rotationSpeed = 0.5f;
		public string pathName;

		public float firstRideTimer;
		public float secondRideTimer;

		private bool startRide = false;
		private bool playerOnBoard = false;
		private GameObject player;

		Vector3 lastPosition;
		

		// Use this for initialization
		void Start()
		{
			lastPosition = transform.position;
			player = GameObject.Find("Player");
			StartCoroutine(startFirstRideTimer());
		}

		// Update is called once per frame
		void Update()
		{
			if (startRide)
			{
				float distance = Vector3.Distance(pathToFollow.pathPoints[currentWayPointID].position, transform.position);
				transform.position = Vector3.MoveTowards(transform.position, pathToFollow.pathPoints[currentWayPointID].position, Time.deltaTime * moveSpeed);

				var rotation = Quaternion.LookRotation(pathToFollow.pathPoints[currentWayPointID].position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

				if (distance <= reach)
				{
					currentWayPointID++;
				}

				if (currentWayPointID >= pathToFollow.pathPoints.Count)
				{
					currentWayPointID = 0;
				}

				if (playerOnBoard)
				{

					player.GetComponent<CharacterController>().enabled = false;
					player.GetComponent<PlayerController>().enabled = false;
				}

			}

			else if(!startRide && playerOnBoard)
            {
				player.GetComponent<CharacterController>().enabled = true;
				player.GetComponent<PlayerController>().enabled = true;
			}

		}
		public IEnumerator startFirstRideTimer()
		{
			yield return new WaitForSeconds(10f);
			startRide = true;
			yield return new WaitForSeconds(firstRideTimer);
			startRide = false;
			StartCoroutine(startSecondRideTimer());
		}

		public IEnumerator startSecondRideTimer()
		{
			yield return new WaitForSeconds(10f);
			startRide = true;
			yield return new WaitForSeconds(secondRideTimer);
			transform.DOMove(lastPosition, 2f);
			transform.DOLocalRotate(new Vector3(0, -150, 0), 1f);
			yield return new WaitForSeconds(1f);
			startRide = false;
			StartCoroutine(startFirstRideTimer());
		}

        private void OnTriggerStay(Collider other)
        {
			if (other.CompareTag("Player")) {
				other.transform.SetParent(this.transform);
				playerOnBoard = true;
			}
        }

        private void OnTriggerExit(Collider other)
        {
			if (other.CompareTag("Player"))
			{
				other.transform.SetParent(null);
				playerOnBoard = false;
			}
		}

    }
}