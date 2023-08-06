using UnityEngine;

namespace Tezos.Game
{
    public class InformationFactory : MonoBehaviour
    {
        public static InformationFactory Instance;
        private IInformationManager _informationManager = null;

        private void Awake()//used to create only one object of this class type
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);

            _informationManager = new InformationManager();
            _informationManager.Init();
        }

        public IInformationManager GetInfoManager()
        {
            return _informationManager;
        }
    }
}