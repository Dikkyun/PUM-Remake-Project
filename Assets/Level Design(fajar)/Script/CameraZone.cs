
using UnityEngine;


[RequireComponent(typeof(Collider))]

public class CameraZone : MonoBehaviour
{
    #region Field
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;

    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    private void Start()
    {
        virtualCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = true;
            Debug.Log(gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = false;
        }
    }
        private void OnValidate()
    {
            GetComponent<Collider>().isTrigger = true;
    }
    #endregion

}


