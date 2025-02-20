using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKFeet : MonoBehaviour
{
    [SerializeField] private Transform frontRightLeg;
    [SerializeField] private Transform frontLeftLeg;
    [SerializeField] private Transform hindRightLeg;
    [SerializeField] private Transform hindLeftLeg;
    private Transform[] allLegs;
    [SerializeField] private Transform frontRightTarget;
    [SerializeField] private Transform frontLeftTarget;
    [SerializeField] private Transform hindRightTarget;
    [SerializeField] private Transform hindLeftTarget;
    private Transform[] allTargets;
    [SerializeField] private GameObject frontRightContraint;
    [SerializeField] private GameObject frontLeftContraint;
    [SerializeField] private GameObject hindRightContraint;
    [SerializeField] private GameObject hindLeftContraint;
    private TwoBoneIKConstraint[] allLegContraints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allLegs = new Transform[4];
        allLegs[0] = frontRightLeg;
        allLegs[1] = frontLeftLeg;
        allLegs[2] = hindRightLeg;
        allLegs[3] = hindLeftLeg;

        allTargets = new Transform[4];
        allTargets[0] = frontRightTarget;
        allTargets[1] = frontLeftTarget;
        allTargets[2] = hindRightTarget;
        allTargets[3] = hindLeftTarget;
        
        allLegContraints = new TwoBoneIKConstraint[4];
        allLegContraints[0] = frontRightContraint.GetComponent<TwoBoneIKConstraint>();
        allLegContraints[1] = frontLeftContraint.GetComponent<TwoBoneIKConstraint>();
        allLegContraints[2] = hindRightContraint.GetComponent<TwoBoneIKConstraint>();
        allLegContraints[3] = hindLeftContraint.GetComponent<TwoBoneIKConstraint>();
    }

    void FixedUpdate()
    {
        
    }
}
