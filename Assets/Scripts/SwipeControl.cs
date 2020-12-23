using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{

    public GameObject m_rendererPrefab;
    public GameObject cube;
    private GameObject m_currentRenderer;
    private Vector3 m_origin;
    private Plane m_cast;
    [SerializeField]private Camera m_camera;
    Touch touch;

    
    private void Start()
    {
      
        //m_camera = Camera.main;
        m_cast = new Plane(m_camera.transform.forward * -1, this.transform.position);
     
    }

    private void Update()
    {
        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            if (IsInput(TouchPhase.Began))
            {
                Ray _ray = m_camera.ScreenPointToRay(Input.touchCount == 1 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition);

                

                if (Physics.Raycast(_ray, out hit))
                {
                    m_origin = hit.point;
                    m_currentRenderer = (GameObject)Instantiate(m_rendererPrefab, m_origin, Quaternion.identity);
                }
            }
            else if (IsInput(TouchPhase.Moved))
            {
                Ray _ray = m_camera.ScreenPointToRay(Input.touchCount == 1 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition);

                if (Physics.Raycast(_ray, out hit))
                {
                    
                    Instantiate(cube, new Vector3(hit.point.x,0,hit.point.z), Quaternion.identity);
                    m_currentRenderer.transform.position = hit.point;
                }
            }
            else if (IsInput(TouchPhase.Ended))
            {
                if (Vector3.Distance(m_currentRenderer.transform.position, m_origin) < 0.1f)
                    Destroy(m_currentRenderer);
            }
        }
    }
  



    private bool IsInput(TouchPhase phase)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0);
            case TouchPhase.Moved:
                return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0);
            default:
                return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0);
        }
    }
}
