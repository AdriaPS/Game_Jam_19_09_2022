using System;
using Codetox.Core;
using Codetox.Messaging;
using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class ShootingController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
        public Variable<Vector2> direction;
        public ValueReference<float> distance;
        public Transform source;

        private void OnEnable()
        {
            rigidbody.velocity = Vector2.zero;
            transform.right = direction.Value;
        }

        private void Update()
        {
            transform.right = direction.Value;
            rigidbody.velocity = Vector2.zero;
        }

        public void ShootCold()
        {
            var hit = Physics2D.Raycast(source.position, source.right, distance.Value);
            if (!hit) return;
            hit.collider.gameObject.Send<Water>(w => w.Freeze(), Scope.Parents);
        }

        public void ShootHot()
        {
            var hit = Physics2D.Raycast(source.position, source.right, distance.Value);
            if (!hit) return;
            hit.collider.gameObject.Send<Water>(w => w.Heat(), Scope.Parents);
        }
    }
}