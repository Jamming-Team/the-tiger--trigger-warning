using UnityEngine;
using System.Collections.Generic;

namespace Tiger
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private ClickableObject _objectPrefab;
        
        [SerializeField] private GameObject usualItemPrefab;
        [SerializeField] private GameObject mimicPrefab;
        [SerializeField] private GameObject spawnArea;
        [SerializeField] Transform _spawningYTransform;
        [SerializeField] private LayerMask spawnCheckLayerMask;
        [SerializeField] private float spawnCheckRadius = 1f;
        
        // START for tests
        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.LeftShift))
            // {
            //     SpawnUsualItems(5);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.RightShift))
            // {
            //     SpawnMimic();
            // }
            //
            // if (Input.GetKeyDown(KeyCode.LeftControl))
            // {
            //     DestroyAllObjects();
            // }
        }
        // END for tests

        public void DestroyAllObjects()
        {
            int prefabLayer = _objectPrefab.gameObject.layer;

            // check all objects
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == prefabLayer)
                {
                    Destroy(obj);
                }
            }
        }

        // void SpawnUsualItems(int count)
        // {
        //     SpawnObjects(count, usualItemPrefab);
        // }
        //
        // void SpawnMimic()
        // {
        //     SpawnObjects(1, mimicPrefab);
        // }


        public void SpawnObjects(List<DataSO.ObjectData> objectVariants)
        {
            List<Vector3> positions = new();
            var (positionY, min, max) = GetSpawnData();

            int spawnedCount = 0;
            int attempts = 0;
            int maxAttempts = objectVariants.Count * 30;
            
            while (spawnedCount < objectVariants.Count)
            {
                Vector3 randomPosition = GenerateRandomPosition(min, max, positionY);
    
                if (CheckSpaceForSpawn(randomPosition))
                {
                    Debug.Log(CheckSpaceForSpawn(randomPosition));
                    positions.Add(randomPosition);
                    var spawnedObject = Instantiate(_objectPrefab, positions[spawnedCount], Quaternion.identity);
                    spawnedObject.Init(objectVariants[spawnedCount]);
                    spawnedCount++;
                }
    
                attempts++;
            }

            // for (int i = 0; i < objectVariants.Count; i++) {
            //     var spawnedObject = Instantiate(_objectPrefab, positions[i], Quaternion.identity);
            //     spawnedObject.Init(objectVariants[i]);
            // }
            // positions.ForEach(pos => {
            //     var spawnedObject = Instantiate(_objectPrefab, pos, Quaternion.identity);
            //     spawnedObject.Init();
            // });
        }
        
        private (float positionY, Vector3 min, Vector3 max) GetSpawnData()
        {
            Bounds bounds = spawnArea.GetComponent<Renderer>().bounds;
            float positionY = _spawningYTransform.position.y;

            Vector3 min = bounds.min + Vector3.one * spawnCheckRadius + Vector3.one;
            Vector3 max = bounds.max - Vector3.one * spawnCheckRadius + Vector3.one;

            return (positionY, min, max);
        }
        
        private Vector3 GenerateRandomPosition(Vector3 minBounds, Vector3 maxBounds, float positionY)
        {
            float x = Random.Range(minBounds.x, maxBounds.x);
            float z = Random.Range(minBounds.z, maxBounds.z);
            return new Vector3(x, positionY, z);
        }
        
        private bool CheckSpaceForSpawn(Vector3 position)
        {
            return !Physics.CheckSphere(position, spawnCheckRadius, spawnCheckLayerMask);
        }
    }
}