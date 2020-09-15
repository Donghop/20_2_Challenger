using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ObjectPool : MonoBehaviour

    {

        public static ObjectPool Instance;



        [SerializeField]
         private GameObject poolingObjectPrefab;



        Queue<Bullet> poolingObjectQueue = new Queue<Bullet>();



        private void Awake()

        {

            Instance = this;



            Initialize(4);

        }



        private void Initialize(int initCount)

        {

            for (int i = 0; i < initCount; i++)

            {

                poolingObjectQueue.Enqueue(CreateNewObject());

            }

        }



        private Bullet CreateNewObject()

        {

            var newObj = Instantiate(poolingObjectPrefab).GetComponent<Bullet>();

            newObj.gameObject.SetActive(false);

            newObj.transform.SetParent(transform);

            return newObj;

        }



        public static Bullet GetObject()

    {
        Debug.Log("get object in");

        if (Instance.poolingObjectQueue.Count > 0)
            {
            Debug.Log("get object if");
            var obj = Instance.poolingObjectQueue.Dequeue();

                obj.transform.SetParent(null);

                obj.gameObject.SetActive(true);

                return obj;

            }

            else
            {
            Debug.Log("get object else");
                var newObj = Instance.CreateNewObject();

                newObj.gameObject.SetActive(true);

                newObj.transform.SetParent(null);

                return newObj;

            }

        }



        public static void ReturnObject(Bullet obj)

        {

            obj.gameObject.SetActive(false);

            obj.transform.SetParent(Instance.transform);

            Instance.poolingObjectQueue.Enqueue(obj);

        }

    }



