using UnityEngine;

public class LightBeam : MonoBehaviour
{
    private int _maxBounce = 20;
    private LineRenderer lightBeamLR;
    [SerializeField] int mirrorLayerIndex;
    [SerializeField] Transform startPoint;

    private void Start()
    {
        lightBeamLR = GetComponent<LineRenderer>();
       
    }
    private void Update()
    {
        castLaser(startPoint.position, startPoint.forward);
    }
    private void castLaser(Vector3 position , Vector3 direction)
    {
        lightBeamLR.positionCount = 1;
        lightBeamLR.SetPosition(0, startPoint.position);
       
        for (int i = 0; i < _maxBounce; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if(Physics.Raycast(ray , out hit))
            {
                lightBeamLR.positionCount++;
                lightBeamLR.SetPosition(i + 1, hit.point);
                Vector3 newdirection = Vector3.Reflect(direction, hit.normal);
                if (hit.collider.gameObject.layer != mirrorLayerIndex)
                {
                    if(hit.collider.tag == "LightPointReciver")
                    {
                        hit.collider.GetComponent<Light>().enabled = true;
                        GameController.Instance.ChangeGameState(GameController.GameState.Ended);
                        enabled = false;
                    }
                    break;
                }
                else
                {
                    direction = newdirection;
                    position = hit.point;
                }
            }
        }
    }
}
