using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class WaveMapGeneratorScript : MonoBehaviour
{
    public List<VoxelTile> TilePrefabs;
    public Vector2Int MapSize = new Vector2Int(x: 10, y: 10);

    private VoxelTile[,] spawnedTiles;

    public float VoxelSize = 0.1f;
    public int TileSideVoxels = 8;

    private Queue<Vector2Int> recalcPossibleTilesQueue = new Queue<Vector2Int>();

    private List<VoxelTile>[,] possibleTiles;

        private void Start()
        {
            spawnedTiles = new VoxelTile[MapSize.x, MapSize.y];

            foreach (VoxelTile tilePrefab in TilePrefabs)
            {
                tilePrefab.CalculateSideColors();
            }
            int countBeforeAdding = TilePrefabs.Count;
            for (int i = 0; i < countBeforeAdding; i++)
            {
                VoxelTile clone;
                switch (TilePrefabs[i].Rotation)
                {
                    case VoxelTile.RotationType.OneRotation:
                        break;

                    case VoxelTile.RotationType.TwoRotations:


                        TilePrefabs[i].Frequency /= 2;
                        if (TilePrefabs[i].Frequency <= 0) TilePrefabs[i].Frequency = 1;

                        clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position + Vector3.back * VoxelSize * TileSideVoxels, Quaternion.identity);
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        break;

                    case VoxelTile.RotationType.FourRotations:
                        TilePrefabs[i].Frequency /= 4;
                        if (TilePrefabs[i].Frequency <= 0) TilePrefabs[i].Frequency = 1;

                        clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position + Vector3.back * VoxelSize * TileSideVoxels + Vector3.back * VoxelSize * TileSideVoxels * 2, Quaternion.identity);
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position + (Vector3.back + Vector3.left) * VoxelSize * TileSideVoxels + Vector3.back * VoxelSize * TileSideVoxels * 4, Quaternion.identity);
                        clone.Rotate90();
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        clone = Instantiate(TilePrefabs[i], position: TilePrefabs[i].transform.position + Vector3.left * VoxelSize * TileSideVoxels + Vector3.back * VoxelSize * TileSideVoxels * 6, Quaternion.identity);
                        clone.Rotate90();
                        clone.Rotate90();
                        clone.Rotate90();
                        TilePrefabs.Add(clone);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Generate();

        }

    int backtracks = 0;






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {

            foreach (VoxelTile spawnedTile in spawnedTiles)
            {
                if (spawnedTile != null) Destroy(spawnedTile.gameObject);
            }
            Generate();
        }


    }


    private void Generate()
    {
        possibleTiles = new List<VoxelTile>[MapSize.x, MapSize.y];

        int maxAttempts = 10;
        int attempts = 0;
        while (attempts++ < maxAttempts)
        {
            for (int x = 0; x < MapSize.x; x++)
                for (int y = 0; y < MapSize.y; y++)
                {
                    possibleTiles[x, y] = new List<VoxelTile>(TilePrefabs);

                }

            VoxelTile tileInCenter = GetRandomTile(TilePrefabs);
            possibleTiles[MapSize.x / 2, MapSize.y / 2] = new List<VoxelTile> { tileInCenter };

            recalcPossibleTilesQueue.Clear();
            EnqueueNeighboursToRecalc(new Vector2Int(x: MapSize.x / 2, y: MapSize.y / 2));

            bool success = GenerateAllPossibleTiles();

            if (success) break;
        }
        PlaceAllTiles();

    }

    private bool GenerateAllPossibleTiles()
    {
        int maxIterations = MapSize.x * MapSize.y;
        int iterations = 0;
        while (iterations++<maxIterations)
        {
            int maxInnerIterations = MapSize.x * MapSize.y;
            int innerIterations = 0;

            while (recalcPossibleTilesQueue.Count > 0 && innerIterations++<maxInnerIterations)
            {
                Vector2Int position = recalcPossibleTilesQueue.Dequeue();
                if (position.x == 0 || position.y == 0 || position.x == MapSize.x - 1 || position.y == MapSize.y - 1)
                {
                    continue;
                }


                List<VoxelTile> possibleTilesHere = possibleTiles[position.x, position.y];

                int countRemoved = possibleTilesHere.RemoveAll(match: t => !IsTilePossibleHere(t, position));

                if (countRemoved > 0) EnqueueNeighboursToRecalc(position);


                if(possibleTilesHere.Count == 0)
                {
                    //На случай, если в данный "слот" не подходит ни один тайл, пробуем по-новой выбрать соседние тайлы.
                    possibleTilesHere.AddRange(TilePrefabs);
                    possibleTiles[position.x + 1, position.y] = new List<VoxelTile>(TilePrefabs);
                    possibleTiles[position.x - 1, position.y] = new List<VoxelTile>(TilePrefabs);
                    possibleTiles[position.x, position.y + 1] = new List<VoxelTile>(TilePrefabs);
                    possibleTiles[position.x, position.y - 1] = new List<VoxelTile>(TilePrefabs);

                    EnqueueNeighboursToRecalc(position);

                    backtracks++;
                }
            }

            if (innerIterations == maxIterations) break;

            List<VoxelTile> maxCountTile = possibleTiles[1, 1];
            Vector2Int maxCountTilePosition = new Vector2Int(1, 1);

            for (int x = 1; x < MapSize.x - 1; x++)
            for (int y = 1; y < MapSize.y - 1; y++)
            {
                if (possibleTiles[x, y].Count > maxCountTile.Count)
                {
                    maxCountTile = possibleTiles[x, y];
                    maxCountTilePosition = new Vector2Int(x, y);
                }

            }

            if(maxCountTile.Count == 1)
            {
                Debug.Log(message: $"Failure, generated for {iterations} iterations, with {backtracks} backtracks.");
                return true;
            }

            VoxelTile tileToCollapse = GetRandomTile(maxCountTile);

            possibleTiles[maxCountTilePosition.x, maxCountTilePosition.y] = new List<VoxelTile> { tileToCollapse };

            EnqueueNeighboursToRecalc(maxCountTilePosition);
        }
        Debug.Log(message: $"Failure, run out of iterations with {backtracks} backtracks");

        return false;
    }



    private bool IsTilePossibleHere(VoxelTile tile, Vector2Int position)
    {
        bool isAllRightTilesImpossible = possibleTiles[position.x - 1, position.y].All(rightTile => !CanAppendTile (existingTile: tile, tileToAppend: rightTile, Vector3.right));
        if (isAllRightTilesImpossible) return false;

        bool isAllLeftTilesImpossible = possibleTiles[position.x + 1, position.y].All(leftTile => !CanAppendTile(existingTile: tile, tileToAppend: leftTile, Vector3.left));
        if (isAllLeftTilesImpossible) return false;

        bool isAllForwardTilesImpossible = possibleTiles[position.x , position.y - 1].All(forwardTile => !CanAppendTile(existingTile: tile, tileToAppend: forwardTile, Vector3.forward));
        if (isAllForwardTilesImpossible) return false;

        bool isAllBackTilesImpossible = possibleTiles[position.x , position.y + 1].All(backTile => !CanAppendTile(existingTile: tile, tileToAppend: backTile, Vector3.back));
        if (isAllBackTilesImpossible) return false;

        return true;
    }

    private void PlaceAllTiles()
    {
        for (int x = 1; x < MapSize.x - 1; x++)
        for (int y = 1; y < MapSize.y - 1; y++)
        {

                PlaceTile(x, y);
        }    
    }


    private void EnqueueNeighboursToRecalc(Vector2Int position)
    {
        recalcPossibleTilesQueue.Enqueue(new Vector2Int(x: position.x + 1, position.y));
        recalcPossibleTilesQueue.Enqueue(new Vector2Int(x: position.x - 1, position.y));
        recalcPossibleTilesQueue.Enqueue(new Vector2Int(position.x, y: position.y + 1));
        recalcPossibleTilesQueue.Enqueue(new Vector2Int(position.x, y: position.y - 1));
    }

    private void PlaceTile(int x, int y)
    {
        if (possibleTiles[x,y].Count == 0) return;

        VoxelTile selectedTile = GetRandomTile(possibleTiles[x,y]);
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

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return availibleTiles[i];
            }
        }

        return availibleTiles[availibleTiles.Count - 1];

    }


    private bool CanAppendTile(VoxelTile existingTile, VoxelTile tileToAppend, Vector3 direction)
    {
        if (existingTile == null) return true;

        if (direction == Vector3.right)

        {
            return Enumerable.SequenceEqual(existingTile.ColorsRight, tileToAppend.ColorsLeft);
        }

        else if (direction == Vector3.left)
        {
            return Enumerable.SequenceEqual(existingTile.ColorsLeft, tileToAppend.ColorsRight);
        }

        else if (direction == Vector3.forward)
        {
            return Enumerable.SequenceEqual(existingTile.ColorsForward, tileToAppend.ColorsBack);
        }

        else if (direction == Vector3.back)
        {
            return Enumerable.SequenceEqual(existingTile.ColorsBack, tileToAppend.ColorsForward);
        }

        else
        {
            throw new ArgumentException(message: "Wrong direction value, should be Vector3.left/right/back/forward", nameof(direction));
        }
    }
}
    

