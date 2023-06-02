using Fusion;
using Network;
using UnityEngine;

public class NetworkRig : NetworkBehaviour
{
    [Header("Rig Visuals")]
    [SerializeField] private GameObject headVisual;
    
    [Header("Rig Components")]
    [SerializeField] private NetworkTransform characterTransform;
    [SerializeField] private NetworkTransform headTransform;
    [SerializeField] private NetworkTransform leftHandTransform;
    [SerializeField] private NetworkTransform rightHandTransform;
    [SerializeField] private NetworkTransform bodyTransform;

    private HardwareRig _hardwareRig;
    private bool IsLocalPlayer => Object.HasStateAuthority;
    
    // Sustituto de Start
    public override void Spawned()
    {
        base.Spawned();

        if (IsLocalPlayer)
        {
            _hardwareRig = FindObjectOfType<HardwareRig>();
            
            // Check errors
            if (_hardwareRig == null)
            {
                Debug.LogError("HardwareRig not found. You probably forgot to add it to the scene.");
            }
            
            // Desactivar cabeza localmente para que la camara no la vea
            headVisual.SetActive(false);
        }
        else
        {
            Debug.Log("Client spawned");
        }
    }


    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        // LLama al evento OnInput que acaba llamando a OnInput de HardwareRig
        if (!GetInput<XRRigInputData>(out var inputData)) return;
        
        // Mueve NetworkRig a la posicion de la red
        characterTransform.transform.SetPositionAndRotation(inputData.CharacterPosition, inputData.CharacterRotation);
        headTransform.transform.SetPositionAndRotation(inputData.HeadPosition, inputData.HeadRotation);
        leftHandTransform.transform.SetPositionAndRotation(inputData.LeftHandPosition, inputData.LeftHandRotation);
        rightHandTransform.transform.SetPositionAndRotation(inputData.RightHandPosition, inputData.RightHandRotation);
        bodyTransform.transform.SetPositionAndRotation(inputData.BodyPosition, inputData.BodyRotation);
    }

    // Sustituto de Update
    public override void Render()
    {
        base.Render();

        if (!IsLocalPlayer) return;
        
        // Mueve NetworkRig a HardwareRig
        headTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig.headTransform.position, _hardwareRig.headTransform.rotation);
        leftHandTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig.leftHandTransform.position, _hardwareRig.leftHandTransform.rotation);
        rightHandTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig.rightHandTransform.position, _hardwareRig.rightHandTransform.rotation);
        bodyTransform.InterpolationTarget.SetPositionAndRotation(_hardwareRig.bodyTransform.position, _hardwareRig.bodyTransform.rotation);
    }
}
