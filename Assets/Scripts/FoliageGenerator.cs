using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageGenerator : MonoBehaviour
{
    public GameObject grassPrefab; // reference to grass object prefab
    public GameObject shroomPrefab; // reference to leaf object prefab
    public float coveragePercentage = 0.5f; // percentage of plane to be covered
    public float spacing = 0.2f; // distance between foliage objects

    // Start is called before the first frame update
    void Start()
    {
        // get the dimensions of the plane
        float width = GetComponent<MeshFilter>().mesh.bounds.size.x;
        float height = GetComponent<MeshFilter>().mesh.bounds.size.z;

        // calculate number of objects to instantiate
        int numObjects = Mathf.RoundToInt(width * height * coveragePercentage / (spacing));

        // instantiate foliage objects
        for (int i = 0; i < numObjects; i++)
        {
            GameObject prefab;
            if (Random.value < 0.5f) // randomly choose between grass and leaf objects
            {
                prefab = grassPrefab;
            }
            else
            {
                prefab = shroomPrefab;
            }

            // instantiate object at random position on the plane
            Vector3 position = new Vector3(Random.Range(-width / 2f, width / 2f), 0f, Random.Range(-height / 2f, height / 2f));
            position += transform.position; // add the position of the parent object
            position.y = transform.position.y- (prefab==grassPrefab ? 2f : 0f); // set the y position to be the same as the parent object
            position += new Vector3(Random.Range(-spacing, spacing), 0f, Random.Range(-spacing, spacing)); // add some random variation to the position
            GameObject foliageObject = Instantiate(prefab, position, Quaternion.identity);
            foliageObject.transform.parent = this.gameObject.transform;
            foliageObject.SetActive(true);
        }
    }
}

