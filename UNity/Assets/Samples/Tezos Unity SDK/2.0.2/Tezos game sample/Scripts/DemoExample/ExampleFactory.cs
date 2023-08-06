using UnityEngine;

namespace TezosSDK.Samples.DemoExample
{
    public class ExampleFactory : MonoBehaviour
    {
        public static ExampleFactory Instance;
        private IExampleManager _exampleManager = null;

        private void Awake()//used to create only one object of this class type
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);

            _exampleManager = new ExampleManager();
            _exampleManager.Init();
        }

        public IExampleManager GetExampleManager()
        {
            return _exampleManager;
        }
    }
}