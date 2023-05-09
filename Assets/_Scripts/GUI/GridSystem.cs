// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Telepatia
// {
//     public class GridSystem : MonoBehaviour
//     {
//         private GridXZ<GridObject> m_grid;
//         void Awake() 
//         {
//             int gridWidth = 10, gridHeight = 10;
//             float cellSize = 10f;
//             m_grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, GridXZ<GridObject> grid, int x, int z);
//         }
        
//         public class GridObject
//         {
//             private GridXZ<GridObject> m_grid;
//             private int m_x, m_z;
//             public GridObject(GridXZ<GridObject> grid, int, x, z)
//             {
//                 m_grid = grid;
//                 m_x = m_x;
//                 m_z = m_z;
//             }
//         }
//     }
// }