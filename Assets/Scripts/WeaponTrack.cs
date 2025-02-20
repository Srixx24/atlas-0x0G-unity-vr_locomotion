using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponTrack : MonoBehaviour
{
    public GameObject bowPrefab;
    public GameObject swordPrefab;
    public GameObject currentWeapon;
    public Transform bowGrabPoint; 
    public Transform swordGrabPoint;
    public InputActionReference switchWeaponAction;


    void Start()
    {
        EquipWeapon(bowPrefab);
        switchWeaponAction.action.Enable();
    }

    void Update()
    {
        if (switchWeaponAction.action.triggered)
            SwitchWeapon();
    }

    void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        Transform grabPoint = weaponPrefab.CompareTag("Bow") ? bowGrabPoint : swordGrabPoint;
        currentWeapon = Instantiate(weaponPrefab, grabPoint.position, grabPoint.rotation);
        currentWeapon.transform.SetParent(grabPoint);
    }

    void SwitchWeapon()
    {
        if (currentWeapon.CompareTag("Bow"))
            EquipWeapon(swordPrefab);
        else
            EquipWeapon(bowPrefab);
    }
}