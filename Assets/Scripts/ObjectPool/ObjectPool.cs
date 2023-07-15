using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ObjectPool
{
    public class Pool<T> where T : MonoBehaviour
    {
        private List<T> _prefabs;
        private List<T> _objects;

        public Pool(List<T> prefabs, int prewarmObjects)
        {
            _prefabs = prefabs;
            _objects = new List<T>();
            for (int i = 0; i < prewarmObjects; i++)
            {
                for (int j = 0; j < _prefabs.Count(); j++)
                {
                    var obj = GameObject.Instantiate(_prefabs[j]);
                    obj.gameObject.SetActive(false);
                    _objects.Add(obj);
                }
            }
        }

        public T Get(){
            Random rand = new Random();
            var objects = _objects.Where(x => !x.isActiveAndEnabled).ToList();
            var obj = objects.ElementAt(rand.Next(0,objects.Count()));

            if (obj == null)
                obj = Create();

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj){
            obj.gameObject.SetActive(false);
        }

        private T Create(){
            Random rand = new Random();
            var obj = GameObject.Instantiate(_prefabs.ElementAt(rand.Next(0,_prefabs.Count())));
            _objects.Add(obj);
            return obj;
        }
    }
}