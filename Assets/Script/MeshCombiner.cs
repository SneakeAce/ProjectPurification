using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private bool _removeChildrenAfterCombine; // Удалять дочерние объекты после объединения?

    public void Initialization()
    {
        CombineMeshes();
    }

    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(); // Находим все MeshFilter в дочерних объектах
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        Matrix4x4 parentTransform = transform.worldToLocalMatrix; // Преобразование в локальное пространство

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = parentTransform * meshFilters[i].transform.localToWorldMatrix;
        }

        // Создаём новый объект для объединённого меша
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        meshFilter.mesh = combinedMesh;

        // Копируем материалы из первого дочернего объекта (можно доработать)
        meshRenderer.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        // Удаляем дочерние объекты после объединения, если включена опция
        if (_removeChildrenAfterCombine)
        {
            foreach (MeshFilter mf in meshFilters)
            {
                if (mf.gameObject != gameObject) // Не удаляем саму турель
                {
                    Destroy(mf.gameObject);
                }
            }
        }

        Debug.Log("Меши объединены!");
    }
}
