using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement
{
    public class SplineMonsterMovement : ISplineMonsterMovement
    {
        private List<Transform> splinePoints = new List<Transform>();
        
        private int currentSplineIndex = 0;
        private MonoBehaviour monoBehaviour;

        

        public SplineMonsterMovement(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour;
        }

        public int PathId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Transform[] FindCheckPoints(int PathId)
        {
            throw new NotImplementedException();
        }

        public void FindSplinePoints(GameObject monster)
        {
            // get spline points 
            splinePoints.Clear();

            foreach (GameObject splinePoint in GameObject.FindGameObjectsWithTag("Spline"))
            {
                var t = splinePoint.GetComponent<SplineContainer>();
                foreach (BezierKnot knot in t.Spline.Knots)
                {
                    // Create a new game object and set its position to the knot position
                    GameObject knotGameObject = new GameObject("Knot");
                    knotGameObject.transform.position = knot.Position;

                    // Add the transform of the new game object to the splinePoints list
                    splinePoints.Add(knotGameObject.transform);
                }
                Debug.Log("Spline point found");
            }
        }

        public void Move(GameObject monster, IMonsterStats monsterStats)
        {
            if (splinePoints == null || splinePoints.Count == 0)
            {
                Debug.LogError("No spline points found.");
                return;
            }

            // Ensure initial position is set to the first spline point
            monster.transform.position = splinePoints[currentSplineIndex].position;

            // Move along the spline
            monoBehaviour.StartCoroutine(SplineMovement(monster,  monsterStats));
        }

        private IEnumerator SplineMovement(GameObject monster, IMonsterStats monsterStats)
        {
            while (currentSplineIndex < splinePoints.Count - 1)
            {
                // Calculate direction towards the next spline point
                Vector3 nextPosition = splinePoints[currentSplineIndex + 1].position;
                Vector3 direction = (nextPosition - monster.transform.position).normalized;

                // Move towards the next spline point
                while (Vector3.Distance(monster.transform.position, nextPosition) > 0.01f)
                {
                    monster.transform.position += direction * monsterStats.MovementSpeed * Time.deltaTime;
                    yield return null;
                }

                // Move to the next spline point
                currentSplineIndex++;
            }

            
            // TODO CMR : a ajuns la sfarsit, scade viata, etc
        }
    }
}

