using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TriggerType
{
    public interface ITriggerOnCollide
    {
        public void OnCollide(GameObject collidedWith);
    }
}
