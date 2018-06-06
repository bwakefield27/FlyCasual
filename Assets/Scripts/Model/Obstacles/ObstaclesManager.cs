﻿using BoardTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Obstacles
{
    public class ObstaclesManager
    {
        public static ObstaclesManager Instance;

        private List<GenericObstacle> Obstacles;

        public ObstaclesManager()
        {
            Instance = this;

            InitializeObstacles();
        }

        private void InitializeObstacles()
        {
            Obstacles = new List<GenericObstacle>();
            foreach (Transform obstacle in Board.BoardTransform.Find("ObstaclesHolder"))
            {
                if (obstacle.name.StartsWith("A"))
                {
                    Obstacles.Add(new Asteroid(obstacle.gameObject));
                }
                else if (obstacle.name.StartsWith("D"))
                {
                    Obstacles.Add(new Debris(obstacle.gameObject));
                }
            }
        }

        public GenericObstacle GetObstacleByName(string obstacleName)
        {
            return Obstacles.First(n => n.ObstacleGO.name == obstacleName);
        }

        public List<GenericObstacle> GetPlacedObstacles()
        {
            return Obstacles.Where(n => n.IsPlaced).ToList();
        }

        public int GetPlacedObstaclesCount()
        {
            return Obstacles.Count(n => n.IsPlaced);
        }

        public GenericObstacle GetRandomFreeObstacle()
        {
            List<GenericObstacle> freeObstacles = Obstacles.Where(n => !n.IsPlaced).ToList();
            int random = UnityEngine.Random.Range(0, freeObstacles.Count);
            return freeObstacles[random];
        }

        public void DestroyObstacle(GenericObstacle obstacle)
        {
            Obstacles.Remove(obstacle);
            Board.Objects.Remove(obstacle.ObstacleGO.GetComponentInChildren<MeshCollider>());
            GameObject.Destroy(obstacle.ObstacleGO);
        }
    }
}
