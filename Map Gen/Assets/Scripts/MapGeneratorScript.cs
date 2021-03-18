using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class MapGeneratorScript : MonoBehaviour
{
    public List <VoxelTile> TilePrefabs;
    public Vector2Int MapSize = new Vector2Int(x: 10, y: 10);

    private VoxelTile[,] spawnedTiles;

    public float VoxelSize = 0.1f;
    public int TileSideVoxels = 8;
    private void Start()
    {
        spawnedTiles = new VoxelTile[MapSize.x, MapSize.y];

        foreach (VoxelTile tilePrefab in TilePrefabs)
        {
            tilePrefab.CalculateSideColors();
        }
        int countBeforeAdding = TilePrefabs.Count;
        for (int i = 0; i< countBeforeAdding; i++)
        {
            VoxelTile clone;
            switch (TilePrefabs[i].Rotation)
            {
                case VoxelTile.RotationType.OneRotation:
                    break;

                case VoxelTile.RotationType.TwoRotations:


                    TilePrefabs[i].Frequency /= 2;
                    if (TilePrefabs[i].Frequency <= 0) TilePrefabs[i].Frequency = 1;

                    clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position, Quaternion.identity );
                    clone.Rotate90();
                    TilePrefabs.Add(clone);
                    break;

                case VoxelTile.RotationType.FourRotations:
                    TilePrefabs[i].Frequency /= 4;
                    if (TilePrefabs[i].Frequency <= 0) TilePrefabs[i].Frequency = 1;

                    clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position, Quaternion.identity);
                    clone.Rotate90();
                    TilePrefabs.Add(clone);
                    clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position, Quaternion.identity);
                    clone.Rotate90();
                    clone.Rotate90();
                    TilePrefabs.Add(clone);
                    clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position, Quaternion.identity);
                    clone.Rotate90();
                    clone.Rotate90();
                    clone.Rotate90();
                    TilePrefabs.Add(clone);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        StartCoroutine(routine: Generate());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StopAllCoroutines();
            foreach (VoxelTile spawnedTile in spawnedTiles)
            {
                if (spawnedTile != null) Destroy(spawnedTile.gameObject);
            }
            StartCoroutine(routine: Generate());
        }
    }

    public IEnumerator Generate()
    {
        for (int x = 1; x < MapSize.x - 1; x++)
        {
            for (int y = 1; y < MapSize.y - 1; y++)
            {
                yield return new WaitForSeconds(0.001f);

                PlaceTile(x, y);
            }
        }
    }
    private void PlaceTile(int x, int y)
    {
        List<VoxelTile> availibleTiles = new List<VoxelTile>();

        foreach (VoxelTile tilePrefab in TilePrefabs)
        {
            if (CanAppendTile(existingTile: spawnedTiles[x - 1, y], tileToAppend: tilePrefab, Vector3.left) &&
                CanAppendTile(existingTile: spawnedTiles[x + 1, y], tileToAppend: tilePrefab, Vector3.right) &&
                CanAppendTile(existingTile: spawnedTiles[x, y - 1], tileToAppend: tilePrefab, Vector3.back) &&
                CanAppendTile(existingTile: spawnedTiles[x, y + 1], tileToAppend: tilePrefab, Vector3.forward))

            {
                availibleTiles.Add(tilePrefab);
            }
        }

        if (availibleTiles.Count == 0) return;

        VoxelTile selectedTile = GetRandomTile(availibleTiles);
        Vector3 position = new Vector3(x, y: 0, z: y) * selectedTile.VoxelSize * selectedTile.TileSideVoxels;
        spawnedTiles[x, y] = Instantiate(selectedTile, position, selectedTile.transform.rotation);
    }


    private VoxelTile GetRandomTile(List<VoxelTile> availibleTiles)
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < availibleTiles.Count; i++)
        {
            chances.Add(availibleTiles[i].Frequency);
        }

        float value = UnityEngine.Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i <chances.Count; i++)
        {
            sum += chances[i];
            if (value<sum)
            {
                return availibleTiles[i];
            }
        }

        return availibleTiles[availibleTiles.Count-1];

    }


    private bool CanAppendTile(VoxelTile existingTile, VoxelTile tileToAppend, Vector3 direction)
    {
        if (existingTile == null) return true;

        if (direction == Vector3.right)

        {
            return Enumerable.SequenceEqual (existingTile.ColorsRight, tileToAppend.ColorsLeft);
        }

        else if (direction == Vector3.left)
        {
            return Enumerable.SequenceEqual (existingTile.ColorsLeft, tileToAppend.ColorsRight);
        }

        else if (direction == Vector3.forward)
        {
            return Enumerable.SequenceEqual (existingTile.ColorsForward, tileToAppend.ColorsBack);
        }

        else if (direction == Vector3.back)
        {
            return Enumerable.SequenceEqual (existingTile.ColorsBack, tileToAppend.ColorsForward);
        }

        else
        {
            throw new ArgumentException (message: "Wrong direction value, should be Vector3.left/right/back/forward", nameof(direction));
        }
    }
}

