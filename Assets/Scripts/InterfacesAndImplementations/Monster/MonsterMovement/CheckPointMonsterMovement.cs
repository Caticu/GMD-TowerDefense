using Assets.Scripts.InterfacesAndImplementations.Monster;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Transform = UnityEngine.Transform;

namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement
{
    public class CheckPointMonsterMovement : ICheckPointMonsterMovement
    {
        public int PathId { get; set; }

        private Transform[] CheckPoints;
        private int CurrentCheckPointIndex { get; set; }

        public void MoveTowardsCheckPoint(GameObject gameObject, float movementSpeed, Transform[] checkPoint)
        {
            if(checkPoint != null)
            {
                CheckPoints = checkPoint;
            }
            if (CurrentCheckPointIndex < CheckPoints.Length)
            {
                Vector3 targetPosition = CheckPoints[CurrentCheckPointIndex].position;
                // Move object
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPosition, movementSpeed * Time.deltaTime);

                // Check if the monster has reached close enough to the target position
                if (Vector2.Distance(gameObject.transform.position, targetPosition) < 0.01f)
                {
                    //Debug.Log($"Hit checkPoint {checkPoints[CurrentCheckPointIndex]} + {targetPosition.ToString()}");
                    CurrentCheckPointIndex = (CurrentCheckPointIndex + 1) % CheckPoints.Length;

                    // Check if it got to the last checkpoint
                    if (CurrentCheckPointIndex + CheckPoints.Length == CheckPoints.Length)
                    {
                       //
                    }
                }

            }
        }
        public Transform[] FindCheckPoints(int PathId)
        {
            // Find all GameObjects with the "CheckPoint" tag
            GameObject[] checkPointObjects = GameObject.FindGameObjectsWithTag("CheckPoint");


            // Filter the GameObjects based on their names starting with "CheckPoint"
            List<Transform> checkPointsList = new List<Transform>();
            foreach (var checkPointObject in checkPointObjects)
            {
                // Extract the ID from the GameObject name
                string name = checkPointObject.name;
                string[] parts = name.Split('.');
                if (parts.Length == 2 && parts[0].StartsWith("CheckPoint"+PathId))
                {
                    checkPointsList.Add(checkPointObject.transform);
                }
            }

            // Sort the checkpoints
            checkPointsList = checkPointsList.OrderBy(t => GetSubIndexFromName(t.name)).ToList();

            return checkPointsList.ToArray();
        }

        private float GetSubIndexFromName(string name)
        {
            string[] parts = name.Split('.'); // Split the name by . 
            if (parts.Length == 2)
            {
                float subIndex;
                if (float.TryParse(parts[1], out subIndex))
                {
                    return subIndex;
                }
            }
            return float.MaxValue; // Return a big value to make sure that wrong checkpoints are placed in the end
        }

       

        
    }
}
