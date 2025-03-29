using UnityEngine;
using System.Collections.Generic;

namespace Tiger
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectPrefab;
        
        [SerializeField]
        private GameObject spawnArea;
        
        // START for tests
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SpawnObjects(5, 2);
            }
        }
        // END for tests

        void SpawnObjects(int numberOfObjects, float distanceBetweenObjects)
        {
            if (!spawnArea) return;

            List<Vector3> positions = new();
            var (positionY, min, max) = GetSpawnData(distanceBetweenObjects);

            int spawnedCount = 0;
            int attempts = 0;
            int maxAttempts = numberOfObjects * 10;
            
            while (spawnedCount < numberOfObjects && attempts < maxAttempts)
            {
                Vector3 randomPosition = GenerateRandomPosition(min, max, positionY);
    
                if (CheckValidDistance(randomPosition, positions, distanceBetweenObjects))
                {
                    positions.Add(randomPosition);
                    spawnedCount++;
                }
    
                attempts++;
            }

            positions.ForEach(pos => Instantiate(objectPrefab, pos, Quaternion.identity));
        }
        
        private (float positionY, Vector3 min, Vector3 max) 
            GetSpawnData(float distanceBetweenObjects)
        {
            Bounds bounds = spawnArea.GetComponent<Renderer>().bounds;
            float positionY = bounds.max.y;

            Vector3 min = bounds.min + Vector3.one * distanceBetweenObjects;
            Vector3 max = bounds.max - Vector3.one * distanceBetweenObjects;

            return (positionY, min, max);
        }
        
        private Vector3 GenerateRandomPosition(Vector3 minBounds, Vector3 maxBounds, float positionY)
        {
            float x = Random.Range(minBounds.x, maxBounds.x);
            float z = Random.Range(minBounds.z, maxBounds.z);
            return new Vector3(x, positionY, z);
        }
        
        private bool CheckValidDistance(Vector3 newPosition, List<Vector3> existingPositions, float minDistance)
        {
            foreach (Vector3 position in existingPositions)
            {
                if (Vector3.Distance(position, newPosition) < minDistance)
                    return false;
            }
            return true;
        }
    }
}