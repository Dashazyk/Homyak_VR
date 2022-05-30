using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Collections.Generic;

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
        // GameObject chunk = new GameObject();

        var chunk = GameObject.Instantiate(
            chunk_prefab, 
            pos * chunk_size + new Vector3(0, floor_height, 0),
            // Quaternion.Euler(1, 1, 1)
            transform.rotation
        );
        // floor.transform.parent = transform;
        // floor.transform.localScale = new Vector3(chunk_size, 1, chunk_size);
        // Debug.Log(floor);

        chunk.GetComponent<ChunkScript>().Generate(pos, chunk_size);
        chunk.transform.SetParent(transform);

        // int bsize = 4;

        // if (pos.magnitude > 3) {
        //     int should_gen = (int)Random.Range(1, 2 + pos.magnitude / 10);
        //     if (should_gen > 1) {
        //         int cnt = (int)Random.Range(1, Mathf.Min(pos.magnitude + 2, chunk_size / (2*bsize)));

        //         for (int i = 0; i < cnt; ++i) 
        //             for (int j = 0; j < cnt; ++j) {
        //                 int bheight = (int)Random.Range(1, 4);
        //                 Vector3 bpos = new Vector3(i, 0, j);
        //                 var building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //                 building.name = "building";
        //                 building.transform.parent = floor.transform;
        //                 building.transform.position = pos * chunk_size + new Vector3(0, floor_height + 1, 0) + new Vector3(i, 0, j) * bsize;
        //                 building.transform.localScale = new Vector3(1.0f * bsize / chunk_size, bheight * bsize * 2, 1.0f * bsize / chunk_size);
        //             }
        //         // building.transform.localScale = new Vector3(1.0f * bsize / chunk_size, pos.magnitude * 10, 1.0f * bsize / chunk_size);
        //     }
        // }

        return chunk;
    }

    void FixedUpdate() {
        // Debug.Log(chunk_prefab);
        var player_pos = playerObject.transform.position;
        player_pos /= chunk_size;
        player_pos = new Vector3(
            (int)(player_pos.x),
            // (int)(player_pos.y),
            0,
            (int)(player_pos.z)
        );
        // Debug.Log(player_pos);

        for (int dx = -view_dstns; dx < view_dstns + 1; dx++ )
            for (int dz = -view_dstns; dz < view_dstns + 1; dz++ ) {
                var chunk_pos = player_pos + new Vector3(dx, 0, dz);
                if (!loaded_chunks.ContainsKey(chunk_pos)) {
                    // Debug.Log("trying to inst");
                    loaded_chunks[chunk_pos] = gen_chunk(chunk_pos);
                    // Debug.Log(floor.transform.position);
                }
            }

        del_cnt -= 1;
        if (del_cnt <= 0) {
            del_cnt = 50;
            Debug.Log("Unloading chunks");
            List<Vector3> del_chunks = new List<Vector3>();
            foreach (KeyValuePair<Vector3, GameObject> chunk in loaded_chunks)  
            {  
                // Debug.Log(chunk.Key);
                // Debug.Log(chunk.Value);  
                var distance = (chunk.Key - player_pos).magnitude;
                if (distance > unload_dst)
                    // Debug.Log(distance);
                    // Destroy(loaded_chunks[chunk.Key]);
                    // loaded_chunks.Remove(chunk.Key);
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
