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
                  
                    GameObject knotGameObject = new GameObject("Knot");
                    knotGameObject.transform.position = knot.Position;
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

            
            monster.transform.position = splinePoints[currentSplineIndex].position;

            
            monoBehaviour.StartCoroutine(SplineMovement(monster,  monsterStats));
        }

        private IEnumerator SplineMovement(GameObject monster, IMonsterStats monsterStats)
        {
            while (currentSplineIndex < splinePoints.Count - 1)
            {
              
                Vector3 nextPosition = splinePoints[currentSplineIndex + 1].position;
                Vector3 direction = (nextPosition - monster.transform.position).normalized;

            
                while (Vector3.Distance(monster.transform.position, nextPosition) > 0.01f)
                {
                    monster.transform.position += direction * monsterStats.MovementSpeed * Time.deltaTime;
                    yield return null;
                }

              
                currentSplineIndex++;
            }
        }
    }
}

