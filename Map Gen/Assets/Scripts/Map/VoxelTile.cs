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

    [HideInInspector] public int[] ColorsRight;
    [HideInInspector] public int[] ColorsForward;
    [HideInInspector] public int[] ColorsLeft;
    [HideInInspector] public int[] ColorsBack;

    public void CalculateSideColors()
    {
        ColorsRight = new int[TileSideVoxels * TileSideVoxels];
        ColorsForward = new int[TileSideVoxels * TileSideVoxels];
        ColorsLeft = new int[TileSideVoxels * TileSideVoxels];
        ColorsBack = new int[TileSideVoxels * TileSideVoxels];

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

        int[] colorsRightNew = new int[TileSideVoxels*TileSideVoxels];
        int[] colorsForwardNew = new int[TileSideVoxels * TileSideVoxels];
        int[] colorsLeftNew = new int[TileSideVoxels * TileSideVoxels];
        int[] colorsBackNew = new int[TileSideVoxels * TileSideVoxels];

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

    private int GetVoxelColor ( int verticalLayer, int horizontalOffset, Vector3 direction)
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
         
            byte colorIndex = (byte)(hit.textureCoord.x * 256);

            if (colorIndex == 0) Debug.LogWarning(message:"Found color 0 in palette, trouble");

            return colorIndex;
        }

        return 0;
    }

}
