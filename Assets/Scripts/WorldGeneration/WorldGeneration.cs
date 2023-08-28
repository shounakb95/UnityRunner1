using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

	//Gameplay
	private float chunkSpawnZ;
	private Queue<Chunk> activeChunks=new Queue<Chunk>();
	private List<Chunk> chunkPool= new List<Chunk>();

	//Configurable field
	[SerializeField] private int firstChunkSpawnPosition = -10;
	[SerializeField] private int chunkOnScreen =5;
	[SerializeField] private float despawnDistance = 5.0f;

	[SerializeField] private List<GameObject> chunkPrefabs;
	[SerializeField] private Transform cameraTransform;

	
	private void Awake()
	{
		ResetWorld();
	}


   private void Start()
   {
		//Check if we have an empty chunkPrefabs List
		if(chunkPrefabs.Count==0)
		{
			Debug.LogError("No chunk Prefab found on the world generator, please assign some chunks ");
			return;
		}

		//Try to assign a cameraTransform
		if(!cameraTransform)
		{
			cameraTransform = Camera.main.transform;
			Debug.Log("We've assigned cameraTransform automatically to the Camera.main'");
		}
   }

  

   public void ScanPosition()
   {
		float cameraZ = cameraTransform.position.z;
		Chunk lastChunk= activeChunks.Peek();

		if(cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
		{
			SpawnNewChunk();
			DeleteLastChunk();
		}
		
   }

   private void SpawnNewChunk()
   {
		//Get a random index for which Prefab to Spawn
		int randomIndex = Random.Range(0,chunkPrefabs.Count);

		//Does it already exist within our pool 
		Chunk chunk=chunkPool.Find(x => !x.gameObject.activeSelf && x.name == (chunkPrefabs[randomIndex].name + "(Clone)") );

		//Cretae a hunk if we're not able to find one to reuse'
		if(!chunk)
		{
			GameObject go= Instantiate(chunkPrefabs[randomIndex], transform);
			chunk = go.GetComponent<Chunk>();
		}

		//Place the object and  show it 
		chunk.transform.position= new Vector3(0,0, chunkSpawnZ);
		chunkSpawnZ +=chunk.chunkLength;

		//Store the value to resue in our pool 
		activeChunks.Enqueue(chunk);
		chunk.ShowChunk();

   }

   private void DeleteLastChunk()
   {
		Chunk chunk= activeChunks.Dequeue();
		chunk.HideChunk();
		chunkPool.Add(chunk);
   }

   public void ResetWorld()
   {
		//Reset ChunkSpawn Z
		chunkSpawnZ= firstChunkSpawnPosition;

		for(int i=activeChunks.Count;i!=0;i--)
			DeleteLastChunk();

		for(int i=0; i< chunkOnScreen;i++)
		SpawnNewChunk();
		
   }
}
