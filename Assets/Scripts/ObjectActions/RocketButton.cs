using UnityEngine;

public class RocketButton : MonoBehaviour
{
    [SerializeField] Rocket rocket;
    [SerializeField] Ammo player;
    [SerializeField] Material btnOffMaterial;
    [SerializeField] Material btnOffMaterialInRange;
    [SerializeField] Material btnOnMaterial;

    float minDistanceFromButton = 1.5f;
    float minAngleAgainstSwitch = 19f;
    float maxAngleAgainstSwitch = 34f;
    bool buttonActivated;

    void Update()
    {
        bool isPlayerFacingButton = IsPlayerFacingButton();

        if (isPlayerFacingButton && Input.GetKeyDown(KeyCode.E))
        {
            rocket.ActivateLaunchSequence();
            SetButtonColor(isPlayerFacingButton, true);
        }
        else
        {
            SetButtonColor(isPlayerFacingButton, false);
        }
    }

    private void SetButtonColor(bool isPlayerFacingButton, bool activateButton)
    {
        if (!buttonActivated)
        {
            if (activateButton)
            {
                buttonActivated = true;
                transform.Find("ButtonActivate").GetComponent<MeshRenderer>().material = btnOnMaterial;
            }
            else if (isPlayerFacingButton)
            {
                transform.Find("ButtonActivate").GetComponent<MeshRenderer>().material = btnOffMaterialInRange;
            }
            else 
            {
                transform.Find("ButtonActivate").GetComponent<MeshRenderer>().material = btnOffMaterial;
            }
        }
        
    }

    private bool IsPlayerFacingButton()
    {
        float distanceFromButton = Vector3.Distance(player.transform.position, transform.position);
        var playerToButtonAngle = Vector3.Angle(player.transform.forward, transform.position - player.transform.position);
        return distanceFromButton < minDistanceFromButton && playerToButtonAngle >= minAngleAgainstSwitch && playerToButtonAngle <= maxAngleAgainstSwitch;
    }
}
