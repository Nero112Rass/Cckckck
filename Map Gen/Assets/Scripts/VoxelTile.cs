using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelTile : MonoBehaviour
{
    public float VoxelSize = 0.1f;
    public int TileSideVoxels = 8;

    public RotationType Rotation;
    public enum RotationType
    {
        OneRotation,
        TwoRotations,
        FourRotations
    }

    [Range(1,100)]
    public int Frequency = 50;

    [HideInInspector] public byte[] ColorsRight;
    [HideInInspector] public byte[] ColorsForward;
    [HideInInspector] public byte[] ColorsLeft;
    [HideInInspector] public byte[] ColorsBack;

    public void CalculateSideColors()
    {
        ColorsRight = new byte[TileSideVoxels * TileSideVoxels];
        ColorsForward = new byte[TileSideVoxels * TileSideVoxels];
        ColorsLeft = new byte[TileSideVoxels * TileSideVoxels];
        ColorsBack = new byte[TileSideVoxels * TileSideVoxels];

        for (int y = 0; y < TileSideVoxels; y++)
        {
            for (int i = 0; i < TileSideVoxels; i++)
            {
                ColorsRight[y* TileSideVoxels + i] = GetVoxelColor( verticalLayer: y, horizontalOffset: i, Vector3.right);
                ColorsForward[y * TileSideVoxels + i] = GetVoxelColor(verticalLayer: y, horizontalOffset: i, Vector3.forward);
                ColorsLeft[y * TileSideVoxels + i] = GetVoxelColor(verticalLayer: y, horizontalOffset: i, Vector3.left);
                ColorsBack[y * TileSideVoxels + i] = GetVoxelColor(verticalLayer: y, horizontalOffset: i, Vector3.back);
            }
        }
        
    }

    public void Rotate90()
    {
        transform.Rotate(xAngle: 0, yAngle: 90, zAngle: 0);

        byte[] colorsRightNew = new byte[TileSideVoxels*TileSideVoxels];
        byte[] colorsForwardNew = new byte[TileSideVoxels * TileSideVoxels];
        byte[] colorsLeftNew = new byte[TileSideVoxels * TileSideVoxels];
        byte[] colorsBackNew = new byte[TileSideVoxels * TileSideVoxels];

        for (int layer = 0; layer<TileSideVoxels; layer++)
        {
            for (int offset = 0; offset < TileSideVoxels; offset++)
            {
                colorsRightNew[layer * TileSideVoxels + offset] = ColorsForward[layer * TileSideVoxels + TileSideVoxels - offset - 1];
                colorsForwardNew[layer * TileSideVoxels + offset] = ColorsLeft[layer * TileSideVoxels + offset];
                colorsLeftNew[layer * TileSideVoxels + offset] = ColorsBack[layer * TileSideVoxels + TileSideVoxels - offset - 1];
                colorsBackNew[layer * TileSideVoxels + offset] = ColorsRight[layer * TileSideVoxels + offset];
            }
        }

        ColorsRight = colorsRightNew;
        ColorsForward = colorsForwardNew;
        ColorsLeft = colorsLeftNew;
        ColorsBack = colorsBackNew;

    }

    private byte GetVoxelColor ( int verticalLayer, int horizontalOffset, Vector3 direction)
    {
        var meshCollider = GetComponentInChildren<MeshCollider>();

        float vox = VoxelSize;
        float half = VoxelSize / 2;

        Vector3 rayStart;

        if ( direction == Vector3.right)
        {
            rayStart = meshCollider.bounds.min +
                            new Vector3 ( x: -half, y: 0, z: half + horizontalOffset*vox);
        }

        else if ( direction == Vector3.forward)
        {
            rayStart = meshCollider.bounds.min +
                            new Vector3 ( x: half + horizontalOffset * vox, y: 0, z: -half );
        }

        else if ( direction == Vector3.left)
        {
            rayStart = meshCollider.bounds.max +
                            new Vector3 ( x: half, y: 0, z: -half - ( TileSideVoxels - horizontalOffset -1) * vox);
        }

        else if ( direction == Vector3.back)
        {
            rayStart = meshCollider.bounds.max +
                            new Vector3 ( x: -half - (TileSideVoxels - horizontalOffset - 1) * vox, y: 0, z: half);
        }
        else
        {
            throw new ArgumentException(message: "Wrong direction value, should be Vector3.left/right/back/forward", nameof(direction));
        }

        rayStart.y = meshCollider.bounds.min.y + half + verticalLayer * vox;



        if (Physics.Raycast(new Ray(origin: rayStart, direction), out RaycastHit hit, vox))
        {
            Mesh mesh = meshCollider.sharedMesh;

            int hitTriangleVertex = mesh.triangles[hit.triangleIndex * 3];
            byte colorIndex = (byte)(mesh.uv[hitTriangleVertex].x * 256);
            return colorIndex;
        }

        return 0;
    }
}
