using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(Vector3 chunk_pos, int chunk_size) {
        int   grid_side = 16;
        float buld_size = chunk_size / grid_side;

        var floor_prefab = Resources.Load("Chunk/Floor") as GameObject;
        var build_prefab = Resources.Load("Building/TargetBuildingPrefab") as GameObject;
        var engun_prefab = Resources.Load("Gun/gunBase") as GameObject;

        var floor = GameObject.Instantiate(
            floor_prefab, 
            new Vector3(0, 0, 0),
            transform.rotation
        );
        floor.transform.SetParent(transform, false);
        floor.transform.localScale = new Vector3(chunk_size, 1, chunk_size);

        int x = (int)chunk_pos.x;
        int z = (int)chunk_pos.z;
        if (chunk_pos.magnitude > 3) {
            if ((((int)Random.Range(0, 2) + x * z) % 5) == 0) {
                for (int dx = 0; dx < grid_side; ++dx)
                    for (int dz = 0; dz < grid_side; ++dz) {
                        if ((int)Random.Range(0, 3) > 0 && (((x + dx) * (z + dz)) % 5 == 0 || (dx * dz + dz * dz) % 5 == 0 /*|| (dz + dx * z) % 6 == 0*/ )) {
                            int bheight = 
                                ((int)(dx+dz*dx+dz / 2)) % (int)Mathf.Max(chunk_pos.magnitude / 100, 7) 
                                +  7 
                                + 15 * System.Convert.ToInt32((int)Random.Range(0, 200) == 1)
                                +  7 * System.Convert.ToInt32((int)Random.Range(0,  10) == 1);

                            var diff = new Vector3(dx, 0, dz);
                            var building = GameObject.Instantiate(
                                build_prefab, 
                                new Vector3(0, 0, 0),
                                
                                transform.rotation
                            );

                            building.name = "building";
                            building.transform.SetParent(transform, false);

                            building.transform.localPosition = diff * buld_size;
                            building.transform.localScale = new Vector3(
                                0.9f * buld_size, 
                                bheight * 2,
                                0.9f * buld_size
                            );
                            building.GetComponent<GetDamagedBehaviour>().SetStat(1, bheight);
                        }
                }
            }
            
            else {
                var gun = GameObject.Instantiate(
                    engun_prefab, 
                    new Vector3(0, 0, 0),
                    
                    transform.rotation
                );
                gun.transform.SetParent(transform, false);
                gun.transform.localPosition += new Vector3(chunk_size / 2, 1, chunk_size / 2);
                gun.GetComponent<GetDamagedBehaviour>().SetStat(100, 50);
            }
        }
    }
}
