using Fusion;
using UnityEngine;

namespace Network
{
    // Objeto que se usa para moverse localmente
    
    public class HardwareRig : MonoBehaviour
    {
        [Header("Rig Components")]
        public Transform characterTransform;
        public Transform headTransform;
        public Transform leftHandTransform;
        public Transform rightHandTransform;
        public Transform bodyTransform;

        private void Awake()
        {
            // Asigna el HardwareRig a NetworkManager para que lo asigne a NetworkManagerEvents
            NetworkManager.Instance.AssignHardwareRig(this);
        }

        // Es llamado desde NetworkRig
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            XRRigInputData inputData = new XRRigInputData();
        
            // Prepara el input desde HardwareRig para enviarlo a la Red
            
            // Character
            inputData.CharacterPosition = characterTransform.position;
            inputData.CharacterRotation = characterTransform.rotation; 
            // Head
            inputData.HeadPosition = headTransform.position;
            inputData.HeadRotation = headTransform.rotation; 
            // Left Hand
            inputData.LeftHandPosition = leftHandTransform.position;
            inputData.LeftHandRotation = leftHandTransform.rotation; 
            // Right Hand
            inputData.RightHandPosition = rightHandTransform.position;
            inputData.RightHandRotation = rightHandTransform.rotation; 
            // Body
            inputData.BodyPosition = bodyTransform.position;
            inputData.BodyRotation = bodyTransform.rotation;

            // Envia el input a la red
            input.Set(inputData);
        }
    }

    public struct XRRigInputData : INetworkInput
    {
       // Head
        public Vector3 HeadPosition;
        public Quaternion HeadRotation;
    
        // Left Hand
        public Vector3 LeftHandPosition;
        public Quaternion LeftHandRotation;
    
        // Right Hand
        public Vector3 RightHandPosition;
        public Quaternion RightHandRotation;
    
        // Body
        public Vector3 BodyPosition;
        public Quaternion BodyRotation;
    
        // Character
        public Vector3 CharacterPosition;
        public Quaternion CharacterRotation;
    }
}