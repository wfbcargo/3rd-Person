using Assets.Scripts.TriggerType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public abstract class Gun : MonoBehaviour, IActionTrigger
    {
        public Bullet Projectile;
        public Transform Origin;
        public bool InfiniteAmmo = true;
        public float AmmoCount = 100;
        public float ShotsPerSecond = 1;

        public float BulletSpeed = 10;

        public bool IsAutomatic = false;

        private float lastShotTime = 0;
        public void TriggerAction()
        {
            if (Time.time - lastShotTime > 1 / ShotsPerSecond && (InfiniteAmmo || AmmoCount > 0))
            {
                Fire();
                lastShotTime = Time.time;
                AmmoCount--;
            }
        }
        protected abstract void Fire();
    }
}
