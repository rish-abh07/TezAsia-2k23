using System.Collections;
using System.Collections.Generic;
using Beacon.Sdk.Beacon.Sign;
using TezosSDK.Tezos.Wallet;
using TezosSDK.View;
using UnityEngine;
using UnityEngine.UI;

namespace Tezos.Game
{
    public class RegisterPanels : MonoBehaviour
    {
        [SerializeField, Header("Components")] private Button _deepLinkPair;
        // [SerializeField, Header("Manager")] private UIManager _uiManager;
        private IInformationManager _informationManager;
        
        private const string PayloadToSign = "Tezos Signed Message: mydap.com 2021-01-14T15:16:04Z Hello world!";
        // Start is called before the first frame update
        private IEnumerator Start()
        {
            // skip a frame before start accessing Database
            yield return null;

            _informationManager = InformationFactory.Instance.GetInfoManager();
         //  _informationManager.GetWalletMessageReceiver().HandshakeReceived += (handshake) => _qrCodeView.SetQrCode(handshake);

            SetButtonState(_deepLinkPair, false, false);
           // SetButtonState(_socialLoginButton, false, false);
           // _qrImage.gameObject.SetActive(false);
#if UNITY_STANDALONE || UNITY_EDITOR
            // make QR code available for Standalone
           // _qrImage.gameObject.SetActive(true);
#elif (UNITY_IOS || UNITY_ANDROID)
		SetButtonState(_deepLinkPair, true, true);
#elif UNITY_WEBGL
		SetButtonState(_deepLinkPair, true, true);
		SetButtonState(_socialLoginButton, true, true);
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void DeepLinkPair()
        {
            _informationManager.Login(WalletProviderType.beacon);
        }
        private void SetButtonState(Button button, bool active, bool interactable)
        {
            button.gameObject.SetActive(active);
            button.interactable = interactable;
        }
       //
    }
}
