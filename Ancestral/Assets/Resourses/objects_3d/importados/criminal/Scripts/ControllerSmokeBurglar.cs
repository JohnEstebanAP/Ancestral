using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ControllerSmokeBurglar : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public MeshRenderer meshRenderer;
    public VisualEffect effect;
    public float refreshRate;
    // Start is called before the first frame update
    void Start()
    {
      
        StartCoroutine(
            UpdateVFXGraph()
            );
        
        
    }

    IEnumerator UpdateVFXGraph() {

        while (gameObject.activeSelf) {


            if (skinnedMesh != null)
            {
                Mesh mesh = new Mesh();
                skinnedMesh.BakeMesh(mesh);
                //skinnedMesh.sharedMesh;            
                Vector3[] vertices = mesh.vertices;
                Mesh mesh2 = new Mesh();
                mesh2.vertices = vertices;
                effect.SetMesh("Mesh", mesh2);
            }
            else
            {
                Debug.LogError("SkinnedMeshRenderer reference is missing!");
            }

            yield return new WaitForSeconds( refreshRate);

        }
    }
}
