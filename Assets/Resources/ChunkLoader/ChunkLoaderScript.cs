using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoaderScript : MonoBehaviour
{
    public GameObject playerObject;

    private int chunk_size   =  64;
    private int view_dstns   =  10;
    private int unload_dst;
    private int floor_height = -10;

    private int del_cnt = 1000;

    public GameObject chunk_prefab;

    public Dictionary<Vector3, GameObject> loaded_chunks = new Dictionary<Vector3, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("chunk loading start");
        chunk_prefab = Resources.Load("Chunk/Chunk") as GameObject;
        unload_dst   = view_dstns + 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject gen_chunk(Vector3 pos) {

        var chunk = GameObject.Instantiate(
            chunk_prefab, 
            pos * chunk_size + new Vector3(0, floor_height, 0),

            transform.rotation
        );


        chunk.GetComponent<ChunkScript>().Generate(pos, chunk_size);
        chunk.transform.SetParent(transform);

        return chunk;
    }

    void FixedUpdate() {
        var player_pos = playerObject.transform.position;
        player_pos /= chunk_size;
        player_pos = new Vector3(
            (int)(player_pos.x),
            0,
            (int)(player_pos.z)
        );

        for (int dx = -view_dstns; dx < view_dstns + 1; dx++ )
            for (int dz = -view_dstns; dz < view_dstns + 1; dz++ ) {
                var chunk_pos = player_pos + new Vector3(dx, 0, dz);
                if (!loaded_chunks.ContainsKey(chunk_pos)) {
                    loaded_chunks[chunk_pos] = gen_chunk(chunk_pos);
                }
            }

        del_cnt -= 1;
        if (del_cnt <= 0) {
            del_cnt = 50;
            Debug.Log("Unloading chunks");
            List<Vector3> del_chunks = new List<Vector3>();
            foreach (KeyValuePair<Vector3, GameObject> chunk in loaded_chunks)  
            {  
                var distance = (chunk.Key - player_pos).magnitude;
                if (distance > unload_dst)
                    del_chunks.Add(chunk.Key);
            } 

            foreach (Vector3 key in del_chunks) {
                Destroy(loaded_chunks[key]);
                loaded_chunks.Remove(key);
            }
            Debug.Log(del_chunks.Count);
        }
    }
}
