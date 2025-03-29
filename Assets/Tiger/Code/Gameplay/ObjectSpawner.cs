using UnityEngine;
using System.Collections.Generic;

namespace Tiger
{
    public class ObjectSpawner : MonoBehaviour
    {
        private float _distanceBetweenObjects = 6f;
        [SerializeField] private GameObject usualItemPrefab;
        [SerializeField] private GameObject mimicPrefab;
        [SerializeField] private GameObject spawnArea;
        
        // START for tests
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SpawnUsualItems(5);
            }
            
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                SpawnMimic();
            }
            
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                DestroyAllObjects();
            }
        }
        // END for tests

        void DestroyAllObjects()
        {
            int prefabLayer = usualItemPrefab.layer;

            // check all objects
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == prefabLayer)
                {
                    Destroy(obj);
                }
            }
        }

        void SpawnUsualItems(int count)
        {
            SpawnObjects(count, usualItemPrefab);
        }
        
        void SpawnMimic()
        {
            SpawnObjects(1, mimicPrefab);
        }

        void SpawnObjects(int objectCount, GameObject prefab)
        {
            List<Vector3> positions = new();
            var (positionY, min, max) = GetSpawnData();

            int spawnedCount = 0;
            int attempts = 0;
            int maxAttempts = objectCount * 10;
            
            while (spawnedCount < objectCount && attempts < maxAttempts)
            {
                Vector3 randomPosition = GenerateRandomPosition(min, max, positionY);
    
                if (CheckValidDistance(randomPosition, positions))
                {
                    positions.Add(randomPosition);
                    spawnedCount++;
                }
    
                attempts++;
            }

            positions.ForEach(pos => Instantiate(prefab, pos, Quaternion.identity));
        }
        
        private (float positionY, Vector3 min, Vector3 max) GetSpawnData()
        {
            Bounds bounds = spawnArea.GetComponent<Renderer>().bounds;
            float positionY = bounds.max.y;

            Vector3 min = bounds.min + Vector3.one * _distanceBetweenObjects;
            Vector3 max = bounds.max - Vector3.one * _distanceBetweenObjects;

            return (positionY, min, max);
        }
        
        private Vector3 GenerateRandomPosition(Vector3 minBounds, Vector3 maxBounds, float positionY)
        {
            float x = Random.Range(minBounds.x, maxBounds.x);
            float z = Random.Range(minBounds.z, maxBounds.z);
            return new Vector3(x, positionY, z);
        }
        
        private bool CheckValidDistance(Vector3 newPosition, List<Vector3> existingPositions)
        {
            foreach (Vector3 position in existingPositions)
            {
                if (Vector3.Distance(position, newPosition) < _distanceBetweenObjects)
                    return false;
            }
            return true;
        }
    }
}