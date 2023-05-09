// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Telepatia.GridSystem
// {
//     public class Grid
//     {
//         public int width, height;
//         public float cellSize;
//         private int[,] gridArray;

//         public Grid(int width, int height)
//         {
//             this.width = width;
//             this.height = height;
//             gridArray = new int[width, height];
//         }

//         private Vector3 GetWorldPosition(int x, int y)
//         {
//             return new Vector3(x, y) * cellSize;
//         }
//     }
// }