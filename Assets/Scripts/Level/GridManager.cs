using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GridManager : MonoBehaviour
{
    public GameObject cylinderPrefab; // Prefab to instantiate
    public float cellSize = 1.0f; // Size of each cell

    // Separate padding for each axis
    public float paddingX = 0.5f; // Padding for the X-axis
    public float paddingZ = 0.5f; // Padding for the Z-axis

    public int rows = 3; // Number of rows for the grid
    public int columns = 3; // Number of columns for the grid

    private bool[,] activeCells; // 2D array for active cells
    [SerializeField] private BoxCollider boxCollider; // Reference to the BoxCollider

    // JSON file path to load data
    private string loadFilePath = "Assets/Resources/gridData.json";

    private void Start()
    {
        LoadGridData(); // Load grid data from JSON
        GenerateGridInBounds(); // Generate the grid
    }

    void GenerateGridInBounds()
    {
        // Check if boxCollider is assigned
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider is not assigned!");
            return;
        }

        Bounds bounds = boxCollider.bounds;

        // Calculate usable space considering padding on the x and z axes
        Vector3 usableSpace = bounds.size - new Vector3(2 * paddingX, 0, 2 * paddingZ);

        // Calculate the number of cells that fit in the usable space
        int cellsX = Mathf.FloorToInt(usableSpace.x / cellSize);
        int cellsZ = Mathf.FloorToInt(usableSpace.z / cellSize);

        // Ensure that the number of rows and columns is limited to cellsX and cellsZ
        rows = Mathf.Min(rows, cellsZ); // Ensure rows don't exceed available space
        columns = Mathf.Min(columns, cellsX); // Ensure columns don't exceed available space

        // Determine the starting position of the grid (centered)
        Vector3 startPosition = bounds.center - new Vector3(usableSpace.x / 2, 0, usableSpace.z / 2);

        // Offset to position cylinders in the center of each cell
        Vector3 offset = new Vector3(cellSize / 2, 0, cellSize / 2);

        // Generate cylinders only in active cells based on grid data
        for (int z = 0; z < rows; z++) // Loop through rows (z-axis)
        {
            for (int x = 0; x < columns; x++) // Loop through columns (x-axis)
            {
                // Ensure the current cell is marked as active
                if (activeCells[z, x]) // Use [z, x] to match grid orientation
                {
                    // Calculate the cell position for instantiation
                    Vector3 cellPosition = startPosition + new Vector3(x * cellSize, 0, z * cellSize) + offset; // Correct cell positioning
                    Instantiate(cylinderPrefab, cellPosition, Quaternion.identity);
                }
            }
        }
    }

  /*  private void OnDrawGizmos()
    {
        if (boxCollider == null)
            return;

        // Check if activeCells is initialized
        if (activeCells == null)
            return;

        Bounds bounds = boxCollider.bounds;

        // Calculate usable space considering padding on the x and z axes
        Vector3 usableSpace = bounds.size - new Vector3(2 * paddingX, 0, 2 * paddingZ);
        int cellsX = Mathf.FloorToInt(usableSpace.x / cellSize);
        int cellsZ = Mathf.FloorToInt(usableSpace.z / cellSize);

        // Determine the starting position of the grid
        Vector3 startPosition = bounds.center - new Vector3(usableSpace.x / 2, 0, usableSpace.z / 2);

        // Draw the wireframe gizmos for the grid
        Gizmos.color = Color.green;
        for (int x = 0; x < cellsX; x++)
        {
            for (int z = 0; z < cellsZ; z++)
            {
                Vector3 cellPosition = startPosition + new Vector3(x * cellSize, 0, z * cellSize);
                Gizmos.DrawWireCube(cellPosition + Vector3.one * (cellSize / 2), Vector3.one * cellSize);
            }
        }

        // Draw the active cells in the gizmos
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (z < activeCells.GetLength(0) && x < activeCells.GetLength(1))
                {
                    Vector3 position = startPosition + new Vector3(x * cellSize, 0, z * cellSize);
                    Gizmos.color = activeCells[z, x] ? Color.white : Color.black; // White for active, black for inactive
                    Gizmos.DrawCube(position + Vector3.one * (cellSize / 2), Vector3.one * (cellSize * 0.9f));
                }
            }
        }
    }*/

    // Method to load grid data from JSON
    private void LoadGridData()
    {
        if (File.Exists(loadFilePath))
        {
            string json = File.ReadAllText(loadFilePath);
            GridData gridData = JsonUtility.FromJson<GridData>(json);

            // Apply loaded data
            rows = gridData.rows;
            columns = gridData.columns;

            // Reinitialize the activeCells grid based on loaded data
            activeCells = new bool[rows, columns];

            // Convert the flat List<bool> back to bool[,]
            for (int z = 0; z < rows; z++)
            {
                for (int x = 0; x < columns; x++)
                {
                    activeCells[z, x] = gridData.grid[z * columns + x];
                }
            }

            Debug.Log("Grid data loaded from " + loadFilePath);
        }
        else
        {
            Debug.LogError("Grid data file not found at " + loadFilePath);
        }
    }

    // Class for serializing grid data
    [System.Serializable]
    public class GridData
    {
        public int rows;
        public int columns;
        public List<bool> grid;
    }
}
