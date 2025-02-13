using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private bool _removeChildrenAfterCombine; // ������� �������� ������� ����� �����������?

    public void Initialization()
    {
        CombineMeshes();
    }

    public void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(); // ������� ��� MeshFilter � �������� ��������
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        Matrix4x4 parentTransform = transform.worldToLocalMatrix; // �������������� � ��������� ������������

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = parentTransform * meshFilters[i].transform.localToWorldMatrix;
        }

        // ������ ����� ������ ��� ������������ ����
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);
        meshFilter.mesh = combinedMesh;

        // �������� ��������� �� ������� ��������� ������� (����� ����������)
        meshRenderer.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        // ������� �������� ������� ����� �����������, ���� �������� �����
        if (_removeChildrenAfterCombine)
        {
            foreach (MeshFilter mf in meshFilters)
            {
                if (mf.gameObject != gameObject) // �� ������� ���� ������
                {
                    Destroy(mf.gameObject);
                }
            }
        }

        Debug.Log("���� ����������!");
    }
}
